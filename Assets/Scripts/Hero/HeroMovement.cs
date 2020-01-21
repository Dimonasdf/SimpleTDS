using Common;
using UnityEngine;

namespace Hero
{
    /// <summary>
    /// Управляет движением героя
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroMovement : MonoBehaviour
    {
        /// <summary>
        /// Коэффициент скорости движения
        /// </summary>
        [SerializeField] private Vector2 speedCoefficient = Vector2.one;

        /// <summary>
        /// Главная камера
        /// </summary>
        [SerializeField] private Camera currentCamera = null;

        /// <summary>
        /// Область, по которой может двигаться герой
        /// </summary>
        [SerializeField] private Collider2D movementArea = null;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MoveHero();
            RotateHero();
        }

        /// <summary>
        /// Вращение героя за позицией мыши
        /// </summary>
        private void RotateHero()
        {
            var eyeDirection = currentCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _rb.MoveRotation(RotationUtil.ToDirection(eyeDirection));
        }

        /// <summary>
        /// Движение героя по вводу пользователя внутри границ допустимой области
        /// </summary>
        private void MoveHero()
        {
            var xMovement = Input.GetAxis("Horizontal");
            var yMovement = Input.GetAxis("Vertical");
            var newPosition = _rb.position + new Vector2(xMovement, yMovement) * speedCoefficient;

            if (movementArea.bounds.Contains(newPosition))
                _rb.MovePosition(newPosition);
        }
    }
}