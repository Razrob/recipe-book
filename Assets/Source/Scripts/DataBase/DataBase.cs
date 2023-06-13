using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataBase : MonoBehaviour
{
    [SerializeField] private bool _enabled;
    [SerializeField] private ServerAnswerHandler _serverAnswerHandler;

    private string _dataBaseIp = "https://g118940.hostru13.fornex.host/db/";

    private Dictionary<ServerOperationType, string> _operationStringIp;

    public event Action<ServerOperationResult> AnswerResult;

    private string _userLoginBuffer;

    private void Start()
    {
        if (!_enabled)
            return;

        _operationStringIp = new Dictionary<ServerOperationType, string>();
        _operationStringIp.Add(ServerOperationType.Registration, "register_user.php");
        _operationStringIp.Add(ServerOperationType.CheckUserPassword, "check_user_pass.php");
        _operationStringIp.Add(ServerOperationType.GetRecipes, "get_recipes.php");
        _operationStringIp.Add(ServerOperationType.GetUserRecipes, "get_user_recipes.php");
        _operationStringIp.Add(ServerOperationType.AddUserRecipe, "add_user_recipe.php");
        _operationStringIp.Add(ServerOperationType.AddRecipe, "add_recipe.php");

        AddUserRecipe("andrey",new Recipe(12,"fdlks","fdkfs","fdjkfs",null,null));
        _serverAnswerHandler.GetUserRecipes += OnHandle;


    }

    private void OnHandle(Recipe[] f)
    {
        for (int i=0;i<f.Length;i++)
        {
            Debug.Log(f[i]);
        }    
    }

    private void AddRecipe(string login, Recipe recipe)
    {
        RecipeServer recipeServer = new RecipeServer();

        recipeServer.id = recipe.ID;
        recipeServer.name = recipe.Name;
        recipeServer.description = recipe.Description;
        recipeServer.content = recipe.Content;

        string jsonRecipeServer = JsonConvert.SerializeObject(recipeServer);

        WWWForm form = new WWWForm();
        form.AddField("Login", login);
        form.AddField("RecipeJson", jsonRecipeServer);

        StartCoroutine(GetServerAnswer(form, data =>
        {
            AnswerResult?.Invoke(new ServerOperationResult(ServerOperationType.AddRecipe, data));
            Debug.Log(data.Text);
            Debug.Log(data.Type);
        }, ServerOperationType.AddRecipe));
    }

    public void AddUserRecipe(string login, Recipe recipe)
    {
        _userLoginBuffer = login;
        AddRecipe(login, recipe);
        _serverAnswerHandler.AddedRecipe += OnAddedUserRecipe;
    }

    private void OnAddedUserRecipe(IntCallbackWithSuccessIndicator intCallbackWithSuccessIndicator)
    {
        int id = intCallbackWithSuccessIndicator.Value;

        _serverAnswerHandler.AddedRecipe -= OnAddedUserRecipe;

        WWWForm form = new WWWForm();
        form.AddField("Login", _userLoginBuffer);
        form.AddField("Id", id);

        StartCoroutine(GetServerAnswer(form, data =>
        {
            AnswerResult?.Invoke(new ServerOperationResult(ServerOperationType.AddUserRecipe, data));
            Debug.Log(data.Text);
            Debug.Log(data.Type);
        }, ServerOperationType.AddUserRecipe));
    }


    public void AddUserRecipe(string login, string password)
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


    public void GetUserRecipes(string login)
    {
        WWWForm form = new WWWForm();

        form.AddField("Login", login);

        StartCoroutine(GetServerAnswer(form, data =>
        {
            AnswerResult?.Invoke(new ServerOperationResult(ServerOperationType.GetUserRecipes, data));
            Debug.Log(data.Text);
            Debug.Log(data.Type);
        }, ServerOperationType.GetUserRecipes));
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
