using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserDbContext userDb;
        public RegisterController(UserDbContext userDb)
        {
            this.userDb = userDb;
        }

        [HttpGet]
        public IActionResult AllUsers()
        {
            var result=userDb.UserRegister.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                userDb.UserRegister.Add(model);
                userDb.SaveChanges();
               return RedirectToAction("AllUsers");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Getbyuserid(int id) 
        {
            RegisterModel result= userDb.UserRegister.FirstOrDefault(x=>x.UserId == id);
            return View(result);
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {

            RegisterModel result = userDb.UserRegister.FirstOrDefault(x => x.UserId == id);
            return View(result);
        }

        [HttpPost]
        public IActionResult EditUser([Bind]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                userDb.UserRegister.Update(model);
                userDb.SaveChanges();
                return RedirectToAction("AllUsers");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var result = userDb.UserRegister.FirstOrDefault(x => x.UserId == id); 
            return View(result);
        }

        [HttpPost,ActionName("DeleteUser")]
        public IActionResult Delete(int id)
        {
            RegisterModel result = userDb.UserRegister.FirstOrDefault(x => x.UserId == id);
            
            if (ModelState.IsValid)
            {
                userDb.UserRegister.Remove(result); userDb.SaveChanges();
                return RedirectToAction("AllUsers");
            }
            return View();
        }
    }
}
