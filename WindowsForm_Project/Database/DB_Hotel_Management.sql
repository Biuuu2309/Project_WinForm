use master
GO
IF EXISTS (SELECT * FROM sysdatabases WHERE name = 'Hotel_Management')
	DROP DATABASE Hotel_Management
GO
CREATE DATABASE Hotel_Management
GO
USE Hotel_Management
GO
CREATE TABLE Room (
	maphong INT PRIMARY KEY,
	roomnumber INT NOT NULL,
	roomtype NVARCHAR(200) NOT NULL CHECK (roomtype IN ('STD', 'SUP', 'DLX', 'SUT')),
	numbed INT NOT NULL CHECK(numbed IN(1, 2, 3)),
	view_room NVARCHAR(200) NOT NULL CHECK(view_room IN ('Simple', 'Good', 'Beautiful')),
	image_room NVARCHAR(200),
	price INT NOT NULL CHECK (price > 0),
)
GO
CREATE TABLE Update_room (
	maphong INT,
	status_room NVARCHAR(200) NOT NULL CHECK(status_room IN ('Reserved', 'Occupied', 'Available', 'Check Out')),
	house_keeping NVARCHAR(200) NOT NULL CHECK(house_keeping IN ('Clean', 'Not Clean', 'In Progress', 'Repair')),
	PRIMARY KEY (maphong),
	FOREIGN KEY (maphong) REFERENCES Room(maphong) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
CREATE TABLE Customer (
	cccd_cus NVARCHAR(200) PRIMARY KEY,
	first_name NVARCHAR(200) NOT NULL,
	last_name NVARCHAR(200) NOT NULL,
	sdt NVARCHAR(200) NOT NULL,
	email NVARCHAR(200) NOT NULL,
	gioitinh NVARCHAR(200) CHECK (gioitinh IN ('Nam', 'Nu')),
	ngaysinh DATETIME CHECK (DATEDIFF(y, ngaysinh, GETDATE()) >= 18),
	address_cus NVARCHAR(200) NOT NULL,
)
GO
CREATE TABLE Employee (
	cccd_em NVARCHAR(200) PRIMARY KEY,
	first_name NVARCHAR(200) NOT NULL,
	last_name NVARCHAR(200) NOT NULL,
	sdt NVARCHAR(200) NOT NULL,
	email NVARCHAR(200) NOT NULL,
	gioitinh NVARCHAR(200) CHECK (gioitinh IN ('Nam', 'Nu')),
	ngaysinh DATETIME CHECK (DATEDIFF(y, ngaysinh, GETDATE()) >= 18),
	luong FLOAT CHECK (luong > 0),
)
GO 
CREATE TABLE Bookings (
    stt INT IDENTITY(1, 1) PRIMARY KEY,
    cccd_cus NVARCHAR(200),
    status_room NVARCHAR(200) NOT NULL CHECK(status_room IN ('Reserved', 'Occupied', 'Available', 'Check Out')),
    house_keeping NVARCHAR(200) NOT NULL CHECK(house_keeping IN ('Clean', 'Not Clean', 'In Progress', 'Repair')),
    roomtype NVARCHAR(200) NOT NULL CHECK (roomtype IN ('STD', 'SUP', 'DLX', 'SUT')),
    numbed INT NOT NULL CHECK(numbed IN(1, 2, 3)),
    view_room NVARCHAR(200) NOT NULL CHECK(view_room IN ('Simple', 'Good', 'Beautiful')),
    date_ci DATETIME NOT NULL,
    date_co DATETIME NOT NULL,
    group_customer INT DEFAULT 0,
    maphong INT UNIQUE,
    roomnumber INT NOT NULL,
    cccd_em NVARCHAR(200),
    price INT NOT NULL CHECK (price > 0),
    FOREIGN KEY (cccd_cus) REFERENCES Customer(cccd_cus) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (maphong) REFERENCES Room(maphong) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (cccd_em) REFERENCES Employee(cccd_em) ON UPDATE CASCADE ON DELETE CASCADE
);
GO
CREATE TABLE Report (
	cccd_cus NVARCHAR(200),
	maphong INT,
	ghichu NTEXT,
	PRIMARY KEY (cccd_cus, maphong),
	FOREIGN KEY (cccd_cus) REFERENCES Customer(cccd_cus) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (maphong) REFERENCES Room(maphong) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
CREATE TABLE Serve (
	stt INT IDENTITY(1, 1) PRIMARY KEY,
	cccd_cus NVARCHAR(200),
	maphong INT,
	other_booking NVARCHAR(200),
	anuong NVARCHAR(200),
	call_serve BIT DEFAULT 0,
	cost INT,
	FOREIGN KEY (cccd_cus) REFERENCES Customer(cccd_cus) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (maphong) REFERENCES Room(maphong) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
CREATE TABLE Chamcong (
	maphieu INT IDENTITY (1,1) PRIMARY KEY,
	cccd_em NVARCHAR(200),
	ngay DATETIME NOT NULL,
	ca1 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca1 IN ('Co', 'Khong')),
	ca2 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca2 IN ('Co', 'Khong')),
	ca3 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca3 IN ('Co', 'Khong')),
	ca4 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca4 IN ('Co', 'Khong')),
	note NTEXT,
	FOREIGN KEY (cccd_em) REFERENCES Employee(cccd_em) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
