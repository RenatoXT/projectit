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
    public class ProjectController : DefaultController<ProjectViewModel>
    {
        public ProjectController()
        {
            DAO = new ProjectDAO();
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

        protected override void ValidateData(ProjectViewModel model, string Operation)
        {
            base.ValidateData(model, Operation);

            //if (string.IsNullOrEmpty(model.description))
            //    ModelState.AddModelError("description", "Preencha a descrição.");

            if (model.code is null)
                ModelState.AddModelError("code", "Campo Obrigatório.");

            if (model.picture != null && model.picture.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada à 2MB.");

            if (ModelState.IsValid)
            {
                if (Operation == "A" && model.picture == null)
                {
                    ProjectViewModel gam = DAO.Query(model.id);
                    model.Byte_picture = gam.Byte_picture;
                }
                else
                {
                    model.Byte_picture = ConvertImageToByte(model.picture);
                }
            }

        }
    }
}