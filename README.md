## To run project:

## From project directory

- `cd BlogApi`

- (if running for the first time)
  `dotnet ef database update`

- `dotnet run`

Now can send GET request to `http://localhost:5000/api/post` and should see a list of all posts

and/or go to `http://localhost:5000/swagger` to see swagger ui which shows all available endpoints and lets you try them out

## On Azure

https://blogapi.azurewebsites.net/swagger

Resource group name: blogApiGroup
app name: blogApi
valutName: blogApiVault
