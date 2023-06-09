using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RecipeWindowScrollScaler : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private GridLayoutGroup[] _gridLayoutGroups;
    [SerializeField] private RectTransform[] _additionalRects;
    [SerializeField] private VerticalLayoutGroup[] _additionalVerticalLayoutGroups;
    [SerializeField] private float _minHeigth;
    [SerializeField] private float _additionalHeigth;
    [SerializeField] private bool _updateEditor;

    private void OnValidate()
    {
        if (!_content)
            _content = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!_updateEditor)
            return;

        float commonContentHeight = 0f;

        foreach (GridLayoutGroup gridLayoutGroup in _gridLayoutGroups)
        {
            int childCount = gridLayoutGroup.transform.ActiveChildCount();

            commonContentHeight +=
                childCount * gridLayoutGroup.cellSize.y +
                (childCount - 1) * gridLayoutGroup.spacing.y +
                gridLayoutGroup.padding.top;
        }

        foreach (RectTransform rect in _additionalRects)
            if (rect.gameObject.activeSelf) 
                commonContentHeight += rect.sizeDelta.y;

        foreach (VerticalLayoutGroup group in _additionalVerticalLayoutGroups)
            foreach (RectTransform transform in group.transform)
                if (transform.gameObject.activeSelf)
                    commonContentHeight += transform.sizeDelta.y;

        commonContentHeight += _additionalHeigth;
        commonContentHeight = Mathf.Max(_minHeigth, commonContentHeight);
        _content.sizeDelta = _content.sizeDelta.SetY(commonContentHeight);
    }
}
