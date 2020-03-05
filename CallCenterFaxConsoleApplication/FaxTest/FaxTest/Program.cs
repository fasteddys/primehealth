using System;
using FAXCOMEXLib;
using System.Linq;
using System.Threading;
using System.IO;
using System.Reflection;

namespace FaxTest
{
  
    class Program
    {

        static bool checkIfFaxDone(FaxSender fs)
        {
            Console.WriteLine("checkIfFaxDone Function");
            Console.WriteLine(fs.FaxIsDone);

            if (fs.FaxIsDone)
            {
                return true;
            }
            else
            {
                int milliseconds = 1000;
                Thread.Sleep(milliseconds);
                return checkIfFaxDone(fs);
            }
        }
        static void addNewLogRunning()
        {
            using (FaxApprovalEntities fax = new FaxApprovalEntities())
            {
                LogsTable newRecoredLog = new LogsTable()
                {
                    CreationDate = DateTime.Now,
                    IsRunning = true,
                };
                fax.LogsTables.Add(newRecoredLog);
                fax.SaveChanges();
            }
        }
        static void changeStatusLogRunning()
        {
            using (FaxApprovalEntities fax = new FaxApprovalEntities())
            {
                var lastLogRecord = fax.LogsTables.ToList().LastOrDefault();
                lastLogRecord.IsRunning = false;
                fax.SaveChanges();
            }

        }
        static void Main(string[] args)
        {

            string SaveFaxAttachmentPath = "", currentPath = "";
            string DepName = "", CompanyName = "", CompanySenderFaxNumber = "", RecivName = "";
            string CompanyRecivFaxNumber = "", Subject = "", DocumentName = "", FilePath = "";
            int MaxNumberOfTries = 0, faxQueueId = -1;
            bool IsRunning = false;
            using (FaxApprovalEntities fax = new FaxApprovalEntities())
            {
                try
                {
                    var lastLogRecord = fax.LogsTables.ToList().LastOrDefault();
                    if (lastLogRecord != null)
                    {
                        IsRunning = lastLogRecord.IsRunning;
                    }
                    else
                    {
                        IsRunning = false;
                    }
                    if (IsRunning != true)
                    {
                        Console.WriteLine("fax service is not Running " + DateTime.Now);
                        Console.WriteLine("fax service is Will Running now " + DateTime.Now);
                        manipulationFile.WriteToFile("fax is not Running" + DateTime.Now);
                        manipulationFile.WriteToFile("fax service is Will Running now " + DateTime.Now);
                        //add new record Running
                        addNewLogRunning();
                        //Start configration Data
                        var FaxConfigurationDataCompanyName = fax.FaxConfigurations.Where(x => x.ConfigurationID == 1).FirstOrDefault();
                        var FaxConfigurationDataDepName = fax.FaxConfigurations.Where(x => x.ConfigurationID == 2).FirstOrDefault();
                        var FaxConfigurationDataFaxNumber = fax.FaxConfigurations.Where(x => x.ConfigurationID == 4).FirstOrDefault();
                        var FaxConfigurationDataRecivName = fax.FaxConfigurations.Where(x => x.ConfigurationID == 5).FirstOrDefault();
                        var FaxConfigurationDataMaxNumberOfTries = fax.FaxConfigurations.Where(x => x.ConfigurationID == 7).FirstOrDefault();
                        var FaxConfigurationSaveFaxAttachmentPath = fax.FaxConfigurations.Where(x => x.ConfigurationID == 10).FirstOrDefault();
                        if (FaxConfigurationDataCompanyName != null)
                        {
                            CompanyName = FaxConfigurationDataCompanyName.ConfigurationValue;
                        }
                        if (FaxConfigurationDataDepName != null)
                        {
                            DepName = FaxConfigurationDataDepName.ConfigurationValue;

                        }
                        if (FaxConfigurationDataFaxNumber != null)
                        {
                            CompanySenderFaxNumber = FaxConfigurationDataFaxNumber.ConfigurationValue;
                        }
                        if (FaxConfigurationDataRecivName != null)
                        {
                            RecivName = FaxConfigurationDataRecivName.ConfigurationValue;
                        }
                        if (FaxConfigurationDataMaxNumberOfTries != null)
                        {
                            MaxNumberOfTries = int.Parse(FaxConfigurationDataMaxNumberOfTries.ConfigurationValue);
                            manipulationFile.WriteToFile("MaxNumberOfTries is=" + MaxNumberOfTries + " at " + DateTime.Now);
                        }
                        if (FaxConfigurationSaveFaxAttachmentPath != null)
                        {
                            SaveFaxAttachmentPath = FaxConfigurationSaveFaxAttachmentPath.ConfigurationValue;
                        }
                        //-----------------------------------------------------------------------------------------------------------------
                        //var CurrentFaxSender = fax.FaxSenderQueues.Where(x => x.EmailApprovalReqFK == EmailApprovalReqFK).FirstOrDefault();
                        var FaxSenders = fax.FaxSenderQueues.Where(x => x.IsSent == false && x.NumberOfTries <= MaxNumberOfTries && x.IsActive == true);
                        manipulationFile.WriteToFile("number of records at list  is=" + FaxSenders.Count() + " at " + DateTime.Now);
                        Console.WriteLine("number of records at list  is=" + FaxSenders.Count() + " at " + DateTime.Now);
                        int counter = 0;
                        bool prevFaxStatus = false;
                        FaxSender fs = new FaxSender();
                        foreach (var CurrentFaxSender in FaxSenders)
                        {
                            if (CurrentFaxSender != null)
                            {
                                CompanyRecivFaxNumber = CurrentFaxSender.RecevingFaxNumber;
                                Subject = CurrentFaxSender.FaxSubject;
                                DocumentName = CurrentFaxSender.AttachmentName;
                                FilePath = CurrentFaxSender.AttachmentPath;
                                faxQueueId = CurrentFaxSender.FaxQueueID;
                                manipulationFile.WriteToFile("FaxCount= " + (counter + 1).ToString());
                                manipulationFile.WriteToFile("faxQueueId= " + faxQueueId);
                                Console.WriteLine("faxQueueId is =" + faxQueueId);
                                currentPath = SaveFaxAttachmentPath + FilePath;//change at send fax
                                Console.WriteLine(currentPath);
                                manipulationFile.WriteToFile(currentPath + " " + DateTime.Now, faxQueueId);
                                manipulationFile.WriteToFile(FilePath + " " + DateTime.Now, faxQueueId);

                                if (counter == 0 || (counter > 0 && prevFaxStatus == true))
                                {
                                    Console.WriteLine("before call SendFax");
                                    if (prevFaxStatus)
                                    {
                                        prevFaxStatus = false;
                                        fs.FaxIsDone = false;
                                    }

                                    //fs.SendFax(faxQueueId, DepName, CompanyName, CompanyRecivFaxNumber, RecivName, Subject, DocumentName, FilePath);//send fax
                                    fs.SendFax(faxQueueId, DepName, CompanyName, CompanyRecivFaxNumber, RecivName, Subject, DocumentName, currentPath);//send fax
                                }
                                counter++;

                                if (checkIfFaxDone(fs))
                                {
                                    prevFaxStatus = true;
                                }
                                Console.WriteLine("fax number faxQueueId= " + faxQueueId + " is Done!");
                                Console.WriteLine("-----------------------------------------------------------");
                            }

                        }
                        manipulationFile.WriteToFile("----------------------------------------------------------- ");
                        //Update current record Running
                        changeStatusLogRunning();
                    }
                    else
                    {
                        Console.WriteLine("now fax service  is Running please wait...");
                        manipulationFile.WriteToFile("now fax service  is Running please wait..." + DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    changeStatusLogRunning();
                    ExceptionHandlerConstants.FaxException(ex, "Main Program ", MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    manipulationFile.WriteToFile("Exception is :-" + ex.Message + " " + DateTime.Now);
                    manipulationFile.WriteExceptionToFile(ex.Message + " " + DateTime.Now);
                }

            }
            Console.ReadLine();
            //Environment.Exit(0);
        }
    }


}
