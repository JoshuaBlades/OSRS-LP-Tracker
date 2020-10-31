using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using OSRSBossTracker.Models;
using System.Threading;

namespace OSRSBossTracker.Controllers
{
    public class StatsController : Controller
    {
        // Create instance of the database context for access
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stats/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stats/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stats/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stats/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Gets user stats from OSRS API
        /// </summary>
        /// <param name="ign"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetStats(string ign)
        {
            // Create a new instance of the model
            Models.UsersModel User = new Models.UsersModel();

            // Create dynamic API URL
            string apiUrl = "https://secure.runescape.com/m=hiscore_oldschool_seasonal/index_lite.ws?player=" + ign;

            // Create Web Request for the API
            WebRequest request = WebRequest.Create(apiUrl);

            // Sort any credentials
            request.Credentials = CredentialCache.DefaultCredentials;

            // Create WebResponse 
            WebResponse response;

            // Try search for username
            try
            {
                // Get the response from the server
                response = request.GetResponse();
            }
            catch
            {
                User.Name = "User not found";
                return View("Index", User);
            }

            // Create string response from server
            string responseFromServer = "";

            // Read the data from the API
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                responseFromServer = reader.ReadToEnd();
            }

            // Close the response.  
            response.Close();

            // Split hiscores data 
            string[] lines = responseFromServer.Split(new[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Split level and minigames/bosses hiscores apart
            // if you think this is ugly feel free to change my methodology
            // string[] stats = new string[72];
            //  string[] minigames = new string[23];
            //  string[] bosses = new string[82];
            //  Array.Copy(lines, 0, stats, 0, 72);
            //  Array.Copy(lines, 72, minigames, 0, 23);
            // Array.Copy(lines, 94, bosses, 0, 82);

            // Set the model name
            User.Name = ign;
            User.TotalLevel = Convert.ToInt32(lines[1]);
            User.LeagueScore = Convert.ToInt32(lines[73]);

            ////set the model boss kc
            //for(int i = 0; i < bosses.Length; i += 2)
            //{
            //    Bosses bossKC = new Bosses(bosses[i], bosses[i + 1]);
            //    User.Bosses.Add(bossKC);
            //}
            Player currentPlayer = new Player(User.Name, User.TotalLevel, User.LeagueScore);

            User.Players.Add(currentPlayer);

            //===== commented this out for now cos just wanted to see boss kills =====
            // Set the model stats
            //foreach (string s in lines)
            //{
            //    User.Stats.Add(s);
            //}

            // Return the data to the correct view
            return View("Index", User);
        }

        // Create a new instance of the model
        new UsersModel User = new UsersModel();

        /// <summary>
        /// Gets all the Foh bois & ladies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFoh()
        {
            // Use this instead
            List<string[]> UsernamesList = new List<string[]>();

            // List of threads to execute the api data collection
            List<Thread> Threads = new List<Thread>();

            // Name and user arrays for the hardcoded members of the clan
            #region Name Strings
            // All the names 
            string names = "Runecraft, Boss Biz, Dedmoo, Driftzzilla, MrReboks, Steeam, Ahvexx, Aigua, Black_Rifle, Blurr62, Dackel, Fignootters, H_a_f_a_d, Organic_Hemp, Super_Tails, Supreme, SynTechRS";
            string names1 = "0ffline, 1yfe, Afriquee, Antiyano, b4iforget, baymax, C_A_M_E_L, Crafty_Man, Demoni, Fatboy_7, FOH_Panda, Girl10116, Hamada, Indy_Ju, inescate, Jd7, Jedi, laa2, Ladm8_LoLo, Massif, Max Luck";
            string names2 = "Monkey_D_C, OS Glory, OS_Swift, Paul_Beer, Phaero, Pompeyo, RepublicofRS, Sec_Blake, SM_Tech_N9ne, Soldjer, St_Chrewin, St_Dutchiaz, Teagan, Toxi, Tu_r_bo, Zachkz";
            string names3 = "7lizard, A_Charmander, a_Horse, A_Magician, A_Message, absorbing, Adrik, Alexa, arny41ife, AT_F, Ayyye, Blue_Limes, bwy, Bye_Miracle, Camping_Carl, Cant_PvM, Cha_Siu_Fan, Chewy_AV, Cho_wn, chxpo";
            string names4 = "Clegane, Danrue, DBV_Orange, DCON, Devika, Double_Eh, Dozers, Duanyi, Dundonik, Duradyl, Em_pa_thize, ExpiredxMilk, IRONF34R, Fayney, Feirse, FigmentsMind, Fluga, For_My_Block, Freaks, Frosty_Fire, GainsBraah";
            string names5 = "Ginger_Elvis, Golem, Greh, Hanzit, Hathrow, Im_Gar, Insta, Its_Liger, JBlieves24, Jepsen, Jim_Mick_Bob, Kevva, L_augh, Lemon5000123, Lerzbot, LL_Cellulose, Longie, LS1, m_7, Manswers, Mithter_T, Mogu";
            string names6 = "Moot_Thought, Mvvs, N_a_a_t_e, Nan, Nancyy, Nealvy, Nerd Body, NutZak, Obsessed_Egg, Ocara_Debrew, Osrs_Nate, Osrs_sean, P0tatoes, Polibo, qbt, Quiet_please, Reinbark, Rongor, Saufen, Shiron_Dank, show_hog";
            string names7 = "Shy_Slays, Sin_Cave, Sinferna, SnapAction, Sosa, Speculate_Me, St_Jakibi, strawbery, Tallgirl-101, TheDuckChris, toes, Toxic_Dice, U_Mirin_Brah, umr, Un_Known, Unexiez, Vayels, Voldy, vutr, Vzir, Wood_Scimmy";
            string names8 = "X_abyss_X, xeeney, xFrenchyy, xplosiv_fury, Zeeeb, zreL, ZyphDoz, 13O, 33s, 4Hymnz, Adoptulous, Advise, AlwaysSorrow, AnthMelo, Arceuxs, Atrics, Aurizon, Azami, B_i_s_o_n, B_S, Backy19, BARKBARKBARK, Big_Drew_FOH";
            string names9 = "Billabongs, BJ_Legend, blbs, Bownerator, Burak67, Bygones, cancer_ruin, Cheezer2000, Claiborne, ClipseZ, Crawn, D0GS0NG, daisyfletchy, Dat_Proline, DatNeck, Dawn_Hero, Declined, DingoSRG, DjGoGu, dpi, DrFlavor";
            string names10 = "DrNachtiga, Ede_n, Exilthh, Falll, FatCaucasian, Fe_All_Pets, Ferb, Finlayze, Gevlekte_Koe, Girl_Dad, Gomen, Good_Carl, GT350_Shelby, Guntown, Gzxp, HC_BTW_LMFAO, Hempwickk, Honket, ii_pav_ii, ikea_shark, Im_Wilks";
            string names11 = "IMissSports, innaRS, Insights, IPreferNotTo, iSinghSoorma, iTz_Scrap, Jerod, JFryGuy, JoonSoo, Jord_Pvm, Jubita, Kadish_Vel, Kizminsky, Knightlock, Knoxy, KoalatyV, Krashcha, Krazykowala, Kre_e, L_I_V_E_Y";
            string names12 = "Lewyn, Lysandra, mawrin, Medan, MisterBaboon, Mosess, Mutations, Nosmo, Not_Sivas, NUMBER1H0ST, Oberstenn, p0_b0, PabIoEscobar, PanthersOG, Parryma, Pasmo, patagonia, Pesticide, Pugy, Rai, Rellnquish, Rolfeez";
            string names13 = "Sakaki, Salsa, ScoobySnacks, Semper_Gucci, SH5, sighs, sloppy_box, Sloth, sock_blaster, space_craft, Spikezx, St_Jon, Steals, StonerMoment, Strongtank11, Sturminator3, summr, Super_man, Talu, Tasty_Hotpot, TensorsFlow";
            string names14 = "Tesk, Tha_Jabroni2, The_Yun_Che, Throws, Toast_Lady, Trizzy, Vory, Wittminator, xDist, xp5, YooItzMark, Yoop_7, Zarmos, Ashdalle, T0oth, Exo_Landon, Roskiss, One_Tick_Man, Calamity_Meg, Maxisbaws, DestXI, Dong Chan";
            string names15 = "dl0_0lb, Conway, flaminkillaa, FunctioningC";
            #endregion

            #region Username Arrays
            string[] Usernames = names.Split(','); UsernamesList.Add(Usernames);
            string[] Usernames1 = names1.Split(','); UsernamesList.Add(Usernames1);
            string[] Usernames2 = names2.Split(','); UsernamesList.Add(Usernames2);
            string[] Usernames3 = names3.Split(','); UsernamesList.Add(Usernames3);
            string[] Usernames4 = names4.Split(','); UsernamesList.Add(Usernames4);
            string[] Usernames5 = names5.Split(','); UsernamesList.Add(Usernames5);
            string[] Usernames6 = names6.Split(','); UsernamesList.Add(Usernames6);
            string[] Usernames7 = names7.Split(','); UsernamesList.Add(Usernames7);
            string[] Usernames8 = names8.Split(','); UsernamesList.Add(Usernames8);
            string[] Usernames9 = names9.Split(','); UsernamesList.Add(Usernames9);
            string[] Usernames10 = names10.Split(','); UsernamesList.Add(Usernames10);
            string[] Usernames11 = names11.Split(','); UsernamesList.Add(Usernames11);
            string[] Usernames12 = names12.Split(','); UsernamesList.Add(Usernames12);
            string[] Usernames13 = names13.Split(','); UsernamesList.Add(Usernames13);
            string[] Usernames14 = names14.Split(','); UsernamesList.Add(Usernames14);
            string[] Usernames15 = names15.Split(','); UsernamesList.Add(Usernames15);
            #endregion

            // Use threads to get all the data without taking too much time
            foreach (var list in UsernamesList)
            {
                Thread t_names = new Thread(() =>
                {
                    foreach (var ign in list)
                        GetAllFoh(ign.ToString().Trim());
                });
                Threads.Add(t_names);
            }

            // Start the threads
            Threads.ForEach(x => x.Start());

            // Make sure the program does not continue executing on the main thread
            Threads.ForEach(x => x.Join());

            // Sort the list by league points
            var SortedPlayer =  User.Players.OrderByDescending(x => x.LeaguePoints).ToList();

            User.Players = SortedPlayer;

            // return the data to the user 
            return View("Index", User);
        }

        /// <summary>
        /// Gets all Foh members' score
        /// </summary>
        /// <param name="ign"></param>
        public void GetAllFoh(string ign)
        {
            // Create dynamic API URL
            string apiUrl = "https://secure.runescape.com/m=hiscore_oldschool_seasonal/index_lite.ws?player=" + ign;

            // Create Web Request for the API
            WebRequest request = WebRequest.Create(apiUrl);

            // Sort any credentials
            request.Credentials = CredentialCache.DefaultCredentials;

            // Create WebResponse 
            WebResponse response;

            try
            {
                // Get the response from the server
                response = request.GetResponse();

                // Create string response from server
                string responseFromServer = "";

                // Read the data from the API
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    responseFromServer = reader.ReadToEnd();
                }

                // Close the response.  
                response.Close();

                // Split hiscores data 
                string[] lines = responseFromServer.Split(new[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // Set the model name
                User.Name = ign;
                User.TotalLevel = Convert.ToInt32(lines[1]);
                User.LeagueScore = Convert.ToInt32(lines[73]);

                // Create a new player
                Player currentPlayer = new Player(User.Name, User.TotalLevel, User.LeagueScore);

                // Add user to list of users 
                User.Players.Add(currentPlayer);
            }
            catch
            {
                User.Name = ign + " not found";
            }
        }
    }
}



