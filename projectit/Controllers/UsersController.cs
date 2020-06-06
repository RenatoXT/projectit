using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectit.DAO;
using projectit.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data.SqlClient;

namespace projectit.Controllers
{
    public class UsersController : DefaultController<UsersViewModel>
    {
        public UsersController()
        {
            DAO = new UsersDAO();
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }


        protected override void ValidateData(UsersViewModel model, string Operation)
        {
            base.ValidateData(model, Operation);

           if (string.IsNullOrEmpty(model.name))
                ModelState.AddModelError("name", "Preencha o o seu nome.");

            if (string.IsNullOrEmpty(model.nickname))
                ModelState.AddModelError("nickname", "Preencha o nickname.");

            if (string.IsNullOrEmpty(model.email))
                ModelState.AddModelError("email", "Preencha o email.");

            if (string.IsNullOrEmpty(model.password))
                ModelState.AddModelError("password", "Preencha a senha");

            if (model.picture == null && Operation == "I")
                ModelState.AddModelError("picture", "Escolha uma imagem");

            if (model.picture != null && model.picture.Length / 1024 / 1024 >= 5)
                ModelState.AddModelError("picture", "Imagem limitada à 5MB.");

            if (ModelState.IsValid)
            {
                if (Operation == "A" && model.picture == null)
                {
                    UsersViewModel picture = DAO.Query(model.id);
                    model.Byte_picture = picture.Byte_picture;
                }
                else
                {
                    model.Byte_picture = ConvertImageToByte(model.picture);
                }
            }
        }
    }
}