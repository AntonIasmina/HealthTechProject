create database HealthTech
use HealthTech
go 
create table ApplicationUser(
	UserId int IDENTITY PRIMARY KEY,
	UserName varchar(150),
	FirstName varchar(50),
	LastName varchar(50),
	CNP int,
	Password varchar(50),
	Email varchar(100),
	);

	DBCC CHECKIDENT ('ApplicationUser', RESEED, 0);

	--User table

create table Speciality(
	SpecialityId int IDENTITY PRIMARY KEY,
	SpecialityName varchar(100),
	SpecialityDescription varchar(200));

	--Additional table for each doctor 
	DBCC CHECKIDENT ('Speciality', RESEED, 0);

create table Doctor(
	DoctorId int IDENTITY PRIMARY KEY,
	SpecialityId int,
	Constraint FK_User_Speciality FOREIGN KEY (SpecialityId) references Speciality(SpecialityId)
	ON DELETE NO ACTION,
	FirstName varchar(50),
	LastName varchar(50),
	CNP int,
	Email varchar(100),
	Password varchar(50),
	UserName varchar(150),

	);
	alter table Doctor
	add BusinessIntervalId int
	alter table Doctor
	add constraint FK_Doctor_BusinessInterval FOREIGN KEY (BusinessIntervalId) references BusinessInterval(BusinessIntervalId) 
	DBCC CHECKIDENT ('Doctor', RESEED, 0);
	
	--Doctor 1 to 1 to user
	--Patient 1 to 1 to user
create table BusinessInterval(
	BusinessIntervalId int IDENTITY PRIMARY KEY,
	StartTime time,
	EndTime time,
	Day int
)


	-- working interval for each doctor 
alter table BusinessInterval
add Day int

create table Appointment(
	AppointmentId int PRIMARY KEY,
	PatientId int,
	DoctorId int,
	AppointmentDate datetime,
	Constraint FK_Appointment_Patient FOREIGN KEY (PatientId) references ApplicationUser(UserId),
	Constraint FK_Appointment_Doctor FOREIGN KEY (DoctorId) references Doctor(DoctorId));
	-- This structure allows you to associate each appointment with a specific patient and doctor, helping to organize and manage appointment data efficiently.
alter table Appointment
add AppointmentDate datetime

alter table Appointment
add AppointmentDescription varchar(200)

insert into ApplicationUser values('test','test','test',123,'test','test');
SELECT * FROM ApplicationUser

use HealthTech
go 
ALTER TABLE ApplicationUser
add Points int
ALTER TABLE ApplicationUser
ADD CONSTRAINT max_points_constraint CHECK (Points <= 200);
use HealthTech
go 
alter Table Appointment
add Discount int
alter Table ApplicationUser
add PointsUsed int
