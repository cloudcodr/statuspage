# StatusPage Web App
Simple StatusPage MVC application. Enables any cloud service to report current status of systems (by group) and  incidents. Includes an ASP.NET MVC frontend and a small API for update.

Uses Bootstrap for rendering. MVC for template.

# Data model
- System. Represents any system, like a website, app, component, element. Hold the status and description. Can be grouped.
- Group. Hold the group of systems. Help to render multiple groups and systems. See MVC Razor (index.cshtml).
- Incident. Represents an un-planned or planned incident.


# API
Update api:token in the web.config to change the REST API token. This is not a secure solution, please modify 'Security/ApiAuthenticationAttribute' if you wish to implement a secure system.
