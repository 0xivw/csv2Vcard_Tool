using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoc
{
    class VCF_File
    {
        String name_of_file;
        String name;
        int age;
        String mail;
        String phone_number;
        String ID;
        public VCF_File()
        {
            this.name_of_file = "";
            this.name = "";
            this.age = 0;
            this.mail = "";
            this.phone_number = "";
        }
        public VCF_File(String name_of_file, String name, int age, String mail, String phone_number)
        {
            this.name_of_file = name_of_file;
            this.name = name;
            this.age = age;
            this.mail = mail;
            this.phone_number = phone_number;
        }
        public String get_name_of_file()
        {
            return this.name_of_file;
        }
        public String get_name()
        {
            return this.name;
        }
        public String get_phone_number()
        {
            return this.phone_number;
        }
        public String get_email()
        {
            return this.mail;
        }
        public String get_ID()
        {
            return this.ID;
        }

        public int get_age()
        {
            return this.age;
        }

        public void set_name(String name)
        {
            this.name = name;
        }
        public void set_mail(String mail)
        {
            this.mail = mail;
        }
        public void set_ID(String name)
        {
            this.ID = name;
        }
        public void set_age(int age)
        {
            this.age = age;
        }

    }
}
