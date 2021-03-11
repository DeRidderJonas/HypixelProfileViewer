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

        public PlayerDetailVM()
        {
            InitializeValues();
        }

        private async void InitializeValues()
        {
            CurrentPlayer = await _hypixelRepository.GetPlayerInfoAsync();
        }
    }
}
