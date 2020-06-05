using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace projectit.Controllers
{
    public class HelperController : Controller
    {
        public static Boolean CheckLogin(ISession session)
        {
            string login = session.GetString("Login");
            if (login == null)
                return false;
            else
                return true;
        }
    }
}