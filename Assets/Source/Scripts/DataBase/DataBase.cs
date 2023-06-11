using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataBase : MonoBehaviour
{
    [SerializeField] private bool _enabled;

    private string _dataBaseIp = "https://g118940.hostru13.fornex.host/db/";

    private Dictionary<ServerOperationType, string> _operationStringIp;

    public event Action<ServerOperationResult> AnswerResult;

    private void Start()
    {
        if (!_enabled)
            return;

        _operationStringIp = new Dictionary<ServerOperationType, string>();
        _operationStringIp.Add(ServerOperationType.Registration, "register_user.php");
        _operationStringIp.Add(ServerOperationType.CheckUserPassword, "check_user_pass.php");
        _operationStringIp.Add(ServerOperationType.GetRecipes, "get_recipes.php");

        GetRecipes();
    }

    public void UserRegistration(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Login", login);
        form.AddField("Pass", password);

        StartCoroutine(GetServerAnswer(form, data =>
        {
            AnswerResult?.Invoke(new ServerOperationResult(ServerOperationType.Registration, data));
            Debug.Log(data.Text);
            Debug.Log(data.Type);    
        }, ServerOperationType.Registration));
    }

    public void GetRecipes()
    {
        WWWForm form = new WWWForm();
        StartCoroutine(GetServerAnswer(form, data =>
        {
            AnswerResult?.Invoke(new ServerOperationResult(ServerOperationType.GetRecipes, data));
            Debug.Log(data.Text);
            Debug.Log(data.Type);
        }, ServerOperationType.GetRecipes));
    }


    public void CheckUserPassword(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Login", login);
        form.AddField("Pass", password);

        StartCoroutine(GetServerAnswer(form, data =>
        {
            AnswerResult?.Invoke(new ServerOperationResult(ServerOperationType.CheckUserPassword, data));
            Debug.Log(data.Text);
            Debug.Log(data.Type);
        }, ServerOperationType.CheckUserPassword));
    }

    private string GetOperationTypeStringIp(ServerOperationType operationType)
    {
        return _operationStringIp[operationType];
    }


    private IEnumerator GetServerAnswer(WWWForm form, Action<ServerAnswerResult> returnData, ServerOperationType operationType)
    {
        UnityWebRequest www = UnityWebRequest.Post(_dataBaseIp+ GetOperationTypeStringIp(operationType), form);

        www.certificateHandler = new BypassCertificate();

        yield return www.SendWebRequest();

        using (www)
        {
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error" + www.error);
                returnData(new ServerAnswerResult(ServerAnswerResultType.Error, www.error));
            }
            else
            {
                Debug.Log("Form upload complete!");
                returnData(new ServerAnswerResult(ServerAnswerResultType.Access, www.downloadHandler.text));
            }   
        }
    }
}
