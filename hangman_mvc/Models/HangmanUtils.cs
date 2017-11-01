using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace hangman_mvc.Models
{
    public class HangmanUtils
    {

        public HangmanUtils()
        {
        }

        public bool CheckAlreadyGuessed(HangmanModel model, string newGuess)
        {
            foreach(string temp in model.GuessList)
            {
                if (temp.Equals(newGuess))
                {
                    return true;
                }
            }
            return false;
        }

        public void GenerateDisplayWordString(HangmanModel model)
        {
            StringBuilder sb = new StringBuilder();
            bool correctLetter = false;
            string winningWord = model.WinningWord;

            for (int i=0; i< winningWord.Length; i++)
            {
                correctLetter = false;
                foreach (string temp in model.GuessList)
                {
                    if (winningWord[i].ToString().Equals(temp))
                    {
                        correctLetter = true;
                    }
                }
                if(!correctLetter)
                {
                    sb.Append("_ ");
                }
                else
                {
                    sb.Append(winningWord[i].ToString()).Append(" ");
                }
            }
            model.DisplayWordString = sb.ToString();
        }

        public void CheckLatestGuess(HangmanModel model, string newGuess)
        {
            StringBuilder sb = new StringBuilder();
            bool correctLetter = false;
            string winningWord = model.WinningWord;
            model.GuessList.Add(newGuess);
            for (int i = 0; i < winningWord.Length; i++)
            {
                if (winningWord[i].ToString().Equals(newGuess))
                {
                    model.CorrectGuessCount++;
                    correctLetter = true;
                }
            }
            if (!correctLetter)
            {
                model.WrongGuessCount++;
                model.WrongGuessList.Add(newGuess);
            }

            sb.Append("Wrong Guesses: ");
            foreach(string temp in model.WrongGuessList)
            {
                sb.Append(temp).Append(" ");
            }
            model.WrongGuesses = sb.ToString();
        }
    }
}