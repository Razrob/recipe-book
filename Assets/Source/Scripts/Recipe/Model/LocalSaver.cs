using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class LocalSaver : MonoBehaviour
{
    [SerializeField] private bool _enabled;

    private bool _loaded;
    private const string USER_RECIPES_LOCAL_SAVE_KEY = "USER_RECIPES_LOCAL_SAVE_KEY";

    private void Start()
    {
        if (!_enabled)
            return;

        Load();
        GlobalModel.Data.RecipesRepo.OnUserRecipesStateChange += Save;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!_enabled)
            return;

        Save();
    }

    private void Load()
    {
        if (!GlobalModel.DataLoaded || !_loaded)
            return;

        _loaded = true;

        if (!PlayerPrefs.HasKey(USER_RECIPES_LOCAL_SAVE_KEY))
            return;

        RecipePack recipePack = JsonConvert.DeserializeObject<RecipePack>(PlayerPrefs.GetString(USER_RECIPES_LOCAL_SAVE_KEY));
        GlobalModel.Data.RecipesRepo.SetUserRecipes(recipePack.Recipes.Values);
    }

    private void Save()
    {
        if (!GlobalModel.DataLoaded || !_loaded)
            return;

        string userRecipes = JsonConvert.SerializeObject(GlobalModel.Data.RecipesRepo.UserRecipePack);
        PlayerPrefs.SetString(USER_RECIPES_LOCAL_SAVE_KEY, userRecipes);
        PlayerPrefs.Save();
    }
}