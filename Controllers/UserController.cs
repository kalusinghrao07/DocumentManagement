using Microsoft.AspNetCore.Mvc;
using SampleDocumentManagement.Authorizations;
using SampleDocumentManagement.Services;
using SampleDocumentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDocumentManagement.Controllers
{
    [AuthorizationFilter]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult User()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserViewModels = _userService.GetAllUserData();
            return View(userViewModel);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
        {
            if (userViewModel != null && userViewModel.File != null)
            {
                string message = "";
                try
                {
                    await _userService.CreateUser(userViewModel);
                    message = "User Data is saved.";
                }
                catch (Exception ex)
                {
                    message = "User Data is not saved.";
                }
                ViewBag.Message = message;
                return RedirectToAction("User");
            }
            return Ok();
        }

        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<UserViewModel> userList = _userService.GetAllUserData();
            return PartialView("_FileDetails", userList);
        }

        [HttpGet]
        public FileResult DownLoadFile(Guid id)
        {
            List<UserViewModel> userList = _userService.GetAllUserData();

            var fileById = (from file in userList
                            where file.DocumentId.Equals(id)
                            select new { file.FileName, file.FileContent }).FirstOrDefault();

            return File(fileById.FileContent, "application/pdf", fileById.FileName);

        }
    }
}
