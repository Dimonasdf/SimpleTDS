using System.Collections.Generic;
using Hero;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Поведение на смену оружия
    /// </summary>
    public class ChangeWeapon : MonoBehaviour
    {
        /// <summary>
        /// UI, отображающий доступное оружие
        /// </summary>
        [SerializeField] private List<UiWeaponUpdater> uiWeaponUpdaters = null;
        /// <summary>
        /// Полный список оружия
        /// </summary>
        [SerializeField] private List<BaseWeapon> allWeapons = null;
        /// <summary>
        /// Объект атаки героя
        /// </summary>
        [SerializeField] private HeroAttack heroAttack = null;

        /// <summary>
        /// Индекс выбранного оружия
        /// </summary>
        private int _selectedWeaponIndex = 0;

        private void Start()
        {
            InitUi();
            SelectWeaponWithIndex(0);
        }

        /// <summary>
        /// Инициализация UI
        /// </summary>
        private void InitUi()
        {
            for (var i = 0; i < uiWeaponUpdaters.Count; i++)
            {
                uiWeaponUpdaters[i].InitView(allWeapons[i].WeaponData);
            }
        }

        /// <summary>
        /// Изменение типа оружия по выбору игрока
        /// </summary>
        private void Update()
        {
            var newWeaponIndex = -1;
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                newWeaponIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                newWeaponIndex = 1;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                newWeaponIndex = _selectedWeaponIndex == 2 ? 0 : _selectedWeaponIndex + 1;
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                newWeaponIndex = _selectedWeaponIndex == 0 ? 2 : _selectedWeaponIndex - 1;
            }

            if (newWeaponIndex == -1) return;

            SelectWeaponWithIndex(newWeaponIndex);
        }


        /// <summary>
        /// Применение выбранного оружия: изменение ui, обновление оружия у героя
        /// </summary>
        /// <param name="index"></param>
        private void SelectWeaponWithIndex(int index)
        {
            uiWeaponUpdaters[_selectedWeaponIndex].Select(false);

            _selectedWeaponIndex = index;
            uiWeaponUpdaters[_selectedWeaponIndex].Select(true);
            heroAttack.UpdateWeapon(allWeapons[_selectedWeaponIndex]);
        }
    }
}