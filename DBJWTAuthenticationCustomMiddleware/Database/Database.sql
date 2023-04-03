
CREATE DATABASE IF NOT EXISTS Authentication;

USE Authentication;

CREATE TABLE
    IF NOT EXISTS roles (
        roleId INT AUTO_INCREMENT PRIMARY KEY,
        roleName VARCHAR(20) UNIQUE
    );

CREATE TABLE
    IF NOT EXISTS users (
        userId INT AUTO_INCREMENT PRIMARY KEY,
        firstName VARCHAR(20) NOT NULL,
        lastName VARCHAR(20) NOT NULL,
        userName VARCHAR(20) NOT NULL UNIQUE,
        password VARCHAR(20) NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS userRoles(
        userId INT,
        roleId INT,
        CONSTRAINT fk_Users FOREIGN KEY(userId) REFERENCES users(userId) ON UPDATE CASCADE ON DELETE CASCADE,
        CONSTRAINT fk_Roles FOREIGN KEY(roleId) REFERENCES roles(roleId) ON UPDATE CASCADE ON DELETE CASCADE
    );
INSERT INTO roles(roleName)

VALUES ('Admin'), ('User'), ('Distrubutor');

INSERT INTO
    users(
        firstName,
        lastName,
        userName,
        password
    )
VALUES (
        'Sahil',
        'Mankar',
        'sahil',
        'sahil123'
    ), (
        'Abhay',
        'Navle',
        'abhay',
        'abhay123'
    ), (
        'Shubham',
        'Teli',
        'shubham',
        'shubham123'
    );


INSERT INTO userRoles(userId,roleId) VALUES (1,1),(1,2),(2,2),(3,3);
SELECT * FROM roles;

SELECT * FROM users;

SELECT * FROM userRoles;


SELECT roles.roleName from roles INNER JOIN userRoles ON roles.roleId=userRoles.roleId 
INNER JOIN users ON users.userId=userRoles.userId where users.userId=3;