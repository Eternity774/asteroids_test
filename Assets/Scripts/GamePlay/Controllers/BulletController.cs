using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Controllers
{
    public class BulletController : AsteroidController
    {
        protected override void InitSpeed()
        {
            _speed = Game.Configs.Player.BulletSpeed;
        }

        public BulletController(IObjectModel model, IObjectView view) : base(model, view)
        {
        }
    }
}