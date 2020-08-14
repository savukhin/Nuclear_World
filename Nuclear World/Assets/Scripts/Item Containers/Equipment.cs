using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Container {
    public Head head;
    public Mask mask;
    public Body body;
    public Cloack cloack;
    public Arms arms;
    public Legs legs;
    public Boots boots;
    public PrimaryWeapon primaryWeapon;
    public AdditionalWeapon additionalWeapon;
    private Dictionary<int, string> numberToEquipment = new Dictionary<int, string>();
    private Dictionary<string, int> equipmentToNumber = new Dictionary<string, int>();

    public Equipment() : base(9) {
                   
    }

    public void GenerateDictionaries() {
        numberToEquipment.Add(0, "Head");
        items[0] = head;
        numberToEquipment.Add(1, "Mask");
        items[1] = mask;
        numberToEquipment.Add(2, "Body");
        items[2] = body;
        numberToEquipment.Add(3, "Cloack");
        items[3] = cloack;
        numberToEquipment.Add(4, "Arms");
        items[4] = arms;
        numberToEquipment.Add(5, "Legs");
        items[5] = legs;
        numberToEquipment.Add(6, "Boots");
        items[6] = boots;
        numberToEquipment.Add(7, "PrimaryWeapon");
        items[7] = primaryWeapon;
        numberToEquipment.Add(8, "AdditionalWeapon");
        items[8] = additionalWeapon;

        foreach (KeyValuePair<int, string> entry in numberToEquipment) {
            equipmentToNumber.Add(entry.Value, entry.Key);
        }
    }

    public GameObject GetPrimaryWeaponPrefab() {
        if (items[equipmentToNumber["PrimaryWeapon"]] == null)
            return null;
        return items[equipmentToNumber["PrimaryWeapon"]].gameObject;
    }

    public GameObject GetAdditionalWeaponPrefab() {
        if (items[equipmentToNumber["AdditionalWeapon"]] == null)
            return null;
        return items[equipmentToNumber["AdditionalWeapon"]].gameObject;
    }

    public override void AdditionalAction(int number) {
        if (numberToEquipment[number] == "PrimaryWeapon")
            subject.CheckPrimaryWeapon();
        else if (numberToEquipment[number] == "AdditionalWeapon")
            subject.CheckAdditionalWeapon();
    }
}
