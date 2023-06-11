using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;
    [SerializeField] private CanvasGroup _wrongPasswordMessage;

    private Sequence _sequence;

    public TMP_InputField LoginField => _loginField;
    public TMP_InputField PasswordField => _passwordField;
    public Button LoginButton => _loginButton;
    public Button RegisterButton => _registerButton;
    public bool IsActive { get; private set; }

    public event Action<LoginUIWindow> OnActiveChange;

    private void Awake()
    {
        gameObject.SetActive(IsActive);
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

    public void SetWrongPasswordMessageActive(bool value, bool autoDisable = true)
    {
        _sequence?.Kill();
        _wrongPasswordMessage.DOKill();
        _wrongPasswordMessage.DOFade(Convert.ToSingle(value), 0.5f);

        if (value && autoDisable)
            _sequence = DOTween.Sequence().AppendInterval(3f).AppendCallback(() => _wrongPasswordMessage.DOFade(0f, 0.5f));
    }
}
