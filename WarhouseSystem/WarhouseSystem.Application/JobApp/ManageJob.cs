using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.JobApp
{
    public class ManageEJob : IManageJob
    {
        private UnitOfWork unit = null;

        MappingJob map = new MappingJob();

        public ManageEJob()
        {
            unit = new UnitOfWork();
        }
        public bool AddJob(JobViewModel model)
        {
            try
            {
                Job Job = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.jobRepository.Add(Job);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateJob(long Id, JobViewModel model)
        {
            if (Id == 0)
            {
                Job Job = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.jobRepository.Add(Job);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                Job Job = unit.jobRepository.GetDataById(Id);
                map.MapModelToEntity(model, Job);
                Utilites.CheckResult = unit.jobRepository.Modify(model.Id, Job);
                unit.Save();
                return Utilites.CheckResult;
            }
        }

        public bool DeleteJob(long Id)
        {
            try
            {
                Utilites.CheckResult = unit.jobRepository.Delete(Id);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public JobViewModel GetJob(long Id)
        {
            Job Job = unit.jobRepository.GetDataById(Id);
            JobViewModel JobViewModel = map.MapEntityToModel(Job);
            return JobViewModel;
        }

        public IEnumerable<JobViewModel> GetAllJobs()
        {
            IEnumerable<Job> AllJobs = unit.jobRepository.GetAllData();
            IEnumerable<JobViewModel> items = AllJobs.Select(map.MapEntityToModel);
            return items;
        }
    }
}
