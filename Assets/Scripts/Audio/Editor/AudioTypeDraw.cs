using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AudioType))]
public class AudioTypeDraw : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
        
		EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int ident = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect audioSelect = new Rect (position.x, position.y, position.width, position.height);

        SerializedProperty audioClip = property.FindPropertyRelative("audioClip");
        EditorGUI.ObjectField(audioSelect, audioClip, GUIContent.none);
        
        EditorGUI.indentLevel = ident;

        EditorGUI.EndProperty();
        
	}
}
