using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    /// <summary>
    /// Управляет обновлением ui компонента для отображения оружия
    /// </summary>
    public class UiWeaponUpdater : MonoBehaviour
    {
        [SerializeField] private Image weaponImage = null;
        [SerializeField] private Image mainImage = null;

        public void InitView(WeaponData weaponData)
        {
            weaponImage.sprite = weaponData.UiImage;
        }

        /// <summary>
        /// Изменяет цвет ui при выборе/отмене выбора
        /// </summary>
        /// <param name="isSelected"></param>
        public void Select(bool isSelected)
        {
            mainImage.color = isSelected ? Color.red : Color.white;
        }
    }
}