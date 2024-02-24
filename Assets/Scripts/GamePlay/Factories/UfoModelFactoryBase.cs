using GamePlay.Models;

namespace GamePlay.Factories
{
    public class UfoModelFactoryBase : ObjectModelFactoryBase
    {
        public override IObjectModel Create()
        {
            return new UFOModel();
        }
    }
}