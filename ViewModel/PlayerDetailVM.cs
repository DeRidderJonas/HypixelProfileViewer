using GalaSoft.MvvmLight;
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
        private IHypixelRepository _hypixelRepository = new HypixelRepositoryFile();

        private Player _currentPlayer;

        public Player CurrentPlayer {
            get { return _currentPlayer; }
            set { _currentPlayer = value; RaisePropertyChanged("CurrentPlayer"); }
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
            CurrentPlayer = await _hypixelRepository.GetPlayerInfoAsync();
            GameModeStatistics = await _hypixelRepository.GetStatisticsForGameMode(_SelectedGameMode);
        }

        private async void UpdateGameModeStats()
        {
            GameModeStatistics = await _hypixelRepository.GetStatisticsForGameMode(_SelectedGameMode);
        }
    }
}
