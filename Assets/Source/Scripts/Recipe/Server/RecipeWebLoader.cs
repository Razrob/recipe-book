using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeWebLoader : AutoSingletonMono<RecipeWebLoader>
{
    private DataBase _dataBase;
    private ServerAnswerHandler _serverAnswerHandler;
    private Coroutine _loadCoroutine;

    private void Start()
    {
        _dataBase = FindObjectOfType<DataBase>(true);
        _serverAnswerHandler = FindObjectOfType<ServerAnswerHandler>(true);

        _serverAnswerHandler.GetRecipes += OnGetRecipes;
        //_serverAnswerHandler.GetUserRecipes += OnGetUserRecipes;

        GlobalModel.Data.RecipesRepo.OnUserReceptAdd += OnUserRecipeAdd;
        GlobalModel.Data.RecipesRepo.OnUserReceptRemove += OnUserRecipeRemove;
        GlobalModel.Data.RecipesRepo.OnUserRecipeModify += OnUserRecipeModified;

        Upload();
    }

    private void OnUserRecipeAdd(Recipe recipe)
    {
        _dataBase.AddUserRecipe(GlobalModel.Data.UserAuthorizationData.UserName, recipe);
    }

    private void OnUserRecipeRemove(Recipe recipe)
    {
        _dataBase.DeleteUserRepice(GlobalModel.Data.UserAuthorizationData.UserName, recipe.Name);
    }

    private void OnUserRecipeModified(Recipe recipe)
    {
        _dataBase.ChangeUserRepice(GlobalModel.Data.UserAuthorizationData.UserName, recipe.Name, recipe);
    }

    private void OnGetRecipes(Recipe[] recipes)
    {
        Recipe[] userRecipes = recipes.Where(r => r.AuthorLogin == GlobalModel.Data.UserAuthorizationData.UserName).ToArray();
        Recipe[] globalRecipes = recipes.Where(r => r.AuthorLogin != GlobalModel.Data.UserAuthorizationData.UserName).ToArray();

        GlobalModel.Data.RecipesRepo.SetUserRecipes(userRecipes);
        GlobalModel.Data.RecipesRepo.SetGlobalRecipes(globalRecipes);
    }

    //private void OnGetUserRecipes(Recipe[] recipes)
    //{
    //    GlobalModel.Data.RecipesRepo.SetUserRecipes(recipes);
    //}

    public void Upload()
    {
        if (_loadCoroutine != null)
            return;

        _loadCoroutine = StartCoroutine(LoadRecipes());
    }

    private IEnumerator LoadRecipes()
    {
        while (!GlobalModel.DataLoaded)
            yield return null;

        while (string.IsNullOrEmpty(GlobalModel.Data.UserAuthorizationData.UserName))
            yield return null;

        _dataBase.GetRecipes();
        //_dataBase.GetUserRecipes(GlobalModel.Data.UserAuthorizationData.UserName);

        ///loading

        //List<Recipe> list = new List<Recipe>();

        //List<Product> products1 = new List<Product>()
        //{
        //    new Product("����", null, "1 ������"),
        //    new Product("�����", null, "1 ������"),
        //    new Product("����", null, "3 �����"),
        //    new Product("������", null, "3 �����"),
        //    new Product("������������", null, "1 ������ �����"),
        //};

        //Recipe recipe1 = new Recipe(
        //    id: 1797636737,
        //    name: "��������",
        //    description: "������ ������������ �������� � ��������, �� 5 ������������",
        //    content: "1. ������ ����� �������� ������� � ���������� � �� 180 ��������. \r\n2. ���� � ������� ������� �� ��������. \r\n3. ��������� ������� ���������� ���� � ������������. \r\n4. ����������� ����� ��������� �����������. ������ ��������, �������� ������� �������� � �������� �� ��� �����. \r\n5. ������ ������� �����, ��������� ����� � �������. \r\n6. � ����������� �� ������� ����� ��������� �� 30 �� 50 �����. ����� ������ ���� ������� � ������ ���� �� ����� ������. ��� ����� �������� � ������������� ����� ������� ��������. ��������� ��������!",
        //    icon: null,
        //    products: products1);


        //List<Product> products2 = new List<Product>()
        //{
        //    new Product("����", null, "400 �������"),
        //    new Product("������� ����", null, "1 �����"),
        //    new Product("���������", null, "1 �����"),
        //    new Product("����", null, "2 �����"),
        //    new Product("���", null, "2 �����"),
        //    new Product("�����", null, "100 �������"),
        //    new Product("����", null, "300 �������"),
        //    new Product("�������", null, "200 �������"),
        //    new Product("������������", null, "2 ������ �����"),
        //};

        //Recipe recipe2 = new Recipe(
        //    id: 1593265923,
        //    name: "������ ����� � ���������",
        //    description: "������� � ����� ������ ����� � ������ ������, ������� � ����������. ����� ���������� ����� �� ������������� ������, ����� ������� ������������ �����. ��� ������� ������, ������� ������ ��� �������.",
        //    content: "1. ��������� ��������� �����, �������� ������� � ����, �������� � �����������. \r\n2. �������� ���� � ������������� � �������� ������ ���������� �����. \r\n3. ��������� ����� � ������ � ��������� � �����������. \r\n4. ������� ���� (����� 300-400 �) �������� ���������� ���������. \r\n5. ��������� �������� � �������� ��������. \r\n6. ��������� ����, ���������, ����, ����� ���������� ���, �������� ����� ������ � ����. �����������. \r\n7. ����� �������� �� 2 �������� �����. �������� ������� ����� �� ��������� � ���������� � ����� �����. ������� ����� ����� ���������� ������� ������ �� �������. \r\n8. �������� �������, �������� ������, ��������� ����. ������ �������� �������. ������� ������� ����� �� �������� �������. ��������� 40 ����� ��� ����������� 180 ��������. \r\n9. ��� � ��! �������� ����� � ���������. ",
        //    icon: null,
        //    products: products2);

        //list.Add(recipe1);
        //list.Add(recipe2);

        //GlobalModel.Data.RecipesRepo.SetGlobalRecipes(list);
    }
}
