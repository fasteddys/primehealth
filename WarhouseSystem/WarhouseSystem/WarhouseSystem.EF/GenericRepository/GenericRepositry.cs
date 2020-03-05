using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WarhouseSystem.DB.Models;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

namespace WarhouseSystem.EF.GenericRepository
{
    public abstract class GenericRepositry<TEntity> where TEntity : class
    {
        private DB.Models.DB _db;
        private DbSet<TEntity> dbSet = null;
        public GenericRepositry(DB.Models.DB db)
        {
            _db = db;
            dbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAllData()
        {
            try
            {
                IQueryable<TEntity> qury = dbSet;
                return qury.ToList();
            }
            catch(Exception ex)
            {
               string s= ex.GetType().Name;
                throw new System.ArgumentNullException("Cann't Found Data");
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllDataAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("Cann't Found Data");
            }
        }

        public virtual TEntity GetDataById(object Id)
        {
            try
            {
                return dbSet.Find(Id);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("Cann't Found Data");
            }
        }

        public virtual async Task<TEntity> GetDataByIdAsync(object Id)
        {
            try
            {
                return await dbSet.FindAsync(Id);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("Cann't Found Data");
            }
        }

        public virtual IEnumerable<TEntity> GetDataByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                IQueryable<TEntity> listData = dbSet.Where(predicate);
                return  listData.ToList();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("Cann't Found Data");
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetDataByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                IQueryable<TEntity> listData = dbSet.Where(predicate);
                return await listData.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("Cann't Found Data");
            }
        }

        public virtual bool Add(TEntity model)
        {
            try
            {
                 dbSet.Add(model);
                 return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(long Id)
        {
            try
            {
                TEntity entity = dbSet.Find(Id);
                dbSet.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Modify(long Id,TEntity model)
        {
            try
            {
               if(Id==0 || Id ==null)
                {
                    dbSet.Add(model);
                }
                else
                {
                    dbSet.AddOrUpdate(model);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual int GetCount()
        {
            return dbSet.Count();
        }
    }
}
