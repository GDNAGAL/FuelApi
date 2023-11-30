using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace FuelApi.Model
{
    public class DriverDetails
    {
        [Key]
        public string? DriverID { get; set; }
        
        public string? FirstName { get; set; }
       
        public string? LastName { get; set; }
        
        public string? FullName { get; set; }
        
        public DateTime DOB { get; set; }
        
        public int LicenseType { get; set; }
        
        public string? LicenseNo { get; set; }
        
        public DateTime LicenseExpiryDate { get; set; }
        
        public string? ContactNo { get; set; }
        
        public string? AltContactNo { get; set; }
        public string? Email { get; set; }
        
        public string? Address { get; set; }
        
        public string? City { get; set; }
        
        public string? State { get; set; }
       
        public int ZipCode { get; set; }
        
        public string? Country { get; set; }


    }
}
