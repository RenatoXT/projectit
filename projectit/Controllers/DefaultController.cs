using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projectit.DAO;
using projectit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projectit.Controllers
{

    public class DefaultController<T> : Controller where T : DefaultViewModel
    {
        protected DefaultDAO<T> DAO { get; set; }

        public virtual IActionResult Index()
        {
            var list = DAO.List();
            return View(list);
        }

        public IActionResult Create(int id)
        {
            ViewBag.Operacao = "I";
            T model = Activator.CreateInstance(typeof(T)) as T;
            PopCombos();
            return View("Form", model);
        }

        public IActionResult Save(T model, string Operation)
        {
            PopCombos();
            try
            {
                ValidateData(model, Operation);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operation;
                    return View("Form", model);
                }
                else
                {
                    if (Operation == "I")
                    {
                        DAO.Insert(model);

                        if (!HelperController.CheckLogin(HttpContext.Session))
                        {
                            HttpContext.Session.SetString("Login", "true");
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                        DAO.Update(model);

                    return RedirectToAction("index");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operation;
                return View("Form", model);
            }
        }

        public IActionResult Edit(int id)
        {
            PopCombos();

            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Query(id);
                if (model == null)
                    return RedirectToAction("index");
                else
                {
                    return View("Form", model);
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                return RedirectToAction("index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                return RedirectToAction("index");
            }
        }

        protected virtual void ValidateData(T model, string Operation)
        {
            if (Operation == "A" && DAO.Query(model.id) == null)
                ModelState.AddModelError("id", "id inválido!");

            if (model.created_at > DateTime.Now)
                ModelState.AddModelError("created_at", "Data de criação inválida");

            if (model.updated_at > DateTime.Now)
                ModelState.AddModelError("updated_at", "Data de atualização inválida");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!HelperController.CheckRegister(HttpContext.Session))
            //    context.Result = RedirectToAction("Create", "Users");
            if (!HelperController.CheckLogin(HttpContext.Session))
                context.Result = RedirectToAction("Create", "Users");
            else
            {
                ViewBag.Login = true;
                base.OnActionExecuting(context);
            }
        }

        private void PopCombos()
        {
            MakeListStatusCombo();
            MakeListProjectsCombo();
            MakeListTeamsCombo();
            MakeListUsersCombo();
        }

        private void MakeListStatusCombo()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem("Stories", "0"));
            list.Add(new SelectListItem("To Do", "1"));
            list.Add(new SelectListItem("In Progress", "2"));
            list.Add(new SelectListItem("Testing", "3"));
            list.Add(new SelectListItem("Done", "4"));

            ViewBag.PostItStatus = list;

        }

        private void MakeListProjectsCombo()
        {
            ProjectDAO dao = new ProjectDAO();
            var models = dao.List();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem("Selecione o Projeto...", "0"));

            foreach (var model in models)
            {
                SelectListItem item = new SelectListItem(model.code, model.id.ToString());
                list.Add(item);
            }
            ViewBag.Projects = list;

        }

        private void MakeListTeamsCombo()
        {
            TeamDAO dao = new TeamDAO();
            var models = dao.List();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem("Selecione a equipe...", "0"));

            foreach (var model in models)
            {
                SelectListItem item = new SelectListItem(model.name, model.id.ToString());
                list.Add(item);
            }
            ViewBag.Teams = list;

        }

        private void MakeListUsersCombo()
        {
            UsersDAO dao = new UsersDAO();
            var models = dao.List();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem("Selecione o usuário...", "0"));

            foreach (var model in models)
            {
                SelectListItem item = new SelectListItem(model.name, model.id.ToString());
                list.Add(item);
            }
            ViewBag.Users = list;

        }
    }
}