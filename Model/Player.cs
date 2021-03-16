using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class Player
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName ="displayname")]
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName ="playername")]
        public string PlayerName { get; set; }
        [JsonProperty(PropertyName ="karma")]
        public int Karma { get; set; }
        [JsonProperty(PropertyName = "achievementPoints")]
        public int AchievementPoints { get; set; }
        [JsonIgnore]
        public bool IsOnline { get; set; }

        //Login Data
        [JsonProperty(PropertyName = "firstLogin")]
        public long FirstLogin { get; set; }
        [JsonIgnore]
        public DateTime FirstLoginDT { get {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(FirstLogin);
                return dateTimeOffset.Date;
            } 
        }

        [JsonProperty(PropertyName = "lastLogin")]
        public long LastLogin { get; set; }
        public DateTime LastLoginDT { get {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(LastLogin);
                return dateTimeOffset.Date;
            } 
        }

        [JsonProperty(PropertyName = "timePlaying")]
        public int TimePlayed { get; set; }
        [JsonIgnore]
        public TimeSpan TimePlayedTS { get {
                return new TimeSpan(0, TimePlayed, 0);
            } 
        }
        //Experience
        [JsonProperty(PropertyName = "networkExp")]
        public double NetworkExperience { get; set; }

        [JsonIgnore]
        public int CurrentNetworkLevel { get {
                //https://hypixel.net/threads/level-xp-chart.1042526/
                //Xp/level needed = (level-2) * 2500 + 10000
                if (NetworkExperience < 10000) return 1;
                if (NetworkExperience < 12000) return 2;
                int level = 3;
                double remainingExperience = NetworkExperience;
                double experienceNeededToNextLevel = (level - 2) * 2500 + 10000;
                while(remainingExperience > experienceNeededToNextLevel)
                {
                    remainingExperience -= experienceNeededToNextLevel;
                    ++level;
                    experienceNeededToNextLevel = (level - 2) * 2500 + 10000;
                }
                return level-1;
            }
        }

        [JsonIgnore]
        public double NetworkLevelExperience { get {
                double currExperience = 10000;
                int currentNetworkLevel = CurrentNetworkLevel;
                for (int i = 3; i < currentNetworkLevel+1; i++)
                {
                    currExperience += (i - 2) * 2500 + 10000;
                }
                return currExperience;
            } 
        }

        [JsonIgnore]
        public int NextNetworkLevel { get {
                return CurrentNetworkLevel + 1;
            } 
        }

        [JsonIgnore]
        public double NextNetworkLevelExperience { get {
                return NetworkLevelExperience + (NextNetworkLevel - 2) * 2500 + 10000;
            } 
        }

        [JsonIgnore]
        public float LevelExperiencePercentage { get {
                double nextLevelNeeded = (NextNetworkLevel - 2) * 2500 + 10000;
                double expToNextLevel = NetworkExperience - NetworkLevelExperience;

                return (float)(expToNextLevel / nextLevelNeeded);
            } 
        }


        [JsonIgnore]
        public BitmapImage Face { 
            get {
                return new BitmapImage(new Uri($"https://cravatar.eu/avatar/{PlayerName}"));      
            } 
        }
    }
}
