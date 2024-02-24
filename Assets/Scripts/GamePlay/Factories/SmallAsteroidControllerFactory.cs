using GamePlay.Controllers;
using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public class SmallAsteroidControllerFactory : ObjectControllerFactory
    {
        public override IObjectController Create(IObjectModel model, IObjectView view)
        {
            return new SmallAsteroidController(model, view);
        }
    }

}