﻿using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    [CustomPropertyDrawer(typeof(ManagedReferenceAttribute), true)]
    internal class ManagedReferenceAttributeDrawer : PropertyDrawerTyped<ManagedReferenceAttribute>
    {
        private readonly ManagedReferenceDropdownDrawer m_drawer;

        public ManagedReferenceAttributeDrawer() : base(SerializedPropertyType.ManagedReference)
        {
            m_drawer = new ManagedReferenceDropdownDrawer(OnGetItems);
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            m_drawer.DrawGUI(position, label, property);
        }

        private IEnumerable<DropdownItem<Type>> OnGetItems()
        {
            List<DropdownItem<Type>> items = TypesDropdownEditorUtility.GetTypeItems(Attribute.TargetType);

            items.Insert(0, new DropdownItem<Type>("None"));

            return items;
        }
    }
}
