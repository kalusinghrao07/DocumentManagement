using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDocumentManagement.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public virtual User User { get; set; }
    }
}
