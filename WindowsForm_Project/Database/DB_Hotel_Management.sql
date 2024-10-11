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
CREATE TABLE Bookings (
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
	price INT NOT NULL CHECK (price > 0),
	PRIMARY KEY (cccd_cus, maphong),
	FOREIGN KEY (cccd_cus) REFERENCES Customer(cccd_cus) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (maphong) REFERENCES Room(maphong) ON UPDATE CASCADE ON DELETE CASCADE,
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
	cccd_cus NVARCHAR(200),
	maphong INT,
	other_booking NVARCHAR(200),
	anuong NVARCHAR(200),
	call_serve BIT DEFAULT 0,
	PRIMARY KEY (cccd_cus, maphong),
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
INSERT INTO Room (maphong, roomnumber, roomtype, numbed, view_room, price)
VALUES
	('1', '101', 'STD', '1', 'Good', '1000000'),
	('2', '102', 'DLX', '2', 'Simple', '2000000'),
	('3', '103', 'SUP', '3', 'Beautiful', '3000000'),
	('4', '104', 'SUT', '1', 'Good', '2000000'),
	('5', '105', 'DLX', '2', 'Beautiful', '3000000')

GO
INSERT INTO Update_room(maphong, status_room, house_keeping)
VALUES
	('1', 'Reserved', 'Clean'),
	('2', 'Occupied', 'Not Clean'),
	('3', 'Available', 'In Progress'),
	('4', 'Check Out', 'Repair'),
	('5', 'Available', 'Clean')
GO
INSERT INTO Employee(cccd_em, first_name, last_name, sdt, email, gioitinh, ngaysinh, luong)
VALUES
	('12012309', 'Biu', 'Biu', '123', 'BiuBiu@gmail.com', 'Nu', '2004-09-23', '200000'),
	('12012308', 'Biu', 'Biuu', '234', 'BiuBiuu@gmail.com', 'Nam', '2004-09-22', '100000'),
	('12012307', 'Biu', 'Biuuu', '345', 'BiuBiuuu@gmail.com', 'Nu', '2004-09-21', '300000'),
	('12012306', 'Biu', 'Biuuuu', '456', 'BiuBiuuuu@gmail.com', 'Nam', '2004-09-20', '400000'),
	('12012305', 'Biu', 'Biuuuuu', '567', 'BiuBiuuuuu@gmail.com', 'Nu', '2004-09-19', '200000')
GO
INSERT INTO Account(username, password, cccd_em)
VALUES
	('BiuBiu', 'BiuBiu', '12012309'),
	('BiuBiuu', 'BiuBiuu', '12012308'),
	('BiuBiuuu', 'BiuBiuuu', '12012307'),
	('BiuBiuuuu', 'BiuBiuuuu', '12012306'),
	('BiuBiuuuuu', 'BiuBiuuuuu', '12012305')
GO
INSERT INTO Customer(cccd_cus, first_name, last_name, sdt, email, gioitinh, ngaysinh, address_cus)
VALUES
	('12022309', 'Haha', 'Hihi', '1234', 'HahaHihi', 'Nam', '2004-09-24', 'HCM'),
	('12032309', 'Haha', 'Hihii', '2345', 'HahaHihii', 'Nu', '2004-09-25', 'HN'),
	('12042309', 'Haha', 'Hihiii', '3456', 'HahaHihiii', 'Nam', '2004-09-26', 'DN'),
	('12052309', 'Haha', 'Hihiiii', '4567', 'HahaHihiiii', 'Nu', '2004-09-27', 'BD'),
	('12062309', 'Haha', 'Hihiiiii', '5678', 'HahaHihiiiii', 'Nam', '2004-09-28', 'NA'),
	('12072309', 'Haha', 'Hihiiiiii', '5678', 'HahaHihiiiii', 'Nam', '2004-09-28', 'NA')
GO
INSERT INTO Bookings(cccd_cus, status_room, house_keeping, roomtype, numbed, view_room, date_ci, date_co, group_customer, maphong, roomnumber, price)
VALUES
	('12022309', 'Reserved', 'Clean', 'STD', '1', 'Good', '2024-09-20', '2024-09-21', '0',  '1', '101', '111110'),
	('12032309', 'Occupied', 'Not Clean', 'DLX', '2', 'Simple', '2024-09-19', '2024-09-22', '0',  '2', '102', '2222221'),
	('12042309', 'Available', 'In Progress', 'SUP', '3', 'Beautiful', '2024-09-18', '2024-09-20', '0',  '3', '103', '222222'),
	('12052309', 'Check Out', 'Repair', 'SUT', '1', 'Good', '2024-09-17', '2024-09-21', '0',  '4', '104', '2222221'),
	('12062309', 'Available', 'Clean', 'DLX', '2', 'Beautiful', '2024-09-16', '2024-09-20', '0',  '5', '105', '2222222')
GO
INSERT INTO Report(cccd_cus, maphong, ghichu)
VALUES
	('12022309', '1', 'Biuuuuuuuuuu'),
	('12032309', '2', 'Beoooooooo'),
	('12052309', '4', 'Bemmmmmmm')
GO
INSERT INTO Chamcong(cccd_em, ngay, ca1, ca2, ca3, ca4, note)
VALUES
	('12012309', '2024-09-20', 'Co', 'Khong', 'Co', 'Khong', 'Good'),
	('12012308', '2024-09-20', 'Khong', 'Co', 'Khong', 'Co', 'Good'),
	('12012307', '2024-09-20', 'Co', 'Khong', 'Co', 'Khong', 'Good'),
	('12012306', '2024-09-20', 'Khong', 'Co', 'Khong', 'Co', 'Good'),
	('12012305', '2024-09-20', 'Co', 'Khong', 'Co', 'Khong', 'Good'),
	('12012309', '2024-09-21', 'Khong', 'Co', 'Khong', 'Co', 'Good'),
	('12012308', '2024-09-21', 'Co', 'Khong', 'Co', 'Khong', 'Good'),
	('12012307', '2024-09-21', 'Khong', 'Co', 'Khong', 'Co', 'Good'),
	('12012306', '2024-09-21', 'Co', 'Khong', 'Co', 'Khong', 'Good'),
	('12012305', '2024-09-21', 'Khong', 'Co', 'Khong', 'Co', 'Good')
GO
INSERT INTO Serve(cccd_cus, maphong, other_booking, anuong, call_serve)
VALUES
	('12022309', '1', 'Khong', 'Khong', '0'),
	('12032309', '2', 'Khong', 'Khong', '0'),
	('12042309', '3', 'Khong', 'Khong', '0'),
	('12052309', '4', 'Khong', 'Khong', '0'),
	('12062309', '5', 'Khong', 'Khong', '0')
GO 
CREATE OR ALTER PROC sp_addbooking @cccd_cus NVARCHAR(200), @status_room NVARCHAR(200), @house_keeping NVARCHAR(200), @roomtype NVARCHAR(200), @numbed INT, @view_room NVARCHAR(200), @maphong INT, @roomnumber INT, @group_customer INT, @date_ci DATETIME, @date_co DATETIME, @price INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS 
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Bookings
					WHERE cccd_cus = @cccd_cus AND maphong = @maphong)
	BEGIN
		INSERT INTO Bookings (cccd_cus, status_room, house_keeping, roomtype, numbed, view_room, maphong, roomnumber, group_customer, date_ci, date_co, price) VALUES (@cccd_cus, @status_room, @house_keeping, @roomtype, @numbed, @view_room, @maphong, @roomnumber, @group_customer, @date_ci, @date_co, @price)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN 
		SET @ErrorMessage = 'Add Booking error'
	END
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
CREATE OR ALTER PROC sp_updateserve @cccd_cus NVARCHAR(200) = NULL, @maphong INT = NULL, @other_booking NVARCHAR(200) = NULL, @anuong NVARCHAR(200) = NULL, @call_serve BIT = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Serve
					WHERE cccd_cus = @cccd_cus AND maphong = @maphong)
	BEGIN 
		INSERT INTO Serve(cccd_cus, maphong, other_booking, anuong, call_serve) VALUES (@cccd_cus, @maphong, @other_booking, @anuong, @call_serve)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE
	BEGIN TRY
		UPDATE Serve
			SET 
				other_booking = COALESCE(@other_booking,other_booking),
				anuong = COALESCE(@anuong,anuong),
				call_serve = COALESCE(@call_serve,call_serve)
			WHERE cccd_cus = @cccd_cus AND maphong = @maphong
			SET @ErrorMessage = 'Successfully'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE()
	END CATCH
