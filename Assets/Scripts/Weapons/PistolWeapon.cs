using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Пистолет. Хранит пул пуль
    /// </summary>
    public class PistolWeapon : BaseWeapon
    {
        /// <summary>
        /// Префаб пули
        /// </summary>
        [SerializeField] private Bullet bulletPrefab = null;

        /// <summary>
        /// Пул пуль
        /// </summary>
        private ObjectPool<Bullet> _pool;

        /// <summary>
        /// Инициализирует пул
        /// </summary>
        private void Awake()
        {
            _pool = new ObjectPool<Bullet>(15, InstantiateBullet, InitBullet);
        }

        /// <summary>
        /// Создает новую пулю
        /// </summary>
        /// <returns>пулю</returns>
        private Bullet InstantiateBullet()
        {
            return Instantiate(bulletPrefab);
        }

        /// <summary>
        /// Инициализирует пулю после извлечения из пула
        /// </summary>
        /// <param name="bullet"></param>
        private void InitBullet(Bullet bullet)
        {
            bullet.Init(Owner, OnBulletDestroy);
        }

        /// <summary>
        /// Возвращает пулю в пул на уничтожение пули
        /// </summary>
        /// <param name="bullet"></param>
        private void OnBulletDestroy(Bullet bullet)
        {
            _pool.Push(bullet);
        }

        /// <summary>
        /// Извлекает пулю из пула и стреляет
        /// </summary>
        protected override void DirectAttack()
        {
            var bullet = _pool.Pull();
            bullet?.Shoot(transform.position, WeaponData.AttackSpeed, WeaponData.AttackDistance);
        }
    }
}