using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Models
{
    public class Quaker
    {
        //Surname	First name	Date	Relationship	Area	Note	Source
        public string Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Date { get; set; }
        public string Relationship { get; set; }
        public string Area { get; set; }
        public string Note { get; set; }
        public string Source { get; set; }

        public static Quaker Create(object[] itemArray)
        {
            var result = new Quaker();
            if(itemArray.Length == 7)
            {
                result.Surname = itemArray[0].ToString();
                result.FirstName = itemArray[1].ToString();
                result.Date = itemArray[2].ToString();
                result.Relationship = itemArray[3].ToString();
                result.Area = itemArray[4].ToString();
                result.Note = itemArray[5].ToString();
                result.Source = itemArray[6].ToString();
            }
            return result;
        }
    }
}