using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    public Sprite aimIcon;
    public enum interactiveObjectType {
        None,
        Chest
    }
    public interactiveObjectType type;
}
