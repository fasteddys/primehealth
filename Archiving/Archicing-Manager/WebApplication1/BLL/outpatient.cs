using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;

namespace WebApplication1.BLL
{
    public class outpatient
    {
        private ArchiveDataContext _db = new ArchiveDataContext();
        public bool addRecord(string claimcode, string setid, string batch, string box, string addby)
        {

            Outpatient tr = new Outpatient();
            tr.ClaimCode = claimcode;
            tr.SetID = setid;
            tr.BatchID = batch;
            tr.BoxID = box;
            tr.AddedBy = addby;
            tr.AddTime = DateTime.Now;
            _db.Outpatients.InsertOnSubmit(tr);
            _db.SubmitChanges();
            return true;

        }
        public List<Outpatient> GetAllSearchByClaimCode(string word)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                    where p.ClaimCode == word
                                    orderby p.ClaimCode ascending
                                    select p).ToList();
            return data;
        }

        public List<Outpatient> GetAllSearchBySetID(string word)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     where p.SetID == word
                                     orderby p.SetID ascending
                                     select p).ToList();
            return data;
        }

        public List<Outpatient> GetAllSearchByBatch(string word)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     where p.BatchID == word
                                     orderby p.BatchID ascending
                                     select p).ToList();
            return data;
        }

        public List<Outpatient> GetAllSearchByBox(string word)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     where p.BoxID == word
                                     orderby p.BoxID ascending
                                     select p).ToList();
            return data;
        }

        public List<Outpatient> GetAllSearchByAddedBy(string word)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     where p.AddedBy == word
                                     orderby p.AddedBy ascending
                                     select p).ToList();
            return data;
        }


        public List<Outpatient> GetAllTransactions()
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     orderby p.ID ascending
                                     select p).ToList();
            return data;
        }
        public List<Outpatient> GetAllTransactionsForEdit(int id)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     where p.ID == id
                                     select p).ToList();
            return data;
        }

        public List<Outpatient> GetAllTransactionsByUser(string user)
        {
            List<Outpatient> data = (from p in _db.Outpatients
                                     where p.AddedBy == user
                                     orderby p.ID descending
                                     select p).ToList();
            return data;
        }

        public void DeleteTrans(int id)
        {
            var data = (from p in _db.Outpatients
                        where p.ID == id
                        select p).ToList();
            _db.Outpatients.DeleteAllOnSubmit(data);
            _db.SubmitChanges();
        }

        public void UpdateTran(int id, string claimcode, string setid, string batch, string box, string numofclamis)
        {
            Outpatient bt = _db.Outpatients.Single(p => p.ID == id);
            bt.ClaimCode = claimcode;
            bt.SetID = setid;
            bt.BatchID = batch;
            bt.BoxID = box;
            bt.NumOfClaims = numofclamis;
            _db.SubmitChanges();
        }

        public string numofclaimsbybatchid(string batchid)
        {
            Outpatient tr = new Outpatient();

            var ClaimCount = (from p in _db.Outpatients where p.BatchID == batchid && p.ClaimCode != null select p.ClaimCode).Count().ToString();


            return ClaimCount;


        }

        public string numofSetIDbybatchid(string batchid)
        {
            Outpatient tr = new Outpatient();

            var setCount = (from p in _db.Outpatients where p.BatchID == batchid && p.SetID != null select p.SetID).Count().ToString();
            return setCount;


        }

        public List<Outpatient> GetTransactionByClaimCode(List<string> claimCodeList)
        {
            IQueryable<Outpatient> data = (from p in _db.Outpatients
                                           select p);
            data = data.Where(t => claimCodeList.Contains(t.ClaimCode));
            return data.ToList();
        }

        public List<Outpatient> GetTransactionBSetID(List<string> setIdList)
        {
            IQueryable<Outpatient> data = (from p in _db.Outpatients
                                           select p);
            data = data.Where(t => setIdList.Contains(t.SetID));
            return data.ToList();
        }

    }
}