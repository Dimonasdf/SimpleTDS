using UnityEngine;
using Weapons;

namespace Hero
{
    /// <summary>
    /// Определяет внешний вид героя
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class HeroView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Изменяет внешний вид при изменении оружия
        /// </summary>
        /// <param name="data">данные оружия</param>
        public void UpdateView(WeaponData data)
        {
            _spriteRenderer.sprite = data.HeroSprite;
        }
    }
}