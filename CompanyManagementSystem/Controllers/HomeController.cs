using CompanyManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Data;
using System.Diagnostics;

namespace CompanyManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FreelanceContext _context;
        public HomeController(FreelanceContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
           
                List<Company> list = new List<Company>();
                list = _context.Companies.ToList();
                ViewBag.CompanyList = list;
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("SomeThing Went Wrong!!", ex);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registor()
        {

            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<JsonResult> Registor(Company comp)
        {
            
              
                    Company cmp = new Company();
                   //  cmp.Id = comp.Id;
                    cmp.Name = comp.Name;
                    cmp.Address = comp.Address;

                    int rows = 0;


                    _context.Companies.Add(cmp);

                    
                    rows = await _context.SaveChangesAsync();
                    if (rows > 0)
                    {
                        return Json("Data Saved Successfuly");
                    }
                    else
                    {
                        //String message = "Failed";
                        return Json("something went wrong!!");
                    }
                }

        [HttpGet]
        public async Task<IActionResult> Update()
        {

            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<JsonResult> Update(Company comp)
        {
                int rows = 0;
                var cat = await _context.Companies.FirstOrDefaultAsync(item => item.Id == comp.Id);
                if (cat == null)
                    throw new ArgumentNullException("No Data Found");
                cat.Name = comp.Name;
                cat.Address = comp.Address;
                rows = await _context.SaveChangesAsync();
            
            if (rows > 0)
            {
                return Json("Data Saved Successfuly");
            }
            else
            {
                //String message = "Failed";
                return Json("something went wrong!!");
            }
            
        }



        [HttpGet]
        public async Task<IActionResult> Delete()
        {

            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
                int rows = 0;
                var cat = await _context.Companies.FirstOrDefaultAsync(item => item.Id == id);
                if (cat == null)
                    throw new ArgumentNullException("No Data Found");
                _context.Companies.Remove(cat);
                rows = await _context.SaveChangesAsync();
                if (rows > 0)
            {
                return Json("Data deleted Successfuly");
             }
            else
            {
                //String message = "Failed";
                return Json("something went wrong!!");
            }

        }

        //public async Company getData(int Id)
        //{
        //    Company compp = new Company();
        //    try
        //    {

            //        List<Company> list = new List<Company>();
            //        list = _context.Companies.ToList();
            //        for(int i=0;i<list.Count; i++)
            //        {
            //            if (list[i].Id ==Id)
            //            {
            //                return list[i];
            //            }
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("SomeThing Went Wrong!!", ex);
            //    }

            //    return compp;

            //}



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
