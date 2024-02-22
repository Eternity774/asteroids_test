using GamePlay.Models;

namespace GamePlay.Factories
{
    public class PlayerModelFactory : ObjectModelFactory
    {
        public override IObjectModel Create()
        {
            Model = new UFOModel();
            return Model;
        }
    }
}