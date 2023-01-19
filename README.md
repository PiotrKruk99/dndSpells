# dndSpells

Goal of the project is to create web api that cache spells data from [D&D 5e API](http://www.dnd5eapi.co) and has endpoints for getting data with custom filters and without delay of source api. The source api has great database but works quite slow and can be search directly only with few filters.

Created api should also has ability to store data of logged in user with selected spells etc.

The next step is to create VueJS mobile first application that will use methods of api. It should be fully functional dnd spells helper with ability to search spells with differente filters, log in user, add and remove spells selected by user etc.

# used technologies

Webapi part is based on `.NET 7` and `Sqlite` library. It is tested with xUnit tests and manually with Swagger.

Frontend application is based on `VueJS` and `Axios` for communication with api.
