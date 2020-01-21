using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Дата-класс для хранения данных оружия
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Sprite uiImage = null;
        [SerializeField] private Sprite heroSprite = null;
        [SerializeField] private float cooldown = 0f;
        [SerializeField] private float attackSpeed = 0f;
        [SerializeField] private float attackDistance = 0f;

        public Sprite UiImage => uiImage;
        public Sprite HeroSprite => heroSprite;
        public float Cooldown => cooldown;

        public float AttackSpeed => attackSpeed;

        public float AttackDistance => attackDistance;
    }
}