using System;
using System.IO;
using FAXCOMEXLib;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using System.Diagnostics;

namespace FaxTest
{

  #region FaxServices
    public class FaxSender
     {
            private FaxServer faxServer;
            private FaxDocument faxDoc;
            private int faxQueueId;
            public bool FaxIsDone;
            public FaxSender()
            {
                manipulationFile.WriteToFile("Service Fax is at  FaxSender Constructor at " + DateTime.Now, this.faxQueueId);
                try
                {
                    faxServer = new FaxServer();
                    faxServer.Connect(Environment.MachineName);
                    RegisterFaxServerEvents();
                    FaxIsDone = false;

                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.FaxException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    Console.WriteLine(ex.Message);
                    manipulationFile.WriteExceptionToFile(ex.Message + " " + DateTime.Now);
                }

            }
            private void RegisterFaxServerEvents()
            {
                //Console.WriteLine("faxQueueId in RegisterFaxServerEvents  is =" + faxQueueId);
                manipulationFile.WriteToFile("Service Fax is at  RegisterFaxServerEvents Function at " + DateTime.Now, this.faxQueueId);
                faxServer.OnOutgoingJobAdded +=
                    new IFaxServerNotify2_OnOutgoingJobAddedEventHandler(faxServer_OnOutgoingJobAdded);
                faxServer.OnOutgoingJobChanged +=
                    new IFaxServerNotify2_OnOutgoingJobChangedEventHandler(faxServer_OnOutgoingJobChanged);
                faxServer.OnOutgoingJobRemoved +=
                    new IFaxServerNotify2_OnOutgoingJobRemovedEventHandler(faxServer_OnOutgoingJobRemoved);

                var eventsToListen =
                    FAX_SERVER_EVENTS_TYPE_ENUM.fsetFXSSVC_ENDED | FAX_SERVER_EVENTS_TYPE_ENUM.fsetOUT_QUEUE |
                    FAX_SERVER_EVENTS_TYPE_ENUM.fsetOUT_ARCHIVE | FAX_SERVER_EVENTS_TYPE_ENUM.fsetQUEUE_STATE |
                    FAX_SERVER_EVENTS_TYPE_ENUM.fsetACTIVITY | FAX_SERVER_EVENTS_TYPE_ENUM.fsetDEVICE_STATUS;

                faxServer.ListenToServerEvents(eventsToListen);
            }
            #region Event Listeners
            private void faxServer_OnOutgoingJobAdded(FaxServer pFaxServer, string bstrJobId)
            {
                Console.WriteLine("OnOutgoingJobAdded event fired. A fax is added to the outgoing queue.");
                manipulationFile.WriteToFile("OnOutgoingJobAdded event fired. A fax is added to the outgoing queue.", this.faxQueueId);

            }
            private void faxServer_OnOutgoingJobChanged(FaxServer pFaxServer, string bstrJobId, FaxJobStatus pJobStatus)
            {
                Console.WriteLine("OnOutgoingJobChanged event fired. A fax is changed to the outgoing queue.");
                manipulationFile.WriteToFile("OnOutgoingJobChanged event fired. A fax is changed to the outgoing queue", this.faxQueueId);
                pFaxServer.Folders.OutgoingQueue.Refresh();
                PrintFaxStatus(pJobStatus, bstrJobId);
            }
            private void faxServer_OnOutgoingJobRemoved(FaxServer pFaxServer, string bstrJobId)
            {
                Console.WriteLine("OnOutgoingJobRemoved event fired. Fax job is removed to outbound queue.");
                manipulationFile.WriteToFile("OnOutgoingJobRemoved event fired. Fax job is removed to outbound queue.", this.faxQueueId);
                manipulationFile.WriteToFile("faxServer will Cancel.", this.faxQueueId);
                manipulationFile.WriteToFile("bstrJobId Is=" + bstrJobId, this.faxQueueId);
                CancelSentFax();

            }
            #endregion
            private void PrintFaxStatus(FaxJobStatus faxJobStatus, string bstrJobId)
            {

                Console.WriteLine("faxQueueId in PrintFaxStatus is =" + faxQueueId);
                manipulationFile.WriteToFile("Service Fax is check about status at " + DateTime.Now, this.faxQueueId);

                #region test cases for fax status
                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesDIALING)
                //{
                //    Console.WriteLine("Dialing...");
                //}//done

                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesBUSY)
                //{
                //    Console.WriteLine("The device encountered a busy signal." + Environment.NewLine);

