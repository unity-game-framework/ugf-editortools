﻿using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ListElement : ListView
    {
        public ListElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                AddToClassList(BaseField<bool>.alignedFieldUssClassName);
            }

            bindingPath = serializedProperty.propertyPath;
        }

        public ListElement()
        {
            reorderable = true;
            allowAdd = true;
            allowRemove = true;
            showBorder = true;
            showFoldoutHeader = true;
            showAddRemoveFooter = true;
            reorderMode = ListViewReorderMode.Animated;
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;

            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent attachToPanelEvent)
        {
            TextField sizeFieldElement = this.Query<TextField>(arraySizeFieldUssClassName);
            VisualElement foldoutElement = this.Query(className: foldoutHeaderUssClassName);
            VisualElement foldoutContentElement = this.Query(className: Foldout.contentUssClassName);

            sizeFieldElement.label = "Size";
            sizeFieldElement.AddToClassList(Foldout.contentUssClassName);
            sizeFieldElement.RemoveFromClassList(arraySizeFieldUssClassName);
            sizeFieldElement.RemoveFromClassList(arraySizeFieldWithHeaderUssClassName);
            sizeFieldElement.RemoveFromClassList(arraySizeFieldWithFooterUssClassName);
            sizeFieldElement.RemoveFromHierarchy();

            UIToolkitEditorUtility.AddFieldClasses(sizeFieldElement);

            foldoutElement.RemoveFromClassList(foldoutHeaderUssClassName);
            foldoutContentElement.Insert(0, sizeFieldElement);
        }
    }
}
