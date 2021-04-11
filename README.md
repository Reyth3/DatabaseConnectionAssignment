# Database Connection Assignment

This is a simple Windows application that pulls data about the table structure from an MSSQL database and displats it to the user.

# Requirements

* A WPF client application with a login form.
* A way to load column data into a WPF control.

# Technical specifications

I decided to go with an approcach I would usually take when making client apps that have to connect to the database - even though this is just a proof-of-concept task, I wanted to keep some of the real-world concerns out of this project:

* ***Do not expose the connection string to the end-user*** - in this case the user credentials are equivalent to the database credentials, but I still put effort into hiding the actual host info from the user by implementing a server-side API to handle database transactions.
* ***Keep the client app maintainable*** - I implemented the MVVM pattern for the client app to keep the code readable and scalable.
* ***Keep the API maintainable*** - I used the MediatR library to separate the data access logic from the API, and followed the SOLID principles to provide a scalable code-base.