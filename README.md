# Identity Multi-Tenant POC
Proof of concept integrating IdentityServer with Finbuckle.MultiTenant

If using Azure Ad authentication, make sure to set the Domain, TenantId, ClientId and ClientSecret values in the user secrets file. This is in the IdentityServer project.

Example is configured to use a MSSQL database called IdentityServer on the local machine. This can be changed in the appsettings.json of the IdentityServer project.

When running this for the first time, make you seed the database by running IdentityServer.exe /seed.