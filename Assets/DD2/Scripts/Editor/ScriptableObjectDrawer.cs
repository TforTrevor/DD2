using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using System.Reflection;
using System.Collections;

namespace DD2.EditorScripts
{
    //[CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    public class ScriptableObjectDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            VisualTreeAsset vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/DD2/Scripts/Editor/ScriptableObjectTemplate.uxml");
            TemplateContainer drawer = vsTree.CloneTree(property.propertyPath);

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DD2/Scripts/Editor/DD2USS.uss");
            root.styleSheets.Add(styleSheet);

            ObjectField objectField = drawer.Q<ObjectField>("stats-field");
            //objectField.objectType = property.GetField().FieldType;
            objectField.BindProperty(property);

            Button button = drawer.Q<Button>("new-button");
            button.RegisterCallback<MouseUpEvent>(e =>
            {
                Stats asset = ScriptableObject.CreateInstance<Stats>();
                string path = "Assets/DD2/Scripts/Scriptable Objects/" + typeof(Stats).Name;
                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/ " + property.serializedObject.targetObject.name + " " + typeof(Stats).Name + ".asset");
                AssetDatabase.CreateAsset(asset, assetPathAndName);
                AssetDatabase.SaveAssets();
                objectField.value = asset;
            });

            Foldout foldout = drawer.Q<Foldout>("foldout");
            foldout.value = false;
            VisualElement foldoutContent = drawer.Q<VisualElement>("foldout-content");
            foldout.RegisterCallback<ChangeEvent<bool>>(e =>
            {
                if (e.newValue)
                {
                    foldoutContent.Add(CreateDropdown(property));
                }
                else
                {
                    foldoutContent.Clear();
                }
            });

            objectField.RegisterCallback<ChangeEvent<string>>(e =>
            {
                if (objectField.value != null)
                {
                    foldoutContent.Clear();
                    foldoutContent.Add(CreateDropdown(new SerializedObject(objectField.value)));
                }
                else
                {
                    foldoutContent.Clear();
                }
            });

            root.Add(drawer);
            return root;
        }

        VisualElement CreateDropdown(SerializedObject targetObject)
        {
            Box container = new Box();
            container.AddToClassList("scriptable-object-dropdown");

            if (targetObject != null)
            {
                SerializedProperty iterator = targetObject.GetIterator();
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        if (iterator.propertyPath == "m_Script")
                        {
                            continue;
                        }

                        PropertyField field = new PropertyField(iterator);
                        field.Bind(targetObject);
                        container.Add(field);
                    }
                    while (iterator.NextVisible(false));
                }
            }
            return container;
        }

        VisualElement CreateDropdown(SerializedProperty property)
        {
            if (property.objectReferenceValue != null)
            {
                SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);
                return CreateDropdown(targetObject);
            }
            return new VisualElement();
        }
    }
}