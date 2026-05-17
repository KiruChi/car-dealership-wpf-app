CREATE DATABASE Company_DB DEFAULT CHARACTER SET utf8;
USE Company_DB;

CREATE TABLE `Brand_Model` (
    `ID_brand_model` SMALLINT AUTO_INCREMENT PRIMARY KEY,
    `brand` VARCHAR(15) NOT NULL,
    `model` VARCHAR(15) NOT NULL
);

CREATE TABLE `Color` (
    `ID_color` SMALLINT AUTO_INCREMENT PRIMARY KEY,
    `color` VARCHAR(15) NOT NULL
);

CREATE TABLE `Body` (
    `ID_body` SMALLINT AUTO_INCREMENT PRIMARY KEY,
    `body` VARCHAR(15) NOT NULL
);

CREATE TABLE `Fuel` (
    `ID_fuel` TINYINT AUTO_INCREMENT PRIMARY KEY,
    `fuel` VARCHAR(15) NOT NULL
);

CREATE TABLE `Transmission` (
    `ID_kpp` TINYINT AUTO_INCREMENT PRIMARY KEY,
    `kpp` VARCHAR(15) NOT NULL
);

CREATE TABLE `Interior_Material` (
    `ID_material` SMALLINT AUTO_INCREMENT PRIMARY KEY,
    `material` VARCHAR(15) NOT NULL
);

CREATE TABLE `Client` (
    `ID_client` INT AUTO_INCREMENT PRIMARY KEY,
    `cl_first_name` VARCHAR(15) NOT NULL,
    `cl_middle_name` VARCHAR(20) NOT NULL,
    `cl_last_name` VARCHAR(20) NOT NULL,
    `cl_phone` VARCHAR(10) NOT NULL,
    `cl_sex` ENUM('Male', 'Female') NOT NULL,
    `cl_birthday` DATE,
    `cl_address` VARCHAR(40) NOT NULL,
    `cl_RNOCPP` BIGINT NOT NULL UNIQUE
);

CREATE TABLE `Branch` (
    `ID_branch` INT AUTO_INCREMENT PRIMARY KEY,
    `b_name` VARCHAR(20) NOT NULL,
    `b_address` VARCHAR(40) NOT NULL,
    `b_phone` VARCHAR(10) NOT NULL,
    `b_email` VARCHAR(30) NOT NULL,
    `b_EDRPOU` BIGINT NOT NULL UNIQUE,
    `b_type` Enum('Motor show', 'Service center') NOT NULL
);

CREATE TABLE `Employee` (
    `ID_employee` INT AUTO_INCREMENT PRIMARY KEY,
    `em_first_name` VARCHAR(15) NOT NULL,
    `em_middle_name` VARCHAR(20) NOT NULL,
    `em_last_name` VARCHAR(20) NOT NULL,
    `em_phone` VARCHAR(10) NOT NULL,
    `em_email` VARCHAR(30),
    `em_sex` ENUM('Male', 'Female') NOT NULL,
    `em_birthday` DATE,
    `em_post` VARCHAR(40) NOT NULL,
    `em_address` VARCHAR(40) NOT NULL,
    `em_passport` BIGINT NOT NULL UNIQUE,
    `em_RNOCPP` BIGINT NOT NULL UNIQUE,
    `ID_motor_show` INT NOT NULL,
    FOREIGN KEY (`ID_motor_show`) REFERENCES `Branch`(`ID_branch`)
);

CREATE TABLE `Car` (
    `ID_car` INT AUTO_INCREMENT PRIMARY KEY,
    `car_name` VARCHAR(30) NOT NULL,
    `ID_brand_model` SMALLINT NOT NULL,
    `car_class` ENUM('A', 'B', 'C', 'D', 'E', 'F', 'S', 'M', 'J'), 
    `ID_color` SMALLINT NOT NULL,
    `ID_body` SMALLINT NOT NULL,
    `ID_material` SMALLINT NOT NULL,
    `ID_kpp` TINYINT NOT NULL,
    `car_year` DATE NOT NULL,
    `car_mileage` INT NOT NULL,
    `ID_fuel` TINYINT NOT NULL,
    `car_engine` VARCHAR(20) NOT NULL,
    `car_drive` ENUM('AWD', 'Front', 'Rear', 'Full') NOT NULL,
    `car_seats` TINYINT,
    `car_heat` ENUM('Yes', 'No') NOT NULL,
    `car_air_cond` ENUM('Yes', 'No') NOT NULL,
    `car_discs` ENUM('Factory', 'Custom') NOT NULL,
    `car_rubber` ENUM('Street', 'Off-road', 'Sport', 'All-purpose'),
    `ID_client` INT NOT NULL,
    `car_vin_code` VARCHAR(17) NOT NULL UNIQUE,
    `car_type_sale` ENUM('Purchase', 'Regular') NOT NULL,
    `ID_motor_show` INT NOT NULL,
    `mc_status` ENUM('Available', 'Sold', 'Coming Soon'),
    FOREIGN KEY (`ID_brand_model`) REFERENCES `Brand_Model`(`ID_brand_model`),
    FOREIGN KEY (`ID_color`) REFERENCES `Color`(`ID_color`),
    FOREIGN KEY (`ID_body`) REFERENCES `Body`(`ID_body`),
    FOREIGN KEY (`ID_material`) REFERENCES `Interior_Material`(`ID_material`),
    FOREIGN KEY (`ID_kpp`) REFERENCES `Transmission`(`ID_kpp`),
    FOREIGN KEY (`ID_fuel`) REFERENCES `Fuel`(`ID_fuel`),
    FOREIGN KEY (`ID_client`) REFERENCES `Client`(`ID_client`),
    FOREIGN KEY (`ID_motor_show`) REFERENCES `Branch`(`ID_branch`)
);

CREATE TABLE `Sale` (
    `ID_sale` INT AUTO_INCREMENT PRIMARY KEY,
    `ID_car` INT NOT NULL,
    `ID_client` INT NOT NULL,
    `ID_employee` INT NOT NULL,
    `sa_date` DATE NOT NULL,
    `sa_price` INT NOT NULL,
    FOREIGN KEY (`ID_car`) REFERENCES `Car`(`ID_car`),
    FOREIGN KEY (`ID_client`) REFERENCES `Client`(`ID_client`),
    FOREIGN KEY (`ID_employee`) REFERENCES `Employee`(`ID_employee`)
);

CREATE TABLE `Check` (
    `ID_check` INT AUTO_INCREMENT PRIMARY KEY,
    `ID_car` INT NOT NULL,
    `ID_service_center` INT NOT NULL,
    `ch_date` DATE NOT NULL,
    `ch_price` INT NOT NULL,
    `cp_price` INT NOT NULL,
    FOREIGN KEY (`ID_car`) REFERENCES `Car`(`ID_car`),
    FOREIGN KEY (`ID_service_center`) REFERENCES `Branch`(`ID_branch`)
);

CREATE TABLE Users (
    ID_User INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(40) NOT NULL UNIQUE,
    Password VARCHAR(40) NOT NULL,
    Role ENUM('Admin', 'Salesperson') NOT NULL
);
INSERT INTO Users (Username, Password, Role) VALUES
    ('Admin1', 'A43', 'Admin'),
    ('Salesperson1', 'S81', 'Salesperson');