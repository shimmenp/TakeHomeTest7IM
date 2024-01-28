Angular Search App

This is a web application built with Angular and ASP.NET Core. It provides an interface for searching through a list of persons.

Getting Started

Please ensure that you have the following installed on your machine:

Visual Studio 2022
Node.js (npm version 21.6.1)
Angular CLI (version 15.2.8)

Cloning the Repository

Open Visual Studio 2022.
Clone this repository to your local machine:
In Visual Studio, go to File > Clone Repository.
Enter the repository URL: [repository URL]
Specify the local path where you want to clone the repository.
Click Clone.

Running the Application

Open the solution file in Visual Studio.
Build the solution to restore NuGet packages and npm dependencies.
Press F5 or click Debug > Start Debugging to run the application.
The application will open in your default web browser.

Features

Search for persons by name or email.
View search results in a tabular format.
Error handling for search failures and empty search terms.

Code Discussion

API

DataService:
The DataService is responsible for getting the person data from the JSON file. It has been created as a singleton within the program startup of the application so the data is only loaded once. 
This is to improve performance. It was considered using ReadAllTextAsync, and therefore making the data retrieval asynchronous. But after consideration and seeing that the file size is small and 
accessed infrequently, the benefits of asynchronous file reading would be minimal. Therefore, it was decided that the additional complexity of adding asynchronous operations was not needed for this small size. 
Of course, this could be reviewed if the application were to grow in size in the future, and also if other data sources such as a database were to be used. The data service implements the IDataService which 
allowed data retrieval to be separated and tested independently.

SearchService:
The SearchService has a dependency on the IDataService through constructor injection. The SearchPersons method takes a searchTerm, checks that the searchTerm has a value (and if not returns an empty list). 
The data is loaded within this method and then the main search tasks place. All searching is done on a lower case basis. The search takes place over first name, last name, and email. The initial requirements 
did not state to search gender so that was not included. The name search requirements needed to search across the full name, so the first name and second name were concatenated together and then the search term 
was used within the search. The email search just uses the single field within the data. SearchService tests cover all the scenarios in the initial requirements. The specific data mentioned in the initial 
requirements has been used so that all scenarios are covered.

SearchController:
The search controller API has one method and is accessible from {rootUrl}api/search with the searchTerm query parameter. The search controller has a dependency upon the SearchService interface and the ILogger 
(NLog has been configured in this application) again via constructor injection. If any exception is encountered within the search method it is caught, logged via the logger, and a 500 error is returned. 
If no search term is received from the method a bad request status message is returned. After performing the search, via the SearchPersons method within the SearchService, a 200 OK error is returned if any 
matching data is returned, or Not Found 404 is returned if no data is found. These scenarios are all tested within SearchController Tests class.

PersonDto:
The assumption was made that all fields were mandatory to create the PersonDto. Therefore a non-default constructor was created where all fields were needed to create the DTO. Therefore, all properties
only needed getters.

Swagger:
Swagger has been set up and accessible via {rootUrl}/swagger. Additional documentation could be added in the future to help with the use of the API.

NLog:
NLog has been configured to only output exceptions. Information logging could be added and configured if deemed necessary.

Angular App

The Angular app is built with Angular 15.2.8 and node version 21.6.1. Please note that version 21.6.1 should not be used in production (as it is an odd number). Seeing as this app is not intended for production, 
it is deemed that this is not an issue.

The Angular app is designed with the recommended folder structure and a module approach has been undertaken.

Core:
The core folder consists of the core module, where services and models are placed.
Search.service.ts: This service simply calls the .NET API with the provided search term and returns an observable array of the person Dto.

Home:
The home component is used as the initial component that app routing module redirects to within the router-outlet directive, and it is essentially used as the home page of the site which can be used to navigate 
to the search page.

Search:
The search component is responsible for the search within the application. The search only takes place when the user clicks the button. It was considered allowing the user to use the enter button to perform the 
search but as this was not mentioned in the initial requirements it was omitted. Any previously searched data or error messages are removed when there is new user input in the search input box. The (input) event 
is used for this purpose. The searchClicked method uses the search method within the search service and subscribes to retrieve data from the observable. Next and error notifications are used to respond to the 
observable. Next is used when data results are returned and error used when no data was returned (as set up in the API). The search will only take place if data has been entered into the input box. If no data is 
entered and the search clicked, the data and messages are cleared. The entire search component is added to its own module.

Shared:
The shared folder and module were created for anything that may be added and considered to be shared in the future.

App:
The app component mainly consists of the main routing of the site, using bootstrap 5 navbar. Navigation works when the window size is reduced so searches should be possible on mobile devices. The app component 
allows navigation to home and search, via the routing.

App-routing:
The app routing module sets up the routing for the application. No guards were needed as there was no logic/authentication needed to navigate to either home or search. An empty URL navigates to home and an 
additional wildcard search route was added so the user would be redirected to the home page if they entered an invalid path.

Technologies Used

ASP.NET Core
Angular 15.2.8
Bootstrap 5
Swagger
NLog
XUnit
Moq
FluentAssertions
