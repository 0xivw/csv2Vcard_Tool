using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoc
{
    class vcard
    {
        String name_of_file;
        String full_name;
        String nick_name;
        String birthday;
        String gender;
        String home_address;
        String job_title;
        String mail;
        String phone_number;
        public vcard()
        {
            this.Name_of_file = "";
            this.Full_name = "";
            this.Birthday = "";
            this.Gender = "";
            this.Home_address = "";
            this.Job_title = "";
            this.Mail = "";
            this.Phone_number = "";
        }

        public string Name_of_file { get => name_of_file; set => name_of_file = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public string Nick_name { get => nick_name; set => nick_name = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Home_address { get => home_address; set => home_address = value; }
        public string Job_title { get => job_title; set => job_title = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Phone_number { get => phone_number; set => phone_number = value; }

        
    }
}
