using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DAL.Repositories
{
    interface IRepository<TT>
    {
        /// <summary>
        /// Get All Data with linq IQueryable 
        /// </summary>
        /// <returns>All Data</returns>
        IQueryable<TT> GetAll();

        /// <summary>
        /// Get All Data with linq IQueryable  Async
        /// </summary>
        /// <returns>All Data</returns>
        Task<IQueryable<TT>> GetAllAsync();

        /// <summary>
        /// Return single row 
        /// </summary>
        /// <param name="id"> table id params object for more customization </param>
        /// <returns>Db Row</returns>
        TT GetById(params object[] id);

        /// <summary>
        /// Return single row 
        /// </summary>
        /// <param name="id"> table id params object for more customization Async</param>
        /// <returns>Db Row</returns>
        Task<TT> GetByIdAsync(params object[] id);

        /// <summary>
        /// Add Data 
        /// </summary>
        /// <param name="t"> object from table</param>
        /// <returns>Return Row Added</returns>
        void Add(TT t);
        /// <summary>
        /// Add Data Async
        /// </summary>
        /// <param name="t"> object from table</param>
        /// <returns>Return Row Added</returns>
        /// <summary>
        /// Add Data Async
        /// </summary>
        /// <param name="t"> object from table</param>
        /// <returns>Return Row Added</returns>
        //Task AddAsync(TT t);
        /// <summary>
        /// Edit Table Row Async
        /// </summary>
        /// <param name="t">Table object</param>mo
        /// <returns> true or false (updated or not) </returns>
        //Task EditAsync(TT t);
        /// <summary>
        /// Detete Rows (For Tables that Haven't IsActive Column
        /// </summary>
        /// <param name="t"> Table object</param>
        /// <returns></returns>
        void Delete(TT t);

        /// <summary>
        /// Detete Rows (For Tables that Haven't IsActive Column
        /// </summary>
        /// <param name="t"> Table object</param>
        /// <returns></returns>
        //Task<bool> DeleteAsync(TT t);
        /// <summary>
        /// Get Table Count
        /// </summary>
        /// <returns></returns>
        int GetTableCount();
        /// <summary>
        /// Find Function , Passs linq Statement
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<TT> Find(Expression<Func<TT, bool>> where);

        /// <summary>
        /// Find Function , Passs linq Statement Async
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IQueryable<TT>> FindAsync(Expression<Func<TT, bool>> where);
        /// <summary>
        /// Single Function , Passs linq Statement
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        TT Single(Expression<Func<TT, bool>> where);
        /// <summary>
        /// First Function , Passs linq Statement
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        TT First(Expression<Func<TT, bool>> where);
        void SaveIncluded(TT t, params string[] includedProperties);
        void SaveExcluded(TT t, params string[] excludedProperties);
    }
}
