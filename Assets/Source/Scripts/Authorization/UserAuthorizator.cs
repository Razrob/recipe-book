using System;
using UnityEngine;

public class UserAuthorizator : MonoBehaviour
{
    [SerializeField] private bool _enabled;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private ServerAnswerHandler _serverAnswerHandler;

    private RecipesListScreen _recipesListScreen;
    private string _userLogin;

    private void Awake()
    {
        if (!_enabled)
            return;

        _recipesListScreen = Screens.GetScreen<RecipesListScreen>();
        _recipesListScreen.LoginUIWindow.SetActive(true);
        _recipesListScreen.RegisterUIWindow.SetActive(false);

        _recipesListScreen.LoginUIWindow.LoginButton.onClick.AddListener(OnLoginScreenLoginButtonClick);
        _recipesListScreen.LoginUIWindow.RegisterButton.onClick.AddListener(OnLoginScreenRegisterButtonClick);

        _recipesListScreen.RegisterUIWindow.LoginButton.onClick.AddListener(OnRegisterScreenLoginButtonClick);
        _recipesListScreen.RegisterUIWindow.RegisterButton.onClick.AddListener(OnRegisterScreenRegisterButtonClick);

        _serverAnswerHandler.CheckUserPasswordCallback += CheckUserPasswordCallback;
        _serverAnswerHandler.RegistrationCallback += RegistrationCallback;
    }

    private void CheckUserPasswordCallback(bool value)
    {
        if (value)
            SuccessLogin();
        else
            _recipesListScreen.LoginUIWindow.SetWrongPasswordMessageActive(true);
    }

    private void RegistrationCallback(TextCallbackWithSuccessIndicator textCallback)
    {
        Debug.Log(textCallback.Text);
        if (!textCallback.Text.Contains("уже есть"))
            SuccessLogin();
        else
            _recipesListScreen.RegisterUIWindow.SetUserAlreadyExistMessageActive(true);
    }

    private void OnLoginScreenRegisterButtonClick()
    {
        _recipesListScreen.LoginUIWindow.SetActive(false);
        _recipesListScreen.RegisterUIWindow.SetActive(true);
    }

    private void OnRegisterScreenLoginButtonClick()
    {
        _recipesListScreen.LoginUIWindow.SetActive(true);
        _recipesListScreen.RegisterUIWindow.SetActive(false);
    }

    private void OnLoginScreenLoginButtonClick()
    {
        _userLogin = _recipesListScreen.LoginUIWindow.LoginField.text;
        _dataBase.CheckUserPassword(
            _recipesListScreen.LoginUIWindow.LoginField.text,
            _recipesListScreen.LoginUIWindow.PasswordField.text);
    }

    private void OnRegisterScreenRegisterButtonClick()
    {
        if (!_recipesListScreen.RegisterUIWindow.Validate())
        {
            _recipesListScreen.RegisterUIWindow.SetPasswordCannotBeEmptyMessageActive(true);
            return;
        }

        _userLogin = _recipesListScreen.RegisterUIWindow.LoginField.text;
        _dataBase.UserRegistration(
            _recipesListScreen.RegisterUIWindow.LoginField.text, 
            _recipesListScreen.RegisterUIWindow.PasswordField.text);
    }

    private void SuccessLogin()
    {
        GlobalModel.Data.UserAuthorizationData.SetUserName(_userLogin);
        _recipesListScreen.LoginUIWindow.SetActive(false);
        _recipesListScreen.RegisterUIWindow.SetActive(false);
    }
}