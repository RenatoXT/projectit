using System;
using Microsoft.AspNetCore.Http;

namespace projectit.Models
{
    public class TeamViewModel : DefaultViewModel
    {
        public string name { get; set; }
        public string skill { get; set; }

        public IFormFile picture { get; set; }
        public byte[] Byte_picture { get; set; }

        public string Base64picture
        {
            get
            {
                if (Byte_picture != null)
                    return Convert.ToBase64String(Byte_picture);
                else
                    return string.Empty;


            }
        }
    }
}
