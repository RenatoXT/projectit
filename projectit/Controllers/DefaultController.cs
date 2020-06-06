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
        protected bool GenNextId { get; set; }

        public IActionResult Index()
        {
            var list = DAO.List();
            return View(list);
        }

        public IActionResult Create(int id)
        {
            ViewBag.Operacao = "I";
            T model = Activator.CreateInstance(typeof(T)) as T;
            //PopViewData("I", model);
            return View("Form", model);
        }

        //protected virtual void PopViewData(string Operation, T model)
        //{
        //    if (GenNextId && Operation == "I")
        //        model.id = DAO.NextId();
        //}

        public IActionResult Save(T model, string Operation)
        {
            try
            {
                ValidateData(model, Operation);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operation;
                    //PopViewData(Operation, model);
                    return View("Form", model);
                }
                else
                {
                    if (Operation == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);

                    return RedirectToAction("index");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operation;
                //PopViewData(Operation, model);
                return View("Form", model);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Query(id);
                if (model == null)
                    return RedirectToAction("index");
                else
                {
                    //PopViewData("A", model);
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
            if (!HelperController.CheckLogin(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Login = true;
                base.OnActionExecuting(context);
            }
        }
    }
}