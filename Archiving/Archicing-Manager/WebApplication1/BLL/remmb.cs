using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;

namespace WebApplication1.BLL
{

    public class remmb
    {
        private ArchiveDataContext _db = new ArchiveDataContext();
        public bool addRecord(string batch, string box, string addby, string com)
        {
            remb tr = new remb();
            tr.BatchID = batch;
            tr.BoxID = box;
            tr.AddedBy = addby;
            tr.Comment = com;
            tr.AddTime = DateTime.Now;
            _db.rembs.InsertOnSubmit(tr);
            _db.SubmitChanges();
            return true;
        }

        public List<remb> GetAllTransactions()
        {
            List<remb> data = (from p in _db.rembs
                               orderby p.ID ascending
                               select p).ToList();
            return data;
        }
        public List<remb> GetAllTransactionsForEdit(int id)
        {
            List<remb> data = (from p in _db.rembs
                                     where p.ID == id
                                     select p).ToList();
            return data;
        }

        public List<remb> GetAllBatchesByUser(string user)
        {
            List<remb> data = (from p in _db.rembs
                               where p.AddedBy == user
                               orderby p.ID descending
                               select p).ToList();
            return data;
        }

        public void DeleteTrans(int id)
        {
            var data = (from p in _db.rembs
                        where p.ID == id
                        select p).ToList();
            _db.rembs.DeleteAllOnSubmit(data);
            _db.SubmitChanges();
        }
        public List<remb> GetAllSearchByBatchNumber(string word)
        {
            List<remb> data = (from p in _db.rembs
                                     where p.BatchID == word
                               orderby p.BatchID ascending
                                     select p).ToList();
            return data;
        }
        public List<remb> GetAllSearchByBoxNumber(string word)
        {
            List<remb> data = (from p in _db.rembs
                               where p.BoxID == word
                               orderby p.BoxID ascending
                               select p).ToList();
            return data;
        }
        public List<remb> GetAllSearchByComment(string word)
        {
            List<remb> data = (from p in _db.rembs
                               where p.Comment == word
                               orderby p.Comment ascending
                               select p).ToList();
            return data;
        }
        public List<remb> GetAllSearchByAddedBy(string word)
        {
            List<remb> data = (from p in _db.rembs
                               where p.AddedBy == word
                               orderby p.AddedBy ascending
                               select p).ToList();
            return data;
        }

        //Date
        public List<remb> GetAllSearchByDate(string word)
        {
            var date = Convert.ToDateTime(word);
            List<remb> data = (from p in _db.rembs
                               where p.AddTime == date
                               orderby p.AddTime ascending
                               select p).ToList();
            return data;
        }

        public void UpdateTran(int id, string batch, string box, string com)
        {
            remb bt = _db.rembs.Single(p => p.ID == id);
            bt.BatchID = batch;
            bt.BoxID = box;
            bt.Comment = com;
            _db.SubmitChanges();
        }

    }
}