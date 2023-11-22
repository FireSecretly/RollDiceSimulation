
USE DiceSimulation;

--  Create the database too
CREATE DATABASE DiceSimulation;
GO

CREATE TABLE DiceRolls (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumberOfDice INT NOT NULL
);

CREATE TABLE DiceRollResults (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DiceRollId INT NOT NULL,
    Result INT NOT NULL,
    FOREIGN KEY (DiceRollId) REFERENCES DiceRolls(Id)
);