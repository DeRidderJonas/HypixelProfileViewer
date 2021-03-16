using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Project_DeRidderJonas_HypixelApi.Model;
using Project_DeRidderJonas_HypixelApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_DeRidderJonas_HypixelApi.ViewModel
{
    class LeaderboardVM : ViewModelBase
    {
        private IHypixelRepository _repo;

        public IHypixelRepository Repository {
            get { return _repo; }
            set { _repo = value; InitializeValues(); }
        }


        public List<GameMode> GameModes { get; } = GameModeRepository.GetGameModes();

        private GameMode _selectedGameMode;

        public GameMode SelectedGameMode {
            get { return _selectedGameMode; }
            set { _selectedGameMode = value; RaisePropertyChanged("SelectedGameMode"); UpdateLeaderboard(); }
        }

        private Leaderboard _currentLeaderboard;

        public Leaderboard CurrentLeaderboard {
            get { return _currentLeaderboard; }
            set { _currentLeaderboard = value; RaisePropertyChanged("CurrentLeaderboard"); }
        }

        public string UUID { get; set; }

        private string _errorMessage;

        public string ErrorMessage {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); RaisePropertyChanged("ErrorMessageVisible"); }
        }

        public Visibility ErrorMessageVisible { get { return string.IsNullOrEmpty(_errorMessage) ? Visibility.Hidden : Visibility.Visible; } }

        public LeaderboardVM()
        {
            _selectedGameMode = GameModes[0];
        }

        private void InitializeValues()
        {
            UpdateLeaderboard();
        }

        private async void UpdateLeaderboard()
        {
            try
            {
                CurrentLeaderboard = await _repo.GetLeaderboardForGameMode(_selectedGameMode);
            }
            catch (Exception e)
            {
                ErrorMessage = $"Something went wrong when gathering leaderboard data. \nError message for technicians: \n{e.Message}";
            }
        }


        private RelayCommand<string> _onPlayerSelectedCommand;

        public RelayCommand<string> OnPlayerSelectedCommand {
            get {
                if (_onPlayerSelectedCommand == null) _onPlayerSelectedCommand = new RelayCommand<string>(OnPlayerSelectionChanged);
                return _onPlayerSelectedCommand;
            }
        }

        private void OnPlayerSelectionChanged(string uuid)
        {
            UUID = uuid;
            (App.Current.MainWindow.DataContext as MainViewModel)?.SwitchPage();
        }
    }
}
