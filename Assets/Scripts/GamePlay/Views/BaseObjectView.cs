using System;
using UnityEngine;

namespace GamePlay.Views
{
    public class BaseObjectView : MonoBehaviour, IObjectView
    {
        public event Action<Collision2D> OnCollision;
        public Vector2 Position
        {
            get => transform.position;
            set
            {
                transform.position = value;
            }
        }

        public Quaternion Rotation
        {
            set
            {
                transform.rotation = value;
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Vector2 GetForward()
        {
            return transform.up;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollision?.Invoke(other);
        }
    }
}