CREATE TABLE Account 
(
	id INT IDENTITY (1,1) PRIMARY KEY,
	username NVARCHAR(200) NOT NULL,
	password NVARCHAR(200) NOT NULL,
	cccd_em NVARCHAR(200) NOT NULL,
	FOREIGN KEY (cccd_em) REFERENCES Employee(cccd_em) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
CREATE TABLE Checkout (
	id INT IDENTITY(1, 1),
	cccd_cus NVARCHAR(200) NOT NULL,
	first_name NVARCHAR(200) NOT NULL,
	last_name NVARCHAR(200) NOT NULL,
	maphong INT NOT NULL,
	sophong INT NOT NULL,
	date_ci DATETIME NOT NULL,
	date_co DATETIME NOT NULL
)
GO
CREATE TABLE Chitieu (
	sttchi INT IDENTITY(1, 1) PRIMARY KEY,
	ngay DATETIME,
	tendogiadung NVARCHAR(200),
	gianhapdogiadung INT,
	tennguyenlieu NVARCHAR(200),
	gianhapnguyenlieu INT,
	tennhuyeupham NVARCHAR(200),
	gianhuyeupham INT
)
GO
CREATE TABLE Salary (
	stt INT IDENTITY(1, 1) PRIMARY KEY,
	ngay DATETIME,
	tongthu INT,
	tongchi INT,
	loinhuandoanhnghiep INT,
)
GO
INSERT INTO Room (maphong, roomnumber, roomtype, numbed, view_room, price)
VALUES
	('1', '101', 'STD', '1', 'Simple', '1000000'),
	('2', '102', 'STD', '2', 'Good', '1200000'),
	('3', '103', 'STD', '3', 'Beautiful', '1400000'),
	('4', '104', 'SUP', '1', 'Simple', '1200000'),
	('5', '105', 'SUP', '2', 'Good', '1400000'),
	('6', '106', 'SUP', '3', 'Beautiful', '1600000'),
	('7', '107', 'DLX', '1', 'Simple', '1400000'),
	('8', '108', 'DLX', '2', 'Good', '1600000'),
	('9', '109', 'DLX', '3', 'Beautiful', '1800000'),
	('10', '110', 'SUT', '1', 'Simple', '1600000'),
	('11', '111', 'SUT', '2', 'Good', '1800000'),
	('12', '112', 'SUT', '3', 'Beautiful', '2000000'),
	('13', '201', 'STD', '1', 'Simple', '1400000'),
	('14', '202', 'STD', '2', 'Good', '1600000'),
	('15', '203', 'STD', '3', 'Beautiful', '1800000'),
	('16', '204', 'SUP', '1', 'Simple', '1800000'),
	('17', '205', 'SUP', '2', 'Good', '2000000'),
	('18', '206', 'SUP', '3', 'Beautiful', '2200000'),
	('19', '207', 'DLX', '1', 'Simple', '2000000'),
	('20', '208', 'DLX', '2', 'Good', '2200000'),
	('21', '209', 'DLX', '3', 'Beautiful', '2400000'),
	('22', '210', 'SUT', '1', 'Simple', '2400000'),
	('23', '211', 'SUT', '2', 'Good', '2600000'),
	('24', '212', 'SUT', '3', 'Beautiful', '2800000'),
	('25', '301', 'DLX', '2', 'Good', '3400000'),
	('26', '302', 'DLX', '2', 'Beautiful', '3600000'),
	('27', '303', 'DLX', '3', 'Beautiful', '4000000'),
	('28', '401', 'SUT', '2', 'Beautiful', '5000000'),
	('29', '402', 'SUT', '2', 'Beautiful', '5000000'),
	('30', '403', 'SUT', '3', 'Beautiful', '6000000')

GO
INSERT INTO Update_room(maphong, status_room, house_keeping)
VALUES
	('1', 'Available', 'Clean'),
	('2', 'Available', 'Repair'),
	('3', 'Available', 'Clean'),
	('4', 'Available', 'Clean'),
	('5', 'Available', 'Repair'),
	('6', 'Available', 'Clean'),
	('7', 'Available', 'Not Clean'),
	('8', 'Available', 'Clean'),
	('9', 'Available', 'Repair'),
	('10', 'Available', 'Clean'),
	('11', 'Available', 'Clean'),
	('12', 'Available', 'Repair'),
	('13', 'Available', 'Repair'),
	('14', 'Available', 'Not Clean'),
	('15', 'Available', 'Clean'),
	('16', 'Available', 'Clean'),
	('17', 'Available', 'Clean'),
	('18', 'Available', 'Repair'),
	('19', 'Available', 'Clean'),
	('20', 'Available', 'Not Clean'),
	('21', 'Available', 'Clean'),
	('22', 'Available', 'Repair'),
	('23', 'Available', 'Repair'),
	('24', 'Available', 'Clean'),
	('25', 'Available', 'Clean'),
	('26', 'Available', 'Not Clean'),
	('27', 'Available', 'Clean'),
	('28', 'Available', 'Repair'),
	('29', 'Available', 'Clean'),
	('30', 'Available', 'Clean')
GO
INSERT INTO Employee(cccd_em, last_name, first_name, sdt, email, gioitinh, ngaysinh, luong)
VALUES
	('CE12012309', 'Andersen', 'Leocadia', '11578773', 'AndersenLeocadia@gmail.com', 'Nu', '2004-09-23', '200000'),
	('CE12012308', 'Lucero', 'Chad', '11613549', 'LuceroChad@gmail.com', 'Nam', '2004-09-22', '200000'),
	('CE12012307', 'Randall', 'Stone', '11903153', 'RandallStone@gmail.com', 'Nu', '2004-09-21', '200000'),
	('CE12012306', 'Braun', 'Emelia', '11680840', 'BraunEmelia@gmail.com', 'Nam', '2004-03-20', '200000'),
	('CE12012305', 'Velez', 'Ridley', '11809979', 'VelezRidley@gmail.com', 'Nu', '2004-01-19', '300000'),
	('CE12012304', 'Sloan', 'Devadas', '11933063', 'SloanDevadas@gmail.com', 'Nam', '2004-09-20', '200000'),
	('CE12012303', 'Proctor', 'Jamaal', '11836481', 'ProctorJamaal@gmail.com', 'Nam', '2004-09-20', '200000'),
	('CE12012302', 'Morris', 'Reba', '11847320', 'MorrisReba@gmail.com', 'Nam', '2004-05-20', '300000'),
	('CE12012301', 'Knight', 'Nomusa', '11709833', 'KnightNomusa@gmail.com', 'Nam', '2004-09-20', '200000'),
	('CE12012300', 'Dunlap', 'Romaine', '11611869', 'DunlapRomaine@gmail.com', 'Nu', '2004-09-12', '300000'),
	('CE12012299', 'Wilcox', 'Regan', '11790275', 'WilcoxRegan@gmail.com', 'Nu', '2004-09-20', '200000'),
	('CE12012298', 'Grimes', 'Forest', '11849664', 'GrimesForest@gmail.com', 'Nu', '2004-09-03', '200000'),
	('CE12012297', 'Gillespie', 'Feliciana', '11936768', 'GillespieFeliciana@gmail.com', 'Nam', '2004-09-20', '200000'),
	('CE12012296', 'Logan', 'Ella', '11847874', 'LoganElla@gmail.com', 'Nam', '2004-09-11', '300000'),
	('CE12012295', 'Black', 'Abel', '11689670', 'BlackAbel@gmail.com', 'Nam', '2004-09-12', '200000'),
	('CE12012294', 'Mills', 'Josceline', '11594204', 'MillsJosceline@gmail.com', 'Nu', '2004-09-20', '200000'),
	('CE12012293', 'Dixon', 'Kit', '11689480', 'DixonKit@gmail.com', 'Nam', '2004-09-20', '200000'),
	('CE12012292', 'Ruiz', 'Annice', '11814376', 'RuizAnnice@gmail.com', 'Nam', '2004-09-20', '200000'),
	('CE12012291', 'Rubio', 'Alesia', '11860874', 'RubioAlesia@gmail.com', 'Nam', '2004-09-20', '300000'),
	('CE12012290', 'Craig', 'Magnolia', '11669831', 'CraigMagnolia@gmail.com', 'Nu', '2004-09-20', '200000'),
	('CE12012289', 'Blanchard', 'Elouan', '11908270', 'BlanchardElouan@gmail.com', 'Nam', '2004-09-20', '400000'),
	('CE12012288', 'Cummings', 'McKinley', '11832090', 'CummingsMcKinley@gmail.com', 'Nam', '2004-09-20', '400000')


GO
INSERT INTO Account(username, password, cccd_em)
VALUES
	('RubioAlesia', 'manage1', 'CE12012291'),
	('LoganElla', 'manage1', 'CE12012296'),
	('DunlapRomaine', 'manage1', 'CE12012300'),
	('MorrisReba', 'manage1', 'CE12012303'),
	('VelezRidley', 'manage1', 'CE12012305')
GO
INSERT INTO Customer(cccd_cus, first_name, last_name, sdt, email, gioitinh, ngaysinh, address_cus)
VALUES
	('100011', 'Tsao', 'Danny', '11820266', 'TsaoDanny@gmail.com', 'Nam', '2000-09-24', '27 Wu Tzu St Tamshui 251  Taiwan'),
	('100012', 'Lei', 'Colleen', '11663635', 'LeiColleen@gmail.com', 'Nu', '2000-09-25', '88 6th Avenue Teda 300457 TIANJIN  China'),
	('100013', 'Roth', 'Nancy', '11842511', 'RothNancy@gmail.com', 'Nam', '2000-09-26', 'Hoefenstrasse 31 Muehlethal  Switzerland'),
	('100014', 'Meneses Contreras', 'Karl-Oscar', '11585702', 'MenesesKarlOscar@gmail.com', 'Nu', '2000-09-27', 'Poniente 134 Ste. 740 02300 México DIF Mexico'),
	('100015', 'Nunez', 'Helmut', '11620651', 'NunezHelmut@gmail.com', 'Nam', '2000-09-28', 'Andador Pinos 345 45235 Zapopan JAL Mexico'),
	('100016', 'Fitzpatrick', 'Dmitry', '11871014', 'FitzpatrickDmitry@gmail.com', 'Nu', '2000-09-01', '22 Hemingford Pl Whitby ON Canada'),
	('100017', 'Andreu', 'Leya', '11880263', 'AndreuLeya@gmail.com', 'Nam', '1998-09-09', 'Nevada de Colima 104 20280 Aguascalientes  Mexico'),
	('100018', 'Ramsey', 'Stephen', '11893645', 'RamseyStephen@gmail.com', 'Nam', '1998-09-09', 'Z-Block No 59  Chennai TN - 600040  India'),
	('100019', 'Xiao-Hui', 'Michael', '11809723', 'XiaHuiMichael@gmail.com', 'Nu', '1998-09-01', 'Unit B-E F19 XinMei Union Square 200120 Shanghai  China'),
	('100020', 'He', 'Jan', '11677573', 'HeJan@gmail.com', 'Nam', '1998-09-09', '5055 Heather Leigh Avenue Mississauga ON Canada'),
	('100021', 'Wisner', 'Ray', '11719458', 'WisnerRay@gmail.com', 'Nu', '1998-09-01', 'Chemin 15F Vernier  Switzerland'),
	('100022', 'Denturck', 'Bill', '11857457', 'DenturckBill@gmail.com', 'Nam', '1998-09-01', 'Septestraat 27 2640 MORTSEL  Belgium'),
	('100023', 'Arnout', 'Marco', '11873306', 'ArnoutMarco@gmail.com', 'Nu', '1998-09-09', 'P.O. Box 4002 Basel  Switzerland'),
	('100024', 'Kopp', 'Victor', '11862577', 'KoppVictor@gmail.com', 'Nu', '1998-09-23', 'Pez Austral 3625 45070 Guadalajara JAL Mexico'),
	('100025', 'Villarruel Enriquez', 'Bep', '11629423', 'VillarruelBep@gmail.com', 'Nam', '1996-09-28', '303 - 615 North Road Coquitlam BC Canada'),
	('100026', 'Marchant', 'René', '11794424', 'MarchantRene@gmail.com', 'Nam', '1996-09-28', 'Emilio Arrieta 23 Pamplona  Spain'),
	('100027', 'de Alava Casado', 'Christophe', '11860677', 'deAlavaCasadoChristophe@gmail.com', 'Nam', '1996-09-09', '383 Elgin St Brantford ON Canada'),
	('100028', 'Boaz', 'Isobel', '11572376', 'BoazIsobel@gmail.com', 'Nam', '1996-09-23', '8 Temasek Boulevard #7 Singapore  Singapore'),
	('100029', 'Koshy', 'Rene', '11824050', 'KoshyRene@gmail.com', 'Nam', '1996-09-01', 'P O Box 112-066 Auckland  New Zealand'),
	('100030', 'Lee', 'Chie Shin', '11759559', 'LeeChieShin@gmail.com', 'Nu', '1996-09-09', '5 rue de la Division Leclerc 78350 LES LOGES EN JOSAS  France'),
	('100031', 'Sanseau', 'Jose', '11811561', 'SanseauJose@gmail.com', 'Nam', '1996-09-09', '775 Steeles Ave West Apt. 1111 Toronto ON Canada'),
	('100032', 'Ciorma', 'Frans', '11570183', 'CiormaFrans@gmail.com', 'Nu', '1996-09-23', 'Rue des Saars 87 Neuchâtel  Switzerland'),
	('100033', 'Peron', 'Jasja', '11872554', 'PeronJasja@gmail.com', 'Nam', '1992-09-01', '45 Kensington Rd Essex  United Kingdom'),
	('100034', 'Worrell', 'Travis', '11611010', 'WorrellTravis@gmail.com', 'Nam', '1992-09-28', '13459 Cedar Way Maple Ridge BC Canada'),
	('100035', 'Bianco', 'Richard', '11707845', 'BiancoRichard@gmail.com', 'Nam', '1992-09-23', 'Rossinistraat 18 4561 VP Hulst  Netherlands'),
	('100036', 'McKnight', 'Gangesh', '11814905', 'McKnightGangesh@gmail.com', 'Nu', '1992-09-23', '1370 Don Mills Rd Toronto ON Canada'),
	('100037', 'Bricks', 'Shari', '11849781', 'BricksShari@gmail.com', 'Nu', '1992-09-28', '1502-1901 Hosumaeul Humansia APT Dongbaek-dong Kiheung-gu Yongin-shi  Korea, Republic of'),
	('100038', 'Kim', 'Nam-Su', '11875178', 'KimNamSu@gmail.com', 'Nam', '1980-09-28', '105 Vars Fantasy Bangalore Karnataka India'),
	('100039', 'Venghatachari', 'Ramachandharan', '11569278', 'VenghatachariRamachandharan@gmail.com', 'Nam', '1980-09-28', 'A-6-08 Sri Teratai Apartment 47100 Bandar Puchong Jaya Selangor Malaysia'),
	('100040', 'Lim', 'SooChing', '11561051', 'LimSooChing@gmail.com', 'Nam', '1980-09-28', 'Softwareweg 4 3821 BC Amersfoort  Netherlands'),
	('100041', 'Hagen', 'Rien', '11859915', 'HagenRien@gmail.com', 'Nu', '1980-09-23', 'Emilio Arrieta 23 Pamplona  Spain')
GO 
INSERT INTO Chitieu(ngay, tendogiadung, gianhapdogiadung, tennhuyeupham, gianhuyeupham, tennguyenlieu, gianhapnguyenlieu)
VALUES
	('2024-01-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-01-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-01-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-02-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-02-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-02-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-03-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-03-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-03-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-04-22', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-04-22', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-04-22', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-05-22', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-05-22', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-05-22', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-06-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-06-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-06-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-07-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-07-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-07-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-08-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-08-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-08-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-09-20', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-09-20', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-09-20', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-10-22', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-10-22', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-10-22', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000'),
	('2024-11-22', 'ban', '100000', 'khan', '50000', 'thit bo', '2000000'),
	('2024-11-22', 'ghe', '200000', 'men', '70000', 'thit heo', '1000000'),
	('2024-11-22', 'bat', '100000', 'nem', '50000', 'thit ga', '1000000')

GO 
CREATE OR ALTER PROC sp_addbooking @cccd_cus NVARCHAR(200), @status_room NVARCHAR(200), @house_keeping NVARCHAR(200), @roomtype NVARCHAR(200), @numbed INT, @view_room NVARCHAR(200), @maphong INT, @roomnumber INT, @group_customer INT, @date_ci DATETIME, @date_co DATETIME, @cccd_em NVARCHAR(200), @price INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS 
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Bookings
					WHERE cccd_cus = @cccd_cus AND maphong = @maphong)
	BEGIN
		INSERT INTO Bookings (cccd_cus, status_room, house_keeping, roomtype, numbed, view_room, maphong, roomnumber, group_customer, date_ci, date_co, cccd_em, price) VALUES (@cccd_cus, @status_room, @house_keeping, @roomtype, @numbed, @view_room, @maphong, @roomnumber, @group_customer, @date_ci, @date_co, @cccd_em, @price)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN 
		SET @ErrorMessage = 'Add Booking error'
	END
