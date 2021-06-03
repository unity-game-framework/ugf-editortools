using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Pages
{
    public class PagesCollectionDrawer : CollectionDrawer
    {
        public int PageIndex
        {
            get { return m_pageIndex; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than zero.");

                m_pageIndex = value;
            }
        }

        public int CountPerPage
        {
            get { return m_countPerPage; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than zero.");

                m_countPerPage = value;
            }
        }

        public int PageCount { get { return Mathf.CeilToInt((float)SerializedProperty.arraySize / CountPerPage); } }

        private readonly string m_pageIndexPrefPath;
        private int m_countPerPage = 10;
        private Styles m_styles;
        private int m_pageIndex;

        private class Styles
        {
            public GUIContent PageLabel { get; } = new GUIContent("Page");
        }

        public PagesCollectionDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
            m_pageIndexPrefPath = serializedProperty.propertyPath;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            m_pageIndex = EditorPrefs.GetInt(m_pageIndexPrefPath);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EditorPrefs.SetInt(m_pageIndexPrefPath, m_pageIndex);
        }

        protected override void OnDrawGUI(Rect position, GUIContent label = null)
        {
            m_styles ??= new Styles();

            base.OnDrawGUI(position, label);
        }

        protected override void OnDrawSize(Rect position)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var rectSize = new Rect(position.x, position.y, position.width, height);
            var rectPage = new Rect(position.x, rectSize.yMax + space, position.width, height);

            EditorGUI.PropertyField(rectSize, PropertySize);

            int max = PageCount > 0 ? PageCount - 1 : 0;

            m_pageIndex = EditorGUI.IntSlider(rectPage, m_styles.PageLabel, m_pageIndex, 0, max);
        }

        protected override float OnGetSizeHeight()
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            return height * 2F + space;
        }

        protected override void OnDrawCollection(Rect position)
        {
            if (PageCount > 0)
            {
                int start = m_pageIndex * CountPerPage;
                int end = start + CountPerPage;

                for (int i = start; i < end && i < SerializedProperty.arraySize; i++)
                {
                    float height = OnElementHeight(i);

                    position.height = height;

                    OnDrawElement(position, i);

                    position.y += height;
                }
            }
        }

        protected override float OnGetCollectionHeight()
        {
            float height = 0F;

            if (PageCount > 0)
            {
                int start = m_pageIndex * CountPerPage;
                int end = start + CountPerPage;

                for (int i = start; i < end && i < SerializedProperty.arraySize; i++)
                {
                    float elementHeight = OnElementHeight(i);

                    height += elementHeight;
                }
            }

            return height;
        }
    }
}