                //}//done
                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesNO_ANSWER)
                //{

                //    Console.WriteLine("The receiving device did not answer the call." + Environment.NewLine);

                //}//done
                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesLINE_UNAVAILABLE)
                //{
                //    Console.WriteLine("The device is not available because it is in use by another application." + Environment.NewLine);


                //}
                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesHANDLED)
                //{
                //    Console.WriteLine("The fax service processed the outbound fax; the fax service provider will transmit the fax." + Environment.NewLine);

                //}
                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesCALL_DELAYED)
                //{
                //    Console.WriteLine("The device delayed a fax call because the sending device received a busy signal multiple times. The device cannot retry the call because dialing restrictions exist. (Some countries/regions restrict the number of retry attempts when a number is busy." + Environment.NewLine);

                //}
                //if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesTRANSMITTING)
                //{
                //    Console.WriteLine("Sending Fax...");
                //}//done
                //if (faxJobStatus.Status == FAX_JOB_STATUS_ENUM.fjsCOMPLETED && faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesCALL_COMPLETED)//done
                //{
                //    Console.WriteLine("Fax is sent successfully.");
                //}
                #endregion

                if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesDIALING)
                {
                    Console.WriteLine("Dialing...");
                    manipulationFile.WriteToFile("Dialing..." + DateTime.Now, this.faxQueueId);

                }

                if (faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesTRANSMITTING)
                {
                    Console.WriteLine("Sending Fax...");
                    manipulationFile.WriteToFile("Sending Fax..." + DateTime.Now, this.faxQueueId);

                }

                if (faxJobStatus.Status == FAX_JOB_STATUS_ENUM.fjsCOMPLETED && faxJobStatus.ExtendedStatusCode == FAX_JOB_EXTENDED_STATUS_ENUM.fjesCALL_COMPLETED)//done
                {
                    Console.WriteLine("Fax is sent successfully.");
                    manipulationFile.WriteToFile("Fax is sent successfully" + DateTime.Now, this.faxQueueId);

                    SuccessSentFax(this.faxQueueId);
                }

                if (faxJobStatus.ExtendedStatusCode != FAX_JOB_EXTENDED_STATUS_ENUM.fjesDIALING && faxJobStatus.ExtendedStatusCode != FAX_JOB_EXTENDED_STATUS_ENUM.fjesTRANSMITTING && faxJobStatus.Status != FAX_JOB_STATUS_ENUM.fjsCOMPLETED && faxJobStatus.ExtendedStatusCode != FAX_JOB_EXTENDED_STATUS_ENUM.fjesCALL_COMPLETED)
                {
                    Console.WriteLine("Fax is Can't sent ");
                    manipulationFile.WriteToFile("Fax is Can't sent" + DateTime.Now, this.faxQueueId);
                    Console.WriteLine("bstrJobId Is=" + bstrJobId);
                    CancelSentFax();
                }

            }
            public void SendFax(int faxQueueId, string DepName, string CompanyName, string CompanyRecivFaxNumber, string RecivName, string Subject, string DocumentName, string FilePath)
            {
                this.faxQueueId = faxQueueId;
                manipulationFile.WriteToFile("---------------------------------------------------", this.faxQueueId);
                manipulationFile.WriteToFile("Service  Send Fax is started at " + DateTime.Now, this.faxQueueId);
                Console.WriteLine("faxQueueId in SendFax is=" + this.faxQueueId);
                try
                {
                    bool FaxDocumentSetupCreated = FaxDocumentSetup(DepName, CompanyName, CompanyRecivFaxNumber, RecivName, Subject, DocumentName, FilePath);

                if (FaxDocumentSetupCreated)
                 {
                    manipulationFile.WriteToFile("FaxDocumentSetupCreated="+FaxDocumentSetupCreated+" "+ DateTime.Now, this.faxQueueId);
                    var myProcesses = Process.GetProcessesByName("AcroRd32");
                    manipulationFile.WriteToFile("before myProcess", this.faxQueueId);
                    foreach (var myProcess in myProcesses)
                    {
                        manipulationFile.WriteToFile("myProcess", this.faxQueueId);
                        if (DateTime.Now.Ticks - myProcess.StartTime.Ticks > TimeSpan.FromSeconds(30).Ticks)
                        {
                            myProcess.Kill();
                            manipulationFile.WriteToFile("myProcess.Kill()", this.faxQueueId);
                        }
                    }
                    var submitReturnValue = faxDoc.Submit(faxServer.ServerName);
                        faxDoc = null;
                    }
                else
                    {
                        faxDoc = null;
                        FalidSentFax(this.faxQueueId);
                        CancelSentFax();
                    }

                }
                catch (System.Runtime.InteropServices.COMException comException)
                {
                    ExceptionHandlerConstants.FaxException(comException, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    Console.WriteLine("Error connecting to fax server. Error Message: " + comException.Message);
                    Console.WriteLine("StackTrace: " + comException.StackTrace);
                    manipulationFile.WriteExceptionToFile(comException.Message + " " + DateTime.Now);
                    manipulationFile.WriteExceptionToFile(comException.StackTrace + " " + DateTime.Now);
                    faxDoc = null;
                    FalidSentFax(this.faxQueueId);
                    CancelSentFax();

                }
            }
            private bool FaxDocumentSetup(string DepName, string CompanyName, string CompanyRecivFaxNumber, string RecivName, string Subject, string DocumentName, string FilePath)
            {
                manipulationFile.WriteToFile("Service Fax is declare  FaxDocumentSetup  at " + DateTime.Now, this.faxQueueId);
                faxDoc = new FaxDocument();
                faxDoc.Priority = FAX_PRIORITY_TYPE_ENUM.fptHIGH;
                faxDoc.ReceiptType = FAX_RECEIPT_TYPE_ENUM.frtNONE;
                faxDoc.AttachFaxToReceipt = true;
                faxDoc.Sender.Name = DepName;
                faxDoc.Sender.Company = CompanyName;
                faxDoc.Body = FilePath;
                faxDoc.Subject = Subject;
                faxDoc.DocumentName = DocumentName;
                faxDoc.Recipients.Add(CompanyRecivFaxNumber, RecivName);

                if (CheckFileIsNotInUse(FilePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            private bool CheckFileIsNotInUse(string FilePath)
            {
                //Check File is not in use when try to attach and send        
                FileStream fs = null;
                try
                {
                    fs = File.OpenRead(FilePath);

                }
                catch (IOException ex)
                {
                    ExceptionHandlerConstants.FaxException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    Console.WriteLine("File is in use. Please release before access.");
                    manipulationFile.WriteToFile("File is in use can't found it. Please release before access. " + DateTime.Now, this.faxQueueId);
                    manipulationFile.WriteExceptionToFile(ex.Message + " " + DateTime.Now);
                    return false;

                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                        Console.WriteLine("File is released.");
                        manipulationFile.WriteToFile("File is released. " + DateTime.Now, this.faxQueueId);

                    }
                }
                return true;
            }
            public void SuccessSentFax(int tempFaxQueueId)
            {
                manipulationFile.WriteToFile("SuccessSentFax Start Edit At DataBase " + DateTime.Now, this.faxQueueId);
                manipulationFile.WriteToFile("tempFaxQueueId is  " + tempFaxQueueId, this.faxQueueId);
                Console.WriteLine("tempFaxQueueId is  " + tempFaxQueueId);
                try
                {
                    using (FaxApprovalEntities faxEntities = new FaxApprovalEntities())
                    {


                        var CurrentFaxSenderInQueue = faxEntities.FaxSenderQueues.Where(f => f.FaxQueueID == tempFaxQueueId).FirstOrDefault();
                        if (CurrentFaxSenderInQueue != null)
                        {
                            FaxSenderQueueDetail NewFaxSenderQueueDetail = new FaxSenderQueueDetail()
                            {
                                FaxQueueFK = tempFaxQueueId,
                                IsSent = true,
                                SuccessSendingTime = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreationDate = DateTime.Now,

                            };
                            faxEntities.FaxSenderQueueDetails.Add(NewFaxSenderQueueDetail);
                            CurrentFaxSenderInQueue.NumberOfTries += 1;
                            CurrentFaxSenderInQueue.IsSent = true;
                            CurrentFaxSenderInQueue.SuccessSendingTime = DateTime.Now;
                            faxEntities.SaveChanges();

                        }


                    }
                    manipulationFile.WriteToFile("SuccessSentFax End Edit At DataBase " + DateTime.Now, this.faxQueueId);
                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.FaxException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    manipulationFile.WriteExceptionToFile(ex.Message + " " + DateTime.Now);


                }

            }
            public void FalidSentFax(int tempFaxQueueId)
            {
                manipulationFile.WriteToFile("FalidSentFax Start Edit At DataBase " + DateTime.Now, this.faxQueueId);
                manipulationFile.WriteToFile("tempFaxQueueId is  " + tempFaxQueueId, this.faxQueueId);
                Console.WriteLine("tempFaxQueueId FalidSentFax is  " + tempFaxQueueId);
                try
                {
                    using (FaxApprovalEntities faxEntities = new FaxApprovalEntities())
                    {

                        var CurrentFaxSenderInQueue = faxEntities.FaxSenderQueues.Where(f => f.FaxQueueID == tempFaxQueueId).FirstOrDefault();
                        if (CurrentFaxSenderInQueue != null)
                        {
                            FaxSenderQueueDetail NewFaxSenderQueueDetail = new FaxSenderQueueDetail()
                            {
                                FaxQueueFK = tempFaxQueueId,
                                IsSent = false,
                                IsActive = true,
                                IsDeleted = false,
                                CreationDate = DateTime.Now,

                            };
                            faxEntities.FaxSenderQueueDetails.Add(NewFaxSenderQueueDetail);
                            CurrentFaxSenderInQueue.NumberOfTries += 1;
                            CurrentFaxSenderInQueue.IsSent = false;
                            faxEntities.SaveChanges();

                        }

                    }
                    manipulationFile.WriteToFile("FalidSentFax End Edit At DataBase " + DateTime.Now, this.faxQueueId);
                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.FaxException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    manipulationFile.WriteExceptionToFile(ex.Message + " " + DateTime.Now);


                }



            }
            //cancel sent fax and call function faild sent Fax
            public void CancelSentFax()
            {
                Console.WriteLine("faxQueueId in  CancelSentFax function is =" + faxQueueId);
                manipulationFile.WriteToFile("CancelSentFaxFunction start " + DateTime.Now, this.faxQueueId);
                try
                {
                    IFaxOutgoingJob outgoingJobs;
                    FaxOutgoingQueue outgoingQueues;
                    outgoingQueues = faxServer.Folders.OutgoingQueue;
                    outgoingQueues.Refresh();
                    FaxOutgoingJobs x = outgoingQueues.GetJobs();
                    manipulationFile.WriteToFile("---------------------------------------------", this.faxQueueId);
                    manipulationFile.WriteToFile("outgoingQueues count is " + x.Count, this.faxQueueId);
                    Console.WriteLine("outgoingQueues count is " + x.Count);
                    foreach (IFaxOutgoingJob tepx in x)
                    {
                        Console.WriteLine("tepx.Id = " + tepx.Id);
                        manipulationFile.WriteToFile("tepx.Id= " + tepx.Id + DateTime.Now, this.faxQueueId);
                        tepx.Cancel();
                        FalidSentFax(this.faxQueueId);
                    }
                    manipulationFile.WriteToFile("---------------------------------------------", this.faxQueueId);
                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.FaxException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "FaxSevices");
                    manipulationFile.WriteToFile("Exception is " + ex.Message + " " + DateTime.Now, this.faxQueueId);
                    Console.WriteLine("Exception is " + ex.Message);
                    manipulationFile.WriteExceptionToFile(ex.Message + " " + DateTime.Now);



                }

                this.FaxIsDone = true;

            }
        }
    #endregion


}



