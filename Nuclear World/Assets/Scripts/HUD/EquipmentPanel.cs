using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : SlotsPanel {
    public GameObject primaryWeaponSlot;
    public GameObject additionalWeaponSlot;
    public Dictionary<int, string> numberToEquipment = new Dictionary<int, string>();
    public Dictionary<string, int> equipmentToNumber = new Dictionary<string, int>();    

    private void Start() {
        if (numberToEquipment.Count == 0)
            GenerateDictionaries();        
    }

    private void GenerateDictionaries() {
        numberToEquipment.Add(0, "Head");
        numberToEquipment.Add(1, "Mask");
        numberToEquipment.Add(2, "Body");
        numberToEquipment.Add(3, "Cloack");
        numberToEquipment.Add(4, "Arms");
        numberToEquipment.Add(5, "Legs");
        numberToEquipment.Add(6, "Boots");
        numberToEquipment.Add(7, "PrimaryWeapon");
        numberToEquipment.Add(8, "AdditionalWeapon");

        foreach (KeyValuePair<int, string> entry in numberToEquipment) {
            equipmentToNumber.Add(entry.Value, entry.Key);
        }

        InitializateSlots();
    }

    public void SetEquipment(Equipment state) {
        if (numberToEquipment.Count == 0)
            GenerateDictionaries();
        container = state;
        Draw();
    }

    private void Draw() {
        if (container.items[equipmentToNumber["PrimaryWeapon"]] != null) {
            AddCell(equipmentToNumber["PrimaryWeapon"], 
                container.items[equipmentToNumber["PrimaryWeapon"]]);
        }
        if (container.items[equipmentToNumber["AdditionalWeapon"]] != null) {
            AddCell(equipmentToNumber["AdditionalWeapon"],
                container.items[equipmentToNumber["AdditionalWeapon"]]);
        }
    }
}
