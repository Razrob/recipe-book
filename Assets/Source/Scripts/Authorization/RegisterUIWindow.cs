using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUIWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_InputField _repeatPasswordField;
    [SerializeField] private Image _repeatPasswordFieldImage;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;
    [SerializeField] private CanvasGroup _userAlreadyExistMessage;
    [SerializeField] private CanvasGroup _passwordCannotBeEmptyMessage;
    [SerializeField] private Color _correctPasswordColor; 
    [SerializeField] private Color _incorrectPasswordColor;

    private Sequence _sequence;

    public TMP_InputField LoginField => _loginField;
    public TMP_InputField PasswordField => _passwordField;
    public Button LoginButton => _loginButton;
    public Button RegisterButton => _registerButton;
    public bool IsActive { get; private set; }

    public event Action<RegisterUIWindow> OnActiveChange;

    private void Awake()
    {
        gameObject.SetActive(IsActive);

        _passwordField.onValueChanged.AddListener(t => UpdateRepeatPasswordFieldColor());
        _repeatPasswordField.onValueChanged.AddListener(t => UpdateRepeatPasswordFieldColor());
    }

    private void UpdateRepeatPasswordFieldColor()
    {
        if (string.IsNullOrEmpty(_passwordField.text) && string.IsNullOrEmpty(_repeatPasswordField.text))
            _repeatPasswordFieldImage.color = Color.white;
        else if (_passwordField.text == _repeatPasswordField.text)
            _repeatPasswordFieldImage.color = _correctPasswordColor.SetAlfa(_repeatPasswordFieldImage.color.a);
        else
            _repeatPasswordFieldImage.color = _incorrectPasswordColor.SetAlfa(_repeatPasswordFieldImage.color.a);
    }

    public bool Validate()
    {
        return !string.IsNullOrEmpty(_passwordField.text) && _passwordField.text == _repeatPasswordField.text;
    }

    public void SetActive(bool value)
    {
        if (IsActive == value)
            return;

        IsActive = value;
        gameObject.SetActive(value);

        //if (IsActive)
        //    RefreshView();

        OnActiveChange?.Invoke(this);
    }

    public void SetUserAlreadyExistMessageActive(bool value, bool autoDisable = true)
    {
        _sequence?.Kill();
        _userAlreadyExistMessage.DOKill();
        _userAlreadyExistMessage.DOFade(Convert.ToSingle(value), 0.5f);

        if (value && autoDisable)
            _sequence = DOTween.Sequence().AppendInterval(3f).AppendCallback(() => _userAlreadyExistMessage.DOFade(0f, 0.5f));
    }

    public void SetPasswordCannotBeEmptyMessageActive(bool value, bool autoDisable = true)
    {
        _sequence?.Kill();
        _passwordCannotBeEmptyMessage.DOKill();
        _passwordCannotBeEmptyMessage.DOFade(Convert.ToSingle(value), 0.5f);

        if (value && autoDisable)
            _sequence = DOTween.Sequence().AppendInterval(3f).AppendCallback(() => _passwordCannotBeEmptyMessage.DOFade(0f, 0.5f));
    }
}
