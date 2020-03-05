using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.EF
{
    public class EmployeeRepository : GenericRepository.GenericRepositry<Employee>
    {
        public DB.Models.DB _dbContext;
        public EmployeeRepository(DB.Models.DB dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Employee Login(EmployeeViewModel model)
        {

            var emp = CheckPassword(model);
            return emp != null ? emp : null;
        }
        public Employee CheckPassword(EmployeeViewModel model)
        {
            Employee emp = getUserByName(model.Name);

            byte[] ComputedPasswordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(emp.PasswordSalt))
            {
                ComputedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
            }
            for (int i = 0; i < emp.PasswordHash.Length; i++)
            {
                if (emp.PasswordHash[i] != ComputedPasswordHash[i])
                {
                    return null;

                }
            }
            return emp;

        }

        Employee getUserByName(string UserName)
        {
          return  GetDataByCondition(x => x.Name == UserName).FirstOrDefault();


        }
    }
}
