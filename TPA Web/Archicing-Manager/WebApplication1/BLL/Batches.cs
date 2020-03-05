using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;

namespace WebApplication1.BLL
{
    public class Batches
    {
        TPASystemDataContext _db = new TPASystemDataContext();

        public bool addRecord(string patch, string box , string name)
        {
            bool IsExists = _db.batchTBs.Any(pa => pa.batch.Equals(patch));
            if (IsExists)
            {
                return false;
            }
            else
            {
                batchTB p = new batchTB();
                p.batch = patch;
                p.box = box;
                p.AddTime = DateTime.Now.ToString();
                p.AddedBy = name;
                _db.batchTBs.InsertOnSubmit(p);
                _db.SubmitChanges();

                return true;
            }
        }

        public void NumOFClaimIncrement(string batch)
        {
            batchTB p = _db.batchTBs.Single(pa => pa.batch == batch);
            if (p.NumOFClaims == null)
            {
                p.NumOFClaims = 0;
                p.NumOFClaims = p.NumOFClaims + 1;
                _db.SubmitChanges();
            }
            else
            {
                p.NumOFClaims = p.NumOFClaims + 1;
                _db.SubmitChanges();
            }
        }
        public List<batchTB> GetAllBatches()
        {
            List<batchTB> data = (from p in _db.batchTBs
                                  orderby p.id ascending
                                  select p).ToList();
            return data;
        }

        public List<batchTB> GetAllBatchesByUser(string user)
        {
            List<batchTB> data = (from p in _db.batchTBs
                                  where p.AddedBy == user
                                  orderby p.id ascending
                                  select p).ToList();
            return data;
        }
        public void DeleteBatch(int id)
        {
            batchTB bt = _db.batchTBs.Single(p => p.id == id);
            _db.batchTBs.DeleteOnSubmit(bt);
            var data = (from p in _db.ClaimTBs
                        where p.BatchNum == bt.batch
                        select p).ToList();
            _db.ClaimTBs.DeleteAllOnSubmit(data);
            _db.SubmitChanges();
        }
        public void UpdateBatch(int id , string batch , string box)
        {
            batchTB bt = _db.batchTBs.Single(p => p.id == id);
            bt.batch = batch;
            bt.box = box;
            _db.SubmitChanges();
        }
         ////////////// Mioustafa Updates /////////////////////////
        public void Updatereq(string name, string month, string year)
        {
           // batchTB bt = _db.batchTBs.Single(p => p.id == id);
            Provider pro = _db.Providers.Single(p => p.PName == name && p.PMonth == month && p.PYear == year);
           // bt.batch = batch;
           // bt.box = box;
            _db.SubmitChanges();
        } 
        
        
        
        
        
        
        //////////////////////////////////////////////////////
       




        public IEnumerable<ClaimTB> GetAllClaimsByBatch(int id)
        {
            var data = (from o in _db.ClaimTBs
                        where o.BatchNum == id.ToString()
                        orderby o.BatchNum ascending
                        select o).ToList();
            return data;
        }

        public bool AddClaim(string claim,string batch,string user)
        {
            bool IsExists = _db.ClaimTBs.Any(pa => pa.ClaimNum.Equals(claim) && pa.BatchNum == batch);
            if (IsExists)
            {
                return false;
            }
            else
            {
                ClaimTB ctb = new ClaimTB();
                ctb.ClaimNum = claim;
                ctb.BatchNum = batch;
                ctb.AddedBy = user;
                ctb.cDate = DateTime.Now.ToString();
                _db.ClaimTBs.InsertOnSubmit(ctb);
                _db.SubmitChanges();

                return true;
            }
        }

        public IEnumerable<ClaimTB> GetAllClaimsByUser(string user)
        {
            var data = (from o in _db.ClaimTBs
                        where o.AddedBy == user
                        orderby o.BatchNum ascending
                        select o).ToList();
            return data;
        }
        public IEnumerable<ClaimTB> GetAllClaims()
        {
            var data = (from o in _db.ClaimTBs
                        orderby o.BatchNum ascending
                        select o).ToList();
            return data;
        }
        public void DeleteClaim(int id)
        {
            ClaimTB bt = _db.ClaimTBs.Single(p => p.id == id);
            _db.ClaimTBs.DeleteOnSubmit(bt);
            string batchnum = bt.BatchNum;
            batchTB bt1 = _db.batchTBs.Single(p => p.batch.Equals(batchnum));
            bt1.NumOFClaims = bt1.NumOFClaims - 1;
            _db.SubmitChanges();
        }
        public void UpdateClaim(int id, string batch, string claim)
        {
            bool IsExists = _db.ClaimTBs.Any(pa => pa.id.Equals(id) && pa.BatchNum.Equals(batch));
            if (IsExists)
            {
                ClaimTB bt = _db.ClaimTBs.Single(p => p.id == id);
                bt.ClaimNum = claim;
                _db.SubmitChanges();
            }
            else
            {
                ClaimTB bt = _db.ClaimTBs.Single(p => p.id == id);
                bt.ClaimNum = claim;
                string oldbatch = bt.BatchNum;
                bt.BatchNum = batch;

                batchTB bt1 = _db.batchTBs.Single(p => p.batch.Equals(oldbatch));
                bt1.NumOFClaims = bt1.NumOFClaims - 1;

                batchTB newbatch = _db.batchTBs.Single(p => p.batch.Equals(batch));
                newbatch.NumOFClaims = bt1.NumOFClaims + 1;
                _db.SubmitChanges();
            }
        }
    }
}