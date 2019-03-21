using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LongRangeAttack))]
public class LongRangeAttackEditor : Editor
{
    private void OnSceneGUI()
    {
        LongRangeAttack fow = (LongRangeAttack)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.LongTargetOffRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.LongTargetAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.LongTargetAngle / 2, false);

        Handles.color = Color.green;

        if (fow.LongTargetOffRadius < fow.LongTargetOnRadius)
        {
            fow.LongTargetOnRadius = fow.LongTargetOffRadius;
        }

        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.LongTargetOnRadius);
        Vector3 DamageAngleA = fow.DirFromAngle(-fow.LongTargetAngle / 2, false);
        Vector3 DamageAngleB = fow.DirFromAngle(fow.LongTargetAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + DamageAngleA * fow.LongTargetOnRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + DamageAngleB * fow.LongTargetOnRadius);
    }
}
