using GamePlay.Controllers;
using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public abstract class ObjectControllerFactory
    {
        public abstract IObjectController Create(IObjectModel model, IObjectView view);
    }
}