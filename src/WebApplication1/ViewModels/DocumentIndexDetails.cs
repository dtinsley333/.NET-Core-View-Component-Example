using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class DocumentIndexDetails
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUserId { get; set; }
    }
}
