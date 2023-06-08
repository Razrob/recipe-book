using System.Collections;
using UnityEngine;

public class RecipeWebUploader : AutoSingletonMono<RecipeWebLoader>
{
    private Coroutine _uploadCoroutine;

    private void Awake()
    {
        Upload();
    }

    public void Upload()
    {
        if (_uploadCoroutine != null)
            return;

        _uploadCoroutine = StartCoroutine(UploadRecipes());
    }

    private IEnumerator UploadRecipes()
    {
        while (!GlobalModel.DataLoaded)
            yield return null;

        ///uploading
        RecipePack pack = GlobalModel.Data.RecipesRepo.RecipePack;
    }
}
