using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor
{
    public string[] stages = new string[] { "PortalTest A", "PortalTest B", "PortalTest C", "PortalTest D", "Field_A" };
    public int index;
   
    public override void OnInspectorGUI()
    {
        Portal portal = (Portal)target;
        index = EditorGUILayout.Popup(index, stages);
        if (GUILayout.Button("Set Destination"))
        {
            portal.destination = stages[index];
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        base.OnInspectorGUI();
    }
    
}
