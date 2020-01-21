using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Common
{
    /// <summary>
    /// Утилита для работы с вращениями
    /// </summary>
    public class RotationUtil
    {
        /// <summary>
        /// Возвращает кватернион, соответствующий вращению в заданном направлении(вращение вокруг оси z)
        /// </summary>
        /// <param name="direction">направление</param>
        /// <returns>кватернион для вращения</returns>
        public static Quaternion ToDirection(Vector3 direction)
        {
            var normalized = direction.normalized;
            var angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;

            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}