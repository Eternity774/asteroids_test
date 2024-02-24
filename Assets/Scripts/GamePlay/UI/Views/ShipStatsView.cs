using GamePlay.Models;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShipStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _position;
        [SerializeField] private TMP_Text _angle;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _laserCharges;
        [SerializeField] private TMP_Text _laserCooldown;

        public void UpdateUI(ShipStatModel model)
        {
            _position.text = model.Position;
            _angle.text = model.Angle;
            _speed.text = model.Speed;
            _laserCharges.text = model.LaserCharges;
            _laserCooldown.text = model.LaserCooldown;
        }
        
    }
}
