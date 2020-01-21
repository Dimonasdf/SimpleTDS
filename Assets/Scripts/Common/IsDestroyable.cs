namespace Common
{
    /// <summary>
    /// Общий интерфейс для объектов, которые могут получить урон
    /// </summary>
    public interface IsDestroyable
    {
        /// <summary>
        /// Действие в момент получения урона
        /// </summary>
        void OnDamage();
    }
}