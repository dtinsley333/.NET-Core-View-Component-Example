using System;

using System.Linq;
using Microsoft.AspNet.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.Threading;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using WebApplication1.ViewModels.Account;

namespace WebApplication1.Controllers
{
	public class DocumentController : Controller
	{
		private ApplicationDbContext dbContext { get; set; }
		public DocumentController(ApplicationDbContext context)
		{
			dbContext = context;
		}
       
        
        
        // GET: /<controller>/
        public async Task<IActionResult> Index()
		{

            var docs = dbContext.Document
                .OrderBy(f=>f.FileName)
                .Select (d=> new DocumentIndexDetails
					   {
						   DocumentId = d.DocumentId,
						   Title = d.Title,
						   FileName = d.FileName,
						   ContentType = d.ContentType,
						   UploadDate = d.UploadDate,
						   UploadUserId = d.UploadUserId
					   }).ToAsyncEnumerable();

            DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
            {
                Documents = await docs.ToList(),
			};

			return View(vm);
            
		}

        [HttpPost("upload")]
        public async Task<IActionResult> Index(IFormFile file, DocumentDetailsViewModel docViewModel)
        {


            if (file != null && file.ContentType.Contains("image"))
            {

                using (MemoryStream ms = new MemoryStream())
                {

                    try
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            reader.BaseStream.CopyTo(ms);
                            byte[] bytes = ms.ToArray();
                            var fileType = file.ContentType;
                            Document doc = new Document()
                            {
                                Title = docViewModel.Title,
                                FileName = fileName,
                                Contents = bytes,
                                ContentType = fileType,
                                UploadDate = DateTime.Now,
                                UploadUserId = "dtinsley91@gmail.com"

                            };
                            dbContext.Document.Add(doc);
                            dbContext.SaveChanges();
                        }

                        ViewBag.Message = "File uploaded successfully";

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR: Unsuccessful upload.  Details:" + ex.Message.ToString();
                    }
                }

            }
            else
            {
                ViewBag.Message = "Please select an image file";
            }
            //go back to index and the new doc should show

            var docs = (from d in dbContext.Document
                        orderby d.FileName
                        select new DocumentIndexDetails
                        {
                            DocumentId = d.DocumentId,
                            Title = d.Title,
                            FileName = d.FileName,
                            ContentType = d.ContentType,
                            UploadDate = d.UploadDate,
                            UploadUserId = d.UploadUserId
                        }).ToAsyncEnumerable();

            DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
            {
                Documents = await docs.ToList()

            };
            return View(vm);
        }
        
           
      
        public ActionResult FileDownLoad(int documentId)
		{
			var fileId = documentId;

			var myFile = dbContext.Document.SingleOrDefault(x => x.DocumentId == fileId);
			if (myFile != null)
			{
				byte[] file = myFile.Contents.ToArray();

				var cd = new System.Net.Mime.ContentDisposition
				{
					FileName = myFile.FileName,

					// always prompt the user for downloading, set to true if you want
					// the browser to try to show the file inline
					Inline = false,
				};

				Response.Headers.Add("Content-Disposition", cd.ToString());
				return File(file, myFile.ContentType, myFile.FileName);
			}
			else return null;

		}
	}
}



