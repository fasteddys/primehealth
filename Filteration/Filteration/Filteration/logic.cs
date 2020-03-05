using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Filteration.Models;
using System.Web.Routing;
using System.Collections.Specialized;

namespace Filteration
{
    public static class logic
    {
        public static void insertbatch(Request request, long[] batches, int id, int ticketnumber)
        {
            TpaManagerEntities db = new TpaManagerEntities();
            Batch batch = new Batch();

            for (int i = 0; i < batches.Length; i++)
            {

                batch.BatchNumber = batches[i].ToString();
                batch.ReqId = id;
                batch.BatchStatus = "NEW";
                batch.TicketNumber = ticketnumber;
                db.Batches.Add(batch);
                db.SaveChanges();
            }
        }
        public static string ViewOrHide(Batch batch, int? id)
        {
            string acess = "";
            TpaManagerEntities db = new TpaManagerEntities();
            var data = (from item in db.Batches


                        where item.ReqId == id
                        select item).ToList();
            bool check = data.All(x => x.BatchStatus == "ReadyToQuality");
            if (check)
            {
                acess = "true";

            }

            else
            {
                acess = "false";
            }
            return acess;

        }

        public static string ViewOrHide2(Batch batch, int? id)
        {
            string acess = "";
            TpaManagerEntities db = new TpaManagerEntities();
            var data = (from item in db.Batches


                        where item.ReqId == id
                        select item).ToList();
            bool check = data.All(x => x.BatchStatus == "UnderReview");
            if (check)
            {
                acess = "true";

            }

            else
            {
                acess = "false";
            }
            return acess;

        }
        
       

       
        
    }
}