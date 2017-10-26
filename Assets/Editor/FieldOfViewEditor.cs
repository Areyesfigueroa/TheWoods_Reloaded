using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {

    //Draws the Angles
    //Change to 2D angle later
    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        
        //Draws the Radius
        //fow.transform.pos = enemyGO coordinates
        //Vector3.Up = (0,1,0)
        //Vector3.Forward = (0,0,1)
        //360
        //fow viewradius = radius
        //3D arc
        //Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        //2D arc
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.viewRadius);

        //Sets up angles
        //Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        //Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        //2D angles are set
        Vector3 viewAngleA2D = fow.DirFromAngle2D(-fow.viewAngle / 2, false);
        Vector3 viewAngleB2D = fow.DirFromAngle2D(fow.viewAngle / 2, false);

        //Draws the Angle, 2D angles are set
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA2D * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB2D * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}
