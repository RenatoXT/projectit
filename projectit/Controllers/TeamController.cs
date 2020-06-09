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
                ModelState.AddModelError("name", "Preencha o seu nome.");

            if (string.IsNullOrEmpty(model.skill))
                ModelState.AddModelError("skill", "Preencha a habilidade principal da equipe.");

            if (model.picture != null && model.picture.Length / 1024 / 1024 >= 5)
                ModelState.AddModelError("picture", "Imagem limitada à 5MB.");

            if (model.user_id <= 0)
                ModelState.AddModelError("user_id", "Selecione os membros do time");

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