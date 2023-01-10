using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Logging;
using Services;

namespace DataBase
{
    public class ReadData
    {
        private readonly UserRepo _user;
        public ReadData(UserRepo user)
        {
            _user = user;
        }

        public async Task ReadDataFromJsonAsync(string path = "..\\users.json")
        {

            if (!File.Exists(path))
                throw new Exception("Path incorrect");

            string jsonText = File.ReadAllText(path);
            
            var JsonUsers = JsonSerializer.Deserialize<List<JsonUser>>(jsonText);
            foreach (JsonUser user in JsonUsers)
            {
                User NewUSER = new User() {
                    Id = int.Parse( user.Id), Name = user.Name, UniversityLocation = user.UniversityLocation,
                    WorkPlace = user.WorkPlace, Field = user.Field,DateOfBirth = user.DateOfBirth,
                };
                await _user.AddUserAsync(NewUSER);
                foreach (var spes in user.Specialties)
                {
                    await _user.AddSpecialityToUserAsync(NewUSER.Id,spes);
                    
                }
                foreach (var conn in user.ConnectionId)
                {
                    await _user.AddOneWayConnectionAsync(NewUSER.Id,int.Parse(conn));
                }
                await _user.SaveChangesAsync();
            }

        }
    }
}
