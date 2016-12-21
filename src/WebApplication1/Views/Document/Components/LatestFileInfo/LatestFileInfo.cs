using System;
using WebApplication1.ViewModels;
using WebApplication1.ViewModels.Account;
using System.Linq;
using Microsoft.AspNet.Mvc;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents.UserInfo
{
    public class LatestFileInfo : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public LatestFileInfo(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            var mostRecentUpload = _context.Document.OrderByDescending(a => a.UploadDate).FirstOrDefault();
            LatestFileInfoViewModel lastestUsers = new LatestFileInfoViewModel
            {
                FileHistoryDescription = $"Most recent file upload was of type {mostRecentUpload.ContentType}. Uploaded { String.Format(mostRecentUpload.UploadDate.ToShortDateString())}. File uploaded by user: {mostRecentUpload.UploadUserId}"
            };
            return View(lastestUsers);
        }
    }
}