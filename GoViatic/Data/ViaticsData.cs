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
                Icon="IconTest",
                ViaticName="Fuel",
                Category="Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 1,
                Icon = "IconTest",
                ViaticName = "Taxi/Uber",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 2,
                Icon = "IconTest",
                ViaticName = "Flight",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 3,
                Icon = "IconTest",
                ViaticName = "Tollbooth",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 4,
                Icon = "IconTest",
                ViaticName = "Bus",
                Category = "Transport"
            });

            Viatics.Add(new ViaticT
            {
                Id = 5,
                Icon = "IconTest",
                ViaticName = "Food",
                Category = "Food&Drinks"
            });

            Viatics.Add(new ViaticT
            {
                Id = 6,
                Icon = "IconTest",
                ViaticName = "Lodging",
                Category = "Hotels"
            });



        }

    }
}
