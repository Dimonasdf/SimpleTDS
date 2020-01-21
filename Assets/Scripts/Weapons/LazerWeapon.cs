using System.Collections;
using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Лазерное оружие
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class LazerWeapon : BaseWeapon
    {
        private LineRenderer _line;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
            _line.enabled = false;
        }

        /// <summary>
        /// Атакует все цели на линии огня заданной дистанции
        /// </summary>
        protected override void DirectAttack()
        {
            var layerMask = LayerMask.GetMask("Enemy");
            var direction = Owner.rotation * Vector3.right;
            var startPosition = transform.position;
            var targets = Physics2D.RaycastAll(startPosition, direction, WeaponData.AttackDistance, layerMask);

            for (var i = 0; i < targets.Length; i++)
            {
                targets[i].collider.GetComponent<IsDestroyable>()?.OnDamage();
            }

            StartCoroutine(DrawAttack(direction, WeaponData.AttackDistance));
        }

        /// <summary>
        /// Отрисовывает линию атаки
        /// </summary>
        /// <param name="direction">направление линии</param>
        /// <param name="distance">длина линии</param>
        /// <returns></returns>
        private IEnumerator DrawAttack(Vector3 direction, float distance)
        {
            _line.enabled = true;
            var position = transform.position;
            _line.SetPosition(0, position);
            _line.SetPosition(1, position + direction.normalized * distance);
            yield return new WaitForSeconds(0.1f);
            _line.enabled = false;
        }
    }
}