# View Component Example
View Component example that retrieves data and displays it inline on a view.

###Partials have been replaced in .Net Core with View Components. Partials still work the way they always have with static html but if you are building your UI elements dynamically you will need to use a view component.####

###This sample application allows users to upload images, the index page for the uploading needed to display information regarding most recently uploaded file. This data needed to be displayed on othe pages so it made sense to create a view component. 

##Steps
1. Create a folder called called "Components" inside the "View" folder for the controller. Example "Views/Documents/Component
2. Create a folder for your component. "Views/Documents/Documents/Components/LatestFileInfo"
3. Components consist of two parts. a. A view  and b. A class that hold the logic for your component. 
4. Create a view model called "LatestFileInfoViewModel.cs"
```
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

```

5. Add a new .cs file called "LatestFileInfo.cs". It will have a public method called "Invoke" which will be called from your view code. 


```
using System;
using WebApplication1.ViewModels;
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
 ```
 
 6. Create a view in the Components folder. It can be called default and contain the following code.

```
 @model WebApplication1.ViewModels.LatestFileInfoViewModel
 @Model.FileHistoryDescription
 ```

7. Add the following to the view where you want the component to display:
 
 ```
  @Component.Invoke("LatestFileInfo")
 ```


