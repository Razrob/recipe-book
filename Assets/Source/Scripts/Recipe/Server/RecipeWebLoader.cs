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
            new Product("Мука", null, "1 стакан"),
            new Product("Сахар", null, "1 стакан"),
            new Product("Яйцо", null, "3 штуки"),
            new Product("Яблоко", null, "3 штуки"),
            new Product("Разрыхлитель", null, "1 чайная ложка"),
        };

        Recipe recipe1 = new Recipe(
            id: 56797539636737,
            name: "Шарлотка",
            description: "Рецепт классической шарлотки с яблоками, из 5 ингредиентов",
            content: "1. Первым делом включите духовку и разогрейте её до 180 градусов. \r\n2. Яйца с сахаром взбейте до пышности. \r\n3. Понемногу всыпьте просеянную муку и разрыхлитель. \r\n4. Жаропрочную форму застелите пергаментом. Яблоки очистите, нарежьте тонкими дольками и выложите на дно формы. \r\n5. Сверху вылейте тесто, отправьте форму в духовку. \r\n6. В зависимости от размера формы выпекайте от 30 до 50 минут. Пирог должен быть румяным и пройти тест на сухую спичку. Вот такая классная и действительно самая простая шарлотка. Приятного чаепития!",
            icon: null,
            products: products1);


        List<Product> products2 = new List<Product>()
        {
            new Product("Фарш", null, "400 граммов"),
            new Product("Куриное филе", null, "1 штука"),
            new Product("Картофель", null, "1 штука"),
            new Product("Яйца", null, "2 штуки"),
            new Product("Лук", null, "2 штуки"),
            new Product("Масло", null, "100 граммов"),
            new Product("Мука", null, "300 граммов"),
            new Product("Сметана", null, "200 граммов"),
            new Product("Разрыхлитель", null, "2 чайные ложки"),
        };

        Recipe recipe2 = new Recipe(
            id: 1593265923,
            name: "Мясной пирог с картошкой",
            description: "Вкусный и очень сытный пирог с мясным фаршем, курицей и картофелем. Чтобы сэкономить время на приготовление пирога, можно сделать бездрожжевое тесто. Это простой рецепт, который всегда вас выручит.",
            content: "1. Растопите сливочное масло, добавьте сметану и яйцо, посолите и перемешайте. \r\n2. Просейте муку с разрыхлителем и замесите мягкое эластичное тесто. \r\n3. Заверните тесто в пленку и отправьте в холодильник. \r\n4. Куриное филе (около 300-400 г) нарежьте маленькими кусочками. \r\n5. Картофель очистите и нарежьте кубиками. \r\n6. Соедините филе, картофель, фарш, мелко нарезанный лук, добавьте смесь перцев и соль. Перемешайте. \r\n7. Тесто поделите на 2 неравные части. Выложите большую часть на пергамент и раскатайте в форме овала. Меньшую часть теста раскатайте пластом такого же размера. \r\n8. Выложите начинку, накройте тестом, защипните края. Вилкой сделайте проколы. Смажьте взбитым яйцом со щепоткой куркумы. Выпекайте 40 минут при температуре 180 градусов. \r\n9. Вот и всё! Остудите пирог и подавайте. ",
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
