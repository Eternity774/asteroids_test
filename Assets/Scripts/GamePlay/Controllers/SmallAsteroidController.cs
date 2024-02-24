using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Controllers
{
    public class SmallAsteroidController : AsteroidController
    {
        public SmallAsteroidController(IObjectModel model, IObjectView view) : base(model, view)
        {
        }

        protected override void InitSpeed()
        {
            _speed = Game.Configs.Asteroids.SmallAsteroidSpeed;
        }
    }
}