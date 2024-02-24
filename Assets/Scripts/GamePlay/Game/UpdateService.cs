using System;
using System.Collections.Generic;
using GamePlay.Views;

namespace GamePlay.Game
{
    //Вынести в сервис локатор
    public class UpdateService : IService
    {
        private class UpdatableData
        {
            public bool IsEmpty;
            public IUpdatable Updatable;
        }
        
        private static List<UpdatableData> _updatables;

        public UpdateService()
        {
            _updatables = new List<UpdatableData>();
        }
        
        public void OnUpdate()
        {
            if (_updatables != null && _updatables.Count > 0)
            {
                int countActions = _updatables.Count;
                for (int i = 0; i < countActions; i++)
                {
                    if (_updatables[i].IsEmpty)
                    {
                        continue;
                    }
                    
                    _updatables[i].Updatable.OnUpdate();
                }
            }
        }
        
        public static void AddUpdateListener(IUpdatable updatable)
        {
            foreach (UpdatableData data in _updatables)
            {
                if (data.IsEmpty)
                {
                    data.Updatable = updatable;
                    data.IsEmpty = false;
                    return;
                }
            }
            
            _updatables.Add(new UpdatableData() { Updatable = updatable, IsEmpty = false });
        }
        
        public static void RemoveUpdateListener(IUpdatable updatable)
        {
            foreach (UpdatableData data in _updatables)
            {
                if (data.Updatable == updatable)
                {
                    data.Updatable = null;
                    data.IsEmpty = true;
                }
            }
        }
    }
}