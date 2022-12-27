using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace PixselCrew.UI
{
    // обвовленный редактра, для нашей кнопки с двумя полями
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CastomButton), true)]
    public class CastomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            // отрисовали два параметра   
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_normal"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_presed"));
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();

        }
    }
}
