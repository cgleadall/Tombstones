using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Models
{
    public class Minister : PersonBase
    {
        public string Id { get; set; }
        public string Dates { get; set; }
        public string Source { get; set; }
        public string UploadFileId { get; set; }

        public Minister(string uploadFileId)
        {
            UploadFileId = uploadFileId;
        }

        public static Minister Create(object[] itemArray, string uploadFileId)
        {
            // Mister   Dates   Source
            var result = new Minister(uploadFileId);

            if (itemArray.Length == 3)
            {
                result.SetCombinedName(itemArray[0].ToString());
                result.Dates = itemArray[1].ToString();
                result.Source = itemArray[2].ToString();
            }
            else
            {
                throw new Exception("Item Array does not have enough items. Only " + itemArray.Length + " of 3 were found.");
            }
            return result;
        }
    }
}