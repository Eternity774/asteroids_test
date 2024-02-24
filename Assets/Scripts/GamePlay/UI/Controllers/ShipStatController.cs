using System;
using GamePlay.Models;
using UI;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class ShipStatController : IUIController
    {
        private ShipStatsView _view;
        private ShipStatModel _model;

        private IPlayerModel _playerModel;
        
        public event Action OnClose;

        public ShipStatController(ShipStatModel model, ShipStatsView view, IPlayerModel playerModel)
        {
            _model = model;
            _view = view;

            _playerModel = playerModel;
            
            _playerModel.OnPositionChanged += OnPositionChanged;
            _playerModel.OnRotationChanged += OnRotationChanged;
            _playerModel.OnSpeedChanged += OnSpeedChanged;
            _playerModel.OnLaserChargesChanged += OnLaserChargesChanged;
            _playerModel.OnLaserCooldownChanged += OnLaserCooldownChanged;
            
            OnPositionChanged(_playerModel.Position);
            OnRotationChanged(_playerModel.Rotation);
            OnSpeedChanged(_playerModel.Speed);
            OnLaserChargesChanged(_playerModel.LaserCharges);
            OnLaserCooldownChanged(_playerModel.LaserCooldown);

            _view.UpdateUI(_model);
        }

        private void OnLaserCooldownChanged(float laserCooldown)
        {
            _model.LaserCooldown = laserCooldown.ToString("0.00");
            UpdateView();
        }

        private void OnLaserChargesChanged(int laserCharges)
        {
            _model.LaserCharges = laserCharges.ToString();
            UpdateView();
        }

        private void OnSpeedChanged(float speed)
        {
            _model.Speed = speed.ToString("0.00");
            UpdateView();
        }

        private void OnRotationChanged(Quaternion rotation)
        {
            _model.Angle = rotation.eulerAngles.z.ToString("0.00");
            UpdateView();
        }

        private void OnPositionChanged(Vector2 position)
        {
            _model.Position = $"x:{position.x} y: {position.y}";
            UpdateView();
        }

        private void UpdateView()
        {
            _view.UpdateUI(_model);
        }

        public void Close()
        {
            if (_view != null)
            {
                GameObject.Destroy(_view);
            }
        }
    }
}