using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineApprovals.DAL.Factory;
using System.Data.Entity.Validation;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using OnlineApprovals.Entities;

namespace OnlineApprovals.DAL.Repositories
{
    public class Repository<TT> : ContainerContextFactory, IDisposable, IRepository<TT> where TT : class
    {
        #region Properties

        /// <summary>
        /// Generic Table from Context
        /// </summary>
        public DbSet<TT> Table { get; set; }
        #endregion   
        #region Constractors

        /// <summary>
        /// Empty Constructor 
        /// </summary>
        public Repository(PhNetworkEntities Context)
        {
            context = Context;
            Table = context.Set<TT>();
        }

        #endregion
        #region Funcss
        /// <summary>
        /// Add Data 
        /// </summary>
        /// <param name="t"> object from table</param>
        /// <returns>Return Row Added</returns>
        public virtual void Add(TT t)
        {
            try
            {
                Table.Add(t);
            }
            catch (DbEntityValidationException e)
            {
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// Detete Rows (For Tables that Haven't IsActive Column
        /// </summary>
        /// <param name="t"> Table object</param>
        /// <returns></returns>
        public virtual void Delete(TT t)
        {
            try
            {
                Table.Remove(t);

            }
            catch (Exception ex)
            {
            }


        }
        /// <summary>
        /// Detete Rows Async(For Tables that Haven't IsActive Column
        /// </summary>
        /// <param name="t"> Table object</param>
        /// <returns></returns>
        /// <summary>
        /// Return single row 
        /// </summary>
        /// <param name="id"> table id params object for more customization </param>
        /// <returns>Db Row</returns>
        public virtual TT GetById(params object[] id)
        {
            try
            {
                return Table.Find(id);

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// Return single row Async
        /// </summary>
        /// <param name="id"> table id params object for more customization </param>
        /// <returns>Db Row</returns>
        /// 
        public async Task<TT> GetByIdAsync(params object[] id)
        {
            try
            {
                return await Table.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Edit Table Row
        /// </summary>
        /// <param name="t">Table object</param>
        /// <param name="excludedProperties"></param>
        /// mo
        /// <returns> true or false (updated or not) </returns>
        public virtual void Edit(TT t)
        {
            try
            {

                context.Entry<TT>(t).State = EntityState.Modified;

            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// Edit Table Row
        /// </summary>
        /// <param name="t">Table object</param>
        /// <param name="excludedProperties"></param>
        /// mo
        /// <returns> true or false (updated or not) </returns>
        public virtual void SaveExcluded(TT t, params string[] excludedProperties)
        {

            try
            {
                if (excludedProperties.Any())
                {

                    Table.Attach(t);
                    context.Configuration.ValidateOnSaveEnabled = false;
                    foreach (var name in excludedProperties)
                    {
                        context.Entry(t).Property(name).IsModified = false;
                    }
                    var takenProp = context.Entry<TT>(t).CurrentValues.PropertyNames.Except(excludedProperties);
                    foreach (var name in takenProp)
                    {

                        context.Entry(t).Property(name).IsModified = true;
                    }
                }
                else
                {
                    context.Entry<TT>(t).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// Edit Table Row
        /// </summary>
        /// <param name="t">Table object</param>
        /// <param name="included"></param>
        /// mo
        /// <returns> true or false (updated or not) </returns>
        public virtual void SaveIncluded(TT t, params string[] included)
        {

            try
            {
                if (included.Any())
                {
                    Table.Attach(t);
                    context.Configuration.ValidateOnSaveEnabled = false;
                    foreach (var name in included)
                    {
                        context.Entry(t).Property(name).IsModified = true;
                    }
                    var excludedProps = context.Entry<TT>(t).CurrentValues.PropertyNames.Except(included);
                    foreach (var name in excludedProps)
                    {
                        context.Entry(t).Property(name).IsModified = false;
                    }
                }
                else
                {
                    context.Entry<TT>(t).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// Edit Table Row Async
        /// </summary>
        /// <param name="t">Table object</param>mo
        /// <returns> true or false (updated or not) </returns>
        public async Task EditAsync(TT t)
        {
            try
            {
                context.Entry<TT>(t).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// Get All Data with linq IQueryable 
        /// </summary>
        /// <returns>All Data</returns>
        public virtual IQueryable<TT> GetAll()
        {
            try
            {
                return Table;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Get All Data with linq IQueryable Async
        /// </summary>
        /// <returns>All Data</returns>
        public async Task<IQueryable<TT>> GetAllAsync()
        {
            try
            {
                // return await Task.Run(() =>  Table  );
                var items = await Table.ToListAsync();
                return items.AsQueryable();
            }


            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Get Table Count
        /// </summary>
        /// <returns></returns>
        public virtual int GetTableCount()
        {
            try
            {
                return Table.Count();
            }

            catch (Exception ex)
            {
                return -1;
            }
        }
        /// <summary>
        /// Get All where statment
        /// </summary>
        /// <param name="where"> linq statement</param>
        /// <returns></returns>
        public virtual IQueryable<TT> Find(Expression<Func<TT, bool>> @where)
        {
            return Table.Where(@where);
        }
        /// <summary>
        /// Get All where statment Async
        /// </summary>
        /// <param name="where"> linq statement</param>
        /// <returns></returns>
        public async Task<IQueryable<TT>> FindAsync(Expression<Func<TT, bool>> where)
        {
            var items = await Table.Where(@where).ToListAsync();
            return items.AsQueryable();
        }
        /// <summary>
        /// Get Single Row where statment
        /// </summary>
        /// <param name="where"> linq statement</param>
        /// <returns></returns>
        public virtual TT Single(Expression<Func<TT, bool>> @where)
        {
            return Table.Single(where) ?? Table?.SingleOrDefault(@where); ;
        }
        /// <summary>
        /// Get First Element of data 
        /// </summary>
        /// <param name="where"> linq statement</param>
        /// <returns></returns>
        public virtual TT First(Expression<Func<TT, bool>> @where)
        {
            return Table?.First(@where) ?? Table?.FirstOrDefault(@where);
        }        
        public virtual void EditRange(IEnumerable<TT> list)
        {
            try
            {
                foreach (var item in list)
                {
                    context.Entry<TT>(item).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public virtual void RemoveRange(IEnumerable<TT> list)
        {
            try
            {
                Table.RemoveRange(list);
            }
            catch (Exception ex)
            {
            }
        }       
        public virtual void AddRange(IEnumerable<TT> list)
        {
            try
            {
                Table.AddRange(list);
            }
            catch (Exception ex)
            {
            }
        }
        
        /// <summary>
                 /// order , skip and take no of rows
                 /// </summary>
                 /// <param name="order"> asscending or descending</param>
                 /// <param name="skipRows">rows to skip</param>
                 /// <param name="takenRows">rows to take</param>
                 /// <returns></returns>
        public virtual IQueryable<TT> Skip(Expression<Func<TT, bool>> order, int skipRows, int? takenRows)
        {
            return takenRows == null ? Table.OrderBy(order).Skip(skipRows) :
             Table.OrderBy(order).Skip(skipRows).Take(takenRows.Value);
        }
        #endregion
        #region DisopseDbObject
        public new void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
