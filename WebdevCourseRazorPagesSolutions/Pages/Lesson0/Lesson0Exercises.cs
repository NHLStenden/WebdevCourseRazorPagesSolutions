using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Exercises.Pages.Lesson0
{
    public class Exercises : CarterModule
    {
        public Exercises()
        {
            Get("/Lesson0/assignment0", Assignment0);

            Get("/Lesson0/assignment1", Assignment1);

            Get("/Lesson0/assignment2", Assignment2);

            Get("/Lesson0/assignment3", Assignment3);

            Get("/Lesson0/assignment4", Assignment4);

            Get("/Lesson0/assignment5", Assignment5Get);

            Post("/Lesson0/assignment5", Assignment5Post);

            //Get("/Lesson1/assignment6", Assignment6Get);
        }

        private Task Assignment0(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.OK;
            response.ContentType = "text/plain";

            response.WriteAsync("Hello World");

            return Task.CompletedTask;
        }

        private Task Assignment1(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.OK;
            request.ContentType = "text/html";

            response.WriteAsync("<h1>Hello World</h1>");

            return Task.CompletedTask;
        }

        /*
         * Met een QueryString is het mogelijk om in de url data mee te geven in een request
         * http://localhost:5000/assignment2?name=test
         * De QueryString is "?name=test". Het vraagteken markeert het begin van de queryString.
         * Vervolgens komt er een key value pair gescheiden door een = teken (key=value).
         * De key is in dit geval name en de value is test.
         *
         * De opdracht is om een request te verwerken en de QueryString te gebruiken om de juiste response te retourneren.
         * Zie ook de bijbehorende test voor het verwachte resultaat.
         *
         * Code Tips:
         *     request.QueryString, request.QueryString.HasValue  request.QueryString.Value,
         *
         *     Handige string methodes:
         *         Split("="), IsNullOrWhiteSpace(...)
         *
         * Zorg ook voor het juiste ContentType, namelijk text/html!
         *
         * http://localhost:5000/assignment2?name=Joris  =>   <h1>Hello Joris</h1>   HttpStatusCode.OK
         * http://localhost:5000/assignment2             =>   <h1>Bad Request</h1>   HttpStatusCode.BadRequest
         * http://localhost:5000/assignment2?name=       =>   <h1>Bad Request</h1>   HttpStatusCode.BadRequest
         */
        private Task Assignment2(HttpRequest request, HttpResponse response)
        {
            response.ContentType = "text/html";

            if (request.QueryString.HasValue)
            {
                var queryString = request.QueryString.Value.Substring(1);

                var parts = queryString.Split("=");
                if (parts.Length == 2 && parts[0] == "name" && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    response.StatusCode = (int) HttpStatusCode.OK;
                    response.WriteAsync($"<h1>Hello {parts[1]}</h1>");
                }
                else
                {
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    response.WriteAsync("<h1>Bad Request</h1>");
                }
            }
            else
            {
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                response.WriteAsync("<h1>Bad Request</h1>");
            }

            return Task.CompletedTask;
        }

        /*
         * Meerdere waardes in dezelfde key (name), ook al is het meestal gebruikelijk maar 1 waarde per key op te sturen!
         * http://localhost:5000/assignment3?name=Joris&name=Lops&age=32 => <ul>Leeftijd: 32 van<li>Joris</li><li>Lops</li></ul>
         */
        private Task Assignment3(HttpRequest request, HttpResponse response)
        {
            response.ContentType = "text/html";

            if (request.Query.ContainsKey("name") && request.Query.ContainsKey("age") &&
                int.TryParse(request.Query["age"][0], out var age))
            {
                response.StatusCode = (int) HttpStatusCode.OK;

                //int age = Convert.ToInt32(request.Query["age"][0]);
                response.WriteAsync("<ul>");
                response.WriteAsync($"Leeftijd: {age} van");
                foreach (var naam in request.Query["name"])
                {
                    response.WriteAsync($"<li>{naam}</li>");
                }

                response.WriteAsync("</ul>");
            }
            else
            {
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                response.WriteAsync("<h1>Bad Request</h1>");
            }

            return Task.CompletedTask;
        }

        /*
         * Als je de volgende URL aanroept dan worden er JavaScripts uitgevoerd.
         * https://localhost:5001/Lesson1/assignment4?somevariable=%3Cscript%3Ealert(%27script%20from%20url%27);%3C/script%3E
         *
         * Dit is zeer gevaarlijk (xss attach, zie https://owasp.org/www-community/attacks/xss/).
         *
         * Dit kan voorkomen worden door ...Encode() en ..Decode() methodes te gebruiken van WebUtility class.
         * Deze methodes kan je als volgt aanroepen:   WebUtility.HtmlEncode(...)

         * Zorg ervoor dat de scripts niet meer uitgevoerd kunnen worden! Door de juiste methodes
         * van WebUtility class te gebruiken.
         *
         *
         */
        private Task Assignment4(HttpRequest request, HttpResponse response)
        {
            //startpunt studenten
            // response.ContentType = "text/html";
            // response.StatusCode = (int) HttpStatusCode.OK;
            //
            // if (request.Query.ContainsKey("somevariable"))
            // {
            //     response.WriteAsync(request.Query["somevariable"]);
            // }
            //
            // response.WriteAsync("<script>alert('javascript from code');</script>");
            //
            // return Task.CompletedTask;

            //oplossing
            response.ContentType = "text/html";
            response.StatusCode = (int) HttpStatusCode.OK;

            if (request.Query.ContainsKey("somevariable"))
                response.WriteAsync(WebUtility.HtmlEncode(WebUtility.UrlDecode(request.Query["somevariable"]))
                    );



            //response.WriteAsync("<script>alert('javascript from code');</script>");

            return Task.CompletedTask;
        }

        /*
         * De get request geeft een formulier weer op het scherm
         *
         * Dit formulier heeft als input drie textboxen, 1 voor voornaam, 1 voor achternaam, 1 voor leeftijd.
         * En natuurlijk een verzend (submit) knop met als tekst Verzenden.
         * De textboxes hebben respectiefelijk het name-attribute firstname, lastname, age
         * Dus b.v. <input name="firstname" type="text" />
         *
         * Het formulier moet gebruik maken van de POST methode, deze roept de Post Handler aan. Deze verwerkt de input.
         * Je kan bij de verzonden data van een "POST-formulier" met request.Form property, b.v. request.Form["firstname"].First().
         * De First() methode haalt de eerste waarde op, het is mogelijk om meerdere waarden te versturen (b.v. checkbox).
         * In het geval van 1 waarde is er dus First() aanroep nodig.
         *
         * De input moet gecontroleerd worden op correctheid en er mogen geen script worden uitgevoerd.
         * Gebruik hiervoor de methode om WebUtility.HtmlEncode(...) om script en html input te encode!
         *
         * in de browser als een gebruiker (hacker) dit intypt in 1 van de tekstboxes.
         * Age moet daarnaast worden gecontroleerd of dat het een geheel getal is.
         * Voornaam en Achternaam moeten minimaal 2 karaketers zijn (check ook of de input leeg is).
         * Als er een fout in de pagina zit moet het formulier opnieuw weergegeven worden met foutmelding(en) "Ongeldige input" achter de desbetreffende input.
         * De ingevoerde waardes moeten blijven staan in het formulier.
         * (je moet dan opnieuw het formulier retourneren, tip maak een functie voor het weergeven van het formulier).
         *
         * Als er geen fouten weergegeven worden dan moet de output als volgt zijn:
         * $"<h1>{lastName}, {firstName} is {age} jaren oud</h1>"
         */
        private Task Assignment5Get(HttpRequest request, HttpResponse response)
        {
            response.ContentType = "text/html";
            response.StatusCode = (int) HttpStatusCode.OK;

            response.WriteAsync(FormAssignment5());

            return Task.CompletedTask;
        }

        private Task Assignment5Post(HttpRequest request, HttpResponse response)
        {
            response.ContentType = "text/html";
            response.StatusCode = (int) HttpStatusCode.OK;

            var firstNameError = string.Empty;
            var lastNameError = string.Empty;
            var ageError = string.Empty;

            var error = false;

            if (!request.Form.ContainsKey("firstname") || request.Form["firstname"].First().Trim().Length <= 2)
            {
                firstNameError = "Ongeldige input";
                error = true;
            }

            if (!request.Form.ContainsKey("lastname") || request.Form["lastname"].First().Trim().Length <= 2)
            {
                lastNameError = "Ongeldige input";
                error = true;
            }

            int ageValue = 0;
            if (!request.Form.ContainsKey("age") || !int.TryParse(request.Form["age"].First(), out ageValue))
            {
                ageError = "Ongeldige input";
                error = true;
            }

            var firstName = WebUtility.HtmlEncode(request.Form["firstname"].First().Trim());
            var lastName = WebUtility.HtmlEncode(request.Form["lastname"].First().Trim());
            var age = WebUtility.HtmlEncode(request.Form["age"].First().Trim());

            if (error)
            {
                var form = FormAssignment5(firstName, lastName, age, firstNameError, lastNameError, ageError);
                response.WriteAsync(form);
            }
            else
            {
                var result = $"<h1>{lastName}, {firstName} is {age} jaren oud</h1>";
                response.WriteAsync(result);
            }

            return Task.CompletedTask;
        }

        private string FormAssignment5(string firstName = "", string lastName = "", string age = "",
            string firstNameError = "",
            string lastNameError = "", string ageError = "")
        {
            var form = $@"
                        <form method='POST'>
                            Voornaam: <input name='firstname' type='text' value='{firstName}'/>{firstNameError}
                            Achternaam: <input name='lastname' type='text' value='{lastName}'/>{lastNameError}
                            Leeftijd: <input name='age' type='text' value='{age}'/>{ageError}
                            <button type='submit'>Verzenden</button>
                        </form>";
            return form;
        }

        // private Task Assignment6Get(HttpRequest request, HttpResponse response)
        // {
        //
        // }


    }
}
