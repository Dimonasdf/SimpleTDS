using System;
using Common;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Определяет поведение врага
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehaviour : MonoBehaviour, IsDestroyable
    {
        private Rigidbody2D _enemyTarget = null;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private float _remainingHpPoints = 1f;
        private Action<EnemyBehaviour> _onDeathAction;
        public EnemyData EnemyData { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Обновляет данные врага при извлечении объекта из пула
        /// </summary>
        /// <param name="data">данные врага</param>
        /// <param name="moveTarget">цель, к которой движется враг</param>
        public void UpdateData(EnemyData data, Rigidbody2D moveTarget)
        {
            _enemyTarget = moveTarget;
            EnemyData = data;
            _sr.color = data.EnemyColor;
            _remainingHpPoints = data.HpPoints;
        }

        /// <summary>
        /// Устанавливает действие на смерть врага
        /// </summary>
        /// <param name="onDeathAction">действие</param>
        public void SetOnDeathAction(Action<EnemyBehaviour> onDeathAction)
        {
            _onDeathAction = onDeathAction;
        }

        /// <summary>
        /// Устанавливает направление движения и скорость для преследования цели
        /// </summary>
        private void FixedUpdate()
        {
            var distance = _enemyTarget.position - _rb.position;
            //остановить движение, если подошли вплотную к цели
            if (distance.magnitude <= 0.05)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            var direction = distance.normalized;
            _rb.velocity = direction * EnemyData.SpeedCoefficient;
            _rb.MoveRotation(RotationUtil.ToDirection(direction));
        }

        /// <summary>
        /// Действие на получение урона
        /// </summary>
        public void OnDamage()
        {
            _remainingHpPoints--;
            if (_remainingHpPoints <= 0)
                _onDeathAction?.Invoke(this);
        }
    }
}