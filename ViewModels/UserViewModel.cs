using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDocumentManagement.ViewModels
{
    public class UserViewModel
    {
        public Guid? Id { get; set; }
        public Guid? DocumentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public byte[] FileContent { get; set; }
        public virtual List<UserViewModel> UserViewModels { get; set; }
    }
}
