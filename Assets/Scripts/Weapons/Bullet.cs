using System;
using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Поведение пули
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IsDestroyable
    {
        private Rigidbody2D _rb;
        private float _maxDistance = 0;
        private Vector2 _startPoint;
        private Action<Bullet> _onDestroyAction;
        private Transform _owner;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Выстрел пули
        /// </summary>
        /// <param name="startPosition">Стартовая позиция пули</param>
        /// <param name="speed">Скорость полета</param>
        /// <param name="maxDistance">Максимальная дистанция полета пули </param>
        public void Shoot(Vector3 startPosition, float speed, float maxDistance)
        {
            _maxDistance = maxDistance;
            _startPoint = startPosition;
            _rb.position = _startPoint;

            var rotation = _owner.rotation;
            _rb.MoveRotation(rotation);

            _rb.velocity = rotation * Vector3.right * speed;
        }


        /// <summary>
        /// Если пуля пролетела максимальную дистанцию и не встретила препятствие, то ее нужно уничтожить
        /// </summary>
        private void FixedUpdate()
        {
            if ((_rb.position - _startPoint).magnitude >= _maxDistance)
            {
                OnDamage();
            }
        }

        /// <summary>
        /// При коллизии пули и врага, врага наносится урон, пуля уничтожается
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            var isDestroyable = other.GetComponent<IsDestroyable>();
            if (isDestroyable == null) return;

            isDestroyable.OnDamage();
            OnDamage();
        }

        /// <summary>
        /// Действие на уничтожение пули
        /// </summary>
        public void OnDamage()
        {
            _onDestroyAction?.Invoke(this);
        }

        /// <summary>
        /// Действие на инициализацию пули
        /// </summary>
        /// <param name="owner">объект-владелец пули</param>
        /// <param name="onDestroyAction">действие на уничтожение</param>
        public void Init(Transform owner, Action<Bullet> onDestroyAction)
        {
            _onDestroyAction = onDestroyAction;
            _owner = owner;
        }
    }
}