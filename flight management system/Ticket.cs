using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class Ticket: IPoco
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int CustomerId { get; set; }

        public Flight FlightObj { get; set; }
        public Customer CustomerObj  { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Ticket()
        {

        }

        public Ticket(int id, int flightId, int customerId, Flight flightObj, Customer customerObj)
        {
            Id = id;
            FlightId = flightId;
            CustomerId = customerId;
            FlightObj = flightObj;
            CustomerObj = customerObj;
        }

        public static bool operator ==(Ticket ticket1, Ticket ticket2)
        {


            if (object.ReferenceEquals(ticket1, null) && object.ReferenceEquals(ticket2, null))
                return true;
            if (object.ReferenceEquals(ticket1, null) || object.ReferenceEquals(ticket2, null))
                return false;

            return ticket1.Id == ticket2.Id;
        }

        public static bool operator !=(Ticket ticket1, Ticket ticket2)
        {
            return !(ticket1 == ticket2);
        }

        public override bool Equals(object obj)
        {
            Ticket ticket = obj as Ticket;
            if (ticket != null)
                return this.Id == ticket.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
