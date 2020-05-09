using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfosAboutNba.Wpf
{
    class MainVM : ViewModelBase
    {
        private MainLogic logic;
		private TeamVM selectedTeam;
		private ObservableCollection<TeamVM> allTeam;

		public ObservableCollection<TeamVM> AllTeam
		{	
			get { return allTeam; }
			set { Set(ref allTeam, value); }
		}


		public TeamVM SelectedTeam
		{
			get { return selectedTeam; }
			set { Set(ref selectedTeam, value); }
		}

		public ICommand AddCmd { get; private set; }
		public ICommand ModCmd { get; private set; }
		public ICommand DelCmd { get; private set; }
		public ICommand LoadCmd { get; private set; }

		public Func<TeamVM, bool> EditorFunc { get; set; }

		public MainVM()
		{
			logic = new MainLogic();

			DelCmd = new RelayCommand(() => logic.ApiDelTeam(selectedTeam));
			AddCmd = new RelayCommand(() => logic.EditTeam(null, EditorFunc));
			ModCmd = new RelayCommand(() => logic.EditTeam(selectedTeam, EditorFunc));
			LoadCmd = new RelayCommand(() => AllTeam = new ObservableCollection<TeamVM>(logic.ApiGetTeams()));
		}
	}
}
