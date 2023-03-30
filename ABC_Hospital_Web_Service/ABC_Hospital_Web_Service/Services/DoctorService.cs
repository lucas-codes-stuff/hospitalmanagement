﻿using ABC_Hospital_Web_Service.Controllers;
using ABC_Hospital_Web_Service.Models;
using System.Text.Json;

namespace ABC_Hospital_Web_Service.Services
{
    public class DoctorService
    {
        private SQLInterface _sqlservice;
        private bool formatJson;
        private readonly ILogger<UserController> _logger;

        public DoctorService(ILogger<UserController> logger, bool format_json = true)
        {
            _sqlservice = new SQLInterface();
            formatJson = format_json;
            _logger = logger;
        }

        public string GetDoctorInfo(string userName)
        {
            // Prepare filter field and value
            string fieldName = "Doctor_Username";
            string filterValue = userName.ToLower();

            // Get Doctor from SQL Service
            DoctorObject doctor = _sqlservice.RetrieveDoctorsFiltered(fieldName, filterValue)[0];

            // Get Doctor's user data from SQL Service
            UserObject temp = _sqlservice.RetrieveUsersFiltered("Username", filterValue)[0];

            // Merge Data
            doctor.Account_Type = temp.Account_Type;
            doctor.Name = temp.Name;
            doctor.Birth_Date = temp.Birth_Date;
            doctor.Gender = temp.Gender;
            doctor.Address = temp.Address;
            doctor.Phone_Number = temp.Phone_Number;
            doctor.Email_Address = temp.Email_Address;
            doctor.Emergency_Contact_Name = temp.Emergency_Contact_Name;
            doctor.Emergency_Contact_Number = temp.Emergency_Contact_Number;
            doctor.Date_Created = temp.Date_Created;

            // Convert Doctor to JSON
            string doctorJson = JsonSerializer.Serialize<DoctorObject>(doctor, new JsonSerializerOptions() { WriteIndented = formatJson });

            return doctorJson;
        }
    }
}
