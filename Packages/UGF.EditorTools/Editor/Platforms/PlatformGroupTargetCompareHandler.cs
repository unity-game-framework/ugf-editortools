using UnityEditor;

namespace UGF.EditorTools.Editor.Platforms
{
    public delegate bool PlatformGroupTargetCompareHandler<in TTarget>(SerializedProperty serializedProperty, TTarget target);
}
