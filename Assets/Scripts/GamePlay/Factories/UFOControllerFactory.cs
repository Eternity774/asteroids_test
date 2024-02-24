using GamePlay.Controllers;
using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Factories
{
    public class UFOControllerFactory : ObjectControllerFactory
    {
        public IObjectModel Player { get; set; }
        
        public override IObjectController Create(IObjectModel model, IObjectView view)
        {
            return new UFOController(model, view, Player);
        }
    }
}