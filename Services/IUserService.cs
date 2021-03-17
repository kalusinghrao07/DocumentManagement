using SampleDocumentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDocumentManagement.Services
{
    public interface IUserService
    {
        Task CreateUser(UserViewModel userViewModel);
        List<UserViewModel> GetAllUserData();
    }
}
