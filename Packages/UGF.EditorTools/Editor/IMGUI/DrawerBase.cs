namespace UGF.EditorTools.Editor.IMGUI
{
    public abstract class DrawerBase
    {
        public void Enable()
        {
            OnEnable();
        }

        public void Disable()
        {
            OnDisable();
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }
    }
}
