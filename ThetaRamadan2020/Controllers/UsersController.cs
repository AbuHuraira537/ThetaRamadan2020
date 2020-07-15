using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThetaRamadan2020.Models;
using Newtonsoft.Json;
using System.Text;

namespace ThetaRamadan2020.Controllers
{
    public class UsersController : Controller
    {
        private readonly thetaramdan2020Context _context;

        public UsersController(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            IEnumerable<Users> users = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50476/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Users1");
                if (response.IsSuccessStatusCode)
                {
                    string use = response.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<IEnumerable<Users>>(use);

                }
            }
        

            return View(users);
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,UserRole,Password,Mobile")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,UserRole,Password,Mobile")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        //public IActionResult adduser(string name)
        //{


        //    try
        //    {
        //        Users user = new Users();
        //        user.Name = name;
        //        user.Password = "134";
        //        user.Mobile = "34345434534";
        //        user.UserRole = "customer";
        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return View(nameof(Index));

        //    }
        //    catch (Exception e)
        //    { 
        //        return View(nameof(Index));
        //    }

        //}
       
        public async Task<IActionResult> ForgetPassword(string mob, string email)
        {
               Users p = await _context.Users.Where(p => p.Mobile == mob && p.Email == email).FirstOrDefaultAsync();
                if (p != null)
            {
                try
                {
                    Mail mail = new Mail();
                    mail.desc = p.Password;
                    if(!mail.sendmail(email))
                    {
                        ViewBag.er = "Some thing error occure";
                        return View();
                    }
                    ViewBag.sucess = "Mail sent to your provided email";
                    return View();
                }
                catch (Exception e)
                {
                    ViewBag.er = e.Message;
                    return View();
                }
            }
            ViewBag.er = "Recovery Email or Mobile is invalid";
            return View();
        }
        public async Task<string> RegisterCustomer(string name,string password,string email,string mobile)
        {
            Users user = new Users();
            user.Name = name;
            user.Mobile = mobile;
            user.Password = password;
            user.UserRole = "Customer";
            user.Email = email;
            Validations validate = new Validations();
           if( validate.PersonValidation(user)!="")
            {
                return validate.PersonValidation(user);
            }
            string res;
            StringContent u = new StringContent(JsonConvert.SerializeObject(user),Encoding.UTF8,"application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44391/");
                HttpResponseMessage response = await client.PostAsync("api/Users1",u);
                if (response.IsSuccessStatusCode)
                {
                    Mail mail = new Mail();
                    mail.sendmail(user.Email);
                   return response.StatusCode.ToString();
                }
                
                res=response.StatusCode.ToString();
            }
            return res;   
        }
        public async Task<string> RegisterStaffOrAdmin(string name, string password, string email, string mobile,string role)
        {
            Users user = new Users();
            user.Name = name;
            user.Mobile = mobile;
            user.Password = password;
            user.UserRole = role;
            user.Email = email;
            Validations validate = new Validations();
            if (validate.PersonValidation(user) != "")
            {
                return validate.PersonValidation(user);
            }
            string res;
            StringContent u = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44391/");
                HttpResponseMessage response = await client.PostAsync("api/Users1", u);
                if (response.IsSuccessStatusCode)
                {
                    Mail mail = new Mail();
                    mail.sendmail(user.Email);
                    return response.StatusCode.ToString();
                }

                res = response.StatusCode.ToString();
            }
            return res;
        }



    }
}
