using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    //every table needs to have common CRUD operations
    //IRepository will be implemented by Repository class
    //T is going to be a placeholder for child repositories that will be replace by entityof that class
    //MovieRepository => Movie for T
    //UserRepository => User for T
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T,bool>> filter);
        Task<T> Add (T entity);
        Task<T> Update (T entity);
        Task Delete (int id);
    }
}
