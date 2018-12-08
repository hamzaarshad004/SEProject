using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SEProject.Models
{

    public class SchoolViewModel
    {
        [Required(ErrorMessage = "Name field can't be null")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "City field can't be null")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address field can't be null")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone No field can't be null")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Input numeric value")]
        [MaxLength(11, ErrorMessage = "Input valid Phone number")]
        [MinLength(11, ErrorMessage = "Input valid Phone number")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Admission Fee field can't be null")]
        [DataType(DataType.Text)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Input numeric value")]
        public string AdmissionFee { get; set; }

        [Required(ErrorMessage = "Fee PG/Middle field can't be null")]
        [DataType(DataType.Text)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Input numeric value")]
        public string FeePG_Middle { get; set; }

        [Required(ErrorMessage = "Fee Matric/O-Level field can't be null")]
        [DataType(DataType.Text)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Input numeric value")]
        public string FeeMatric_Olevels { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Image")]
        public string file { get; set; }

        public int id { get; set; }

        public int Rating { get; set; }

        public List<Rating_Reviews> Ratings_List { get; set; }
    }

    public class Rating_Review
    {
        public int SchoolID { get; set; }

        public string AddedBy { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Review { get; set; }
    }

}