-- Create Databases
CREATE DATABASE IF NOT EXISTS dmsfrebus;
DROP DATABASE IF EXISTS dmsfnotification;
CREATE DATABASE dmsfnotification;

-- Setup Notification Service Database
USE dmsfnotification;

CREATE TABLE notifications
(
    id            varchar(32) primary key,
    type          int,
    containedData blob,
    vaultId       varchar(32)
);

-- Test Data
INSERT INTO notifications
values ('392F54F794D74E59A42D27701675749D', 0, NULL, '392F54F794D74E59A42D27701675749D');