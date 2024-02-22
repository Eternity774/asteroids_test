using GamePlay.Controllers;
using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public class PlayerControllerFactory
    {
        public IObjectController Controller { get; private set; }
        
        public IObjectController Create(IObjectModel model, IPlayerView view)
        {
            Controller = new PlayerController(model, view);
            return Controller;
        }
    }
}