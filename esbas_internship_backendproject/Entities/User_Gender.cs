﻿namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User_Gender
    {
#nullable disable
        [Key]
        public int User_GenderID { get; set; }

        public string User_Gender_Name { get; set; }

        public bool Status { get; set; } = true;

        public User_Gender() { }
    }
}
