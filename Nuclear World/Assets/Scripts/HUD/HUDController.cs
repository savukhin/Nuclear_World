using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
    public GameObject menu;
    public GameObject inventoryEquipPanel;
    public InventoryPanel inventoryPanel;
    public EquipmentPanel equipmentPanel;
    public bool menuOpened = false;
    [NonSerialized]
    public Character player;

    void Start() {
        
    }

    public void Close() {
        menu.SetActive(false);
        inventoryEquipPanel.SetActive(false);
    }

    public void SetInventory(Inventory state) {
        inventoryPanel.SetInventory(state);
    }

    public void SetEquipment(Equipment state) {
        equipmentPanel.SetEquipment(state);
    }

    public void OpenMenu() {
        if (inventoryEquipPanel.activeSelf) {
            inventoryEquipPanel.SetActive(false);
            menu.SetActive(true);
        } else {
            menu.SetActive(!menu.activeSelf);
        }
    }

    public void OpenInventory() {
        if (menu.activeSelf) {
            menu.SetActive(false);
            inventoryEquipPanel.SetActive(true);
        }
        else {
            inventoryEquipPanel.SetActive(!inventoryEquipPanel.activeSelf);
        }
    }
}
