using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class DropAreaHandler
    {
        public Type AcceptType { get; }

        public event DropAreaAcceptHandler Accepted;

        public DropAreaHandler(Type acceptType)
        {
            AcceptType = acceptType ?? throw new ArgumentNullException(nameof(acceptType));
        }

        public void Handle(Rect position)
        {
            OnHandle(position);
        }

        public void Accept(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            OnAccept(target);

            Accepted?.Invoke(target);
        }

        public bool Validate(Object target, out Object result)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return OnValidate(target, out result);
        }

        protected virtual bool OnValidate(Object target, out Object result)
        {
            if (AcceptType.IsInstanceOfType(target))
            {
                result = target;
                return true;
            }

            result = default;
            return false;
        }

        protected virtual void OnAccept(Object target)
        {
        }

        private void OnHandle(Rect position)
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
                            if (target != null && Validate(target, out Object result))
                            {
                                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                                if (currentEvent.type == EventType.DragPerform)
                                {
                                    Accept(result);

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
