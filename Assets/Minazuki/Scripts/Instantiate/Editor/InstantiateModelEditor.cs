using UnityEditor;
using UnityEngine;
namespace Minazuki.Editor
{
    /// <summary>
    /// 实例化模型编辑器设置
    /// </summary>
    [CustomPropertyDrawer(typeof(InstantiateModel))]
    public class InstantiateModelEditor : PropertyDrawer
    {
        /// <summary>
        /// 行高
        /// </summary>
        private int lineHeight;
        /// <summary>
        /// 是否展开
        /// </summary>
        private bool isExpanded = false;
        /// <summary>
        /// 获取属性高度
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            lineHeight = (int)EditorGUIUtility.singleLineHeight;
            float height = lineHeight;
            height+= lineHeight+ Common.lineSpacing;

            if(isExpanded )//如果展开
            {
                if (property.FindPropertyRelative("setParent").boolValue)//选中设置父节点对象
                {
                    height += lineHeight * 6 + Common.lineSpacing * 4;
                }
                else//未选中设置父节点对象
                {
                    height += lineHeight+ Common.lineSpacing;
                }

                if (property.FindPropertyRelative("setReference").boolValue)//选中设置引用对象
                {
                    height+= lineHeight * 5 + Common.lineSpacing * 3;
                }
                else//未选中设置引用对象
                {
                    height += lineHeight;
                }

            }
            else//未展开
            {
                height = EditorGUIUtility.singleLineHeight;
            }
            return height;
        }

        

        /// <summary>
        /// 绘制属性
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var rect = position;

            //设置标题
            var title = "";
            if (property.FindPropertyRelative("prefab").objectReferenceValue != null)
            {
                title += property.FindPropertyRelative("prefab").objectReferenceValue.name;
            }
            if (property.FindPropertyRelative("parentPath").stringValue.Trim().Length > 0)
            {
                title += "=>" + Common.GetLastGameObjectNameByFullPath(property.FindPropertyRelative("parentPath").stringValue.Trim());
            }
            if (property.FindPropertyRelative("referencePath").stringValue.Trim().Length > 0)
            {
                title += "=" + Common.GetLastGameObjectNameByFullPath(property.FindPropertyRelative("referencePath").stringValue.Trim());
            }

            //绘制标题
            rect = new Rect(rect.x, rect.y, rect.width, lineHeight);
            isExpanded = EditorGUI.BeginFoldoutHeaderGroup(rect, isExpanded, title);

            if (isExpanded)//如果展开
            {
                //绘制缩进
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel++;

                //绘制预制件属性
                rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight);
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("prefab"), new GUIContent("Prefab"));

                //绘制设置父节点对象属性
                rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight);
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("setParent"), new GUIContent("Set Parent"));

                if (property.FindPropertyRelative("setParent").boolValue)//如果选中设置父节点
                {
                    //绘制父节点对象属性
                    rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight);
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("parentGameObject"), new GUIContent("Parent GameObject"));
                    if (property.FindPropertyRelative("parentGameObject").objectReferenceValue != null)
                    {
                        //根据父节点对象获取父节点路径
                        property.FindPropertyRelative("parentPath").stringValue = Common.GetFullPathByGameObject((Transform)property.FindPropertyRelative("parentGameObject").objectReferenceValue);
                    }
                    //绘制父节点路径属性
                    rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight * 3);
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("parentPath"), new GUIContent("Parent Path"));
                    //绘制目标类型属性
                    rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 3 + Common.lineSpacing), rect.width, lineHeight);
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("targetType"), new GUIContent("Target Type"));

                    if (property.FindPropertyRelative("targetType").enumValueIndex == (int)TargetType.Tag)
                    {
                        //如果目标类型为Tag绘制目标标签属性
                        rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight);
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("tag"), new GUIContent("Tag"));
                    }
                    else
                    {
                        property.FindPropertyRelative("tag").stringValue = null;
                    }

                    if(property.FindPropertyRelative("targetType").enumValueIndex != (int)TargetType.Self)
                    {
                        property.FindPropertyRelative("setReference").boolValue = false;
                    }
                }
                else
                {
                    property.FindPropertyRelative("parentGameObject").objectReferenceValue = null;
                    property.FindPropertyRelative("parentPath").stringValue = null;
                    property.FindPropertyRelative("targetType").enumValueIndex = 0;
                    property.FindPropertyRelative("tag").stringValue = null;
                }

                if (property.FindPropertyRelative("targetType").enumValueIndex == 0)
                {
                    //如果目标类型为滋生，绘制设置引用对象属性
                    rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight);
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("setReference"), new GUIContent("Set Reference"));
                }
                else
                {
                    property.FindPropertyRelative("setReference").boolValue = false;
                }

                if (property.FindPropertyRelative("setReference").boolValue)//如果选中设置引用对象
                {
                    //绘制引用对象属性
                    rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight);
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("referenceGameObject"), new GUIContent("Referencet GameObject"));
                    if (property.FindPropertyRelative("referenceGameObject").objectReferenceValue != null)
                    {
                        //根据引用对象获取引用路径
                        property.FindPropertyRelative("referencePath").stringValue = Common.GetFullPathByGameObject((Transform)property.FindPropertyRelative("referenceGameObject").objectReferenceValue);
                    }
                    //绘制引用路径属性
                    rect = new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + Common.lineSpacing), rect.width, lineHeight * 3);
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("referencePath"), new GUIContent("Parent referencePath"));
                }
                else
                {
                    property.FindPropertyRelative("referenceGameObject").objectReferenceValue = null;
                    property.FindPropertyRelative("referencePath").stringValue = null;
                }

                EditorGUI.indentLevel = indent;
            }

            EditorGUI.EndFoldoutHeaderGroup();

            EditorGUI.EndProperty();


        }
    }
}