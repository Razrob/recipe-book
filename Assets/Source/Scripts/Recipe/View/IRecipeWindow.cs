using System;

public interface IRecipeWindow
{
    public RecipeWindowID RecipeWindowID { get; }
    public bool IsActive { get; }
    public void SetActive(bool value);
    public event Action<IRecipeWindow> OnActiveChange;
}
