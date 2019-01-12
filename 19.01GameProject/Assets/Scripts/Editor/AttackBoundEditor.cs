using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterAttack))]
public class AttackBoundEditor : Editor
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInspectorGUI()
    {
        CharacterAttack mp = (CharacterAttack)target;

        EditorGUILayout.Space();

        mp.center_z = EditorGUILayout.Slider("Center(z)", mp.center_z, 0, 5);

        mp.width = EditorGUILayout.Slider("Width", mp.width, 0, 5);
        mp.height = EditorGUILayout.Slider("Height", mp.height, 0, 5);
        mp.length = EditorGUILayout.Slider("Length", mp.length, 0, 10);

        EditorGUILayout.Space();
    }
}
