# Identity Server 4 POC
There are three .net core projects under the Quickstart solution.

IdentityServer: this is the Identity Server project, which is used as an Identity Provider for the API and UI app.

Api: this is an API which is protected by the authentication feature of IdentityServer project. It uses Bearer token for authentication.


Syncriver: this is a sample UI which allows users to login and logout using the IdentityServer.
This UI demonestrates how to make API call by using the access token from the authentication provider, i.e IdentityServer.
