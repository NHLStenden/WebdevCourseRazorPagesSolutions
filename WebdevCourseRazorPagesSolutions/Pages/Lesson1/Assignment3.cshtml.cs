using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson1
{
    public class Assignment3 : PageModel
    {
        [BindProperty]
        public decimal Value { get; set; }

        [BindProperty]
        public decimal Input { get; set; }

        public void OnGet()
        {

        }

        public void OnPostAdd()
        {
            Value += Input;
        }

        public void OnPostSub()
        {
            Value -= Input;
        }

        public void OnPostMul()
        {
            Value *= Input;
        }

        public IActionResult OnPostDiv()
        {
            if (Input == 0)
            {
                return BadRequest("Delen door nul is niet toegestaan");
            }
            Value /= Input;
            return Page();
        }
    }
}
