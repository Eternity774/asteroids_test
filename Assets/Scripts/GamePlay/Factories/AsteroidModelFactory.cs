using GamePlay.Models;

namespace GamePlay.Factories
{
    public class AsteroidModelFactory : ObjectModelFactory
    {
        public override IObjectModel Create()
        {
            Model = new AsteroidModel();
            return Model;
        }
    }
}