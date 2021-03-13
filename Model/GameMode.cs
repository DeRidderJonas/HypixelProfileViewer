using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class GameMode
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ShortName { get; set; }

        public BitmapImage Image { get {
                return new BitmapImage(new Uri($"pack://application:,,,/Resources/Images/{Name}.png"));    
            } 
        }

        public Type Type {
            get {
                return Type.GetType($"Project_DeRidderJonas_HypixelApi.Model.{Name}Statistics");
            }
        }
    }
}
