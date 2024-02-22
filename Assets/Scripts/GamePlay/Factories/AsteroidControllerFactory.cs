using GamePlay.Controllers;
using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public class AsteroidControllerFactory : ObjectControllerFactory
    {
        public override IObjectController Create(IObjectModel model, IObjectView view)
        {
            Controller = new AsteroidController();
            return Controller;
        }
    }
}