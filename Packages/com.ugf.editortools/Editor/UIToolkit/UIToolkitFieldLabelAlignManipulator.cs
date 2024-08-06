using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    internal class UIToolkitFieldLabelAlignManipulator : Manipulator
    {
        private float m_labelWidthRatio;
        private float m_labelExtraPadding;
        private float m_labelBaseMinWidth;
        private float m_labelExtraContextWidth;
        private VisualElement m_cachedContextWidthElement;
        private VisualElement m_cachedInspectorElement;

        private static readonly CustomStyleProperty<float> m_sLabelWidthRatioProperty = new CustomStyleProperty<float>("--unity-property-field-label-width-ratio");
        private static readonly CustomStyleProperty<float> m_sLabelExtraPaddingProperty = new CustomStyleProperty<float>("--unity-property-field-label-extra-padding");
        private static readonly CustomStyleProperty<float> m_sLabelBaseMinWidthProperty = new CustomStyleProperty<float>("--unity-property-field-label-base-min-width");
        private static readonly CustomStyleProperty<float> m_sLabelExtraContextWidthProperty = new CustomStyleProperty<float>("--unity-base-field-extra-context-width");

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            target.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
            target.parent?.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            target.UnregisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
            target.parent?.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        private void OnAttachToPanel(AttachToPanelEvent attachToPanelEvent)
        {
            if (attachToPanelEvent.destinationPanel != null
                && attachToPanelEvent.destinationPanel.contextType != ContextType.Player)
            {
                m_cachedInspectorElement = null;
                m_cachedContextWidthElement = null;

                VisualElement currentElement = target.parent;

                while (currentElement != null)
                {
                    if (currentElement.ClassListContains("unity-inspector-element"))
                    {
                        m_cachedInspectorElement = currentElement;
                    }

                    if (currentElement.ClassListContains("unity-inspector-main-container"))
                    {
                        m_cachedContextWidthElement = currentElement;
                        break;
                    }

                    currentElement = currentElement.parent;
                }

                if (m_cachedInspectorElement != null)
                {
                    m_labelWidthRatio = 0.45F;
                    m_labelExtraPadding = 37F;
                    m_labelBaseMinWidth = 123F;
                    m_labelExtraContextWidth = 1F;
                }
            }
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent customStyleResolvedEvent)
        {
            if (customStyleResolvedEvent.customStyle.TryGetValue(m_sLabelWidthRatioProperty, out var labelWidthRatio))
            {
                m_labelWidthRatio = labelWidthRatio;
            }

            if (customStyleResolvedEvent.customStyle.TryGetValue(m_sLabelExtraPaddingProperty, out var labelExtraPadding))
            {
                m_labelExtraPadding = labelExtraPadding;
            }

            if (customStyleResolvedEvent.customStyle.TryGetValue(m_sLabelBaseMinWidthProperty, out var labelBaseMinWidth))
            {
                m_labelBaseMinWidth = labelBaseMinWidth;
            }

            if (customStyleResolvedEvent.customStyle.TryGetValue(m_sLabelExtraContextWidthProperty, out var labelExtraContextWidth))
            {
                m_labelExtraContextWidth = labelExtraContextWidth;
            }

            Align();
        }

        private void OnGeometryChange(GeometryChangedEvent geometryChangedEvent)
        {
            Align();
        }

        private void Align()
        {
            if (m_cachedInspectorElement != null)
            {
                float totalPadding = m_labelExtraPadding;
                float spacing = target.worldBound.x - m_cachedInspectorElement.worldBound.x - m_cachedInspectorElement.resolvedStyle.paddingLeft;

                totalPadding += spacing;
                totalPadding += target.resolvedStyle.paddingLeft;

                float minWidth = m_labelBaseMinWidth - spacing - target.resolvedStyle.paddingLeft;

                VisualElement contextWidthElement = m_cachedContextWidthElement ?? m_cachedInspectorElement;

                target.style.minWidth = Mathf.Max(minWidth, 0F);

                float newWidth = (contextWidthElement.resolvedStyle.width + m_labelExtraContextWidth) * m_labelWidthRatio - totalPadding;

                if (Mathf.Abs(target.resolvedStyle.width - newWidth) > float.Epsilon)
                {
                    target.style.width = Mathf.Max(0F, newWidth);
                }
            }
        }
    }
}
