using GamePlay.Models;

namespace GamePlay.Factories
{
    public class DefaultModelFactory : ObjectModelFactoryBase
    {
        public override IObjectModel Create()
        {
            return new DefaultModel();
        }
    }
}