using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public abstract class ObjectModelFactoryBase 
    {
        public abstract IObjectModel Create();
    }
}
