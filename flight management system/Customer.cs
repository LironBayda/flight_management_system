using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
   public class Customer: IUser,IPoco
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNo { get; set; }
        public int UserId { get; set; }


        public User UserObj { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Customer()
        {

        }

        public Customer(int id, string firstName, string lastName, string address, string phoneNo, string creditCardNo, int userId, User userObj)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNo = phoneNo;
            CreditCardNo = creditCardNo;
            UserId = userId;
            UserObj = userObj;
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
