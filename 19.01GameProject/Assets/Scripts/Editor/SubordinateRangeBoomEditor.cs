using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SubordinateRangeBoom))]
public class SubordinateRangeBoomEditor : Editor
{
    private void OnSceneGUI()
    {
        SubordinateRangeBoom fow = (SubordinateRangeBoom)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.BoomRadius);
        Vector3 boomAngleA = fow.DirFromAngle(-fow.TargetAngle / 2, false);
        Vector3 boomAngleB = fow.DirFromAngle(fow.TargetAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + boomAngleA * fow.BoomRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + boomAngleB * fow.BoomRadius);
    }
}
