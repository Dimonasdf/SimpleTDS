using System.Collections;
using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Нож. Атака ближнего боя. 
    /// </summary>
    public class KnifeWeapon : BaseWeapon
    {
        [SerializeField] private SpriteRenderer attackEffectSprite = null;

        protected override void DirectAttack()
        {
            var layerMask = LayerMask.GetMask("Enemy");
            var centerPosition = transform.position + new Vector3(WeaponData.AttackDistance / 3, 0);
            var target = Physics2D.OverlapBox(centerPosition,
                new Vector2(WeaponData.AttackDistance / 2, WeaponData.AttackDistance), 0,
                layerMask);

            if (target != null)
                target.GetComponent<IsDestroyable>()?.OnDamage();


            StartCoroutine(DrawAttack());
        }

        /// <summary>
        /// Отображение атаки
        /// </summary>
        /// <returns></returns>
        private IEnumerator DrawAttack()
        {
            attackEffectSprite.enabled = true;
            yield return new WaitForSeconds(0.2f);
            attackEffectSprite.enabled = false;
        }
    }
}