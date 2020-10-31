using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace OSRSBossTracker.Models
{
    public class UsersModel
    {
        public string Name { get; set; }

        public int LeagueScore { get; set; }

        public int TotalLevel { get; set; }

        //public List<string> Stats = new List<string>();

        public List<Player> Players = new List<Player>();

        //public List<string> BossList = new List<string>() { "Abyssal Sire", "Alchemical Hydra", "Barrows Chests", "Bryophyta", "Chambers of Xeric", "Chambers of Xeric: Challenge Mode", "Chaos Elemental",
        //   "Chaos Fanatic", "Commander Zilyana", "Corporeal Beast", "Crazy Archaeologist", "Dagannoth Prime","Dagannoth Rex", "Dagannoth Supreme", "Deranged Archeologist", "General Graardor", "Giant Mole", 
        //    "Grotesque Guardians", "Hespori", "Kalphite Queen", "King Black Dragon", "Kraken", "Kree'Arra", "K'ril Tsutsaroth", "Mimic", "Obor",
        //    "Sarachnis", "Scorpia", "Skotizo", "The Gauntlet", "The Corrupted Gauntlet", "Theatre of Blood", "Thermonuclear Smoke Devil", "TzKal-Zuk", "TzTok-Jad", "Venenatis",
        //                                     "Vet'ion", "Vorkath", "Wintertodt", "Zalcano", "Zulrah"};
    }
}
public class Player
{
    public string Name;
    public int LeaguePoints;
    public int TotalLevel;
    public Player (string _name, int _totalLevel, int _leaguePoints)
    {
        this.Name = _name;
        this.TotalLevel = _totalLevel;
        this.LeaguePoints = _leaguePoints;
    }
}