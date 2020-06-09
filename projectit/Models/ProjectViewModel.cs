using System;
using Microsoft.AspNetCore.Http;

namespace projectit.Models
{
    public class ProjectViewModel : DefaultViewModel
    {
        public string code { get; set; }
        public string description { get; set; }
        public int team_id { get; set; }

        public IFormFile picture { get; set; }
        public byte[] Byte_picture { get; set; }

        public string ImagemEmBase64
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
