# Music_SQL_App

This project is a music player app built using C# and MySQL. The app allows users to search for songs in the database and insert new records.

# Project ER Diagram: ERDiagram.jpg

# Prohect Demo: AnSQLDatabaseAlbums.gif


Project Demo
## Getting Started

To get started with this project, you will need to have MySQL installed on your computer..

Once you have MySQL installed, you can create the first database by running the following SQL queries:

– Create the first database CREATE DATABASE music_player;

– Use the music_player database USE music_player;

– Create the songs table CREATE TABLE songs ( id INT AUTO_INCREMENT PRIMARY KEY, title VARCHAR(255) NOT NULL, artist VARCHAR(255) NOT NULL, album VARCHAR(255), genre VARCHAR(255), year INT );


After creating the first database and running these SQL queries, you can then build the front-end starter using Windows Forms app in C#. This will allow you to connect your app to the database.

## Connecting to the Database

To connect this app to the database, you will need to use a connection string. Here's an example of what your connection string might look like:

string connectionString = “server=localhost;user=root;database=music_player;port=3306;password=YOUR_PASSWORD”;


Make sure to replace `YOUR_PASSWORD` with your actual MySQL password.

Once you have connected your app to the database, you can then create a built-in function that allows users to search for songs in the database. You can do this by writing SQL queries that search for songs based on their title, artist, album, genre or year.

## Inserting New Records

In addition to allowing users to search for songs in the database, this app allows an option for inserting new records. You can do this by creating a form that allows users to enter information about a new song and then inserting that information into the `songs` table.

Here's an example of what an INSERT query might look like:
INSERT INTO songs (title,  artist, album, genre, year) VALUES ('Song Title', 'Artist Name', 'Album Name', 'Genre', 2021);

## Deleting Records

The app also provides an option for deleting records from the database. Users can select a song and delete it from the `songs` table.

## Creating Another Database

After creating the first database and building the front-end starter for the app, another database was created using MySQL Workbench. This allowed for the creation of a foreign key relationship between the two databases.

To create this foreign key relationship between the two databases, tables were created in both databases that have matching columns. For example, if there is a `songs` table in the first database and an `artists` table in the second database, a foreign key relationship between these two tables was created by adding an `artist_id` column to the `songs` table and referencing it in the `artists` table.

Once this foreign key relationship between these two tables was created, they were joined together using JOIN queries. This allowed for data retrieval from both tables at once.

## Running Compound Queries

In addition to running simple SELECT queries on these databases, compound queries that combine multiple operations into one query were also run. For example, UNION or INTERSECT operators were used to combine multiple SELECT statements into one query.

## Conclusion

In conclusion, this project demonstrates how to build a music player app using C# and MySQL. The app allows users to search for songs in the database,

