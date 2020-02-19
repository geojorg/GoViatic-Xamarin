using System.Collections.Generic;

namespace GoViatic.Models
{
	public static class ViaticTypeData
	{
		public static IList<ViaticType> ViaticsList { get; private set; }

		static ViaticTypeData()
		{
			ViaticsList = new List<ViaticType>();

			ViaticsList.Add(new ViaticType
			{
				Name = "Food",
				Icon = ""
			});
			ViaticsList.Add(new ViaticType
			{
				Name = "Transport",
				Icon = ""
			});
			ViaticsList.Add(new ViaticType
			{
				Name = "Accommodation",
				Icon = ""
			});
			ViaticsList.Add(new ViaticType
			{
				Name = "Other",
				Icon = ""
			});

		}
	}
}
