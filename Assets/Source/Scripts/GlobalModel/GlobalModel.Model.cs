public partial class GlobalModel
{
    public class Model
    {
        public RecipesRepo RecipesRepo { get; private set; }

        public Model()
        {
            RecipesRepo = new RecipesRepo();
        }
    }
}
