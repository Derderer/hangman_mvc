using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hangman_mvc.Models
{
    public class HangmanModel
    {
        public string HeadHidden { get; set; }
        public string LeftArmHidden { get; set; }
        public string RightArmHidden { get; set; }
        public string LeftLegHidden { get; set; }
        public string RightLegHidden { get; set; }
        public string TopBodyHidden { get; set; }
        public string BottomBodyHidden { get; set; }
        public string WinningWord { get; set; }
        public string DisplayWordString { get; set; }

        [Required]
        public string GuessedLetter { get; set; }
        public List<string> GuessList { get; set; }
        public List<string> WrongGuessList { get; set; }
        public string Guesses { get; set; }
        public string WrongGuesses { get; set; }
        public int WrongGuessCount;
        public int CorrectGuessCount;

        public HangmanModel()
        {
            WrongGuessCount = 0;
            CorrectGuessCount = 0;
            HeadHidden = string.Empty;
            LeftArmHidden = string.Empty;
            RightArmHidden = string.Empty;
            LeftLegHidden = string.Empty;
            RightLegHidden = string.Empty;
            TopBodyHidden = string.Empty;
            BottomBodyHidden = string.Empty;

            WinningWord = string.Empty;
            DisplayWordString = string.Empty;
            GuessedLetter = string.Empty;
            GuessList = new List<string>();
            WrongGuessList = new List<string>();

            Guesses = string.Empty;
            WrongGuesses = string.Empty;
        }
    }
}