create database projectA 
use projectA
create table DoctorInfo(
DoctorId int identity(1,1),
FirstName varchar(30),
LastName varchar(30),
Sex char(1),
Specialization varchar(50),
VisitFrom varchar(10),
VisitTo varchar(10)
constraint Pk_Doc primary key(DoctorId) 
)
exec sp_help 'DoctorInfo'

create table PatientInfo(
PatientId int identity(1,1),
FirstName varchar(30),
LastName varchar(30),
Sex char(1),
Age int,
DateOfBirth varchar(20)
constraint Pk_Patient primary key(PatientId)
)

Create table UserLogin(
SlNo int identity(1,1),
UserName varchar(20)unique,
FirstName varchar(20),
LastName varchar(20),
PassWord varchar(10)
)
insert into UserLogin values('Dillisk','Giri','Dharan','Dillis20') 
select * from UserLogin

create table Appointments(
AppointmentId int identity(1,1) primary key,
PatientId int,
Specialization varchar(50),
DoctorName varchar(50),
VisitDate varchar(20),
AppointmentTime varchar(10)
)

create proc Insdoctor
@firstname varchar(30),
@lastname varchar(30),
@sex char(1),
@specialization varchar(50),
@visitfrom varchar(10),
@to varchar(10)
as
insert into DoctorInfo ([FirstName],[LastName],[Sex],[Specialization],[VisitFrom],[VisitTo]) 
values(@firstname,@lastname,@sex,@specialization,@visitfrom,@to) 

Insdoctor 'Dilli','Kumaran','M','Opthomology','08:00','2:0'
select * from DoctorInfo

create proc Inspatient
@firstname varchar(30),
@lastname varchar(30),
@sex char(1),
@age int,
@dateofbirth varchar(20)
as
insert into PatientInfo([FirstName],[LastName],[Sex],[Age],[DateOfBirth]) 
values(@firstname,@lastname,@sex,@age,@dateofbirth)

Inspatient 'Dilli','kumaran','M',12,'12/07/2022'
select * from PatientInfo

create proc Specdoc
@specialization varchar(50)
as
select Specialization
from DoctorInfo
where Specialization=@specialization

create proc Insappointment
@patientid int,
@specialization varchar(50),
@doctorname varchar(50),
@visitdate varchar(20),
@appointmenttime varchar(10)
as
insert into Appointments([PatientId],[Specialization],[DoctorName],[VisitDate],[AppointmentTime])
values(@patientid,@specialization,@doctorname,@visitdate,@appointmenttime)

create proc Cancelappointment
@appointmentid int
as
delete from Appointments
where AppointmentId=@appointmentid

create proc Showappointments
as
select AppointmentId,PatientId,Specialization,DoctorName,VisitDate,AppointmentTime
from Appointments
Showappointments

create proc Editappoint
@appointmentid int,
@patientid int,
@specialization varchar(50),
@doctorname varchar(50),
@visitdate varchar(20),
@appointmenttime varchar(10)
as
update Appointments set PatientId=@patientid,Specialization=@specialization,DoctorName=@doctorname,VisitDate=@visitdate,AppointmentTime=@appointmenttime where AppointmentId=@appointmentid

create proc getbypatientid
@patientid int,
@visitdate varchar(20)
as
select AppointmentId,Specialization,DoctorName,VisitDate,AppointmentTime
from Appointments
where PatientId=@patientid and VisitDate=@visitdate

create proc Validate 
@username varchar(20)
as
select PassWord from UserLogin where Username = @username
insert into UserLogin values('Dilliskumaran','Dilli','kumaran','Dillis20')
select * from UserLogin
select * from [dbo].[DoctorInfo]
select * from PatientInfo
-