END
GO
CREATE OR ALTER PROC sp_addtongchi @ngay DATETIME, @tendogiadung NVARCHAR(200), @gianhapdogiadung INT, @tennguyenlieu NVARCHAR(200), @gianhapnguyenlieu INT, @tennhuyeupham NVARCHAR(200), @gianhuyeupham INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	INSERT INTO Chitieu(ngay, tendogiadung, gianhapdogiadung, tennguyenlieu, gianhapnguyenlieu, tennhuyeupham, gianhuyeupham) VALUES (@ngay, @tendogiadung, @gianhapdogiadung, @tennguyenlieu, @gianhapnguyenlieu, @tennhuyeupham, @gianhuyeupham)
	SET @ErrorMessage = 'Successfully'
END
GO
CREATE OR ALTER PROC sp_account @id INT, @username NVARCHAR(200), @password NVARCHAR(200), @cccd_em NVARCHAR(200), @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Account
					WHERE cccd_em = @cccd_em)
	BEGIN 
		INSERT INTO Account(username, password, cccd_em) VALUES (@username, @password, @cccd_em)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Them Account khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_updateserve @cccd_cus NVARCHAR(200) = NULL, @maphong INT = NULL, @other_booking NVARCHAR(200) = NULL, @anuong NVARCHAR(200) = NULL, @call_serve BIT = NULL, @cost INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS                      
