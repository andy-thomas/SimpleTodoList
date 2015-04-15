ASP.Net MVC; Web API; KnockoutJS; JQueryUI; SignalR

This is a demo application which is based in MVC to display a single page application.
The page uses KnockoutJS to bind the data, which ajax calls to send and receive JSON data.
The AJAX calls go against a ASP.Net Web API controller.
It also uses SignalR to publish updates to all browsers which are running the site.
It is based on Brad Wilson's talk - Microsoft’s Modern Web Stack, Starring ASP.NET Web API,
see https://vimeo.com/43603472
I then added a due date (and made the id a guid, rather than a identity value)
It is intended for use on Visual Studio 2013.
The repository is simply a static List<Task> instance on the Controller; no SQL Server required.