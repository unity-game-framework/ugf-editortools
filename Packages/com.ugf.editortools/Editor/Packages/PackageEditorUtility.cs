using System;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace UGF.EditorTools.Editor.Packages
{
    public static class PackageEditorUtility
    {
        public static PackageInfo GetPackage(string name)
        {
            return TryGetPackage(name, out PackageInfo package) ? package : throw new ArgumentException($"Package Info not found by the specified name: '{name}'.");
        }

        public static bool TryGetPackage(string name, out PackageInfo package)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            PackageInfo[] packages = PackageInfo.GetAllRegisteredPackages();

            for (int i = 0; i < packages.Length; i++)
            {
                package = packages[i];

                if (package.name == name)
                {
                    return true;
                }
            }

            package = default;
            return false;
        }
    }
}
