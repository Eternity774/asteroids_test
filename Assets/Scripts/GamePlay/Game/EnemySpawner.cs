using System.Collections.Generic;
using System.Linq;
using GamePlay.Configs;
using GamePlay.Controllers;
using GamePlay.Factories;
using GamePlay.Models;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Game
{
    public class EnemySpawner : IUpdatable
    {
        private const string BigAsteroid = "BigAsteroid";
        private const string SmallAsteroid = "SmallAsteroid";
        private const string UFO = "UFO";
        
        private ObjectViewFactory _viewFactory;
        private DefaultModelFactory _modelFactory;
        private AsteroidControllerFactory _bigControllerFactory;
        private SmallAsteroidControllerFactory _smallControllerFactory;
        private UFOControllerFactory _ufoControllerFactory;

        private float _asteroidSpawnTimer;
        private float _ufoSpawnTimer;

        private GameModel _gameModel;
        
        private Camera _camera;

        private List<IObjectController> _spawnedEnemies;
        
        public EnemySpawner(IObjectModel playerModel, GameModel gameModel)
        {
            _camera = Camera.main;

            _gameModel = gameModel;

            _spawnedEnemies = new List<IObjectController>();
            
            _modelFactory = new DefaultModelFactory();
            _viewFactory = new ObjectViewFactory();
            _bigControllerFactory = new AsteroidControllerFactory();
            _smallControllerFactory = new SmallAsteroidControllerFactory();
            _ufoControllerFactory = new UFOControllerFactory
            {
                Player = playerModel
            };

            UpdateService.AddUpdateListener(this);
        }

        public void Restart()
        {
            _asteroidSpawnTimer = Configs.Asteroids.SpawnDelay;
            _ufoSpawnTimer = Configs.UFO.SpawnDelay;

            int controllersCount = _spawnedEnemies.Count;
            for (int i = 0; i < controllersCount; i++)
            {
                _spawnedEnemies[i].Die(true);
            }
        }

        private void SpawnUFO()
        {
            var model = _modelFactory.Create();
            
            Vector2 position = _camera.ViewportToWorldPoint(RandomPointOutsideViewport());
            model.Position = position;

            var view = _viewFactory.Create(UFO);

            var controller = _ufoControllerFactory.Create(model, view);
            controller.OnDie += OnEnemyDie;
            
            _spawnedEnemies.Add(controller);
            
            UpdateService.AddUpdateListener((IUpdatable)controller);
        }

        private void SpawnBigAsteroid()
        {
            var model = _modelFactory.Create();
            
            Vector2 position = _camera.ViewportToWorldPoint(RandomPointOutsideViewport());
            model.Position = position;

            Vector2 randomPosition = RandomPointInsideCircle();
            Vector2 direction = randomPosition - position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            model.Rotation = targetRotation;

            var view = _viewFactory.Create(BigAsteroid);

            var controller = _bigControllerFactory.Create(model, view);
            controller.OnDie += OnBigAsteroidDie;
            
            _spawnedEnemies.Add(controller);
            
            UpdateService.AddUpdateListener((IUpdatable)controller);
        }

        private void SpawnSmallAsteroid(Vector2 position, Vector2 forward)
        {
            var model = _modelFactory.Create();
            model.Position = position;

            float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            model.Rotation = targetRotation;

            var view = _viewFactory.Create(SmallAsteroid);

            var controller = _smallControllerFactory.Create(model, view);
            controller.OnDie += OnEnemyDie;
            
            _spawnedEnemies.Add(controller);
            
            UpdateService.AddUpdateListener((IUpdatable)controller);
        }

        private void OnEnemyDie(IObjectController enemyController, IObjectView objectView, bool silent)
        {
            enemyController.OnDie -= OnEnemyDie;

            if (!silent)
            {
                _gameModel.Points += 100;
            }
            
            _viewFactory.SetViewFree(objectView);

            UpdateService.RemoveUpdateListener((IUpdatable)enemyController);
        }

        private void OnBigAsteroidDie(IObjectController asteroidController, IObjectView objectView, bool silent)
        {
            asteroidController.OnDie -= OnBigAsteroidDie;

            if (!silent)
            {
                Vector2 forward = objectView.GetForward();

                Vector2 right = new Vector2(forward.y, -forward.x);
                Vector2 left = -right;
            
                SpawnSmallAsteroid(objectView.Position,right);
                SpawnSmallAsteroid(objectView.Position, left);
            }
            
            OnEnemyDie(asteroidController, objectView, silent);
        }

        private Vector2 RandomPointOutsideViewport()
        {
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(-0.2f, 0.2f);
            if (x >= 0) x += 1;
            if (y >= 0) y += 1;
            return new Vector2(x, y);
        }

        private Vector2 RandomPointInsideCircle()
        {
            Vector2 randomPoint = Random.insideUnitCircle * 0.5f;
            return _camera.ViewportToWorldPoint(randomPoint);
        }
        
        public void OnUpdate()
        {
            if (_gameModel.IsGameFinished)
            {
                return;
            }

            SpawnTimers();
        }

        private void SpawnTimers()
        {
            _asteroidSpawnTimer -= Time.deltaTime;
            if (_asteroidSpawnTimer <= 0)
            {
                SpawnBigAsteroid();
                _asteroidSpawnTimer = Configs.Asteroids.SpawnDelay;
            }

            _ufoSpawnTimer -= Time.deltaTime;
            if (_ufoSpawnTimer <= 0)
            {
                SpawnUFO();
                _ufoSpawnTimer = Configs.UFO.SpawnDelay;
            }
        }
    }
}