BEGIN
	INSERT INTO Serve(cccd_cus, maphong, other_booking, anuong, call_serve, cost) VALUES (@cccd_cus, @maphong, @other_booking, @anuong, @call_serve, @cost)
		SET @ErrorMessage = 'Successfully'
	BEGIN TRY
		UPDATE Serve
			SET 
				cccd_cus = COALESCE(@cccd_cus, cccd_cus),
				other_booking = COALESCE(@other_booking,other_booking),
				anuong = COALESCE(@anuong,anuong),
				call_serve = COALESCE(@call_serve,call_serve),
				cost = COALESCE(@cost,cost)
			WHERE cccd_cus = @cccd_cus AND maphong = @maphong
			SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE()
	END CATCH
END
GO
CREATE OR ALTER PROC sp_addroom @maphong INT, @roomnumber INT, @roomtype NVARCHAR(200), @numbed INT, @view_room NVARCHAR(200), @image_room NVARCHAR(200), @price INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Room
					WHERE maphong = @maphong)
	BEGIN
		INSERT INTO Room(maphong, roomnumber, roomtype, numbed, view_room, image_room, price) VALUES (@maphong, @roomnumber, @roomtype, @numbed, @view_room, @image_room, @price)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Them phong khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_updatereport @cccd_cus NVARCHAR(200) = NULL, @maphong INT = NULL, @ghichu NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Report
					WHERE cccd_cus = @cccd_cus AND maphong = @maphong)
	BEGIN 
		INSERT INTO Report(cccd_cus, maphong, ghichu) VALUES (@cccd_cus, @maphong, @ghichu)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN TRY
		UPDATE Report
			SET 
				ghichu = COALESCE(@ghichu, ghichu)
			WHERE cccd_cus = @cccd_cus AND maphong = @maphong
			SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE()
	END CATCH
