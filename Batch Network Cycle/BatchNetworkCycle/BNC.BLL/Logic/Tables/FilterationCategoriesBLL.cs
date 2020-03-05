using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;

namespace BNC.BLL.Logic.Tables
{
    public class FilterationCategoriesBLL : Repository<FilterationCategoriesDIM>
    {
        DateTime creationDate;

        public FilterationCategoriesBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        //14)  inpatient outPatient prime
        public string getFilterationCategoryName(int FilterationCategoryFK)
        {
            return Find(fc => fc.FilterationCategoriesID == FilterationCategoryFK).FirstOrDefault().FilterationCategoryName;
        }
    }
}
