using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class PlayerId
    {
        public string Name { get; set; }
        public string Uuid { get; set; }

        public string DisplayName { get {
                if (string.IsNullOrEmpty(Name)) return Uuid;
                return Name;
            }
        }
    }
}
