using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
    public GameObject menu;
    public Sprite standartAimSprite;
    public GameObject aim;
    public GameObject inventoryEquipChestPanel;
    public GameObject dialogueDark;
    public InventoryPanel inventoryPanel;
    public EquipmentPanel equipmentPanel;
    public DialoguePanel dialoguePanel;
    public ChestPanel chestPanel;
    public bool menuOpened = false;
    [NonSerialized]
    public Character player;
    public bool inMenu = false;

    public void ChangeAimSprite(Sprite s) {
        if (s == null)
            s = standartAimSprite;
        aim.GetComponent<Image>().sprite = s;
    }

    void Start() {
        dialoguePanel.owner = this;
    }

    public void Close() {
        inMenu = false;
        menu.SetActive(false);
        inventoryEquipChestPanel.SetActive(false);
        dialogueDark.SetActive(false);
        inventoryPanel.Close();
        chestPanel.Close();
        equipmentPanel.Close();
    }

    public void SetInventory(Inventory state) {
        inventoryPanel.SetInventory(state);
    }

    public void SetEquipment(Equipment state) {
        equipmentPanel.SetEquipment(state);
    }

    public void CheckMenu() {
        if (inMenu) {
            Close();
            return;
        }
        inMenu = true;
        Close();
        menu.SetActive(true);
    }

    public void CheckInventory() {
        if (menu.activeSelf || !inventoryEquipChestPanel.activeSelf) {
            OpenInventory();
        } else {
            Close();
        }        
    }

    public void CheckChest(Chest chest) {
        if (!inMenu)
            OpenChest(chest);
    }

    public void CheckDialogue(NPC npc) {
        inMenu = true;
        dialogueDark.SetActive(true);
        dialoguePanel.SetNPC(npc);
        dialoguePanel.Open();
    }

    private void OpenInventory() {
        Close();
        inMenu = true;
        inventoryEquipChestPanel.SetActive(true);
        chestPanel.Close();
        equipmentPanel.Open();
        inventoryPanel.Open();
    }

    public void OpenChest(Chest chest) {
        Close();
        inMenu = true;
        inventoryEquipChestPanel.SetActive(true);
        chestPanel.SetChest(chest.container);
        chestPanel.Open();
        equipmentPanel.Close();
        inventoryPanel.Open();
    }
}