END
GO
CREATE OR ALTER PROC sp_addemployee @cccd_em INT, @first_name NVARCHAR(200), @last_name NVARCHAR(200), @sdt NVARCHAR(200), @email NVARCHAR(200), @gioitinh NVARCHAR(200), @ngaysinh DATETIME, @luong FLOAT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Employee
					WHERE cccd_em = @cccd_em)
	BEGIN 
		INSERT INTO Employee(cccd_em, first_name, last_name, sdt, email, gioitinh, ngaysinh, luong) VALUES (@cccd_em, @first_name, @last_name, @sdt, @email, @gioitinh, @ngaysinh, @luong)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN 
		SET @ErrorMessage = 'Them Employee khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_deleteroom @maphong INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF EXISTS (	SELECT 1 FROM Room
				WHERE maphong = @maphong)
	BEGIN
		DELETE FROM Room WHERE maphong = @maphong
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Xoa Phong khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_updateroom @maphong INT = NULL, @roomnumber INT = NULL, @roomtype NVARCHAR(200) = NULL, @numbed INT = NULL, @view_room NVARCHAR(200) = NULL,@image_room NVARCHAR(200) = NULL, @price INT = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN 
	BEGIN TRY
		UPDATE Room
		SET
			maphong = COALESCE(@maphong, maphong),
			roomnumber = COALESCE(@roomnumber, roomnumber),
			roomtype = COALESCE(@roomtype, roomtype),
			numbed = COALESCE(@numbed, numbed),
			view_room = COALESCE(@view_room, view_room),
			image_room = COALESCE(@image_room, image_room),
			price = COALESCE(@price, price)
		WHERE maphong = @maphong;

		SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END

GO 
CREATE OR ALTER PROC sp_updateemployee @cccd_em INT = NULL, @first_name NVARCHAR(200) = NULL, @last_name NVARCHAR(200) = NULL, @sdt NVARCHAR(200) = NULL, @email NVARCHAR(200) = NULL, @gioitinh NVARCHAR(200) = NULL, @ngaysinh DATETIME = NULL, @luong FLOAT = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	BEGIN TRY 
		UPDATE Employee
		SET 
			cccd_em = COALESCE(@cccd_em, cccd_em),
			first_name = COALESCE(@first_name, first_name), 
			last_name = COALESCE(@last_name, last_name), 
			sdt = COALESCE(@sdt, sdt), 
			email = COALESCE(@email, email), 
			gioitinh = COALESCE(@gioitinh, gioitinh),
			ngaysinh = COALESCE(@ngaysinh, ngaysinh), 
			luong = COALESCE(@luong, luong)
		WHERE cccd_em = @cccd_em;
		SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END
GO
CREATE OR ALTER PROC sp_addcustomer @cccd_cus NVARCHAR(200), @first_name NVARCHAR(200), @last_name NVARCHAR(200), @sdt NVARCHAR(200), @email NVARCHAR(200), @gioitinh NVARCHAR(200), @ngaysinh DATETIME, @address_cus NVARCHAR(200), @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Customer
					WHERE cccd_cus = @cccd_cus)
	BEGIN
		INSERT INTO Customer(cccd_cus, first_name, last_name, sdt, email, gioitinh, ngaysinh, address_cus) VALUES (@cccd_cus, @first_name, @last_name, @sdt, @email, @gioitinh, @ngaysinh, @address_cus)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Them customer khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_updatecustomer @cccd_cus NVARCHAR(200) = NULL, @first_name NVARCHAR(200) = NULL, @last_name NVARCHAR(200) = NULL, @sdt NVARCHAR(200) = NULL, @email NVARCHAR(200) = NULL, @gioitinh NVARCHAR(200) = NULL, @ngaysinh DATETIME = NULL, @address_cus NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	BEGIN TRY
		UPDATE Customer
		SET
			cccd_cus = COALESCE(@cccd_cus, cccd_cus),
			first_name = COALESCE(@first_name, first_name),
			last_name = COALESCE(@last_name, last_name), 
			sdt = COALESCE(@sdt, sdt),
			email = COALESCE(@email, email),	
			gioitinh = COALESCE(@gioitinh, gioitinh),
			ngaysinh = COALESCE(@ngaysinh, ngaysinh),
			address_cus = COALESCE(@address_cus, address_cus)
		WHERE cccd_cus = @cccd_cus;

		SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END

GO
CREATE OR ALTER PROC sp_addroomupdate @maphong INT = NULL, @status_room NVARCHAR(200) = NULL, @house_keeping NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Update_room
					WHERE maphong = @maphong)
	BEGIN 
		INSERT INTO Update_room(maphong, status_room, house_keeping) VALUES (@maphong, @status_room, @house_keeping)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN TRY
		UPDATE Update_room
		SET
			maphong = COALESCE(@maphong, maphong),
			status_room = COALESCE(@status_room, status_room),
			house_keeping = COALESCE(@house_keeping, house_keeping)
		WHERE maphong = @maphong
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END
GO
CREATE OR ALTER PROC sp_deleteaccount @id INT = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Account WHERE id = @id)
	BEGIN
		DELETE FROM Account WHERE id = @id
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Xoa account khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_addchamcong @cccd_em NVARCHAR(200), @ngay DATETIME, @ca1 NVARCHAR(200), @ca2 NVARCHAR(200), @ca3 NVARCHAR(200), @ca4 NVARCHAR(200), @note NTEXT, @ErrorMessage NVARCHAR(200) OUTPUT 
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Employee WHERE cccd_em = @cccd_em)
	BEGIN
		INSERT INTO Chamcong(cccd_em, ngay, ca1, ca2, ca3, ca4, note) VALUES (@cccd_em, @ngay, @ca1, @ca2, @ca3, @ca4, @note)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Cham cong khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_addcheckout @cccd_cus NVARCHAR(200), @first_name NVARCHAR(200), @last_name NVARCHAR(200), @maphong INT, @sophong INT, @date_ci DATETIME, @date_co DATETIME, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	INSERT INTO Checkout(cccd_cus, first_name, last_name, maphong, sophong, date_ci, date_co) VALUES (@cccd_cus, @first_name, @last_name, @maphong, @sophong, @date_ci, @date_co)
	SET @ErrorMessage = 'Successfully'
