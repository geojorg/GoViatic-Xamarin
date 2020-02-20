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
				Icon = "ic_email"
			});
			ViaticsList.Add(new ViaticType
			{
				Name = "Transport",
				Icon = "ic_email"
			});
			ViaticsList.Add(new ViaticType
			{
				Name = "Accommodation",
				Icon = "ic_email"
			});
			ViaticsList.Add(new ViaticType
			{
				Name = "Other",
				Icon = "ic_email"
			});

		}
	}
}
