using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RandomStruct))]
public class RandomStructInspector : PropertyDrawer
{
    int labelWidth = 150;
    int inputFieldWidth = 70;
    int offset = 10;
    int propHeight = 18;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        //EditorGUI.PrefixLabel(new Rect(position.x, position.y + propHeight, propHeight, position.width), GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Curve"));
        
        Rect rangeLabel = new Rect (position.x, position.y, labelWidth, propHeight);
        Rect fromIntField = new Rect (position.x, position.y, inputFieldWidth, propHeight);
        Rect toIntField = new Rect (position.x + inputFieldWidth + offset, position.y, inputFieldWidth, propHeight);
        Rect curveField = new Rect (position.x, position.y + propHeight, position.width, propHeight);
        SerializedProperty fromIntProp = property.FindPropertyRelative("fromInt");
        fromIntProp.intValue = EditorGUI.IntField(fromIntField, fromIntProp.intValue);

        SerializedProperty toIntProp = property.FindPropertyRelative("toInt");
        toIntProp.intValue   = EditorGUI.IntField(toIntField, toIntProp.intValue);

        EditorGUI.EndProperty();


        EditorGUI.BeginProperty(new Rect(position.x, position.y + propHeight, propHeight, position.width), label, property);
        SerializedProperty curveProp = property.FindPropertyRelative("subdividePercent");
        curveProp.animationCurveValue = EditorGUI.CurveField(curveField, curveProp.animationCurveValue);
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
       return propHeight * 2;
    }
    /*
    public override void OnInspectorGUI()
    {
        //RandomStruct str = (RandomStruct) target;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Range", GUILayout.Width(labelWidth));
        fromInt = EditorGUILayout.IntField(fromInt, GUILayout.Width(inputFieldWidth));
        toInt = EditorGUILayout.IntField(toInt, GUILayout.Width(inputFieldWidth));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Rand Percent");
        subdividePercent = EditorGUILayout.CurveField(subdividePercent);
    }*/
}
