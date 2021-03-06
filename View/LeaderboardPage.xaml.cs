using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_DeRidderJonas_HypixelApi.View
{
    /// <summary>
    /// Interaction logic for LeaderboardPage.xaml
    /// </summary>
    public partial class LeaderboardPage : Page
    {
        public LeaderboardPage()
        {
            InitializeComponent();
        }

        private void ScrollToTop(object sender, SelectionChangedEventArgs e)
        {
            if (lstPlayer == null || lstPlayer.Items.Count == 0) return;

            var enumerator = lstPlayer.ItemsSource.GetEnumerator();
            enumerator.MoveNext();
            lstPlayer.ScrollIntoView(enumerator.Current);
        }
    }
}
