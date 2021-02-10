using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson1
{


    public class Exercise2 : PageModel
    {
        public enum Direction
        {
            Left, Right, Forward, Backward
        }

        [BindProperty(SupportsGet = true, Name = "direction")]
        public List<Direction> Directions { get; set; } = new List<Direction>();

        public string GetDirectionParameters()
        {
            string result = "";

            foreach (var direction in Directions)
            {
                result += $"direction={direction}&";
            }

            return result;
        }

        public void OnGet(/*[FromQuery] string[] direction */)
        {
            // b answer
            // foreach (var dir in direction)
            // {
            //     switch (dir.ToLower())
            //     {
            //         case "left": Directions.Add(Direction.Left); break;
            //         case "right": Directions.Add(Direction.Right); break;
            //         case "forward": Directions.Add(Direction.Forward); break;
            //         case "backward": Directions.Add(Direction.Backward); break;
            //         case "clear": Directions.Clear(); return;
            //     }
            // }

            //a answer
            // if (Request.Query.ContainsKey("direction"))
            // {
            //     var directions = Request.Query["direction"];
            //     foreach (var direction in directions)
            //     {
            //         switch (direction.ToLower())
            //         {
            //             case "left": Directions.Add(Direction.Left); break;
            //             case "right": Directions.Add(Direction.Right); break;
            //             case "forward": Directions.Add(Direction.Forward); break;
            //             case "backward": Directions.Add(Direction.Backward); break;
            //             case "clear": Directions.Clear(); return;
            //         }
            //     }
            // }

        }
    }
}
