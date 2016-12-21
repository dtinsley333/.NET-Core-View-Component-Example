using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class LatestFileInfoViewModel
    {
        public int UserId { get; set; }
        public string FileName{get;set;}
        public string UploadUserId { get; set; }
        public string ContentType { get; set; }
        public string UserName { get; set; }
        public string FileHistoryDescription { get; set; }
    }
}
