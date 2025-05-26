CREATE DATABASE SETrendzzDB;
GO

USE SETrendzzDB;
GO

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);

INSERT INTO Roles (RoleName) VALUES ('Teacher'), ('Student');


CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    RoleID INT NOT NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

INSERT INTO Roles (RoleName) VALUES ('Admin');


INSERT INTO Users (FullName, Email, PasswordHash, RoleID)
VALUES ('Admin', 'AdminEmail@gmail.com', 'SlF1sFlnnCrP9NdMzANQ+S7hxOVzML9RpxeFjzmmcXg=', 3);

UPDATE Users
SET PasswordHash = 'Y5zdpw0AV4XH3N0smtVknWSsQbrGBPlQkUsjPnwR0x8='
WHERE Email = 'AdminEmail@gmail.com';




SELECT * FROM Users;

------------
-- Update Users Table
ALTER TABLE Users
ADD ProfilePicture NVARCHAR(200),  -- store image URL or path
    Bio NVARCHAR(300);             -- user’s bio

-- New Table for Follow System
CREATE TABLE Followers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FollowerID INT,     -- who follows
    FollowingID INT,    -- who is being followed
    FOREIGN KEY (FollowerID) REFERENCES Users(UserID),
    FOREIGN KEY (FollowingID) REFERENCES Users(UserID)
);

-- New Table for Posts
CREATE TABLE Posts (
    PostID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    Title NVARCHAR(100),
    Content TEXT,
    ImageURL NVARCHAR(200),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

