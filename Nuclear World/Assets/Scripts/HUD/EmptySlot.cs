using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EmptySlot : MonoBehaviour {
    public Cell cell = null;
    public SlotsPanel owner;
    public int number;

    public enum EquipTypes {
        Any,
        Head,
        Mask,
        Body,
        Cloack,
        Arms,
        Legs,
        Boots,
        PrimaryWeapon,
        AdditionalWeapon
    }
    public EquipTypes type;
}
