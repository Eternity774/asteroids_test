using System.Collections.Generic;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Factories
{
    public class ObjectViewFactory
    {
        private class SpawnedViewData
        {
            public string ObjectName;
            public bool IsFree;
            public IObjectView View;
        }
        
        private List<SpawnedViewData> _spawnedViews;

        public ObjectViewFactory()
        {
            _spawnedViews = new List<SpawnedViewData>();
        }
        
        public IObjectView Create(string objectName)
        {
            var prefab = Resources.Load<GameObject>(objectName);
            
            IObjectView view;
            foreach (SpawnedViewData data in _spawnedViews)
            {
                if (data.ObjectName == objectName)
                {
                    if (data.IsFree)
                    {
                        data.IsFree = false;
                        data.View.GetGameObject().SetActive(true);
                        return data.View;
                    }
                }
            }
            
            view = GameObject.Instantiate(prefab).GetComponent<IObjectView>();
            _spawnedViews.Add(new SpawnedViewData(){ ObjectName = objectName, IsFree = false, View = view});

            return view;
        }

        public void SetViewFree(IObjectView view)
        {
            foreach (SpawnedViewData data in _spawnedViews)
            {
                if (data.View == view)
                {
                    data.IsFree = true;
                    data.View.GetGameObject().SetActive(false);
                }
            }
        }
    }
}