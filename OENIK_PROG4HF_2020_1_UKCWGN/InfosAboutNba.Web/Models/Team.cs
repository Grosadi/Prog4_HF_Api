using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfosAboutNba.Web.Models
{
    // FORM MODEL
    public class Team
    {
        [Display(Name = "Team ID")]
        [Required]
        public int ID { get; set; }

        [Display(Name = "Team's Fantasy Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Display(Name = "Team's Hometown")]
        [Required]
        [StringLength(100)]
        public string HomeTown { get; set; }

        [Display(Name = "Year of Fundation")]
        [Required]
        public int Found { get; set; }

        [Display(Name = "Win Percentage since Foundation")]
        [Required]
        public double WinPercentageSinceFounded { get; set; }

        [Display(Name = "Win Percentage in Season")]
        [Required]
        public double WinPercentageInSeason { get; set; }

        [Display(Name = "Number of Championships")]
        [Required]
        public int NumberOfChampionships { get; set; }
    }
}