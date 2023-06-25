using System;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListDrawer : DrawerBase
    {
        public SerializedProperty SerializedProperty { get; }
        public SerializedProperty PropertySize { get; }
        public ReorderableList List { get; }
        public bool DisplayAsSingleLine { get; set; }
        public bool EnableDragAndDropAdding { get; set; } = true;

        public event Action Added;
        public event Action Removed;
        public event Action Selected;
        public event Action SelectionUpdated;

        public ReorderableListDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
            PropertySize = SerializedProperty.FindPropertyRelative("Array.size");

            List = new ReorderableList(serializedProperty.serializedObject, serializedProperty)
            {
                headerHeight = EditorGUIUtility.standardVerticalSpacing,
                drawHeaderCallback = OnDrawHeader,
                drawElementCallback = OnDrawElement,
                elementHeightCallback = OnElementHeight,
                onAddCallback = OnListAdd,
                onRemoveCallback = OnListRemove,
                onSelectCallback = OnListSelect
            };
        }

        public void DrawGUILayout()
        {
            OnDrawGUILayout();
        }

        public void DrawGUI(Rect position)
        {
            OnDrawGUI(position);
        }

        public float GetHeight()
        {
            return OnGetHeight();
        }

        protected virtual void OnDrawGUILayout(GUIContent label = null)
        {
            label ??= new GUIContent(SerializedProperty.displayName);

            float height = GetHeight();
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, height);

            OnDrawGUI(position, label);
        }

        protected virtual void OnDrawGUI(Rect position, GUIContent label = null)
        {
            label ??= new GUIContent(SerializedProperty.displayName);

            var foldoutPosition = new Rect(position.x, position.y, position.width, OnGetFoldoutHeight());
            bool foldout = OnDrawFoldout(foldoutPosition, label);

            if (EnableDragAndDropAdding)
            {
                OnDragAndDrop(position);
            }

            if (foldout)
            {
                float space = EditorGUIUtility.standardVerticalSpacing;
                var sizePosition = new Rect(position.x, foldoutPosition.y + foldoutPosition.height + space, position.width, OnGetSizeHeight());
                var listPosition = new Rect(position.x, sizePosition.y + sizePosition.height + space, position.width, OnGetListHeight());

                listPosition = EditorGUI.IndentedRect(listPosition);

                using (new IndentIncrementScope(1))
                {
                    OnDrawSize(sizePosition);

                    float padding = List.draggable ? ReorderableList.Defaults.dragHandleWidth : ReorderableList.Defaults.padding;

                    using (new LabelWidthChangeScope(-padding, true))
                    using (new IndentLevelScope(0))
                    {
                        List.DoList(listPosition);
                    }
                }
            }
        }

        protected virtual float OnGetHeight()
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float foldout = OnGetFoldoutHeight();
            float size = OnGetSizeHeight();
            float list = OnGetListHeight();

            return SerializedProperty.isExpanded ? foldout + space + size + space + list : foldout;
        }

        protected virtual bool OnDrawFoldout(Rect position, GUIContent label)
        {
            SerializedProperty.isExpanded = EditorGUI.Foldout(position, SerializedProperty.isExpanded, label, true);

            return SerializedProperty.isExpanded;
        }

        protected virtual float OnGetFoldoutHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected virtual void OnDrawSize(Rect position)
        {
            EditorGUI.PropertyField(position, PropertySize);
        }

        protected virtual float OnGetSizeHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected virtual float OnGetListHeight()
        {
            return List.GetHeight();
        }

        protected virtual void OnDrawHeader(Rect position)
        {
        }

        protected virtual void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            position.height = height;
            position.y += space;

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            if (OnElementHasVisibleChildren(propertyElement))
            {
                position.xMin += height - space;
            }

            OnDrawElementContent(position, propertyElement, index, isActive, isFocused);
        }

        protected virtual void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            if (OnElementHasVisibleChildren(serializedProperty))
            {
                EditorGUI.PropertyField(position, serializedProperty, true);
            }
            else
            {
                if (serializedProperty.propertyType == SerializedPropertyType.ManagedReference)
                {
                    EditorGUI.PropertyField(position, serializedProperty);
                }
                else
                {
                    EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
                }
            }
        }

        protected virtual float OnElementHeight(int index)
        {
            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            float value = OnElementHeightContent(propertyElement, index);

            value += EditorGUIUtility.standardVerticalSpacing * 2F;

            return value;
        }

        protected virtual float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUI.GetPropertyHeight(serializedProperty, true);
        }

        protected virtual bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return !DisplayAsSingleLine && serializedProperty.hasVisibleChildren;
        }

        protected virtual void OnAdd()
        {
            SerializedProperty.InsertArrayElementAtIndex(SerializedProperty.arraySize);

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(SerializedProperty.arraySize - 1);

            switch (propertyElement.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                {
                    propertyElement.objectReferenceValue = null;
                    break;
                }
                case SerializedPropertyType.ManagedReference:
                {
                    propertyElement.managedReferenceValue = null;
                    break;
                }
            }

            propertyElement.serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnRemove()
        {
            SerializedProperty.DeleteArrayElementAtIndex(List.index);
            SerializedProperty.serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnSelect()
        {
        }

        protected virtual void OnSelectionUpdate()
        {
        }

        protected virtual bool OnDragAndDropValidate(Object target, out Object result)
        {
            if (SerializedProperty.arrayElementType == nameof(GlobalId))
            {
                if (AssetIdEditorUtility.CheckAssetIdAttributeType(SerializedProperty, target))
                {
                    result = target;
                    return true;
                }

                result = default;
                return false;
            }

            return SerializedPropertyEditorUtility.TryValidateObjectFieldAssignment(SerializedProperty, target, out result);
        }

        protected virtual void OnDragAndDropAccept(Object target)
        {
            SerializedProperty.InsertArrayElementAtIndex(SerializedProperty.arraySize);

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(SerializedProperty.arraySize - 1);

            if (SerializedProperty.arrayElementType == nameof(GlobalId))
            {
                GlobalIdEditorUtility.SetAssetToProperty(propertyElement, target);
            }
            else
            {
                propertyElement.objectReferenceValue = target;
            }
        }

        private void OnListAdd(ReorderableList list)
        {
            OnAdd();

            Added?.Invoke();

            OnSelectionUpdate();

            SelectionUpdated?.Invoke();
        }

        private void OnListRemove(ReorderableList list)
        {
            OnRemove();

            Removed?.Invoke();

            OnSelectionUpdate();

            SelectionUpdated?.Invoke();
        }

        private void OnListSelect(ReorderableList list)
        {
            OnSelect();

            Selected?.Invoke();

            OnSelectionUpdate();

            SelectionUpdated?.Invoke();
        }

        private void OnDragAndDrop(Rect position)
        {
            int id = EditorIMGUIUtility.GetLastControlId();
            Event currentEvent = Event.current;

            switch (currentEvent.type)
            {
                case EventType.DragExited:
                {
                    if (GUI.enabled)
                    {
                        HandleUtility.Repaint();
                    }

                    break;
                }
                case EventType.DragUpdated:
                case EventType.DragPerform:
                {
                    if (position.Contains(currentEvent.mousePosition) && GUI.enabled)
                    {
                        Object[] references = DragAndDrop.objectReferences;
                        bool accepted = false;

                        foreach (Object target in references)
                        {
                            if (target != null && OnDragAndDropValidate(target, out Object result))
                            {
                                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                                if (currentEvent.type == EventType.DragPerform)
                                {
                                    OnDragAndDropAccept(result);

                                    DragAndDrop.activeControlID = 0;

                                    accepted = true;
                                }
                                else
                                {
                                    DragAndDrop.activeControlID = id;
                                }
                            }
                        }

                        if (accepted)
                        {
                            GUI.changed = true;
                            DragAndDrop.AcceptDrag();
                        }
                    }

                    break;
                }
            }
        }
    }
}
