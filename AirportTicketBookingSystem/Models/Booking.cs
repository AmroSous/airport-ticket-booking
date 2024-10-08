﻿using Newtonsoft.Json;

namespace AirportTicketBookingSystem.Models
{
    public class Booking
    {
        [JsonConstructor]
        public Booking(string id, Passenger passenger, Flight flight, TravelClassEnum travelClassEnum, DateTime bookingDate)
        {
            Passenger = passenger;
            Flight = flight;
            TravelClassEnum = travelClassEnum;
            Id = id;
            BookingDate = bookingDate;
        }

        public string Id { get; init; }

        public Passenger Passenger { get; init; }

        public Flight Flight { get; init; }

        public TravelClassEnum TravelClassEnum { get; init; }

        public DateTime BookingDate { get; init; }

        public override string ToString()
        {
            return $"""
                Book ID: {Id}
                Flight #{Flight.Id}: {Flight.DepartureCountry} ──> {Flight.DestinationCountry} @ {Flight.DepartureDate}
                Booking Date: {BookingDate}
                Seat: {TravelClassEnum}
                """;
        }
    }
}
