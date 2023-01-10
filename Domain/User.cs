using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Domain
{
    public class User
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        public string UniversityLocation { get; set; }

        public string Field { get; set; }

        public string WorkPlace { get; set; }

        public string Profile { get; set; }

        public List<SpecialtyUser> UserSpecialties { get; set; }

    }

    public class JsonUser 
    {
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("universityLocation")]
        public string UniversityLocation { get; set; }

        [JsonPropertyName("field")]
        public string Field { get; set; }

        [JsonPropertyName("workplace")]
        public string WorkPlace { get; set; }

        [JsonPropertyName("connectionId")]
        public List<string> ConnectionId { get; set; }

        [JsonPropertyName("specialties")]
        public List<string> Specialties { get; set; }
    }

    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public List<SpecialtyUser> SpecialtyUsers { get; set; }
    }
    public class SpecialtyUser
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SpecialtyId { get; set; }

        #region Foreign Key
        public User User { get; set; }
        public Specialty Specialty { get; set; }
        #endregion
    }



    public class Connection
    {
        [Key]
        public int Id { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }
    }
}
