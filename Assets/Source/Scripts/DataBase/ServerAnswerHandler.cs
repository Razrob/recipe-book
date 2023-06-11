using Newtonsoft.Json;
using System;
using UnityEngine;

public class ServerAnswerHandler : MonoBehaviour
{
    [SerializeField] private DataBase _dataBase;

    public event Action<TextCallbackWithSuccessIndicator> RegistrationCallback;
    public event Action<bool> CheckUserPasswordCallback;
    public event Action<Recipe[]> GetRecipes;

    private void Awake()
    {
        _dataBase.AnswerResult += OnAnswerResult;
    }

    private void OnAnswerResult(ServerOperationResult serverOperationResult)
    {
        CheckServerAccess(serverOperationResult);
        switch (serverOperationResult.Type)
        {
            case ServerOperationType.Registration:
                RegistrationCallback?.Invoke(new TextCallbackWithSuccessIndicator(true, serverOperationResult.Result.Text));
                break;

            case ServerOperationType.CheckUserPassword:
                if (serverOperationResult.Result.Text == "true")
                {
                    CheckUserPasswordCallback?.Invoke(true);
                    break;
                }
                if (serverOperationResult.Result.Text == "false")
                {
                    CheckUserPasswordCallback?.Invoke(false);
                    break;
                }
                throw new Exception("Incorrect answer server:"+ serverOperationResult.Result.Text);

            case ServerOperationType.GetRecipes:

                RecipeServer[] recipes = JsonConvert.DeserializeObject<RecipeServer[]>(serverOperationResult.Result.Text);
                Debug.Log(JsonConvert.SerializeObject(recipes[0]));

                break;
        }
    }

    private void CheckServerAccess(ServerOperationResult serverOperationResult)
    {
        if (serverOperationResult.Result.Type == ServerAnswerResultType.Error)
            throw new Exception(serverOperationResult.Result.Text);
    }
}
