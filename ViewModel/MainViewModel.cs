using GalaSoft.MvvmLight;
using Project_DeRidderJonas_HypixelApi.Repository;
using Project_DeRidderJonas_HypixelApi.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_DeRidderJonas_HypixelApi.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        //private IHypixelRepository _repo = new HypixelRepositoryFile();
        private IHypixelRepository _repo = new HypixelRepositoryWeb();
        public LeaderboardPage LeaderboardPage { get; } = new LeaderboardPage();
        public PlayerDetailPage PlayerDetailPage { get; } = new PlayerDetailPage();

        private Page _currentPage;

        public Page CurrentPage {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChanged("CurrentPage"); }
        }

        public MainViewModel()
        {
            (PlayerDetailPage.DataContext as PlayerDetailVM).Repository = _repo;
            (LeaderboardPage.DataContext as LeaderboardVM).Repository = _repo;
            
            CurrentPage = LeaderboardPage;
        }

        public void SwitchPage()
        {
            if (CurrentPage is LeaderboardPage)
            {
                string selectedUUID = (LeaderboardPage.DataContext as LeaderboardVM).UUID;
                if (selectedUUID == null) return;

                (PlayerDetailPage.DataContext as PlayerDetailVM).UUID = selectedUUID;
                CurrentPage = PlayerDetailPage;
            }
            else
            {
                CurrentPage = LeaderboardPage;
            }
        }
    }
}
