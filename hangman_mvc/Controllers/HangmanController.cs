using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hangman_mvc.Models;
using System.Net.Http;

namespace hangman_mvc.Controllers
{
    public class HangmanController : Controller
    {
        // GET: 
        // URL:  /Hangman
        public ActionResult Index()
        {
            string chosenWord = string.Empty;

            // use the WEB API CONTROLLER as a normal class
            ValuesController ctl = new ValuesController();
            chosenWord = ctl.Get().FirstOrDefault();


            //ValuesController values = new ValuesController();
            //IEnumerable<string> words = values.Get();
            //chosenWord = words.ElementAt(0);



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49200/api/");
                // perform an HTTP GET query
                var responseTask = client.GetAsync("values");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<string>>();
                    readTask.Wait();
                    chosenWord = readTask.Result[0];
                }
                else
                {
                    chosenWord = string.Empty;
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }



            //  use the included ASCII TEXT FILE
            var filePath = HttpContext.ApplicationInstance.Context.Server.MapPath("/Content/wordlist.txt");
            HangmanUtils utils = new HangmanUtils();
            HangmanWordList wordlist = new HangmanWordList(filePath);
            //string winningWord = wordlist.GetWinningWord();
            chosenWord = wordlist.GetWinningWord();



                        
            ViewBag.Title = "Hangman Game Demo";
            ViewBag.Message = "Keep guessing letters until you win";

            HangmanModel hangmanModel = new HangmanModel();
            hangmanModel.WinningWord = chosenWord;

            hangmanModel.HeadHidden = "hidden";
            hangmanModel.LeftArmHidden = "hidden";
            hangmanModel.TopBodyHidden = "hidden";
            hangmanModel.RightArmHidden = "hidden";
            hangmanModel.BottomBodyHidden = "hidden";
            hangmanModel.LeftLegHidden = "hidden";
            hangmanModel.RightLegHidden = "hidden";
            hangmanModel.GuessedLetter = "";

            utils.GenerateDisplayWordString(hangmanModel);

            ///////////////////////////////Response.Write(chosenWord);

            Session["winningword"] = chosenWord;
            Session["hangmanmodel"] = hangmanModel;

            return View(hangmanModel);
        }


        
        // POST: 
        // URL:  /Hangman/Guess
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Guess(FormCollection col)
        {
            ViewBag.Title = "Hangman Game Demo";
            ViewBag.Message = "Keep guessing letters until you win";

            HangmanModel hangmanModel = (HangmanModel)Session["hangmanmodel"];
            HangmanUtils utils = new HangmanUtils();

            string newGuess = col["guess"];

            if (String.IsNullOrEmpty(newGuess))
            {
                ModelState.AddModelError("Guess", "Enter a letter guess");

                Session["hangmanmodel"] = hangmanModel;
                return View(hangmanModel);
            }

            if (utils.CheckAlreadyGuessed(hangmanModel, newGuess))
            {
                ModelState.AddModelError("Guess", "Already guessed that letter");
                Session["hangmanmodel"] = hangmanModel;
                return View(hangmanModel);
            }

                        
            utils.CheckLatestGuess(hangmanModel, newGuess);
            utils.GenerateDisplayWordString(hangmanModel);

            hangmanModel.HeadHidden = (hangmanModel.WrongGuessCount >= 1) ? string.Empty : "hidden";
            hangmanModel.LeftArmHidden = (hangmanModel.WrongGuessCount >= 2) ? string.Empty : "hidden";
            hangmanModel.TopBodyHidden = (hangmanModel.WrongGuessCount >= 3) ? string.Empty : "hidden";
            hangmanModel.RightArmHidden = (hangmanModel.WrongGuessCount >= 4) ? string.Empty : "hidden";
            hangmanModel.BottomBodyHidden = (hangmanModel.WrongGuessCount >= 5) ? string.Empty : "hidden";
            hangmanModel.LeftLegHidden = (hangmanModel.WrongGuessCount >= 6) ? string.Empty : "hidden";
            hangmanModel.RightLegHidden = (hangmanModel.WrongGuessCount >= 7) ? string.Empty : "hidden";

            Session["hangmanmodel"] = hangmanModel;
            return View(hangmanModel);
        }






        




        // GET: Hangman/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hangman/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hangman/Create
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

        // GET: Hangman/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hangman/Edit/5
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

        // GET: Hangman/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hangman/Delete/5
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
    }
}
