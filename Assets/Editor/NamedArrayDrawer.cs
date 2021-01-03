// Author: JohnnyA
// Collaborators: Harry Donovan
// References: https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
// File Source: Assets\Editor\NamedArrayDrawer.cs
// Dependencies: Assets\Scripts\Helpers\NamedArrayAttribute.cs
// Description: A script (see references for source) not written by me that allows naming of array elements in inspector.

using UnityEngine;
using UnityEditor;
 
[CustomPropertyDrawer (typeof(NamedArrayAttribute))]public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            // Edited as casting with (NamedArrayAttribute) didn't work.
            NamedArrayAttribute namedArrayAttribute = attribute as NamedArrayAttribute;
            // Edited to stop property being instantly reset to 0.
            property.intValue = EditorGUI.IntField(rect, namedArrayAttribute.names[pos], property.intValue);
        } catch {
            EditorGUI.ObjectField(rect, property, label);
        }
    }
}