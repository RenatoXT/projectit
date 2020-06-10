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
using Microsoft.AspNetCore.Mvc.Filters;

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

            //if (string.IsNullOrEmpty(model.confirm_password) || model.confirm_password == model.password)
            //    ModelState.AddModelError("confirm_password", "Confirme a senha");
            

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

        public override IActionResult Index()
        {
            if (!HelperController.CheckLogin(HttpContext.Session))
            {
                ViewBag.Operacao = "I";
                UsersViewModel model = new UsersViewModel();
                return View("Form", model);
            }
            else
            {
                var list = DAO.List();
                return View(list);
            }

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HelperController.CheckLogin(HttpContext.Session))
            {
                ViewBag.Login = true;
                base.OnActionExecuting(context);
            }
        }


    }
}