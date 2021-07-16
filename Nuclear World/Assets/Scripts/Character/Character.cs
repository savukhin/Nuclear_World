using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {
    public LayerMask platformLayerMask;
    public float moveSpeed = 3f;
    public float jumpForce = 10f;
    protected Rigidbody rb;
    public GameObject model;
    public Inventory inventory_prefab;
    public Equipment equipment_prefab;
    protected Inventory inventory;
    protected Equipment equipment;
    protected Weapon currentWeapon;
    protected int currentWeaponNumber = 1;
    public GameObject weaponSpot;
    public Animator animator;
    public float health;

    protected virtual void Start() {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        if (equipment_prefab != null) {
            equipment = Instantiate(equipment_prefab);
            equipment.GenerateDictionaries();
            equipment.subject = this;
        }
        if (inventory_prefab != null) {
            inventory = Instantiate(inventory_prefab);
            inventory.subject = this;
        }
    }

    protected virtual void Update() {}

    protected void Jump() {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    public bool IsGrounded(float extraDimension = 0.1f) {
        //return Physics.BoxCast(model.GetComponent<Collider>().bounds.center, model.GetComponent<Collider>().bounds.size / 2 - new Vector3(extraDimension, extraDimension, extraDimension), new Vector3(1f, -1f, 1f), model.transform.rotation, 2 * extraDimension, platformLayerMask.value);
        //return Physics.BoxCast(model.GetComponent<Collider>().bounds.center, model.GetComponent<Collider>().bounds.size / 2 - new Vector3(extraDimension, extraDimension, extraDimension), new Vector3(1f, -1f, 1f), model.transform.rotation, 2 * extraDimension);
        return true;
    }

    public void CheckPrimaryWeapon() {
        if (currentWeaponNumber != 1)
            return;
        ChangeWeapon(1);
    }

    public void CheckAdditionalWeapon() {
        if (currentWeaponNumber != 2)
            return;
        ChangeWeapon(2);
    }

    protected void CheckWeapon() {
        if (currentWeaponNumber == 1
            && currentWeapon != equipment.GetPrimaryWeaponPrefab().GetComponent<PrimaryWeapon>()) {

            ChangeWeapon(1);
        }
        else if (currentWeaponNumber == 2
            && currentWeapon != equipment.GetAdditionalWeaponPrefab().GetComponent<AdditionalWeapon>())
        {
            ChangeWeapon(2);
        }
    }

    protected bool IsWearWeapon() {
        return currentWeapon != null;
    }

    protected void ChangeWeapon(int num) {
        if (currentWeapon != null) {
            Destroy(currentWeapon);
            Destroy(currentWeapon.gameObject);
        }

        Vector3 weaponPosition = weaponSpot.transform.position;
        Quaternion weaponRotation = weaponSpot.transform.rotation;

        if (num == 1 && equipment.GetPrimaryWeaponPrefab() != null) {
            //currentWeapon = Instantiate(equipment.GetPrimaryWeaponPrefab().GetComponent<PrimaryWeapon>(), weaponPosition, weaponRotation, transform);
            currentWeapon = Instantiate(equipment.GetPrimaryWeaponPrefab().GetComponent<PrimaryWeapon>(), weaponSpot.transform);
        } else if (num == 2 && equipment.GetAdditionalWeaponPrefab() != null) {
            //currentWeapon = Instantiate(equipment.GetAdditionalWeaponPrefab().GetComponent<AdditionalWeapon>(), weaponPosition, weaponRotation, transform);
            currentWeapon = Instantiate(equipment.GetAdditionalWeaponPrefab().GetComponent<AdditionalWeapon>(), weaponSpot.transform);
        }
        currentWeaponNumber = num;
    }

    protected void Fire() {
        currentWeapon.GetComponent<Weapon>().strike();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        rb.AddForce((-transform.forward + transform.up) * 2, ForceMode.Impulse);
    }
}
