using GamePlay.Models;

namespace GamePlay.Factories
{
    public class PlayerModelFactoryBase : ObjectModelFactoryBase
    {
        public override IObjectModel Create()
        {
            return new PlayerModel();
        }
    }
}