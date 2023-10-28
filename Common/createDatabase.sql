-- Create Databases
CREATE DATABASE IF NOT EXISTS dmsfrebus;
DROP DATABASE IF EXISTS dmsfnotification;
CREATE DATABASE dmsfnotification;

-- Setup Notification Service Database
USE dmsfnotification;

CREATE TABLE notifications
(
    id             varchar(36) primary key default (UUID()),
    type           int,
    contained_data blob,
    vault_id       varchar(36)
);

-- Test Data
INSERT INTO notifications
values ('a4ec716b-7589-11ee-b812-ac74b1445b97', '0',
        '"{""From"":""string"",""To"":""string"",""Cc"":""string"",""Bcc"":""string"",""Subject"":""string"",""Body"":""string"",""NotificationType"":0,""Id"":""66bb930a-48f1-46c0-8ccc-97723254256c""}"',
        'b073e2c0-7d61-4e52-9d35-9828b812600f');