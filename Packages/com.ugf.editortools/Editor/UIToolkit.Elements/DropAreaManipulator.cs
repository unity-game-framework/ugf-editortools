using System;
using UnityEditor;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class DropAreaManipulator : PointerManipulator
    {
        public DropAreaElement AreaElement { get; }
        public Type AcceptType { get; }

        public event DropAreaManipulatorAcceptHandler Accepted;

        public DropAreaManipulator(DropAreaElement areaElement, Type acceptType)
        {
            AreaElement = areaElement ?? throw new ArgumentNullException(nameof(areaElement));
            AcceptType = acceptType ?? throw new ArgumentNullException(nameof(acceptType));

            target = AreaElement;
        }

        public void Accept(Object targetObject)
        {
            if (targetObject == null) throw new ArgumentNullException(nameof(targetObject));

            OnAccept(targetObject);

            Accepted?.Invoke(targetObject);
        }

        public bool Validate(Object targetObject, out Object result)
        {
            if (targetObject == null) throw new ArgumentNullException(nameof(targetObject));

            return OnValidate(targetObject, out result);
        }

        protected virtual bool OnValidate(Object targetObject, out Object result)
        {
            if (AcceptType.IsInstanceOfType(targetObject))
            {
                result = targetObject;
                return true;
            }

            result = default;
            return false;
        }

        protected virtual void OnAccept(Object targetObject)
        {
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(OnPointerDown);
            target.RegisterCallback<DragEnterEvent>(OnDragEnter);
            target.RegisterCallback<DragLeaveEvent>(OnDragLeave);
            target.RegisterCallback<DragUpdatedEvent>(OnDragUpdate);
            target.RegisterCallback<DragPerformEvent>(OnDragPerform);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            target.UnregisterCallback<DragEnterEvent>(OnDragEnter);
            target.UnregisterCallback<DragLeaveEvent>(OnDragLeave);
            target.UnregisterCallback<DragUpdatedEvent>(OnDragUpdate);
            target.UnregisterCallback<DragPerformEvent>(OnDragPerform);
        }

        private void OnPointerDown(PointerDownEvent pointerDownEvent)
        {
        }

        private void OnDragEnter(DragEnterEvent dragEnterEvent)
        {
        }

        private void OnDragLeave(DragLeaveEvent dragLeaveEvent)
        {
        }

        private void OnDragUpdate(DragUpdatedEvent dragUpdatedEvent)
        {
            bool hasValid = false;

            for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
            {
                Object objectReference = DragAndDrop.objectReferences[i];

                if (objectReference != null && Validate(objectReference, out _))
                {
                    hasValid = true;
                    break;
                }
            }

            if (hasValid)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            }
        }

        private void OnDragPerform(DragPerformEvent dragPerformEvent)
        {
            for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
            {
                Object objectReference = DragAndDrop.objectReferences[i];

                if (objectReference != null && Validate(objectReference, out Object result))
                {
                    Accept(result);
                }
            }
        }
    }
}
