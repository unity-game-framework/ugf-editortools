using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.Preferences
{
    [Serializable]
    public class PreferenceEditorValueData<TValue>
    {
        [SerializeField] private TValue m_value;

        public TValue Value { get { return m_value; } set { m_value = value; } }
    }
}
