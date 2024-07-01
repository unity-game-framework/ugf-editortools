using System;
using System.Reflection;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    public static class AssemblyUtility
    {
        public static Assembly GetAssemblyByFullName(string fullName)
        {
            return TryGetAssemblyByFullName(fullName, out Assembly assembly) ? assembly : throw new ArgumentException($"Assembly not found by the specified full name: '{fullName}'.");
        }

        public static bool TryGetAssemblyByFullName(string fullName, out Assembly assembly)
        {
            if (string.IsNullOrEmpty(fullName)) throw new ArgumentException("Value cannot be null or empty.", nameof(fullName));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                assembly = assemblies[i];

                if (assembly.FullName == fullName)
                {
                    return true;
                }
            }

            assembly = default;
            return false;
        }
    }
}
