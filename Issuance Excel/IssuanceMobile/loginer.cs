using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuanceMobile
{
    class loginer
    {
        public string Medical_id {get; set;}
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string gender { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public string BranchName { get; set; }
        public string Activated { get; set; }
       
        public loginer (string Medical_id ,string Password ,string Name ,string Email ,string gender ,string Type ,string ClientName ,string BranchName ,string Activated )
        {
            this.Medical_id = Medical_id;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
            this.gender = gender;
            this.Type = Type;
            this.ClientName = ClientName;
            this.BranchName = BranchName;
            this.Activated = Activated;
           
        }

        public loginer()
        {
            this.Medical_id = Medical_id;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
            this.gender = gender;
            this.Type = Type;
            this.ClientName = ClientName;
            this.BranchName = BranchName;
            this.Activated = Activated;
            
        }
    }
}
