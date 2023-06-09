using UnityEngine;

[ExecuteAlways]
public class RectPositionSetter : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _sizeYMultiplier;
    [SerializeField] private bool _updateEditor;

    private void OnValidate()
    {
        if (!_rect)
            _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!_updateEditor)
            return;

        _rect.localPosition = _rect.localPosition.SetY(_rect.sizeDelta.y * _sizeYMultiplier);
    }
}
