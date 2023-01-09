using Linked.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Xml.Linq;
using Linked.Api;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Linq;

namespace CSharpCornerJSONArticle
{
    public class user
    {
        [DisplayName("Upload File")]
        public IFormFile? IMG_ProfileImge { get; set; }
        public string id { get; set; }
        public string dateOfBirth { get; set; }
        public string name { get; set; }
        public string workplace { get; set; }
        public string field { get; set; }
        public string universityLocation { get; set; }
        public List<string> specialties { get; set; }
        public List<string> connectionId { get; set; }
       
        class Program
        {
            static void Main(string[] args)
            {
                List<user> LIST_User = new List<user>();
                string jsonText = File.ReadAllText("..\\..\\..\\..\\Linked\\wwwroot\\forms\\jsonGraph.txt");
                List<user> list_Users2 = new List<user>();
                list_Users2 = JsonSerializer.Deserialize<List<user>>(jsonText);
                foreach(user User_UserItem in list_Users2)
                {
                    Linked.Models.User.user NewUSER = new Linked.Models.User.user() { id = int.Parse(User_UserItem.id), name = User_UserItem.name, universityLocation = User_UserItem.universityLocation, workplace = User_UserItem.workplace, field = User_UserItem.field , specialties = User_UserItem.specialties  };
                    List<Linked.Models.User.user> COnectionId = new List<Linked.Models.User.user>();
                    foreach(string q in User_UserItem.connectionId)
                    {
                      COnectionId.Add(new api().FUNC_SearchUser(int.Parse(q)));
                    }
                    NewUSER.connectionId = COnectionId;
                    new api().FUNC_AddPersonToDataBase(NewUSER);
                }
                
            }
        }
    }
}
