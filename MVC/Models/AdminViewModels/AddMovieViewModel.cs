using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AdminViewModels
{
    public class AddMovieViewModel
    {
        [Required]
        [DisplayName("Title")]
        [StringLength(75)]
        public string Title { get; set; }

        [Required]
        [DisplayName("RunTime")]
        public int RunTime { get; set; }

        [Required]
        [DisplayName("Image")]
        public string Image { get; set; }

        //An error message:
        public string errorMessage { get; set; }
    }
}