# .NET Console Demos: Token verification / Authorization

This demo shows you how to:
* Make a simple API call using the Google API client.
* Make an authorized API call using the Google API client.
* Authorize a user for Google+ app activities

All using the Google API client library from .NET console applications.


## Running the GoogleDotNetDemo

1. Open the project solution in Visual Studio
2. Right click the solution and select Manage NuGet Packages
3. Restore the packages
4. Press F5 to build and run the app
5. Copy and paste an access token into the console app
6. Press enter and the token info should be displayed

## Running the GoogleAuthDemo

1. Open the project solution in Visual Studio
2. Right click the solution and select Manage NuGet Packages
3. Restore the packages
4. Update the client ID and secret with new credentials from the [Google APIs console](https://code.google.com/apis/console)
5. Select the GoogleAuthDemo app from the Visual Studio launch/debug icon
6. Press F5 to build and run the app
7. Sign in when the app starts

## Running the AuthWithAppActivities demo

1. Open the project solution in Visual Studio
2. Right click the solution and select Manage NuGet Packages
3. Restore the packages
4. Update the client ID and secret with new credentials from the [Google APIs console](https://code.google.com/apis/console)
5. Select the AuthWithAppActivities app from the Visual Studio launch/debug icon
6. Press F5 to build and run the app
7. Sign in when the app starts, you shuold see the bottom dialog box indicating app activity permissions are requested.
8. After you authorize the app, the app will write app activities to Google and upon success show the result.

## Running the ServiceAccountDemo

1. Open the project solution in Visual Studio with .NET 4.5 or better
2. Right click the solution and select Manage NuGet Packages
3. Restore the packages
4. Update the service account email and certificate with new credentials from the [Google APIs console](https://code.google.com/apis/console)
5. Press F5 to run the sample
6. The app will retrieve authorization credentials and will verify the tokens against the OAuth 2.0 server
