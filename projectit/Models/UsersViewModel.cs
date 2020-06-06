using System;
using Microsoft.AspNetCore.Http;

namespace projectit.Models
{
    public class UsersViewModel : DefaultViewModel
    {
        public string nickname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }

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
