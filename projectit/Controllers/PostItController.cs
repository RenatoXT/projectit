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
    public class PostItController : DefaultController<PostItViewModel>
    {
        public PostItController()
        {
            DAO = new PostItDAO();
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


        protected override void ValidateData(PostItViewModel model, string Operation)
        {
            base.ValidateData(model, Operation);

            if (string.IsNullOrEmpty(model.header))
                ModelState.AddModelError("header", "Preencha o título da ação.");

            if (string.IsNullOrEmpty(model.doing_by) && model.status != "Stories")
                ModelState.AddModelError("doing_by", "Preencha o responsável.");

            if (string.IsNullOrEmpty(model.body))
                ModelState.AddModelError("body", "Preencha a descrição da ação.");

            if (string.IsNullOrEmpty(model.status))
                ModelState.AddModelError("status", "Determine o status da ação.");

            if (Operation != "A")
                if (model.project_id <= 0)
                ModelState.AddModelError("project_id", "Determine um projeto");

        }
    }
}