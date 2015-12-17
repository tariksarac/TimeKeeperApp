using System.Linq;

namespace timeBase
{
    public interface baseInterface<T>
    {
        timeContext baseContext();
        void Insert(T entity);
        void Delete(T entity);
        void Update(T oldEntity, T newEntity);
        T Get(int Id);
        IQueryable<T> Get();
        void Commit();
    }
}
