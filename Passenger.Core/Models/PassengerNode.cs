using System;

namespace Passenger.Core.Models
{
    public class PassengerNode
    {
        public Node Node { get; protected set; }
        public Passenger Passenger { get; protected set; }
    }
}