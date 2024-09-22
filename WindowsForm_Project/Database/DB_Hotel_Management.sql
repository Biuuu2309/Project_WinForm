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
	numbed INT NOT NULL,
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
	date_ci DATETIME NOT NULL,
	date_co DATETIME NOT NULL,
	maphong INT,
	group_customer INT DEFAULT 0,
	PRIMARY KEY (maphong, cccd_cus),
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
	call_serve BIT,
	cccd_em NVARCHAR(200),
	PRIMARY KEY (cccd_cus, maphong),
	FOREIGN KEY (cccd_cus) REFERENCES Customer(cccd_cus) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (maphong) REFERENCES Room(maphong) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (cccd_em) REFERENCES Employee(cccd_em) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
CREATE TABLE Chamcong (
	cccd_em NVARCHAR(200),
	ngay DATETIME NOT NULL,
	ca1 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca1 IN ('Co', 'Khong')),
	ca2 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca2 IN ('Co', 'Khong')),
	ca3 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca3 IN ('Co', 'Khong')),
	ca4 NVARCHAR(200) NOT NULL DEFAULT 'Khong' CHECK(ca4 IN ('Co', 'Khong')),
	note NTEXT,
	tongluong FLOAT NOT NULL,
	PRIMARY KEY (cccd_em),
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
CREATE OR ALTER PROC sp_account @id INT, @username NVARCHAR(200), @password NVARCHAR(200), @cccd_em NVARCHAR(200), @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Account
					WHERE id = @id)
	BEGIN 
		INSERT INTO Account(username, password, cccd_em) VALUES (@username, @password, @cccd_em)
		SET @ErrorMessage = 'Them Account thanh cong'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Them Account khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_addroom @maphong INT, @roomnumber INT, @roomtype NVARCHAR(200), @numbed INT, @view_room NVARCHAR(200), @price INT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Room
					WHERE maphong = @maphong)
	BEGIN
		INSERT INTO Room(maphong, roomnumber, roomtype, numbed, view_room, price) VALUES (@maphong, @roomnumber, @roomtype, @numbed, @view_room, @price)
		SET @ErrorMessage = 'Them phong thanh cong'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Them phong khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_addemployee @cccd_em INT, @first_name NVARCHAR(200), @last_name NVARCHAR(200), @sdt NVARCHAR(200), @email NVARCHAR(200), @gioitinh NVARCHAR(200), @ngaysinh DATETIME, @luong FLOAT, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	IF NOT EXISTS (	SELECT 1 FROM Employee
					WHERE cccd_em = @cccd_em)
	BEGIN 
		INSERT INTO Employee(cccd_em, first_name, last_name, sdt, email, gioitinh, ngaysinh, luong) VALUES (@cccd_em, @first_name, @last_name, @sdt, @email, @gioitinh, @ngaysinh, @luong)
		SET @ErrorMessage = 'Them Employee thanh cong'
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
		SET @ErrorMessage = 'Da xoa phong thanh cong'
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

		SET @ErrorMessage = 'Update successful'
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
		SET @ErrorMessage = 'Update successful'
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
		SET @ErrorMessage = 'Them customer thanh cong'
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

		SET @ErrorMessage = 'Update Successful'
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END

GO
CREATE OR ALTER PROC sp_addroomupdate @maphong INT = NULL, @status_room NVARCHAR(200) = NULL, @house_keeping NVARCHAR(200) = NULL, @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
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
		SET @ErrorMessage = 'Xoa account thanh cong'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Xoa account khong thanh cong'
	END
END
GO
CREATE OR ALTER PROC sp_addchamcong @cccd_em NVARCHAR(200), @ngay DATETIME, @ca1 NVARCHAR(200), @ca2 NVARCHAR(200), @ca3 NVARCHAR(200), @ca4 NVARCHAR(200), @note NTEXT, @tongluong FLOAT, @ErrorMessage NVARCHAR(200) OUTPUT 
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Employee WHERE cccd_em = @cccd_em)
	BEGIN
		INSERT INTO Chamcong(cccd_em, ngay, ca1, ca2, ca3, ca4, note, tongluong) VALUES (@cccd_em, @ngay, @ca1, @ca2, @ca3, @ca4, @note, @tongluong)
		SET @ErrorMessage = 'Cham cong thanh cong'
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Cham cong khong thanh cong'
	END
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
