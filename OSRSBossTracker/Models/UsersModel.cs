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

        public List<string> Stats = new List<string>();

        public List<Bosses> Bosses = new List<Bosses>();

        public List<string> BossList = new List<string>() { "Abyssal Sire", "Alchemical Hydra", "Barrows Chests", "Bryophyta", "Chambers of Xeric", "Chambers of Xeric: Challenge Mode", "Chaos Elemental",
           "Chaos Fanatic", "Commander Zilyana", "Corporeal Beast", "Crazy Archaeologist", "Dagannoth Prime","Dagannoth Rex", "Dagannoth Supreme", "Deranged Archeologist", "General Graardor", "Giant Mole", 
            "Grotesque Guardians", "Hespori", "Kalphite Queen", "King Black Dragon", "Kraken", "Kree'Arra", "K'ril Tsutsaroth", "Mimic", "Obor",
            "Sarachnis", "Scorpia", "Skotizo", "The Gauntlet", "The Corrupted Gauntlet", "Theatre of Blood", "Thermonuclear Smoke Devil", "TzKal-Zuk", "TzTok-Jad", "Venenatis",
                                             "Vet'ion", "Vorkath", "Wintertodt", "Zalcano", "Zulrah"};
    }
}
public class Bosses
{
    public string Kc;
    public string Rank;
    public Bosses(string _rank, string _kc)
    {
        this.Kc = _kc;
        this.Rank = _rank;
    }
}