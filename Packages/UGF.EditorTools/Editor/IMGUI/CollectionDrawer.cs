using System;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class CollectionDrawer : DrawerBase
    {
        public SerializedProperty SerializedProperty { get; }
        public SerializedProperty PropertySize { get; }
        public bool EnableDragAndDropAdding { get; set; } = true;

        public CollectionDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
            PropertySize = SerializedProperty.FindPropertyRelative("Array.size");
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
                OnDragAndDrop(foldoutPosition);
            }

            if (foldout)
            {
                float space = EditorGUIUtility.standardVerticalSpacing;
                var sizePosition = new Rect(position.x, foldoutPosition.yMax + space, position.width, OnGetSizeHeight());
                var collectionPosition = new Rect(position.x, sizePosition.yMax + space, position.width, OnGetCollectionHeight());

                using (new IndentIncrementScope(1))
                {
                    OnDrawSize(sizePosition);
                    OnDrawCollection(collectionPosition);
                }
            }
        }

        protected virtual float OnGetHeight()
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float foldout = OnGetFoldoutHeight();
            float size = OnGetSizeHeight();
            float list = OnGetCollectionHeight();

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

        protected virtual void OnDrawCollection(Rect position)
        {
            for (int i = 0; i < SerializedProperty.arraySize; i++)
            {
                float height = OnElementHeight(i);

                position.height = height;

                OnDrawElement(position, i);

                position.y += height;
            }
        }

        protected virtual float OnGetCollectionHeight()
        {
            float height = 0F;

            for (int i = 0; i < SerializedProperty.arraySize; i++)
            {
                float elementHeight = OnElementHeight(i);

                height += elementHeight;
            }

            return height;
        }

        protected virtual void OnDrawElement(Rect position, int index)
        {
            float height = EditorGUIUtility.singleLineHeight;

            position.height = height;

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            OnDrawElementContent(position, propertyElement, index);
        }

        protected virtual void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index)
        {
            EditorGUI.PropertyField(position, serializedProperty, true);
        }

        protected virtual float OnElementHeight(int index)
        {
            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            float value = OnElementHeightContent(propertyElement, index);

            value += EditorGUIUtility.standardVerticalSpacing;

            return value;
        }

        protected virtual float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUI.GetPropertyHeight(serializedProperty, true);
        }

        protected virtual bool OnDragAndDropValidate(Object target, out Object result)
        {
            if (SerializedProperty.arrayElementType is nameof(GlobalId) or nameof(Hash128))
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

            switch (SerializedProperty.arrayElementType)
            {
                case nameof(GlobalId):
                {
                    GlobalIdEditorUtility.SetAssetToProperty(propertyElement, target);
                    break;
                }
                case nameof(Hash128):
                {
                    string path = AssetDatabase.GetAssetPath(target);
                    string guid = AssetDatabase.AssetPathToGUID(path);

                    propertyElement.hash128Value = GlobalId.TryParse(guid, out GlobalId id) ? id : default;
                    break;
                }
                default:
                {
                    propertyElement.objectReferenceValue = target;
                    break;
                }
            }
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
