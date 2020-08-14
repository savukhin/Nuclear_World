using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : Character {
    public HUDController HUD;
    
    void Start() {
        initialize();
        rb = GetComponent<Rigidbody>();
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

    void Update() {
        //CheckWeapon();
        MoveAndRotate();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();
        if (Input.GetKeyDown(KeyCode.Tab)) {
            ChangeWeapon(currentWeaponNumber % 2 + 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            HUD.OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.I)) {
            HUD.OpenInventory();
        }

    }

    private void MoveAndRotate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float angleToZ = transform.eulerAngles.y * Mathf.Deg2Rad;
		float angleToX = (360 - transform.eulerAngles.y) * Mathf.Deg2Rad;

		Vector3 direction = new Vector3((h*Mathf.Cos(angleToX) + v*Mathf.Sin(angleToZ)), 0f, (h*Mathf.Sin(angleToX) + v*Mathf.Cos(angleToZ))).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * 3f, 0f);
        //if (direction.magnitude > 0.3) {
		//	float newAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		//	transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
		//}
    }
}
