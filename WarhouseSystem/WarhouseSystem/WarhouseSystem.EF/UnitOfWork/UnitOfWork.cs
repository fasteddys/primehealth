using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
namespace WarhouseSystem.EF.UnitOfWork
{
    public  class UnitOfWork : IDisposable
    {
        private  DB.Models.DB _dbContext;

        public UnitOfWork()
        {
            _dbContext = new DB.Models.DB();
        }
        private static readonly object lockPad = new object();

        private EmployeeRoleRepository _employeeRoleRepository;

        private StatusTypeRepository _statusTypeRepository;

        private TransactionTypeRepository _transactionTypeRepository;

        private EmployeeRepository _employeeRepository;

        private CompanyRepository _companyRepository;

        private JobRepository _jobRepository;

        private DepartmentRepository _departmentRepository;

        private StockRepository _stockRepository;

        private CategoryRepository _categoryRepository;

        private SubCategoryRepository _subCategoryRepository;

        private ItemRepository _itemRepository;

        private ProcessTransactionRepository _processTransactionRepository;

        public ProcessTransactionRepository processTransactionRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._processTransactionRepository == null)
                        this._processTransactionRepository = new ProcessTransactionRepository(_dbContext);
                }
                return this._processTransactionRepository;
            }
        }

        public EmployeeRepository employeeRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._employeeRepository == null)
                        this._employeeRepository = new EmployeeRepository(_dbContext);
                }
                return this._employeeRepository;
            }
        }

        public EmployeeRoleRepository employeeRoleRepository
        {
            get
            {
                lock(lockPad)
                {
                    if (this._employeeRoleRepository == null)
                        this._employeeRoleRepository = new EmployeeRoleRepository(_dbContext);
                }
                return this._employeeRoleRepository;
            }
        }

        public CompanyRepository companyRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._companyRepository == null)
                        this._companyRepository = new CompanyRepository(_dbContext);
                }
                return this._companyRepository;
            }
        }

        public StatusTypeRepository statusTypeRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._statusTypeRepository == null)
                        this._statusTypeRepository = new StatusTypeRepository(_dbContext);
                }
                return this._statusTypeRepository;
            }
        }

        public TransactionTypeRepository transactionTypeRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._transactionTypeRepository == null)
                        this._transactionTypeRepository = new TransactionTypeRepository(_dbContext);
                }
                return this._transactionTypeRepository;
            }
        }
        public JobRepository jobRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._jobRepository == null)
                        this._jobRepository = new JobRepository(_dbContext);
                }
                return this._jobRepository;
            }
        }
        public StockRepository stockRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._stockRepository == null)
                        this._stockRepository = new StockRepository(_dbContext);
                }
                return this._stockRepository;
            }
        }
     
        public DepartmentRepository departmentRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._departmentRepository == null)
                        this._departmentRepository = new DepartmentRepository(_dbContext);
                }
                return this._departmentRepository;
            }
        }
        

        public CategoryRepository categoryRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._categoryRepository == null)
                        this._categoryRepository = new CategoryRepository(_dbContext);
                }
                return this._categoryRepository;
            }
        }

        public SubCategoryRepository subCategoryRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._subCategoryRepository == null)
                        this._subCategoryRepository = new SubCategoryRepository(_dbContext);
                }
                return this._subCategoryRepository;
            }
        }

        public ItemRepository itemRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (this._itemRepository == null)
                        this._itemRepository = new ItemRepository(_dbContext);
                }
                return this._itemRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
     

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