END

GO
CREATE OR ALTER PROC sp_deletebooking @roomnumber INT = NULL, @cccd_cus NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Bookings WHERE roomnumber = @roomnumber AND cccd_cus = @cccd_cus)
	BEGIN
		DELETE FROM Bookings WHERE roomnumber = @roomnumber AND cccd_cus = @cccd_cus
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Check out khong thanh cong'
	END
END

GO
CREATE OR ALTER PROC sp_updatestatusroom @roomnumber NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	BEGIN TRY
		UPDATE Update_room
		SET
			status_room = 'Available'
		WHERE maphong IN (	SELECT maphong
							FROM Room
							WHERE roomnumber = @roomnumber);
		SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END
GO
CREATE OR ALTER PROC sp_updatestatusroom2 @roomnumber NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	BEGIN TRY
		UPDATE Update_room
		SET
			status_room = 'Occupied'
		WHERE maphong IN (	SELECT maphong
							FROM Room
							WHERE roomnumber = @roomnumber);
		SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END

GO
SELECT Chamcong.cccd_em, first_name, last_name, sdt, email, gioitinh, ngaysinh, ngay, ca1, ca2, ca3, ca4, luong, note, SUM(CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END + CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END + CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END + CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END) AS total_shifts
FROM Chamcong
INNER JOIN Employee ON Chamcong.cccd_em = Employee.cccd_em
WHERE Chamcong.cccd_em = Employee.cccd_em
GROUP BY Chamcong.cccd_em, first_name, last_name, sdt, email, gioitinh, ngaysinh, ngay, ca1, ca2, ca3, ca4, luong, note

SELECT DISTINCT Employee.cccd_em, first_name, last_name, DATEDIFF(DAY, MIN(ngay), GETDATE()) AS days_since_start, COUNT(*) AS total_shifts, luong, luong * COUNT(*) AS total_salary
FROM Employee
INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
WHERE Employee.cccd_em = Chamcong.cccd_em AND (ca1 = 'Co' OR ca2 = 'Co' OR ca3 = 'Co' OR ca4 = 'Co')
GROUP BY Employee.cccd_em, first_name, last_name, luong
ORDER BY Employee.cccd_em

SELECT 
    Chamcong.cccd_em, first_name, last_name, ngay, ca1, ca2, ca3, ca4, CAST(note AS NVARCHAR(MAX)) AS note
FROM Chamcong
INNER JOIN Employee ON Chamcong.cccd_em = Employee.cccd_em
GROUP BY Chamcong.cccd_em, first_name, last_name, ngay, ca1, ca2, ca3, ca4, CAST(note AS NVARCHAR(MAX))


SELECT 
    Employee.cccd_em, 
    first_name, 
    last_name, 
    DATEDIFF(DAY, MIN(ngay), GETDATE()) AS days_since_start, 
    SUM(
        CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END + 
        CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END + 
        CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END + 
        CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END
    ) AS total_shifts, 
    luong, 
    luong * SUM(
        CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END + 
        CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END + 
        CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END + 
        CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END
    ) AS total_salary
FROM Employee
INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
GROUP BY Employee.cccd_em, first_name, last_name, luong
ORDER BY Employee.cccd_em;

SELECT house_keeping
FROM Update_room
WHERE status_room = 'Available'
SELECT Room.maphong 
FROM Room 
INNER JOIN Update_room ON Room.maphong = Update_room.maphong
WHERE status_room = 'Available' AND house_keeping = 'Clean' AND roomtype = 'DLX' AND numbed = 2 AND view_room = 'Beautiful'

SELECT Room.price * DATEDIFF(DAY, '2024-09-02', '2024-09-04') AS price
FROM Room 
WHERE roomtype = 'STD' AND numbed = 2 AND view_room = 'Good' AND maphong IN (	SELECT maphong 
																				FROM Update_room 
																				WHERE status_room = 'Available' AND house_keeping = 'Clean')

SELECT Room.price * DATEDIFF(DAY, @date_ci, @date_co) AS price
FROM Room 
WHERE roomtype = @roomtype AND numbed = @numbed AND view_room = @view_room AND maphong IN (	SELECT maphong 
																				FROM Update_room 
																				WHERE status_room = @status_room AND house_keeping = @house_keeping)



SELECT Room.price * DATEDIFF(DAY, @date_ci, @date_co) AS price
FROM Room 
INNER JOIN Bookings ON Room.maphong = Bookings.maphong
WHERE Room.maphong = (	SELECT Room.maphong 
						FROM Room 
						INNER JOIN Update_room ON Room.maphong = Update_room.maphong
						WHERE status_room = @status_room AND house_keeping = @house_keeping AND roomtype = @roomtype AND numbed = @numbed AND view_room = @view_room)

SELECT house_keeping
                             FROM Update_room 
                             WHERE maphong IN (  SELECT maphong 
                                				 FROM Update_room 
                                				 WHERE status_room = @status_room)
SELECT roomtype
                                FROM Room 
                                WHERE maphong IN (  SELECT maphong 
                                				    FROM Update_room 
                                				    WHERE status_room = @status_room AND house_keeping = @house_keeping)
SELECT * FROM Bookings


SELECT Room.maphong, roomnumber, roomtype, numbed, view_room, house_keeping, status_room, price
FROM Room
INNER JOIN Update_room ON Room.maphong = Update_room.maphong
WHERE Room.maphong NOT IN (	SELECT maphong
							FROM Bookings)

SELECT *
FROM Customer
WHERE cccd_cus NOT IN (	SELECT cccd_cus
						FROM Bookings)

SELECT first_name + ' ' + last_name as fullname
FROM Customer
INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
WHERE numbed = 1

SELECT * 
FROM Update_room

SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
FROM Bookings 
WHERE numbed = 1

SELECT house_keeping
FROM Update_room
INNER JOIN Room ON Update_room.maphong = Room.maphong
WHERE numbed = 1

SELECT COUNT(maphong) as free
FROM Room
WHERE maphong NOT IN (	SELECT maphong
						FROM Bookings)
SELECT first_name, last_name, maphong, date_ci
FROM Customer
INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus

SELECT *
FROM Checkout


SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 1 AND Update_room.house_keeping = 'Clean'

SELECT * 
FROM Room
INNER JOIN Update_room ON Room.maphong = Update_room.maphong


SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 1
SELECT * FROM Customer

