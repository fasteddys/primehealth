using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OnlineApprovals.Entities;
using System.IO;
using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.Utilities;

namespace OnlineApprovals.BLL
{
    public class FileIO : Repository<OnlineApprovals_RequestAttachment>
    {
        public FileIO(PhNetworkEntities Context) : base(Context)
        {

        }

        public bool FileUpload(IEnumerable<HttpPostedFileBase> files,  int RequestDetailsID, int RequestID, int AttachmentTypeID , out string ValidationMessage)
        {
            try
            {
                string Path = "";
                foreach (var item in files)
                {
                    if (item.ContentLength > 0)
                    {
                        OnlineApprovals_RequestAttachment Temp = new OnlineApprovals_RequestAttachment();

                        Temp.AttachmentName = item.FileName;
                        Temp.AttachmentPath = Path;
                        Temp.AttachmentTypeID = AttachmentTypeID;
                        Temp.IsDeleted = false;
                        Temp.RequestDetailsID = RequestID;
                        Temp.RequestDetailsID = RequestDetailsID;
                        item.SaveAs(Path);
                        context.OnlineApprovals_RequestAttachment.Add(Temp);
                        context.SaveChanges();                        
                    }
                }
                ValidationMessage = "File(s) Uploaded Successfully";
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "FileIO.cs", "FileUpload");
                ValidationMessage = "File(s) Uploading Failed";
                return false;
            }

        }

        



        //Backup












    }

}
