using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxWireframe))]
public class MovementField : Editor {

	void OnSceneGUI()
    {
        Handles.color = Color.yellow;
        BoxWireframe boxField = (BoxWireframe)target;
        Handles.DrawWireCube(boxField.transform.position, boxField.size);
    }
}
