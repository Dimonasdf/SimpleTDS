using UnityEngine;
using Weapons;

namespace Hero
{
    /// <summary>
    /// Определяет атаку героя
    /// </summary>
    [RequireComponent(typeof(HeroView))]
    public class HeroAttack : MonoBehaviour
    {
        /// <summary>
        /// Выбранное стрелковое оружие
        /// </summary>
        private BaseWeapon _selectedShootWeapon;
        /// <summary>
        /// Оружие ближнего боя
        /// </summary>
        [SerializeField]
        private BaseWeapon _meleWeapon = null;
        /// <summary>
        /// Вид героя
        /// </summary>
        private HeroView _heroView;

        private void Awake()
        {
            _heroView = GetComponent<HeroView>();
        }

        /// <summary>
        /// Обновляет оружие героя и меняет его вид
        /// </summary>
        /// <param name="newShootWeapon">новое стрелковое оружие</param>
        public void UpdateWeapon(BaseWeapon newShootWeapon)
        {
            _selectedShootWeapon = newShootWeapon;
            _heroView.UpdateView(newShootWeapon.WeaponData);
            newShootWeapon.Owner = transform;
        }

        /// <summary>
        /// Осуществляет атаку стрелковым оружием по действию мыши или оружием ближнего боя по кнопке C
        /// </summary>
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _selectedShootWeapon?.Attack();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _meleWeapon?.Attack();
            }
        }
    }
}