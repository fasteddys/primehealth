using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.JobApp
{
   public  class MappingJob
    {
        public Job MapModelToEntity(JobViewModel Model, Job Entity)
        {
            Entity.Name = Model.Name;
            Entity.Id = Model.Id;
            return Entity;
        }
        public Job MapModelToEntity(JobViewModel Model)
        {
            return new Job()
            {
                Id = Model.Id,
                Name = Model.Name,
                CreationTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                DepartmentId = Model.DepartmentId
            };
        }
        public JobViewModel MapEntityToModel(Job Entity)
        {
            return new JobViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                DepartmentId = Entity.DepartmentId
            };
        }
    }
}
