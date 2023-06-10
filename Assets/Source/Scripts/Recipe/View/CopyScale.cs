using UnityEngine;

[ExecuteAlways]
public class CopyScale : MonoBehaviour
{
    [SerializeField] private RectTransform _copyFrom;
    [SerializeField] private bool _scaleX;
    [SerializeField] private bool _scaleY;
    [SerializeField] private float _minScaleX;
    [SerializeField] private float _minScaleY;

    private RectTransform _rectTransform;

    private void Update()
    {
        if (!_rectTransform)
            _rectTransform = GetComponent<RectTransform>();

        if (!_copyFrom)
            return;

        //_copyFrom.

        if (_scaleX && _copyFrom.sizeDelta.x > 0.001f)
            _rectTransform.sizeDelta = _rectTransform.sizeDelta.SetX(Mathf.Max(_minScaleX, _copyFrom.sizeDelta.x));

        if (_scaleY && _copyFrom.sizeDelta.y > 0.001f)
            _rectTransform.sizeDelta = _rectTransform.sizeDelta.SetY(Mathf.Max(_minScaleY, _copyFrom.sizeDelta.y));
    }
}