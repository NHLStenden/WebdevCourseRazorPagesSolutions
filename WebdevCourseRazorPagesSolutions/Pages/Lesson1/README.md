# State Management

State Management is erg belangrijk voor website ontwikkelaars. Daarom gaan we er uitgebreid mee oefenen.
We maken gebruik van een Content Page (.cshtml) i.c.m een Page Model (.cs.cshtml), zodat we de html en logica kunnen scheiden.
In de Content Page (view) gebruiken we Razor. Razor is C# gecombineerd met html. 
1. [Razor Pages](https://www.learnrazorpages.com/razor-pages)
2. [Razor Syntax Overview](https://www.mikesdotnetting.com/article/153/inline-razor-syntax-overview)
3. [State Management In Razor Pages](https://www.learnrazorpages.com/razor-pages/state-management)

## Opdracht 1 QueryString - Scorebord 

Het idee is om een simpel scorebord te maken. 
De toestand (state) wordt bijgehouden in de QueryString.

De cshtml structuur om mee te beginnen (overal waar ``###`` moet Razor code komen te staan): 
```razor
@page
@model Exercises.Pages.Lesson1.Assignment1

@{
    Layout = null;
}

<div>
    <p>
        <span id="scoreHome">###</span> - <span id="scoreAway">###</span>
    </p>
    <div>
        <a id="incrementHome" href="###">Home ++</a>
        <a id="decrementHome" href="###">Home --</a>

        <a id="incrementAway" href="###">Away ++</a>
        <a id="decrementAway" href="###">Away --</a>
    </div>
    <div>
        <a id="reset" href="###">Reset</a>
    </div>
</div>
```
Er zijn drie manier om te werken met querystring, pas elke manier toe:
1. ``Request.Query["someKey"]``  vergeet niet te controleren of de key bestaat!
2. Het meegeven van de input als Parameter in de methode die aangeroepen wordt, zoals in: ``public void OnGet([FromQuery] string a, [FromQuery] string b)``.
Het is niet perse noodzakelijk om ``[FromQuery]`` maar het geeft wel duidelijk de intentie weer!
3. Het gebruik van bind property attribuut (annotatie) 
```c#
[BindProperty(SupportsGet = true, Name = "awayScore")]
public int Away { get; set; }
```

## Opdracht 2 QueryString - Gaan we Links of gaan we Rechts

Het idee is dat we een route gaan bijhouden die we hebben afgelegd. 
Iedere keer als we afslaan klikken we op de desbtreffende link (a).
We willen dus de gehele route bijhouden, dus meerdere waarden per query parameter.
De truc om meerdere waarden door te geven is als volgt: ``<a href='?a=x&a=y>...''``.

De cshtml structuur om mee te beginnen staat hieronder, overal waar ``###`` moet worden aangevuld:

```razor
@{
    //hier maak ik de queryString, door een methode op de 
}

<a href="###">Left</a>
<a href="###">Right</a>
<a href="###">Forward</a>
<a href="###">Backward</a>

<hr>
<a href="###">Clear</a>

<hr>

<ul id="route">
    <!-- hieronder staat een voorbeeld route, om de test zijn case sensetive (dus Left en niet left) -->

    <li>Left</li>
    <li>Right</li>
    <li>Forward</li>
    <li>Backward</li>
</ul>
```

## Opdracht 3 Rekenmachine

Maak een simpele rekenmachine. Dit keer gaan we een form (POST) gebruiken 
met hidden input om het tussenresultaat te onthouden. 
Een rekenmachine heeft meerdere knoppen daarvoor zijn handlers handig in gebruikt. 
De "startwaarde" is 0.

[hidden form fields](https://www.learnrazorpages.com/razor-pages/state-management#hidden-form-fields)
[Handlers](https://www.learnrazorpages.com/razor-pages/handler-methods)

```razor
...
<div>
    <p id="result">###</p>
</div>

<form method="post" id="calculatorForm">
    <input ### type="hidden" value="###">
    <input ### type="text" value="###">
    <button id="addBtn" type="submit" ###>+</button>
    <button id="subBtn" type="submit" ###>-</button>
    <button id="mulBtn" type="submit" ###>*</button>
    <button id="divBtn" type="submit" ###>/</button>
</form>
```

*Let op:* BindProprety maakt gebruik van Model Binding, en het aanpassen van de waarde in de Page Model heeft geen effect in een POST! In een GET Request werkt het wel! 
:-(. Dit heeft mij erg veel tijd (fustratie) gekost om hier achter te komen dus onthoud dit!
[zie uitleg + slechte workaround, liever niet gebruiken](https://stackoverflow.com/questions/53669863/change-bound-property-in-onpost-in-model-is-invalid/53675887#53675887)
Zorgt er dus voor dat je zelf de value zet van de input's!
Model validatie is super handig, maar dat is een onderwerp voor volgende week!

Indien we delen door 0 willen we een ``BadRequest("Delen door nul is niet toegestaan")`` response teruggeven en anders de ``Page()``.
[Action Result](https://www.learnrazorpages.com/razor-pages/action-results)
[IActionResult Type](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0#iactionresult-type)
 
## Opdracht 4 - Route Data 
We kunnen gegevens meegeven als Route Data.
We willen een pagina kunnen aanroepen met de volgende url: ``/category/subcategory/productId`` structuur.
1. subcategory is optioneel. Als deze leeg is druk dan ``"Geen subcategory`` af in de ``<h2 id="subCategoryHeading">``
2. productId is optioneel. Als deze leeg is druk dan ``"Geen productId"`` af in de ``<h3 id="productIdHeading">``.
Category & subcategory zijn strings, en productId moet een getal zijn groter dan 0.
[Route Data](https://www.learnrazorpages.com/razor-pages/routing#route-data)

Gebruik het onderstaande startpunt:

```razor
<h1 id="categoryHeading">###</h1>
<h2 id="subCategoryHeading">###</h2>
<h3 id="productIdHeading">###</h3>
```

Maak een *Custom route constraints* die controleert of category en subcategory de volgende structuur hebben "cat{number}" of "sub{nunber}".
B.v. cat23, subcat100.
Zie voor een voorbeeld (even naar beneden scrollen):
[Custom route constraints](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-5.0#route-template-reference)

## Cookies

Er zijn drie soorten knoppen die een gebruiker kan indrukken over zijn gemoedstoestand, namelijk:
blij, teleurgesteld, boos. Hoe vaak een bepaalde knop is ingedrukt willen we graag bijhouden in een Cookie en natuurljk weergeven aan de gebruiker. 

[Cookies](https://www.learnrazorpages.com/razor-pages/cookies)
[What Are Cookies? And How They Work | Explained for Beginners!](https://www.youtube.com/watch?v=rdVPflECed8&ab_channel=CreateaProWebsite)





Probeer alles in 1 cookie op te slaan d.m.v. een object.
```c#
public class MoodCounter {
    public int Happy { get; set; }
    public int Disappointed { get; set; }
    public int Angry { get; set; }
}
```

## Sessies

  


## Bruiden - Sessie en Cookies
Voor deze opdracht (bruidenopdracht) is het handig om een nieuwe razor applicatie aan te maken (zonder authenticatie).

Het idee is om een simpel login systeem te maken, voor bruiden en hun gasten.
De bruiden moeten de locatie en trouwdatum kunnen opgeven en gasten moeten dit kunnen zien.
 
Het idee is dat we een Cookie aanmaken met de userid die een unieke unieke code bevat. 
Een unieke code kunnen we generen met ``Guid.New()``.  
Er zijn twee rollen (bruidspaar, bezoeker).

Maak de volgende webpagina's (Razor Pages):
1. Register.cshtml - Registratie pagina (username, password, rol (bruidspaar of gast) ). 
Een user kunnen we opslaan in database, echter dit hebben we nog niet behandeld.
Een truc is een static variabele te gebruiken, dan blijven de gegevens (Users)  bestaan zoalang de server draait.
Om dit te faciliteren is er de klasse ``StaticUserRepo`` gemaakt, die je kan gebruiken! 
Om een user toe te voegen, ``StaticUserRepo.AddUser(userid, user)``.

2. Login.cshtml - Inlog Pagina, als de gebruiker een geldige password/username combinatie opgeeft dan wordt hij ingelogd.
Deze pagina deze zet de userid cookie dan, als je succesvol bent ingelogd wordt je geredirect naar Overview.cshtml.
3. Logout.cshtml - Uitlog Pagina, deze verwijderd de cookie. Laat de gebruiker weten dat hij succesvol uit uitgelogt.

4. Overview.cshtml - Overzichtspagina 
..* als bruidpaar zie je hier je trouwdatum, locatie staan, echter als deze nog niet zijn ingevuld wordt je geredirect naar pagina AddWeddingDate.cshtml. 
..* als gast zie je een pagina met de tekst 'Fijne bruiloft'
Ophalen van de gebruikersgevens kan als volgt: ``StaticUserRepo.GetUser(userid)``. 
 
5. AddWeddingDate.cshtml - Op deze pagina kan de bruidspaar zijn trouwdatum opgeven. Dit wordt tijdelijke opgeslagen in een sessie variabele. Daarna wordt het bruidspaar doorgestuurd naar AddLocation.cshtml.
6. AddLocation.cshtml - Op deze pagina kan het bruidspaar zijn trouwlocatie opgeven. Dit wordt tijdelijke opgeslagen in een sessie variabele. Daarna wordt het bruidspaar doorgestuurd naar Confirm.cshtml.
7. Confirm.cshtml - De locatie en datum worden weergegeven.
..* Het bruidspaar kan de confirm knop drukken, dan worden de voorkeuren (locatie en datum) opgeslagen.
   Dit kan je doen door een gebruiker op te halen met ``GetUser()`` en vervolgens de properties aan te passen (Location, Date). 
   Het **opslaan** gat hier automatisch, met een echte database zoals later zal blijken niet! 
   De sessie variabele(n) moet(en) verwijderd worden.
..* Indien het bruidspaar cancel knop indrukt, dan wordt het bruidspaar terug gestuurd naar pagina AddWeddingDate.cshtml.

De volgende pagina's:
Overview.cshtml, AddWeddingDate.cshtml, AddLocation.cshtml, Confirm.cshtml 
zijn alleen beschikbaar voor ingelogde gebruikers, anders redirecten naar Login.cshtml.


