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

        public void AddGroup(string name, GUIContent label, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            AddGroup(name, label);

            m_types.Add(name, type);
        }

        protected override void OnGroupRemoved(string name)
        {
            base.OnGroupRemoved(name);

            m_types.Remove(name);
        }

        protected override void OnGroupsCleared()
        {
            base.OnGroupsCleared();

            m_types.Clear();
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
