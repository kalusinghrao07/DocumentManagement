using SampleDocumentManagement.Models;
using SampleDocumentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDocumentManagement.Services
{
    public class UserService : IUserService
    {
        private readonly DocumentDbContext _context;
        public UserService(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(UserViewModel userViewModel)
        {
            byte[] fileArray = null;
            if (userViewModel.File != null && userViewModel.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await userViewModel.File.CopyToAsync(ms);
                    fileArray = ms.ToArray();
                }
            }
            // save user data
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Name = userViewModel.Name;
            user.Address = userViewModel.Address;

            // save file details
            Document document = new Document();
            document.Id = Guid.NewGuid();
            document.UserId = user.Id;
            document.FileName = userViewModel.File.FileName;
            document.FileContent = fileArray;

            await Task.Run(() =>
            {
                _context.Set<User>().Add(user);
                _context.Set<Document>().Add(document);
                _context.SaveChanges();
            });
        }

        public List<UserViewModel> GetAllUserData()
        {
            var users = _context.User;
            var documents = _context.Document;

            var userListData = (from usr in users
                                join doc in documents on usr.Id equals doc.UserId
                                select new UserViewModel()
                                {
                                    Id = usr.Id,
                                    Name = usr.Name,
                                    Address = usr.Address,
                                    FileName = doc.FileName,
                                    DocumentId = doc.Id,
                                    FileContent = doc.FileContent
                                }).ToList();
            return userListData;
        }
    }
}
