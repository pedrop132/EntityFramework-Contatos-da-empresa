﻿namespace Empresa.Models
{
    public class Configs
    {
        //DENY SELECT, INSERT, UPDATE, DELETE ON Configs TO Public; //Who is public?
        public int AccountId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
