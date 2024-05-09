using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public abstract class DropdownWindowContent<TArgument> : DropdownWindowContent
    {
        public TArgument Argument { get; protected set; }

        public DropdownWindowContentArgumentHandler<TArgument> CloseHandler { get; }

        protected DropdownWindowContent(Rect dropdownPosition, DropdownWindowContentArgumentHandler<TArgument> closeHandler, TArgument argument) : base(dropdownPosition)
        {
            CloseHandler = closeHandler ?? throw new ArgumentNullException(nameof(closeHandler));

            Argument = argument;
        }

        public override void OnClose()
        {
            base.OnClose();

            CloseHandler?.Invoke(Argument);
        }
    }
}
