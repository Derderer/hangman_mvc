using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hangman_mvc.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            HangmanEntities db = new HangmanEntities();
            Random generator = new Random((int)DateTime.Now.Ticks);
            int index = generator.Next(db.word_list.Count());
            string chosenWord = string.Empty;

            var selections = from word in db.word_list
                      where word.Id == index
                      select word;

            var selection = selections.Take(1).Single();

            chosenWord = selection.word;
            return new string[] { chosenWord };
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
