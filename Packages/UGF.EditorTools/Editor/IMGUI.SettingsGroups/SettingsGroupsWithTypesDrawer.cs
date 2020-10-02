using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public class SettingsGroupsWithTypesDrawer : SettingsGroupsDrawer
    {
        public IReadOnlyDictionary<string, Type> Types { get; }

        private readonly Dictionary<string, Type> m_types = new Dictionary<string, Type>();

        public SettingsGroupsWithTypesDrawer()
        {
            Types = new ReadOnlyDictionary<string, Type>(m_types);
        }

        public void AddGroupType(string name, Type type)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (type == null) throw new ArgumentNullException(nameof(type));

            m_types.Add(name, type);
        }

        public bool RemoveGroupType(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            return m_types.Remove(name);
        }

        public void ClearGroupTypes()
        {
            m_types.Clear();
        }

        public void AddGroup(string name, GUIContent label, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            AddGroup(name, label);
            AddGroupType(name, type);
        }

        protected override void OnGroupRemoved(string name)
        {
            base.OnGroupRemoved(name);

            RemoveGroupType(name);
        }

        protected override void OnGroupsCleared()
        {
            base.OnGroupsCleared();

            ClearGroupTypes();
        }

        protected override void OnCreateSettings(SerializedProperty propertyGroups, string name, SerializedProperty propertySettings)
        {
            object settings = OnCreateSettingsInstance(name);

            propertySettings.managedReferenceValue = settings;
            propertySettings.serializedObject.ApplyModifiedProperties();
        }

        protected virtual object OnCreateSettingsInstance(string name)
        {
            if (!m_types.TryGetValue(name, out Type type)) throw new ArgumentException($"Type not found for specified group: '{name}'.");

            object settings = Activator.CreateInstance(type) ?? throw new ArgumentException($"Instance not created from specified type: '{type}'.");

            return settings;
        }
    }
}
