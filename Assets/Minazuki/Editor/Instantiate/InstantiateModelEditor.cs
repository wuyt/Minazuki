using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Minazuki.Editor
{
    [CustomPropertyDrawer(typeof(InstantiateModel))]
    public class InstantiateModelEditor : PropertyDrawer
    {
        private const int lineSpacing = 2;
        private int lineHeight;
        private const int lableWidth = 140;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            lineHeight = (int)EditorGUIUtility.singleLineHeight;
            return EditorGUIUtility.singleLineHeight * 8 + lineSpacing * 7;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var rect = position;
            EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel++;


            rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + lineSpacing), rect.width, lineHeight);
            EditorGUI.PrefixLabel(rect, new GUIContent("prefab"));
            rect=new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("prefab"), GUIContent.none);


            rect = new Rect(rect.x - lableWidth, rect.y + (EditorGUIUtility.singleLineHeight + lineSpacing), rect.width, lineHeight);
            EditorGUI.PrefixLabel(rect, new GUIContent("gameObjectInScene"));
            rect = new Rect(rect.x + lableWidth, rect.y, rect.width, rect.height);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("gameObjectInScene"), GUIContent.none);
            if (property.FindPropertyRelative("gameObjectInScene").objectReferenceValue != null)
            {
                property.FindPropertyRelative("fullPath").stringValue = Common.GetFullPathByGameObject((Transform)property.FindPropertyRelative("gameObjectInScene").objectReferenceValue);
            }

            rect = new Rect(rect.x - lableWidth, rect.y + (EditorGUIUtility.singleLineHeight + lineSpacing), rect.width, lineHeight);
            EditorGUI.PrefixLabel(rect, new GUIContent("fullPath"));
            rect = new Rect(rect.x + lableWidth, rect.y, rect.width, rect.height*3);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("fullPath"), GUIContent.none);

            rect = new Rect(rect.x - lableWidth, rect.y + (EditorGUIUtility.singleLineHeight*3 + lineSpacing), rect.width, lineHeight);
            EditorGUI.PrefixLabel(rect, new GUIContent("targetType"));
            rect = new Rect(rect.x + lableWidth, rect.y, rect.width, rect.height);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("targetType"), GUIContent.none);


            if (property.FindPropertyRelative("targetType").enumValueIndex == (int)TargetType.Tag)
            {
                rect = new Rect(rect.x - lableWidth, rect.y + (EditorGUIUtility.singleLineHeight + lineSpacing), rect.width, lineHeight);
                EditorGUI.PrefixLabel(rect, new GUIContent("tag"));
                rect = new Rect(rect.x + lableWidth, rect.y, rect.width, rect.height);
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("tag"), GUIContent.none);
            }
            else
            {
                property.FindPropertyRelative("tag").stringValue = null;
            }


            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}


