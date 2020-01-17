using GoViatic.Models;
using System.Collections.Generic;

namespace GoViatic.Data
{
    public class ViaticsData
    {
        public static IList<ViaticT> Viatics { get; private set; }

        static ViaticsData()
        {
            Viatics = new List<ViaticT>();

            Viatics.Add(new ViaticT
            {
                Id= 0,
                Icon="Fuel",
                ViaticName="Fuel",
                Category="Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 1,
                Icon = "Taxi",
                ViaticName = "Taxi/Uber",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 2,
                Icon = "Flight",
                ViaticName = "Flight",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 3,
                Icon = "Tollbooth",
                ViaticName = "Tollbooth",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 4,
                Icon = "Bus",
                ViaticName = "Bus",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 5,
                Icon = "Train",
                ViaticName = "Train",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 6,
                Icon = "Food",
                ViaticName = "Food",
                Category = "Food&Drinks"
            });

            Viatics.Add(new ViaticT
            {
                Id = 7,
                Icon = "Hotel",
                ViaticName = "Lodging",
                Category = "Hotels"
            });

            Viatics.Add(new ViaticT
            {
                Id = 8,
                Icon = "Shops",
                ViaticName = "Shopping",
                Category = "Other"
            });

            Viatics.Add(new ViaticT
            {
                Id = 9,
                Icon = "Boat",
                ViaticName = "Boat",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 10,
                Icon = "Other",
                ViaticName = "Other",
                Category = "Other"
            });
        }

    }
}