SELECT DISTINCT first_name + ' ' + last_name as full_name, COUNT(*) AS total_shifts, luong * COUNT(*) AS total_salary
                                FROM Employee
                                INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
                                WHERE Employee.cccd_em = Chamcong.cccd_em AND (ca1 = 'Co' OR ca2 = 'Co' OR ca3 = 'Co' OR ca4 = 'Co')
                                GROUP BY Employee.cccd_em, first_name, last_name, luong




SELECT SUM(luong * COUNT(*)) AS total_salary
FROM Employee
INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
WHERE Employee.cccd_em = Chamcong.cccd_em AND (ca1 = 'Co' OR ca2 = 'Co' OR ca3 = 'Co' OR ca4 = 'Co')
GROUP BY Employee.cccd_em, first_name, last_name, luong
ORDER BY Employee.cccd_em


SELECT 
    (SELECT SUM(total_salary) 
     FROM (
         SELECT luong * COUNT(*) AS total_salary
         FROM Employee
         INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
         WHERE ca1 = 'Co' OR ca2 = 'Co' OR ca3 = 'Co' OR ca4 = 'Co'
         GROUP BY Employee.cccd_em, first_name, last_name, luong
     ) AS SalaryPerEmployee) 
    +
    (SELECT SUM(cost) FROM Serve) AS grand_total;
	 




---thu
SELECT SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)) AS total_booking
FROM Bookings
INNER JOIN Serve ON Bookings.cccd_cus = Serve.cccd_cus


SELECT 
    (SELECT SUM(total_booking) 
     FROM (
         SELECT SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)) AS total_booking
         FROM Bookings
         INNER JOIN Serve ON Bookings.cccd_cus = Serve.cccd_cus
     ) AS total) 
    +
    (SELECT COUNT(*) * 100000 FROM Chitieu) AS grand_total;
---chi
SELECT SUM(CAST(gianhapdogiadung AS INT)) + SUM(CAST(gianhuyeupham AS INT)) + SUM(CAST(gianhapnguyenlieu AS INT)) AS total_chitieu FROM Chitieu
---luongnv
SELECT SUM(CAST(total_salary AS INT)) AS grand_total_salary
FROM (
    SELECT 
        SUM(CAST(luong AS INT) * diem_danh.so_ca) AS total_salary
    FROM 
        Employee
    INNER JOIN 
        Chamcong ON Employee.cccd_em = Chamcong.cccd_em
    CROSS APPLY 
        (
            SELECT 
                CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
                CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
                CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
                CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
        ) AS diem_danh
    GROUP BY 
        Employee.cccd_em, first_name, last_name, luong
) AS per_employee_salary;
---loinhuan
SELECT 
    (SELECT SUM(CAST(total_booking AS INT)) 
     FROM (
         SELECT SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)) AS total_booking
         FROM Bookings
         INNER JOIN Serve ON Bookings.cccd_cus = Serve.cccd_cus
     ) AS total_bk) 
    -
    (SELECT SUM(CAST(total_chitieu AS INT)) 
     FROM (
         SELECT SUM(CAST(gianhapdogiadung AS INT)) + SUM(CAST(gianhuyeupham AS INT)) AS total_chitieu
         FROM Chitieu
     ) AS total_ct) 
	-
	(SELECT SUM(CAST(total_salary AS INT)) AS grand_total_salary
	FROM (
	    SELECT 
	        SUM(CAST(luong AS INT) * diem_danh.so_ca) AS total_salary
	    FROM 
	        Employee
	    INNER JOIN 
	        Chamcong ON Employee.cccd_em = Chamcong.cccd_em
	    CROSS APPLY 
	        (
	            SELECT 
	                CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
	                CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
	                CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
	                CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
	        ) AS diem_danh
	    GROUP BY 
	        Employee.cccd_em, first_name, last_name, luong
	) AS per_employee_salary);

--------------------------------------------------
WITH MonthYear AS (
    SELECT 
        --YEAR(date_co) AS year,
        MONTH(date_co) AS month
    FROM 
        Bookings
    GROUP BY 
        --YEAR(date_co), 
		MONTH(date_co)
)

SELECT 
    --my.year,
    my.month,
    COALESCE(SUM(CAST(price AS DECIMAL(10, 2))) + SUM(CAST(cost AS DECIMAL(10, 2))), 0) AS total_booking
FROM 
    MonthYear AS my
LEFT JOIN 
    Bookings ON --YEAR(Bookings.date_co) = my.year AND 
	MONTH(Bookings.date_co) = my.month
LEFT JOIN 
    Serve ON Bookings.cccd_cus = Serve.cccd_cus
GROUP BY 
    --my.year, 
	my.month
ORDER BY 
    --my.year, 
	my.month;
--------------------------------------------------
SELECT 
    --year,
    month,
    SUM(total_chitieu + grand_total_salary) AS total_costs
FROM 
    (
        -- Truy vấn chi tiêu
        SELECT 
            --YEAR(ngay) AS year,
            MONTH(ngay) AS month,
            SUM(gianhapdogiadung) + SUM(gianhuyeupham) AS total_chitieu,
            0 AS grand_total_salary
        FROM 
            Chitieu
        GROUP BY 
           --YEAR(ngay), 
		   MONTH(ngay)
        
        UNION ALL
        
        -- Truy vấn lương nhân viên
        SELECT 
            --YEAR(ngay) AS year,
            MONTH(ngay) AS month,
            0 AS total_chitieu,
            SUM(luong * diem_danh.so_ca) AS grand_total_salary
        FROM 
            Employee
        INNER JOIN 
            Chamcong ON Employee.cccd_em = Chamcong.cccd_em
        CROSS APPLY 
            (
                SELECT 
                    CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
                    CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
                    CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
                    CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
            ) AS diem_danh
        GROUP BY 
            --YEAR(ngay), 
			MONTH(ngay), Employee.cccd_em, first_name, last_name, luong
    ) AS combined
GROUP BY 
    --year, 
	month
ORDER BY 
    --year, 
	month;






	WITH MonthYear AS (
    SELECT 
        MONTH(date_co) AS month
    FROM 
        Bookings
    GROUP BY 
        MONTH(date_co)
)

SELECT 
    my.month,
    COALESCE(SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)), 0) AS total_booking
FROM 
    MonthYear AS my
LEFT JOIN 
    Bookings ON MONTH(Bookings.date_co) = my.month
LEFT JOIN 
    Serve ON Bookings.cccd_cus = Serve.cccd_cus
GROUP BY 
    my.month
