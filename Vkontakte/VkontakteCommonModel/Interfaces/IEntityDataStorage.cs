using VkontakteInfrastructure.Model;

namespace VkontakteInfrastructure.Interfaces
{
    public interface IEntityDataStorage
    {
        void SaveEntity<T>(T entity);
        T LoadEntity<T>();

        void DeleteEntity<T1>();
    }
}