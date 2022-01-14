using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore.Models;
using Dapper;
using NetCore.Service;
using NetCore.ViewModels;

namespace NetCore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ISingle _Single;
        private readonly IScoped _Scoped;
        private readonly ITransient _Transient;
        
        private IConfiguration _config;
        //private ConnectionStrings _ConnectionStrings { set; get; }

        private IMember2Service _Member2Service;

        public HomeController(
            ISingle Single,
            IScoped scoped,
            ITransient Transient,
            IConfiguration config,
            //IOptions<ConnectionStrings> ConnectionStrings,
            IMember2Service Member2Service)
        {
            _Single = Single;
            _Scoped = scoped;
            _Transient = Transient;

            _config = config;

            //_ConnectionStrings = ConnectionStrings.Value;

            _Member2Service = Member2Service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.SingleTemp = _Single;
            ViewBag.ScopedTemp = _Scoped;
            ViewBag.TransientTemp = _Transient;

            return View();
        }

        public IActionResult democonfig()
        {
            DemoUser DemoUser = new DemoUser();
            _config.GetSection("DemoUser").Bind(DemoUser);

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var sDemoUser = JsonSerializer.Serialize(DemoUser, options);

            var sConfigValue = _config.GetValue<string>("ConfigValue");
            var sGoogle = _config.GetValue<string>("DemoUrl:Google");

            ViewBag.sDemoUser = sDemoUser;
            ViewBag.sConfigValue = sConfigValue;
            ViewBag.sGoogle = sGoogle;

            return View();
        }

        public IActionResult CRUD()
        {
            var data = _Member2Service.getAllData();
            //ViewBag.Member2 = data;
            var Model = new CURDViewModel();
            Model.DataList = data.ToList();

            Model.Url= _config.GetValue<string>("AjaxUrl");

            return View(Model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Create(Member2 model)
        {
            if (model.Birthday == null) model.Birthday = DateTime.Now;

            _Member2Service.Insert(model);

            //return RedirectToAction("CRUD");

            var data = _Member2Service.getAllData();
            //ViewBag.Member2 = data;
            var Model = new CURDViewModel();
            Model.DataList = data.ToList();
            return View("CRUD", Model);
        }

        public IActionResult Delete(string DeleteID)
        {
            _Member2Service.Delete(DeleteID);

            //return RedirectToAction("CRUD");

            var data = _Member2Service.getAllData();
            //ViewBag.Member2 = data;
            var Model = new CURDViewModel();
            Model.DataList = data.ToList();
            return View("CRUD", Model);
        }

        public IActionResult Update(Member2 model)
        {
            if (model.Birthday == null) model.Birthday = DateTime.Now;

            _Member2Service.Update(model);

            //return RedirectToAction("CRUD");

            var data = _Member2Service.getAllData();
            //ViewBag.Member2 = data;
            var Model = new CURDViewModel();
            Model.DataList = data.ToList();
            return View("CRUD", Model);
        }
        [HttpPost]
        public JsonResult Update_ver2(Member2 model)
        {
            if (model.Birthday == null) model.Birthday = DateTime.Now;

            _Member2Service.Update(model);

            return Json("");
        }

        [HttpPost]
        public JsonResult getData(string ID)
        {
            var Data = _Member2Service.getData(ID);

            return Json(Data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
