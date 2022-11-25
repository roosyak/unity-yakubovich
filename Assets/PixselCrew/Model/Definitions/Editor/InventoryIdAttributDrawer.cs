using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace PixselCrew.Model
{
    [CustomPropertyDrawer(typeof(InventoryIdAttribut))]
    public class InventoryIdAttributDrawer : PropertyDrawer
    {
        /*
         отрисовка выпадающего списка в редакторе 
         */
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var desf = DefsFacade.I.Items.ItemsForEditor;
            var ids = new List<string>();
            foreach (var item in desf)
                ids.Add(item.Id);

            var index = ids.IndexOf(property.stringValue);
            index = EditorGUI.Popup(position, property.displayName, index, ids.ToArray());

            property.stringValue = ids[index];
        }
    }
}
