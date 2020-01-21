using System;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Простая реализация пула объектов. Объекты создаются по мере необходимости, пока не будет достигнут максимальный размер пула. В случае наполнения, пул не расширяется.
    /// </summary>
    /// <typeparam name="T">тип объектов, хранимых в пуле</typeparam>
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private int _maxPoolSize;
        private Action<T> _initObjectAction;
        private Func<T> _instantiateFunc;
        private T[] _poolObjects;
        private int _currentSize = 0;

        /// <summary>
        /// Конструктор пула
        /// </summary>
        /// <param name="maxSize">Максимальный размера</param>
        /// <param name="instantiateFunc">Функция создания объектов пула</param>
        /// <param name="initAction">Функция инициализации объектов перед повторным использованием</param>
        public ObjectPool(int maxSize, Func<T> instantiateFunc, Action<T> initAction = null)
        {
            _maxPoolSize = maxSize;
            _initObjectAction = initAction;
            _instantiateFunc = instantiateFunc;
            _poolObjects = new T[_maxPoolSize];
        }

        /// <summary>
        /// Возвращает объект в пул
        /// </summary>
        /// <param name="obj">Возвращаемый объект</param>
        public void Push(T obj)
        {
            if (obj == null) return;
            obj.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Возвращает 1ый неактивный элемент из пула. Если пул наполнен не полностью и нет свободных объектов, то создается новый объект. 
        /// </summary>
        /// <returns>свободный объект</returns>
        public T Pull()
        {
            var obj = FindNotActive();
            if (obj == null) return null;
            InitObject(obj);
            return obj;
        }

        /// <summary>
        /// Возвращает 1ый неактивный элемент или создает новый, если пул еще не заполнен
        /// </summary>
        /// <returns>возвращает свободный объект</returns>
        private T FindNotActive()
        {
            var obj = FindFirstNotActive();
            if (obj == null && _currentSize < _maxPoolSize)
            {
                obj = InstantiateNewObject();
            }

            return obj;
        }

        /// <summary>
        /// Инициализирует объект перед использованием.
        /// </summary>
        /// <param name="obj">Объект для инициализации</param>
        private void InitObject(T obj)
        {
            _initObjectAction?.Invoke(obj);
            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// Создает новый объект
        /// </summary>
        /// <returns>возвращает созданный объект</returns>
        private T InstantiateNewObject()
        {
            var obj = _instantiateFunc.Invoke();
            _poolObjects[_currentSize] = obj;
            _currentSize++;
            return obj;
        }

        private T FindFirstNotActive()
        {
            for (var i = 0; i < _currentSize; i++)
            {
                var obj = _poolObjects[i];
                if (obj == null) return null;
                if (!obj.gameObject.activeSelf) return obj;
            }

            return null;
        }
    }
}