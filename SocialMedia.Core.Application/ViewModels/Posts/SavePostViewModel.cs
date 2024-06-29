using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Posts
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string? VisualContentPath { get; set; }
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }
        [DataType(DataType.Text)]
        public string? VisualContentType { get; set; }
        
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? UserId { get; set; }

    }
}
