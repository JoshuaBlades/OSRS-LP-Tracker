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

        public List<Bosses> bosses = new List<Bosses>();
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