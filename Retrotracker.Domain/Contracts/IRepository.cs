namespace Retrotracker.Domain;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetByID(string id);
    T Add(T entity);
    T Delete(string id);
    T Update(T entity);
}