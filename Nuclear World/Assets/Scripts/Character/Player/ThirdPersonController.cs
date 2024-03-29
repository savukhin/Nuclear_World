﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : Character {
    public HUDController HUD;
    public GameObject thirdPersonCamera;

    protected override void Start() {
        base.Start();
        ChangeWeapon(currentWeaponNumber);
        HUD.player = this;
        if (inventory == null) {
            inventory = new Inventory();
        }
        HUD.SetInventory(inventory);
        if (equipment == null)
            equipment = new Equipment();
        HUD.SetEquipment(equipment);
    }

    protected override void Update() {
        if (!HUD.inMenu) {
            MoveAndRotate();
            if (Input.GetMouseButtonDown(0) && IsGrounded())
                Fire();
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                Jump();
            if (Input.GetKeyDown(KeyCode.Tab)) {
                ChangeWeapon(currentWeaponNumber % 2 + 1);
            }            
            CheckTargetInFront();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            HUD.CheckMenu();
        } else if (Input.GetKeyDown(KeyCode.I)) {
            HUD.CheckInventory();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            if (!HUD.inMenu) {
                GameObject interactiveTarget = GetTargetInFront();
                if (interactiveTarget.GetComponent<InteractiveObject>())
                    UseInteractiveObject(interactiveTarget.GetComponent<InteractiveObject>());
            } else {
                HUD.Close();
            }
        }        
    }

    private void UseInteractiveObject(InteractiveObject obj) {
        if (obj.type == InteractiveObject.interactiveObjectType.Chest)
            HUD.CheckChest(obj.gameObject.GetComponent<Chest>());
        else if (obj.type == InteractiveObject.interactiveObjectType.NPC)
            HUD.CheckDialogue(obj.gameObject.GetComponent<NPC>());
    }

    private GameObject GetTargetInFront() {
        RaycastHit result = new RaycastHit();
        Physics.Raycast(thirdPersonCamera.transform.position, thirdPersonCamera.transform.rotation * Vector3.forward, out result, 10f);
        if (result.collider == null)
            return null;        
        return result.collider.gameObject;
    }

    private void CheckTargetInFront() {
        RaycastHit result = new RaycastHit();
        Physics.Raycast(thirdPersonCamera.transform.position, thirdPersonCamera.transform.rotation * Vector3.forward,  out result, 10f);
        Debug.DrawRay(thirdPersonCamera.transform.position, thirdPersonCamera.transform.rotation * Vector3.forward * 100, Color.blue);
        if (result.collider != null && result.collider.tag == "Interactive Object")
            HUD.ChangeAimSprite(result.collider.gameObject.GetComponent<InteractiveObject>().aimIcon);
        else
            HUD.ChangeAimSprite(null);
        
    }

    private void MoveAndRotate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float angleToZ = transform.eulerAngles.y * Mathf.Deg2Rad;
		float angleToX = (360 - transform.eulerAngles.y) * Mathf.Deg2Rad;

		Vector3 direction = new Vector3((h*Mathf.Cos(angleToX) + v*Mathf.Sin(angleToZ)), 0f, (h*Mathf.Sin(angleToX) + v*Mathf.Cos(angleToZ))).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        RaycastHit hit;
        Vector3 rotation = new Vector3(-Input.GetAxis("Mouse Y") * 2f, 0f, 0f);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * 3f, 0f);
        thirdPersonCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * 2f, 0f, 0f);
        
        if (Physics.Raycast(thirdPersonCamera.transform.position, thirdPersonCamera.transform.forward, out hit, 100) && hit.collider.tag != "Volume") {
            var targetPoint = hit.point;
            var q = Quaternion.LookRotation(targetPoint - weaponSpot.transform.position);
            weaponSpot.transform.rotation = Quaternion.RotateTowards(weaponSpot.transform.rotation, q, 90 * Time.deltaTime);
        } else {
            weaponSpot.transform.Rotate( - Input.GetAxis("Mouse Y") * 2f, 0f, 0f);
        }
    }
}
