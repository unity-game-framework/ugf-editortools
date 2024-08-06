using System;
using UGF.EditorTools.Runtime.IMGUI.SettingsGroups;

namespace UGF.EditorTools.Runtime.IMGUI.PlatformSettings
{
    [Serializable]
    public class PlatformSettings<TSettings> : SettingsGroups<TSettings> where TSettings : class
    {
    }
}