ORDER BY 
    my.month;




	SELECT 
    month,
    SUM(total_chitieu + grand_total_salary) AS total_costs
FROM 
    (
        -- Truy vấn chi tiêu
        SELECT 
            MONTH(ngay) AS month,
            SUM(CAST(gianhapdogiadung AS INT)) + SUM(CAST(gianhuyeupham AS INT)) AS total_chitieu,
            0 AS grand_total_salary
        FROM 
            Chitieu
        GROUP BY 
            MONTH(ngay)

        UNION ALL
        
        -- Truy vấn lương nhân viên
        SELECT 
            MONTH(ngay) AS month,
            0 AS total_chitieu,
            SUM(CAST(luong AS INT) * diem_danh.so_ca) AS grand_total_salary
        FROM 
            Employee
        INNER JOIN 
            Chamcong ON Employee.cccd_em = Chamcong.cccd_em
        CROSS APPLY 
            (
                SELECT 
                    CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
                    CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
                    CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
                    CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
            ) AS diem_danh
        GROUP BY 
            MONTH(ngay), Employee.cccd_em
    ) AS combined
GROUP BY 
    month
ORDER BY 
    month;


SELECT COUNT(maphong) FROM Update_room WHERE status_room = 'Available'


SELECT COUNT(maphong) as free
                        FROM Room
                        WHERE maphong NOT IN (	SELECT maphong
                              						FROM Bookings)

SELECT date_ci
                                FROM Checkout


SELECT image_room FROM Room


-- Tính tổng thu từ Bookings
WITH TotalBookings AS (
    SELECT 
        YEAR(date_co) AS year,
        MONTH(date_co) AS month,
        SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)) AS total_booking
    FROM 
        Bookings
    GROUP BY 
        YEAR(date_co), MONTH(date_co)
),

-- Tính tổng chi tiêu từ Chitieu
TotalChitieu AS (
    SELECT 
        YEAR(ngay) AS year,
        MONTH(ngay) AS month,
        SUM(gianhapdogiadung) + SUM(gianhuyeupham) + SUM(soluong_nguyenlieu * 100000) AS total_chitieu
    FROM 
        Chitieu
    GROUP BY 
        YEAR(ngay), MONTH(ngay)
)

-- Kết hợp kết quả từ Bookings và Chitieu
SELECT 
    COALESCE(tb.year, tc.year) AS year,
    COALESCE(tb.month, tc.month) AS month,
    COALESCE(tb.total_booking, 0) AS total_booking,
    COALESCE(tc.total_chitieu, 0) AS total_chitieu,
    COALESCE(tb.total_booking, 0) - COALESCE(tc.total_chitieu, 0) AS net_income
FROM 
    TotalBookings AS tb
FULL OUTER JOIN 
    TotalChitieu AS tc ON tb.year = tc.year AND tb.month = tc.month
ORDER BY 
    year, month;









-- Tạo CTE để liệt kê các tháng có dữ liệu từ Bookings
WITH MonthYear AS (
    SELECT 
        YEAR(date_co) AS year,
        MONTH(date_co) AS month
    FROM 
        Bookings
    GROUP BY 
        YEAR(date_co), MONTH(date_co)
),

-- Tính tổng thu từ Bookings cho từng tháng
TotalBookings AS (
    SELECT 
        my.year,
        my.month,
        COALESCE(SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)), 0) AS total_booking
    FROM 
        MonthYear AS my
    LEFT JOIN 
        Bookings ON MONTH(Bookings.date_co) = my.month AND YEAR(Bookings.date_co) = my.year
    LEFT JOIN 
        Serve ON Bookings.cccd_cus = Serve.cccd_cus
    GROUP BY 
        my.year, my.month
),

-- Tính tổng chi tiêu từ Chitieu cho từng tháng
TotalChitieu AS (
    SELECT 
        YEAR(ngay) AS year,
        MONTH(ngay) AS month,
        SUM(gianhapdogiadung) + SUM(gianhuyeupham) + SUM(COUNT(*) * 100000) AS total_chitieu
    FROM 
        Chitieu
    GROUP BY 
        YEAR(ngay), MONTH(ngay)
)

-- Kết hợp tổng thu từ Bookings và tổng chi từ Chitieu
SELECT 
    COALESCE(tb.year, tc.year) AS year,
    COALESCE(tb.month, tc.month) AS month,
    COALESCE(tb.total_booking, 0) AS total_booking,
    COALESCE(tc.total_chitieu, 0) AS total_chitieu,
    COALESCE(tb.total_booking, 0) - COALESCE(tc.total_chitieu, 0) AS net_income
FROM 
    TotalBookings AS tb
FULL OUTER JOIN 
    TotalChitieu AS tc ON tb.year = tc.year AND tb.month = tc.month
ORDER BY 
    year, month;


----------------------------------------
WITH MonthYear AS (
    SELECT 
        MONTH(date_co) AS month,
        YEAR(date_co) AS year
    FROM 
        Bookings
    GROUP BY 
        MONTH(date_co), YEAR(date_co)
)

SELECT 
    my.month,
    my.year,
    COALESCE(SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)), 0) + 
    COALESCE(SUM(material_count.material_cost), 0) AS total_booking
FROM 
    MonthYear AS my
LEFT JOIN 
    Bookings ON MONTH(Bookings.date_co) = my.month AND YEAR(Bookings.date_co) = my.year
LEFT JOIN 
    Serve ON Bookings.cccd_cus = Serve.cccd_cus
LEFT JOIN 
    (
        SELECT 
            MONTH(ngay) AS month,
            YEAR(ngay) AS year,
            COUNT(tennguyenlieu) * 70000 AS material_cost
        FROM 
            Chitieu
        GROUP BY 
            MONTH(ngay), YEAR(ngay)
    ) AS material_count ON my.month = material_count.month AND my.year = material_count.year
GROUP BY 
    my.month, my.year
ORDER BY 
    my.year, my.month;


	-------------------

	SELECT 
    COALESCE(SUM(CAST(price AS INT)), 0) + 
    COALESCE(SUM(CAST(cost AS INT)), 0) + 
    COALESCE(SUM(material_count.material_cost), 0) AS total_income
FROM 
    Bookings
INNER JOIN 
    Serve ON Bookings.cccd_cus = Serve.cccd_cus
LEFT JOIN 
    (
        SELECT 
            COUNT(tennguyenlieu) * 70000 AS material_cost
        FROM 
            Chitieu
    ) AS material_count ON 1 = 1;
