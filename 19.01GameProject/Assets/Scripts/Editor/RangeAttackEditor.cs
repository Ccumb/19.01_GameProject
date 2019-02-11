using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RangeAttack))]
public class RangeAttackEditor : Editor
{
    private void OnSceneGUI()
    {
        RangeAttack fow = (RangeAttack)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.TargetOffRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.TargetAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.TargetAngle / 2, false);

        Handles.color = Color.green;
        if (fow.TargetOnRadius > fow.TargetOffRadius)
        {
            fow.TargetOnRadius = fow.TargetOffRadius;
        }
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.TargetOnRadius);
        Vector3 DamageAngleA = fow.DirFromAngle(-fow.TargetAngle / 2, false);
        Vector3 DamageAngleB = fow.DirFromAngle(fow.TargetAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + DamageAngleA * fow.TargetOnRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + DamageAngleB * fow.TargetOnRadius);
    }
}
