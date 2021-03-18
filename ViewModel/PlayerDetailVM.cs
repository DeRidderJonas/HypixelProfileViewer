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
    class PlayerDetailVM : ViewModelBase
    {
        private IHypixelRepository _repo;

        public IHypixelRepository Repository {
            get { return _repo; }
            set { _repo = value; InitializeValues(); }
        }


        private string _uuid = "746b0410b2e347e7ad05a5473d35c097";

        public string UUID {
            get { return _uuid; }
            set { _uuid = value; RaisePropertyChanged("UUID"); UpdatePlayer(); }
        }

        private Player _currentPlayer;

        public Player CurrentPlayer {
            get { return _currentPlayer; }
            set { _currentPlayer = value; RaisePropertyChanged("CurrentPlayer"); UpdateGameModeStats(); }
        }

        public List<GameMode> GameModes { get; } = GameModeRepository.GetGameModes();

        private GameMode _SelectedGameMode;

        public GameMode SelectedGameMode {
            get { return _SelectedGameMode; }
            set { _SelectedGameMode = value; RaisePropertyChanged("SelectedGameMode"); UpdateGameModeStats(); }
        }

        private IGameModeStatistics _gameModeStats;

        public IGameModeStatistics GameModeStatistics {
            get { return _gameModeStats; }
            set { _gameModeStats = value; RaisePropertyChanged("GameModeStatistics"); }
        }

        private string _errorMessage;

        public string ErrorMessage {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); RaisePropertyChanged("ErrorMessageVisible"); RaisePropertyChanged("StatsVisibility"); }
        }

        public Visibility ErrorMessageVisible { get { return string.IsNullOrEmpty(_errorMessage) ? Visibility.Hidden : Visibility.Visible; } }
        public Visibility StatsVisibility { get { return string.IsNullOrEmpty(_errorMessage) ? Visibility.Visible : Visibility.Hidden; } }

        public PlayerDetailVM()
        {
            _SelectedGameMode = GameModes[0];
        }

        private void InitializeValues()
        {
            UpdatePlayer();
        }

        private async void UpdatePlayer()
        {
            ErrorMessage = "";
            try
            {
                CurrentPlayer = new Player() { DisplayName = "Loading" };
                GameModeStatistics = new HungerGamesStatistics();
                CurrentPlayer = await _repo.GetPlayerInfoAsync(_uuid);
                UpdateGameModeStats();
            }
            catch (Exception e)
            {
                CurrentPlayer = new Player();
                GameModeStatistics = new HungerGamesStatistics();
                ErrorMessage = $"Something went wrong when reading player data\nError message for technicians:\n{e.Message}";
            }
        }

        private async void UpdateGameModeStats()
        {
            try
            {
                GameModeStatistics = await _repo.GetStatisticsForGameMode(_SelectedGameMode);
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(_errorMessage)) return;
                GameModeStatistics = new HungerGamesStatistics();
                ErrorMessage = $"Something went wrong when reading game mode statistics\nError message for technicians:\n{e.Message}";
            }
        }

        private RelayCommand _backCommand;

        public RelayCommand BackCommand {
            get {
                if (_backCommand == null) _backCommand = new RelayCommand(GoBack);
                return _backCommand;
            }
        }

        private void GoBack()
        {
            (App.Current.MainWindow.DataContext as MainViewModel)?.SwitchPage();
        }
    }
}
