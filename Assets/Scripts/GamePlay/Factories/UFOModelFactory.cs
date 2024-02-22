using GamePlay.Models;

namespace GamePlay.Factories
{
    public class UFOModelFactory : ObjectModelFactory
    {
        public override IObjectModel Create()
        {
            Model = new UFOModel();
            return Model;
        }
    }
}