END
SELECT * FROM Serve
GO
CREATE OR ALTER PROC sp_addroom @maphong INT, @roomnumber INT, @roomtype NVARCHAR(200), @numbed INT, @view_room NVARCHAR(200), @price INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Room
					WHERE maphong = @maphong)
	BEGIN
		INSERT INTO Room(maphong, roomnumber, roomtype, numbed, view_room, price) VALUES (@maphong, @roomnumber, @roomtype, @numbed, @view_room, @price)
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
CREATE OR ALTER PROC sp_updateroom @maphong INT = NULL, @roomnumber INT = NULL, @roomtype NVARCHAR(200) = NULL, @numbed INT = NULL, @view_room NVARCHAR(200) = NULL, @price INT = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
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
	IF NOT EXISTS (SELECT 1 FROM Checkout WHERE @cccd_cus = cccd_cus)
	BEGIN 
		INSERT INTO Checkout(cccd_cus, first_name, last_name, maphong, sophong, date_ci, date_co) VALUES (@cccd_cus, @first_name, @last_name, @maphong, @sophong, @date_ci, @date_co)
		SET @ErrorMessage = 'Successfully'
	END
	ELSE 
	BEGIN
		SET @ErrorMessage = ERROR_MESSAGE()
	END
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

GO
SELECT DISTINCT Employee.cccd_em, first_name, last_name, DATEDIFF(DAY, MIN(ngay), GETDATE()) AS days_since_start, COUNT(*) AS total_shifts, luong, luong * COUNT(*) AS total_salary
FROM Employee
INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
WHERE Employee.cccd_em = Chamcong.cccd_em AND (ca1 = 'Co' OR ca2 = 'Co' OR ca3 = 'Co' OR ca4 = 'Co')
GROUP BY Employee.cccd_em, first_name, last_name, luong
ORDER BY Employee.cccd_em

GO
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