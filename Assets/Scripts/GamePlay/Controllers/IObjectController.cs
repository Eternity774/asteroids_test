using System;
using GamePlay.Views;

namespace GamePlay.Controllers
{
    public interface IObjectController
    {
        event Action<IObjectController, IObjectView, bool> OnDie;
        void Die(bool silent);
    }
}
