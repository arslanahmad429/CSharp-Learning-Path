CREATE DATABASE EventDB;
GO
USE EventDB;
GO

CREATE TABLE Events (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100),
    Category NVARCHAR(50),
    Date DATETIME,
    Venue NVARCHAR(100)
);

INSERT INTO Events (Title, Category, Date, Venue) VALUES ('Annual Tech Fest', 'Technology', '2026-12-15', 'Main Auditorium');
INSERT INTO Events (Title, Category, Date, Venue) VALUES ('Sports Week', 'Sports', '2026-11-20', 'University Ground');
INSERT INTO Events (Title, Category, Date, Venue) VALUES ('Book Fair', 'Academic', '2026-11-10', 'Library Hall');
