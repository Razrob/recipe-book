using Newtonsoft.Json;
using System;
using UnityEngine;

public class ServerAnswerHandler : MonoBehaviour
{
    [SerializeField] private DataBase _dataBase;

    public event Action<TextCallbackWithSuccessIndicator> RegistrationCallback;
    public event Action<bool> CheckUserPasswordCallback;

    public event Action<bool> DeleteUserRecipe;
    public event Action<bool> ChangeUserRecipe;

    public event Action<Recipe[]> GetRecipes;
    public event Action<Recipe[]> GetUserRecipes;
    public event Action<IntCallbackWithSuccessIndicator> AddedRecipe;
    public event Action<IntCallbackWithSuccessIndicator> AddedUserRecipe;

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
                CheckUserPasswordCallback?.Invoke(false);
                break;

            case ServerOperationType.GetRecipes:
                {
                    Debug.Log(serverOperationResult.Result.Text);
                    RecipeServer[] serverRecipes = JsonConvert.DeserializeObject<RecipeServer[]>(serverOperationResult.Result.Text);
                    Recipe[] recipes = new Recipe[serverRecipes.Length];
                    for (int i =0; i< serverRecipes.Length; i++)
                    {
                        recipes[i] = new Recipe(serverRecipes[i]);
                    }
                    GetRecipes?.Invoke(recipes);
                    break;
                }

            case ServerOperationType.GetUserRecipes:
                {
                    Debug.Log(serverOperationResult.Result.Text);
                    RecipeServer[] serverRecipes = JsonConvert.DeserializeObject<RecipeServer[]>(serverOperationResult.Result.Text);
                    Recipe[] recipes = new Recipe[serverRecipes.Length];
                    for (int i = 0; i < serverRecipes.Length; i++)
                    {
                        recipes[i] = new Recipe(serverRecipes[i]);
                    }
                    GetUserRecipes?.Invoke(recipes);
                    break;
                }

            case ServerOperationType.AddRecipe:
                AddedRecipe?.Invoke(new IntCallbackWithSuccessIndicator(true,int.Parse(serverOperationResult.Result.Text)));
                break;

            case ServerOperationType.AddUserRecipe:
                AddedUserRecipe?.Invoke(new IntCallbackWithSuccessIndicator(true, int.Parse(serverOperationResult.Result.Text)));
                break;

            case ServerOperationType.DeleteUserRecipe:
                if (serverOperationResult.Result.Text == "true")
                {
                    DeleteUserRecipe?.Invoke(true);
                }
                else
                {
                    DeleteUserRecipe?.Invoke(false);
                }
                break;

            case ServerOperationType.ChangeUserRecipe:
                if (serverOperationResult.Result.Text == "true")
                {
                    ChangeUserRecipe?.Invoke(true);
                }
                else
                {
                    ChangeUserRecipe?.Invoke(false);
                }
                break;
        }
    }

    private void CheckServerAccess(ServerOperationResult serverOperationResult)
    {
        if (serverOperationResult.Result.Type == ServerAnswerResultType.Error)
            throw new Exception(serverOperationResult.Result.Text);
    }
}