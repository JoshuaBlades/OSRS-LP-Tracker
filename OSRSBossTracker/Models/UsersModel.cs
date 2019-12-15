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
    }
}