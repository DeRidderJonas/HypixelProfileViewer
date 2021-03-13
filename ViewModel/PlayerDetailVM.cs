using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Project_DeRidderJonas_HypixelApi.Model;
using Project_DeRidderJonas_HypixelApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.ViewModel
{
    class PlayerDetailVM : ViewModelBase
    {
        private IHypixelRepository _hypixelRepository = new HypixelRepositoryWeb();

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


        public PlayerDetailVM()
        {
            _SelectedGameMode = GameModes[0];
            InitializeValues();
        }

        private async void InitializeValues()
        {
            CurrentPlayer = await _hypixelRepository.GetPlayerInfoAsync(_uuid);
            GameModeStatistics = await _hypixelRepository.GetStatisticsForGameMode(_SelectedGameMode);
        }

        private async void UpdatePlayer()
        {
            CurrentPlayer = await _hypixelRepository.GetPlayerInfoAsync(_uuid);
        }

        private async void UpdateGameModeStats()
        {
            GameModeStatistics = await _hypixelRepository.GetStatisticsForGameMode(_SelectedGameMode);
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
