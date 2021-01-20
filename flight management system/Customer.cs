using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    class Customer: IUser,IPoco
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Phone_No { get; set; }
        public string Credit_Card_No { get; set; }
        public int User_Id { get; set; }


        public User user;


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Customer()
        {

        }

        public Customer(int id, string first_Name, string last_Name, string address, string phone_No, string credit_Card_No, int user_Id, User user)
        {
            Id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Address = address;
            Phone_No = phone_No;
            Credit_Card_No = credit_Card_No;
            User_Id = user_Id;
            this.user = user;
        }

        public static bool operator ==(Customer  customer1, Customer  customer2)
        {


            if (object.ReferenceEquals(customer1, null) && object.ReferenceEquals(customer2, null))
                return true;
            if (object.ReferenceEquals(customer1, null) || object.ReferenceEquals(customer2, null))
                return false;

            return customer1.Id == customer2.Id;
        }

        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }

        public override bool Equals(object obj)
        {
            Customer customer = obj as Customer;
            if (customer != null)
                return this.Id == customer.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
