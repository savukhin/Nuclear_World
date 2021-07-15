using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public int position;
    public Item item;
    public Image image;
    public LayerMask emptySlotLayerMask;
    public GameObject currentSlot;
    private Transform parent;

    public void SetItem(Item state) {
        item = state;
        image.sprite = state.icon;
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        parent = transform.parent;
        transform.SetParent(transform.parent.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData) {        
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.0f);
        if (colliders.Length == 0) {

        } else
            if ((colliders[0].gameObject.GetComponent<EmptySlot>().type.ToString() == item.GetType().ToString()
                    || colliders[0].gameObject.GetComponent<EmptySlot>().type.ToString() == "Any")
                
                && (colliders[0].gameObject.GetComponent<EmptySlot>().cell == null
                    || currentSlot.GetComponent<EmptySlot>().type.ToString() == colliders[0].gameObject.GetComponent<EmptySlot>().cell.item.GetType().ToString()
                    || currentSlot.GetComponent<EmptySlot>().type.ToString() == "Any")) {

            GameObject newSlot = colliders[0].gameObject;
            Cell other = newSlot.GetComponent<EmptySlot>().cell;            
            newSlot.GetComponent<EmptySlot>().cell = this;
            currentSlot.GetComponent<EmptySlot>().cell = other;

            SlotsPanel otherOwner = newSlot.GetComponent<EmptySlot>().owner;
            otherOwner.ChangeItem(newSlot.GetComponent<EmptySlot>().number, item);
            SlotsPanel thisOwner = currentSlot.GetComponent<EmptySlot>().owner;            

            if (other != null) {                
                thisOwner.ChangeItem(currentSlot.GetComponent<EmptySlot>().number, other.item);

                GameObject temp = other.currentSlot;
                other.currentSlot = currentSlot;
                currentSlot = temp;
                other.transform.position = other.currentSlot.transform.position;
                transform.SetParent(other.transform);
            } else {                
                thisOwner.ChangeItem(currentSlot.GetComponent<EmptySlot>().number, null);
                currentSlot = newSlot;
            }            
        }
        

        transform.position = currentSlot.transform.position;
    }

    public void OnPointerClick(PointerEventData eventData) {

    }
}