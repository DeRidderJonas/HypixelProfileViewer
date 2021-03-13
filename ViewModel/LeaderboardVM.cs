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
    class LeaderboardVM : ViewModelBase
    {
        private IHypixelRepository _hypixelRepository = new HypixelRepositoryFile();

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

        public LeaderboardVM()
        {
            _selectedGameMode = GameModes[0];
            InitializeValues();
        }

        private async void InitializeValues()
        {
            CurrentLeaderboard = await _hypixelRepository.GetLeaderboardForGameMode(_selectedGameMode);
        }

        private async void UpdateLeaderboard()
        {
            CurrentLeaderboard = await _hypixelRepository.GetLeaderboardForGameMode(_selectedGameMode);
        }
    }
}
