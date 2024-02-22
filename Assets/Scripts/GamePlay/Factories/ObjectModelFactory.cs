using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public abstract class ObjectModelFactory 
    {
        public IObjectModel Model { get; protected set; }

        public abstract IObjectModel Create();
    }
}
