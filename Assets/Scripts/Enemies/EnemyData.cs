using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Дата класс для хранения данных врагов
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        /// <summary>
        /// скорость движения врага
        /// </summary>
        [SerializeField] private float speedCoefficient = 1f;

        /// <summary>
        /// количество hp
        /// </summary>
        [SerializeField] private float hpPoints = 1f;

        /// <summary>
        /// цвет спрайта врага
        /// </summary>
        [SerializeField] private Color enemyColor = Color.white;

        public float SpeedCoefficient => speedCoefficient;
        public float HpPoints => hpPoints;
        public Color EnemyColor => enemyColor;
    }
}