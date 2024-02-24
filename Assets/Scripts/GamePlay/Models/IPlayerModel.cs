using System;

namespace GamePlay.Models
{
    public interface IPlayerModel : IObjectModel
    {
        event Action<float> OnSpeedChanged;
        event Action<int> OnLaserChargesChanged;
        event Action<float> OnLaserCooldownChanged;
        
        float Speed { get; set; }
        int LaserCharges { get; set; }
        float LaserCooldown { get; set; }
    }
}