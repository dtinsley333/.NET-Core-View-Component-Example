# View Component Example

###Partials have been replaced in .Net Core with View Components. Partials still work the way they always have with static html but if you are building your UI elements dynamically you will need to use a view component.####
This View Component example retrieves data and displays it inline on a view in an asp.net mvc .net core application. You will need Visual Studio 2015 to open the application. The log in information in the database connection string has been blanked out. You will create a view model to hold the data that will be displayed on the view component, a view component class and a view that will behave like a partial view in the days of old. You will invoke the view from the hosting view page. 


####This sample application allows users to upload images, the index page for the uploading needed to display information regarding most recently uploaded file. This data needed to be displayed on othe pages so it made sense to create a view component. The highlighted text is being produced by a view component that is retrieving the data from the a database. ####

![Image](https://github.com/dtinsley333/ViewComponentExample/blob/master/ViewComponent.png?raw=true)


##Steps
1. Create a folder called called "Components" inside the "View" folder for the controller. Example "Views/Documents/Components
2. Create a folder for your component. "Views/Documents/Documents/Components/LatestFileInfo"
3. View Components consist of two parts. A. A view  and B. A class that holds the logic for your component. 
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

  
  5.Add a new .cs file called "LatestFileInfo.cs". It will have a public method called "Invoke" which will be called from your view code. 


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
 
 6 .Create a view in the Components folder. It can be called default and contain the following code.

 ```
 
 @model WebApplication1.ViewModels.LatestFileInfoViewModel
 @Model.FileHistoryDescription
 
 
 ```

7.Add the following to the view where you want the component to display
 
 ```
  @Component.Invoke("LatestFileInfo")
  
 ```


