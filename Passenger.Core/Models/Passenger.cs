using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Models
{
    public class Passenger
    {
        public  Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Node Address { get; protected set; }
    }
}
