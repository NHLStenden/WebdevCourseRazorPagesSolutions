using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Exercises.Pages.Lesson1
{
    public class Assignment5 : PageModel
    {
        public class MoodCounter {
            public int Happy { get; set; }
            public int Disappointed { get; set; }
            public int Angry { get; set; }
        }

        public MoodCounter Counter { get; set; }

        public MoodCounter GetCounter()
        {
            if (Request.Cookies.ContainsKey("MoodCounter"))
            {
                return JsonConvert.DeserializeObject<MoodCounter>(Request.Cookies["MoodCounter"]);
            }

            return new MoodCounter();
        }

        public void SaveAsCookie(MoodCounter value)
        {
            Response.Cookies.Append("MoodCounter", JsonConvert.SerializeObject(value));
        }

        public void OnGet()
        {
            Counter = GetCounter();
        }

        public void OnPost(string action)
        {
            Counter = GetCounter();
            switch (action)
            {
                case "Angry": Counter.Angry++; break;
                case "Happy": Counter.Happy++; break;
                case "Disappointed": Counter.Disappointed++; break;
                case "DeleteCookie":
                {
                    Response.Cookies.Delete("MoodCounter");
                    Counter = new MoodCounter();
                    break;
                }
            }

            SaveAsCookie(Counter);
        }


    }
}
