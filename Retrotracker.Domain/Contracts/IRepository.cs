namespace Retrotracker.Domain;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetByID(string id);
    T Add(T entity);
    bool Delete(T entity);
    T Update(T entity);
}