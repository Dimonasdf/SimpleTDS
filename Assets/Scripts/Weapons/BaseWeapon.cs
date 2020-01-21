using System.Collections;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Базовый класс, определяющий атаку оружия
    /// </summary>
    public abstract class BaseWeapon : MonoBehaviour
    {
        /// <summary>
        /// Данные оружия
        /// </summary>
        [SerializeField] private WeaponData weaponData = null;
        /// <summary>
        /// Корутина атаки, если атака начата, то следующую атаку можно начать после кулдауна
        /// </summary>
        private Coroutine _currentCoroutine = null;
        /// <summary>
        /// Владелец оружия
        /// </summary>
        public Transform Owner { get; set; }

        /// <summary>
        /// Непосредственно реализация атаки
        /// </summary>
        protected abstract void DirectAttack();

        /// <summary>
        /// Запускает атаку, если прошел кулдаун после прошлой атаки
        /// </summary>
        public void Attack()
        {
            if (_currentCoroutine != null) return;
            DirectAttack();
            _currentCoroutine = StartCoroutine(nameof(Cooldown));
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(WeaponData.Cooldown);
            _currentCoroutine = null;
        }

        public WeaponData WeaponData => weaponData;
    }
}
