using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.JobApp
{
    public interface IManageJob
    {
        bool AddJob(JobViewModel model);

        bool UpdateJob(long Id, JobViewModel model);

        bool DeleteJob(long Id);

        JobViewModel GetJob(long Id);

        IEnumerable<JobViewModel> GetAllJobs();
    }
}
