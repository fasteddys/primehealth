using CardCycle.DAL;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;

namespace CardCycle.BLL
{
    public class Client
    {
        DataContextDataContext db = new DataContextDataContext();

        public bool AddNewClient(string Name, string Notes, string ActiveCode , string type)
        {
            bool IsExists = db.ClientTBs.Any(pa => pa.ClientName.Equals(Name));
            if(IsExists)
            {
                return false;
            }
            else
            {
                ClientTB Ct = new ClientTB();
                Ct.ClientName = Name;
                Ct.ClientNotes = Notes;
                Ct.ClientStatus = ActiveCode;
                Ct.Type = type;
                db.ClientTBs.InsertOnSubmit(Ct);
                db.SubmitChanges();
                return true;
            }
        }
        public void UpdateClientNotes(int id, string notes, string Cname , string activecode , string clientype)
        {
            ClientTB acc = db.ClientTBs.Single(p => p.id == id);
            acc.ClientNotes = notes;
            acc.ClientName = Cname;
            acc.ClientStatus = activecode;
            acc.Type = clientype;
            db.SubmitChanges();
        }
        public void DeleteClient(int id)
        {
            ClientTB acc = db.ClientTBs.Single(p => p.id == id);
            db.ClientTBs.DeleteOnSubmit(acc);
            db.SubmitChanges();
        }
        public IEnumerable<ClientTB> GetAllClients()
        {
            var data = (from p in db.ClientTBs
                                 orderby p.id ascending
                                 select p).ToList();
            return data;
        }
        public IEnumerable<ClientTB> GetAllSearchByCName(string clientname)
        {
            var data = (from p in db.ClientTBs
                        where SqlMethods.Like(p.ClientName,clientname + "%")
                        orderby p.id ascending
                        select p).ToList();
            return data;
        }
        public void GetClientNotesByName(string name,ref string clientnotes)
        {
            var data = from p in db.ClientTBs
                       where p.ClientName == name
                       select p;
            clientnotes = data.First().ClientNotes;
        }
        public void GetClientNotesByID(int id, ref string clientnotes, ref string clientname, ref string clientstatus , ref string clienttype)
        {
            var data = from p in db.ClientTBs
                       where p.id == id
                       select p;

            clientnotes = data.First().ClientNotes;
            clientname = data.First().ClientName;
            clientstatus = data.First().ClientStatus;
            clienttype = data.First().Type;

        }
    }
}