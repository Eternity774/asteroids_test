using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseScreenView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _points;

        public TMP_Text Points => _points;

        public event Action OnRestartButtonClicked;
        
        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestart);
        }

        private void OnRestart()
        {
            OnRestartButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestart);
        }
    }
}