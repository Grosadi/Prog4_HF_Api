using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfosAboutNba.Wpf
{
    class TeamVM : ObservableObject
    {
		private int id;
		private string name;
		private string homeTown;
		private int found;
		private double winPercentageInSeason;
		private double winPercentageSinceFounded;
		private int numberOfChampionships;

		public int NumberOfChampionships
		{
			get { return numberOfChampionships; }
			set { Set(ref numberOfChampionships, value); }
		}


		public double WinPercentageSinceFounded
		{
			get { return winPercentageSinceFounded; }
			set { Set(ref winPercentageSinceFounded,value); }
		}


		public double WinPercentageInSeason
		{
			get { return winPercentageInSeason; }
			set { Set(ref winPercentageInSeason, value); }
		}


		public int Found
		{
			get { return found; }
			set { Set(ref found, value); }
		}


		public string HomeTown
		{
			get { return homeTown; }
			set { Set(ref homeTown, value); }
		}


		public string Name
		{
			get { return name; }
			set { Set(ref name, value); }
		}


		public int ID
		{
			get { return id; }
			set { Set(ref id, value); }
		}

		public void CopyFrom(TeamVM other)
		{
			if (other == null) return;
			this.ID = other.id;
			this.Name = other.Name;
			this.HomeTown = other.HomeTown;
			this.Found = other.Found;
			this.WinPercentageInSeason = other.WinPercentageInSeason;
			this.WinPercentageSinceFounded = other.WinPercentageSinceFounded;
			this.NumberOfChampionships = other.NumberOfChampionships;
		}

	}
}
