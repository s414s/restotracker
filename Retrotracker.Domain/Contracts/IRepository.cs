namespace SocialX.Domain;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetByID(Guid id);
    T Add(T entity);
    T Delete(Guid id);
    T Update(T entity);
}