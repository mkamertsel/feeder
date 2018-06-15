# Welcome to Feeder!!

## Launch application

To run the application in visual studio, set the solution startup project as a "Multiple Startup Project" and select Feeder.proj and Web.proj.

To run unit tests just right button click on solution and pick "Run Unit Tests" (ReSharper needed)

To change db connection string use Web.config in section       <add key="feederConnection" value="local"/>. Default value is 'local'.

To change base adress of Web.Api controls use Web.config in section       <add key="httpBaseAdress" value="http://localhost:51790/api/"/>. Default value is 'http://localhost:51790/api/'.

## Used extensions:
- Caliburn Micro
- Simple Injector
- NUnit
- Moq
- Extended WPF Toolkit (Busy Indicator)
- System.Reactive 

## Projects description
- Configuration: contains processing of application settings specified in the configuration file
- Core: contains general data, entity models used in the application
- CustomClient: contains HttpClient and Avaliable Actions to connect services
- Dal: contains mappers and repositories to work with DB
- DB: contains entity models which are used to work with external DB
- Feeder: WPF application
- Services: contains services which are call CustomClient to get data
- Web: contains Web Api controllers which are provide data from Dal to Services 


