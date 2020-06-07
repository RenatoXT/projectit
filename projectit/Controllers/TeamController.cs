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
    public class TeamController : DefaultController<TeamViewModel>
    {
        public TeamController()
        {
            DAO = new TeamDAO();
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


        protected override void ValidateData(TeamViewModel model, string Operation)
        {
            base.ValidateData(model, Operation);

            if (string.IsNullOrEmpty(model.name))
                ModelState.AddModelError("name", "Preencha o o seu nome.");

            if (model.picture != null && model.picture.Length / 1024 / 1024 >= 5)
                ModelState.AddModelError("picture", "Imagem limitada à 5MB.");

            if (ModelState.IsValid)
            {
                if (Operation == "A" && model.picture == null)
                {
                    TeamViewModel picture = DAO.Query(model.id);
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