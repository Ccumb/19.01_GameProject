using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FixedRangeBoom))]
public class FixedRangeBoomEditor : Editor
{
    private void OnSceneGUI()
    {
        FixedRangeBoom fow = (FixedRangeBoom)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.BoomRadius);
        Vector3 boomAngleA = fow.DirFromAngle(-fow.TargetAngle / 2, false);
        Vector3 boomAngleB = fow.DirFromAngle(fow.TargetAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + boomAngleA * fow.BoomRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + boomAngleB * fow.BoomRadius);
    }
}
