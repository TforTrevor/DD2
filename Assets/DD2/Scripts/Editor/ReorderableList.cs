using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using System.Reflection;

namespace DD2.EditorScripts
{
    //[CustomPropertyDrawer(typeof(ReorderableAttribute), true)]
    public class ReorderableList : VisualElement
    {
        public ReorderableList(SerializedProperty property)
        {
            VisualTreeAsset vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/DD2/Scripts/Editor/ReorderableListTemplate.uxml");
            TemplateContainer container = vsTree.CloneTree(property.propertyPath);

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DD2/Scripts/Editor/DD2USS.uss");
            styleSheets.Add(styleSheet);

            container.Q<Label>("header").text = property.displayName;
            Box content = container.Q<Box>("content");

            for (int i = 0; i < property.arraySize; i++)
            {
                content.Add(MakeRow(property.GetArrayElementAtIndex(i)));
            }

            Add(container);
        }

        public VisualElement MakeRow(SerializedProperty property)
        {
            VisualTreeAsset vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/DD2/Scripts/Editor/ListRowTemplate.uxml");
            TemplateContainer container = vsTree.CloneTree(property.propertyPath);

            PropertyField field = container.Q<PropertyField>("property-field");
            field.bindingPath = property.propertyPath;
            field.Bind(property.serializedObject);
            //PropertyField test = new PropertyField(property);
            //return test;

            return container;
        }
    }
}