using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GlobalActions : MonoBehaviour {
    public void ExitTheGame() {
        EditorApplication.Exit(0);
    }
}
