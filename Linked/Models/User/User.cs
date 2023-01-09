using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Linked.Models.User
{
    public class user
    {
        [DisplayName("Upload File")]
        public IFormFile? IMG_ProfileImge { get; set; }
        public int id { get; set; }
        public DateTime? dateOfBirth = new DateTime();
        public string name { get; set; }
        public string workplace { get; set; }
        public string field { get; set; }
        public string universityLocation { get; set; }
        public List<string>? specialties { get; set; }
        public List<user>? connectionId { get; set; }
    }
}
