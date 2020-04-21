using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace DD2.EditorScripts
{
    public static class UIElementsEditorHelper
    {
        public static void FillDefaultInspector(VisualElement container, SerializedObject serializedObject, bool hideScript)
        {
            SerializedProperty property = serializedObject.GetIterator();
            if (property.NextVisible(true)) // Expand first child.
            {
                do
                {
                    if (property.propertyPath == "m_Script" && hideScript)
                    {
                        continue;
                    }
                    VisualElement field;
                    if (property.isArray)
                    {
                        field = new ReorderableList(property);
                    }
                    else
                    {
                        field = new PropertyField(property);
                        field.name = "PropertyField:" + property.propertyPath;
                    }

                    if (property.propertyPath == "m_Script" && serializedObject.targetObject != null)
                    {
                        field.SetEnabled(false);
                    }

                    container.Add(field);
                }
                while (property.NextVisible(false));
            }
        }
    }

    //[CustomEditor(typeof(MonoBehaviour), true)]
    public class EntityEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            UIElementsEditorHelper.FillDefaultInspector(root, serializedObject, false);
            return root;
        }
    }
}