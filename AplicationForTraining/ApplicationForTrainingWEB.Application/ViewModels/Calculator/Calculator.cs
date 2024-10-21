using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForTrainingWEB.Application.ViewModels.Calculator
{
    public class Calculator
    {
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        public List<string> Genders { get; set; } = new List<string> { "Man", "Woman" };
        [Display(Name = "Objective")]
        public string Goal { get; set; }
        public List<string> Goals { get; set; } = new List<string> { "Reduction", "Maintenance", "Mass" };
        [Display(Name = "PAL activity factor")]
        public double Pal { get; set; }
        public List<double> Pals { get; set; } = new List<double> { 1.2, 1.4, 1.6, 1.8, 2.0 };
        public double Weight { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double? CPM { get; set; }
        public double? PPM { get; set; }

    }
}