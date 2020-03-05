using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;


namespace WebApplication1.BLL
{

    public class trans
    {
        private ArchiveDataContext _db = new ArchiveDataContext();
        public bool addRecord(string claimcode, string setid, string batch, string box, string providername, string addby)
        {

            Transaction tr = new Transaction();
            tr.Pre_AuthID = claimcode;
            tr.SetID = setid;
            tr.BatchID = batch;
            tr.BoxID = box;
            tr.Provider_Name = providername;
            tr.AddedBy = addby;
            tr.AddTime = DateTime.Now;
            _db.Transactions.InsertOnSubmit(tr);
            _db.SubmitChanges();
            return true;

        }

        public List<Transaction> GetAllTransactionsForEdit(int id)
        {
            List<Transaction> data = (from p in _db.Transactions
                                     where p.ID == id
                                     select p).ToList();
            return data;
        }
        public List<Transaction> GetAllSearchByPreAuth(string word)
        {
            List<Transaction> data = (from p in _db.Transactions
                                     where p.Pre_AuthID == word
                                     orderby p.Pre_AuthID ascending
                                     select p).ToList();
            return data;
        }
        public List<Transaction> GetAllSearchByProvider(string word)
        {
            List<Transaction> data = (from p in _db.Transactions
                                      where p.Provider_Name == word
                                      orderby p.Provider_Name ascending
                                      select p).ToList();
            return data;
        }
        public List<Transaction> GetAllSearchByBatch(string word)
        {
            List<Transaction> data = (from p in _db.Transactions
                                      where p.BatchID == word
                                      orderby p.BatchID ascending
                                      select p).ToList();
            return data;
        }
        public List<Transaction> GetAllSearchBySet(string word)
        {
            List<Transaction> data = (from p in _db.Transactions
                                      where p.SetID == word
                                      orderby p.SetID ascending
                                      select p).ToList();
            return data;
        }
        public List<Transaction> GetAllSearchByBox(string word)
        {
            List<Transaction> data = (from p in _db.Transactions
                                      where p.BoxID == word
                                      orderby p.BoxID ascending
                                      select p).ToList();
            return data;
        }
        public List<Transaction> GetAllSearchByAddedBy(string word)
        {
            List<Transaction> data = (from p in _db.Transactions
                                      where p.AddedBy == word
                                      orderby p.AddedBy ascending
                                      select p).ToList();
            return data;
        }
        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> data = (from p in _db.Transactions
                                      orderby p.ID ascending
                                      select p).ToList();
            return data;
        }

        public List<Transaction> GetAllBatchesByUser(string user)
        {
            List<Transaction> data = (from p in _db.Transactions
                                      where p.AddedBy == user
                                      orderby p.ID descending
                                      select p).ToList();
            return data;
        }

        public void DeleteTrans(int id)
        {
            var data = (from p in _db.Transactions
                        where p.ID == id
                        select p).ToList();
            _db.Transactions.DeleteAllOnSubmit(data);
            _db.SubmitChanges();
        }

        public void UpdateTran(int id, string pre, string setid, string batch, string box, string pname)
        {
            Transaction bt = _db.Transactions.Single(p => p.ID == id);
            bt.Pre_AuthID = pre;
            bt.SetID = setid;
            bt.BatchID = batch;
            bt.BoxID = box;
            bt.Provider_Name = pname;
            _db.SubmitChanges();
        }


        public string numofclaimsbybatchid(string batchid)
        {
            trans tr = new trans();

            var ClaimCount = (from p in _db.Transactions where p.BatchID == batchid && p.Pre_AuthID != null select p.Pre_AuthID).Count().ToString();


            return ClaimCount;


        }

        public string numofSetIDbybatchid(string batchid)
        {
            Outpatient tr = new Outpatient();

            var setCount = (from p in _db.Transactions where p.BatchID == batchid && p.SetID != null select p.SetID).Count().ToString();
            return setCount;


        }

        public List<Transaction> GetTransactionByClaimCode(List<string> claimCodeList)
        {
            IQueryable<Transaction> data = (from p in _db.Transactions
                                            select p);

            data = data.Where(t => claimCodeList.Contains(t.Pre_AuthID));

            return data.ToList();
        }

        public List<Transaction> GetTransactionBSetIDList(List<string> setIdList)
        {
            IQueryable<Transaction> data = (from p in _db.Transactions
                                            select p);

            data = data.Where(t => setIdList.Contains(t.SetID));
            return data.ToList();
        }



    }
}