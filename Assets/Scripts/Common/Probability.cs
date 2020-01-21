using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Утильный класс для работы с вероятностью
    /// </summary>
    public static class Probability
    {
        private static Random _random = new Random();

        /// <summary>
        /// Возвращает рендомный элемент из списка
        /// </summary>
        /// <param name="list">лист</param>
        /// <typeparam name="T">тип объекта списка</typeparam>
        /// <returns>возвращает элемент списка</returns>
        public static T NextRandomInList<T>(IList<T> list)
        {
            if (list == null) return default;
            var index = _random.Next(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// Возвращает значение из диапазона
        /// </summary>
        /// <param name="min">минимальное значение диапазона</param>
        /// <param name="max">максимальное значение из диапазона</param>
        /// <returns>целое число</returns>
        public static int NextIntFromRange(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Возвращает рендомный элемент из списка. Каждый элемент списка имеет поле, соответствующее вероятности появления этого элемента
        /// </summary>
        /// <param name="list">лист значений</param>
        /// <param name="probabilityField">функция получения вероятности элемента списка</param>
        /// <typeparam name="T">тип элемента списка</typeparam>
        /// <returns>элемент списка</returns>
        public static T NextInProbabilityList<T>(IList<T> list, Func<T, float> probabilityField)
        {
            if (list == null) return default;

            var probability = _random.NextDouble() * 100;
            var sumProbability = 0f;
            var i = 0;
            for (; i < list.Count; i++)
            {
                sumProbability += probabilityField.Invoke(list[i]);
                if (sumProbability >= probability) break;
            }

            if (i >= list.Count)
                i = list.Count - 1;

            return list[i];
        }
    }
}