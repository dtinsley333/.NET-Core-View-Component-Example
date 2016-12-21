using System;
using System.Collections.Generic;
using System.Linq;

using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;

namespace WebApplication1.ViewModels
{
    public class DocumentDetailsViewModel
    {
        [Required]
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Please give your document a name.")]
        [StringLength(100, ErrorMessage = "Limit the File Name to less than 250 characters.")]
        public string Title { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public byte[] Contents { get; set; }
        public string ContentType { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public string UploadUserId { get; set; }
        public List<DocumentIndexDetails> Documents { get; set; }
        
        [FileExtensions(Extensions = "JPG,jpg,jpeg,gif,png,pdf")]
        [Required(ErrorMessage = "Please select an image or pdf file")]
        public IFormFile File { get; set; }

    }
}