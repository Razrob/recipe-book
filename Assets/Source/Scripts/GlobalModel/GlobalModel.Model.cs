using Newtonsoft.Json;
using System;

public partial class GlobalModel
{
    public class Model
    {
        public RecipesRepo RecipesRepo { get; private set; }
        [JsonIgnore] public UserAuthorizationData UserAuthorizationData { get; private set; }

        public Model()
        {
            RecipesRepo = new RecipesRepo();
            UserAuthorizationData = new UserAuthorizationData();
        }
    }
}
