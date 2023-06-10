using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeWebLoader : AutoSingletonMono<RecipeWebLoader>
{
    private Coroutine _loadCoroutine;

    private void Start()
    {
        Upload();
    }

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

        ///loading

        List<Recipe> list = new List<Recipe>();

        List<Product> products1 = new List<Product>()
        {
            new Product("����", null, "1 ������"),
            new Product("�����", null, "1 ������"),
            new Product("����", null, "3 �����"),
            new Product("������", null, "3 �����"),
            new Product("������������", null, "1 ������ �����"),
        };

        Recipe recipe1 = new Recipe(
            id: 56797539636737,
            name: "��������",
            description: "������ ������������ �������� � ��������, �� 5 ������������",
            content: "1. ������ ����� �������� ������� � ���������� � �� 180 ��������. \r\n2. ���� � ������� ������� �� ��������. \r\n3. ��������� ������� ���������� ���� � ������������. \r\n4. ����������� ����� ��������� �����������. ������ ��������, �������� ������� �������� � �������� �� ��� �����. \r\n5. ������ ������� �����, ��������� ����� � �������. \r\n6. � ����������� �� ������� ����� ��������� �� 30 �� 50 �����. ����� ������ ���� ������� � ������ ���� �� ����� ������. ��� ����� �������� � ������������� ����� ������� ��������. ��������� ��������!",
            icon: null,
            products: products1);


        List<Product> products2 = new List<Product>()
        {
            new Product("����", null, "400 �������"),
            new Product("������� ����", null, "1 �����"),
            new Product("���������", null, "1 �����"),
            new Product("����", null, "2 �����"),
            new Product("���", null, "2 �����"),
            new Product("�����", null, "100 �������"),
            new Product("����", null, "300 �������"),
            new Product("�������", null, "200 �������"),
            new Product("������������", null, "2 ������ �����"),
        };

        Recipe recipe2 = new Recipe(
            id: 1593265923,
            name: "������ ����� � ���������",
            description: "������� � ����� ������ ����� � ������ ������, ������� � ����������. ����� ���������� ����� �� ������������� ������, ����� ������� ������������ �����. ��� ������� ������, ������� ������ ��� �������.",
            content: "1. ��������� ��������� �����, �������� ������� � ����, �������� � �����������. \r\n2. �������� ���� � ������������� � �������� ������ ���������� �����. \r\n3. ��������� ����� � ������ � ��������� � �����������. \r\n4. ������� ���� (����� 300-400 �) �������� ���������� ���������. \r\n5. ��������� �������� � �������� ��������. \r\n6. ��������� ����, ���������, ����, ����� ���������� ���, �������� ����� ������ � ����. �����������. \r\n7. ����� �������� �� 2 �������� �����. �������� ������� ����� �� ��������� � ���������� � ����� �����. ������� ����� ����� ���������� ������� ������ �� �������. \r\n8. �������� �������, �������� ������, ��������� ����. ������ �������� �������. ������� ������� ����� �� �������� �������. ��������� 40 ����� ��� ����������� 180 ��������. \r\n9. ��� � ��! �������� ����� � ���������. ",
            icon: null,
            products: products2);

        list.Add(recipe1);
        list.Add(recipe2);

        //list.Add(recipe1);
        //list.Add(recipe2);
        //list.Add(recipe1);
        //list.Add(recipe2);
        //list.Add(recipe1);
        //list.Add(recipe2);
        //list.Add(recipe1);
        //list.Add(recipe2);
        //list.Add(recipe1);
        //list.Add(recipe2);
        //list.Add(recipe1);
        //list.Add(recipe2);

        GlobalModel.Data.RecipesRepo.SetGlobalRecipes(list);
    }
}
