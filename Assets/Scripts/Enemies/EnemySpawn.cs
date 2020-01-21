using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Менеджер создания врагов. Хранит врагов в пуле. Достает врагов в соответствии с конфигурацией
    /// </summary>
    public class EnemySpawn : MonoBehaviour
    {
        /// <summary>
        /// Максимальное количество врагов одновременно на экране
        /// </summary>
        [SerializeField] private int maxEnemyCount = 10;

        /// <summary>
        /// Список допустимых типов врагов и вероятность их появления
        /// </summary>
        [SerializeField] private List<EnemySpawnProbability> spawnTypeProbability = null;

        /// <summary>
        /// Объект-владелец точек спауна
        /// </summary>
        [SerializeField] private GameObject spawnPointsOwner = null;

        /// <summary>
        /// Префаб врага
        /// </summary>
        [SerializeField] private EnemyBehaviour enemyPrefab = null;

        /// <summary>
        /// Цель, которую будут преследовать враги
        /// </summary>
        [SerializeField] private Rigidbody2D enemyTarget = null;

        /// <summary>
        /// Закешированые значения точек спауна
        /// </summary>
        private IList<Vector3> _spawnPoints;

        /// <summary>
        /// Пул врагов
        /// </summary>
        private ObjectPool<EnemyBehaviour> _enemiesPool;

        /// <summary>
        /// Количество активных в момент времени врагов
        /// </summary>
        private int _currentActive = 0;

        /// <summary>
        /// Действие на смерть врага
        /// </summary>
        public Action<EnemyData> OnDestroyEnemy { get; set; }


        private void Start()
        {
            CacheSpawnPoints();
            InitPool();
            SpawnEnemies(maxEnemyCount / 2);
        }

        /// <summary>
        /// Спаун врагов. Количество определяется случайно исходя из минимально необходимого числа и максимального допустимого числа 
        /// </summary>
        /// <param name="min">минимально необходимое число врагов для спауна</param>
        private void SpawnEnemies(int min)
        {
            var spawnCount = Probability.NextIntFromRange(min, maxEnemyCount - _currentActive);
            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = _enemiesPool.Pull();
                if (enemy != null)
                    _currentActive++;
            }
        }

        /// <summary>
        /// Инициализация пула
        /// </summary>
        private void InitPool()
        {
            _enemiesPool = new ObjectPool<EnemyBehaviour>(maxEnemyCount, InstantiateEnemy, InitEnemy);
        }

        /// <summary>
        /// Функиия создания врага
        /// </summary>
        /// <returns></returns>
        private EnemyBehaviour InstantiateEnemy()
        {
            var enemy = Instantiate(enemyPrefab, transform);
            enemy.SetOnDeathAction(OnDeathEnemy);
            return enemy;
        }

        /// <summary>
        /// Действие на смерть врага. Возвращает врага в пул, запускает генерацию новых врагов
        /// </summary>
        /// <param name="enemy">погибший враг</param>
        private void OnDeathEnemy(EnemyBehaviour enemy)
        {
            _currentActive--;
            _enemiesPool.Push(enemy);
            OnDestroyEnemy?.Invoke(enemy.EnemyData);
            SpawnEnemies(0);
        }

        /// <summary>
        /// Определяет рендомную точку спауна для врага
        /// </summary>
        /// <param name="enemy">враг</param>
        private void ChooseRandomEnemyPosition(EnemyBehaviour enemy)
        {
            enemy.transform.position = Probability.NextRandomInList(_spawnPoints);
        }

        /// <summary>
        /// Инициализация врага. Задается позиция и данные врага
        /// </summary>
        /// <param name="obj">враг</param>
        private void InitEnemy(EnemyBehaviour obj)
        {
            ChooseRandomEnemyPosition(obj);
            ChooseRandomEnemyData(obj);
        }

        /// <summary>
        /// Выбираются рендомные данные врага из заданных в конфигурации
        /// </summary>
        /// <param name="enemyBehaviour">враг</param>
        private void ChooseRandomEnemyData(EnemyBehaviour enemyBehaviour)
        {
            var enemyType = Probability.NextInProbabilityList(spawnTypeProbability, (sp) => sp.spawnProbability);
            enemyBehaviour.UpdateData(enemyType.enemyType, enemyTarget);
        }

        /// <summary>
        /// Кеширование позиций точек спауна
        /// </summary>
        private void CacheSpawnPoints()
        {
            _spawnPoints = new List<Vector3>();
            if (spawnPointsOwner == null) return;
            var parent = spawnPointsOwner.transform;
            for (var i = 0; i < parent.childCount; i++)
            {
                _spawnPoints.Add(parent.GetChild(i).position);
            }
        }
    }

    /// <summary>
    /// Класс для храниния конфигурации данных врага и вероятности его появления
    /// </summary>
    [Serializable]
    public class EnemySpawnProbability
    {
        public EnemyData enemyType;
        public float spawnProbability;
    }
}