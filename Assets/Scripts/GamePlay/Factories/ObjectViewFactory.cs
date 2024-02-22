using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Factories
{
    public class ObjectViewFactory
    {
        public IObjectView View { get; protected set; }

        public IObjectView Create(string objectName)
        {
            var prefab = Resources.Load<GameObject>(objectName);
            View = GameObject.Instantiate(prefab).GetComponent<IObjectView>();
            return View;
        }
    }
}