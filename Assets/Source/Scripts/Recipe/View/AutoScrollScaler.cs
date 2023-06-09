using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AutoScrollScaler : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private RectTransform _copyRect;
    [SerializeField] private float _minHeigth;
    [SerializeField] private float _additionalHeigth;
    [SerializeField] private bool _updateEditor;
    [SerializeField] private bool _updateCopyRectPositionY;

    private void OnValidate()
    {
        if (!_content)
            _content = GetComponent<RectTransform>();

        if (!_gridLayoutGroup)
            _gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
    }

    private void Update()
    {
        if (!_updateEditor)
            return;

        float commonContentHeight;

        if (!_copyRect)
        {
            int childCount = _gridLayoutGroup.transform.ActiveChildCount();

            commonContentHeight =
                childCount * _gridLayoutGroup.cellSize.y +
                (childCount - 1) * _gridLayoutGroup.spacing.y +
                _gridLayoutGroup.padding.top;
        }
        else
        {
            commonContentHeight = _copyRect.sizeDelta.y;

            if (_updateCopyRectPositionY)
                _copyRect.localPosition = _copyRect.localPosition.SetY(_copyRect.sizeDelta.y * -0.5f);
        }

        commonContentHeight += _additionalHeigth;
        commonContentHeight = Mathf.Max(_minHeigth, commonContentHeight);
        _content.sizeDelta = _content.sizeDelta.SetY(commonContentHeight);
    }
}
