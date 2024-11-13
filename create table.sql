-- Table: CompanyInfo
use deTai4
CREATE TABLE CompanyInfo (
    CompanyId INT PRIMARY KEY,
    CompanyName VARCHAR(255),
    Address VARCHAR(255),
    PhoneNumber VARCHAR(15),
    Email VARCHAR(255)
);

-- Table: Blogs
CREATE TABLE Blogs (
    Id INT PRIMARY KEY,
    Title VARCHAR(255),
    Content TEXT,
    CreateDate DATE,
    Author VARCHAR(255)
);

-- Table: Reports
CREATE TABLE Reports (
    ReportId INT PRIMARY KEY,
    ReportDate DATE,
    TotalProjects INT,
    TotalQuotationAmount DECIMAL(18, 2),
    ActivePromotions INT
);

-- Table: Promotion
CREATE TABLE Promotion (
    PromotionId INT PRIMARY KEY,
    PromotionName VARCHAR(255),
    Discount DECIMAL(5, 2),
    StartDate DATE,
    EndDate DATE
);

-- Table: _EFMigrationsHistory
CREATE TABLE _EFMigrationsHistory (
    MigrationId INT PRIMARY KEY,
    ProductVersion VARCHAR(255)
);

-- Table: User
CREATE TABLE [User] (
    UserId INT PRIMARY KEY,
    Username VARCHAR(255),
    PasswordHash VARCHAR(255),
    FullName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(15)
);

-- Table: Staff
CREATE TABLE Staff (
    StaffId INT PRIMARY KEY,
    UserId INT,
    Role VARCHAR(50),
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

-- Table: Customer
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    UserId INT,
    Address VARCHAR(255),
    PhoneNumber VARCHAR(15),
    FullName VARCHAR(255),
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
CREATE TABLE Design (
    DesignId INT PRIMARY KEY,
    DesignName VARCHAR(255),
    Description TEXT,
    ImageUrl TEXT,
    Cost DECIMAL(18, 2)
);
-- Table: Project
CREATE TABLE Project (
    ProjectId INT PRIMARY KEY,
    CustomerId INT,
    DesignId INT,
    ProjectName VARCHAR(255),
    Rating INT,
    StartDate DATE,
    EndDate DATE,
    Status VARCHAR(50),
    RequestDetails TEXT,
    QuotationAmount DECIMAL(18, 2),
    QuotationDetails TEXT,
    QuotationDate DATE,
    UserId INT,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (DesignId) REFERENCES [Design](DesignId),
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
CREATE TABLE Service (
    ServiceId INT PRIMARY KEY,
    ServiceName VARCHAR(255),
    Description TEXT,
    Cost DECIMAL(18, 2)
);
-- Table: Order
CREATE TABLE CustomerOrder (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    ServiceId INT,
    TotalCost DECIMAL(18, 2),
    OrderDate DATE,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (ServiceId) REFERENCES [Service](ServiceId)
);

-- Table: Service


-- Table: Invoice
CREATE TABLE Invoice (
    InvoiceId INT PRIMARY KEY,
    OrderId INT,
    TotalAmount DECIMAL(18, 2),
    PaymentMethod VARCHAR(50),
    PaymentDate DATE,
    PaymentStatus VARCHAR(50),
    CustomerId INT,
    FOREIGN KEY (OrderId) REFERENCES CustomerOrder(OrderId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

-- Table: Design


-- Table: MaintenanceResult
CREATE TABLE MaintenanceResult (
	MaintenanceResultId INT PRIMARY KEY,
    ServiceId INT,
    StaffId INT,
    ResultDescription TEXT,
    ResultDate DATE,
    ProjectId INT,
    FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId),
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId)
);

-- Table: Feedback
CREATE TABLE Feedback (
    FeedbackId INT PRIMARY KEY,
    OrderId INT,
    Rating INT,
    Comment TEXT,
    FeedbackDate DATE,
    FOREIGN KEY (OrderId) REFERENCES CustomerOrder(OrderId)
);

-- Table: Quotations
CREATE TABLE Quotations (
    QuotationId INT PRIMARY KEY,
    ProjectId INT,
    Amount DECIMAL(18, 2),
    Details TEXT,
    CreateDate DATE,
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId)
);
-- Bảng trung gian giữa Project và Staff để quản lý các nhân viên tham gia dự án
CREATE TABLE ProjectStaff (
    ProjectId INT,
    StaffId INT,
    PRIMARY KEY (ProjectId, StaffId),
    CONSTRAINT FK_ProjectStaff_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_ProjectStaff_Staff FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);

-- Bảng trung gian giữa Service và Staff để quản lý các nhân viên cung cấp dịch vụ bảo dưỡng
CREATE TABLE ServiceStaff (
    ServiceId INT,
    StaffId INT,
    PRIMARY KEY (ServiceId, StaffId),
    CONSTRAINT FK_ServiceStaff_Service FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId),
    CONSTRAINT FK_ServiceStaff_Staff FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);
INSERT INTO Blogs (Title, Content, CreatedDate, Author)
VALUES 
    (N'Lịch Sử và Ý Nghĩa Của Hồ Cá Koi Trong Văn Hoá Nhật Bản', 
     N'Hồ cá koi không chỉ là một thú chơi tao nhã mà còn mang ý nghĩa văn hóa sâu sắc trong văn hóa Nhật Bản. Bài viết này sẽ tìm hiểu về nguồn gốc của cá koi, ý nghĩa của các màu sắc và hình dạng khác nhau, cũng như những giá trị tinh thần mà nó mang lại trong cuộc sống người Nhật.', 
     GETDATE(), 
     N'Trihn'),

    (N'Cách Xây Dựng Hồ Cá Koi Đẹp Trong Sân Vườn', 
     N'Để có một hồ cá koi đẹp và phong thủy, bạn cần chuẩn bị kỹ lưỡng từ khâu thiết kế đến xây dựng. Bài viết này hướng dẫn cách lựa chọn kích thước, vật liệu, cũng như các yếu tố cần thiết như hệ thống lọc nước, cây thủy sinh và đèn trang trí để tạo ra một hồ cá koi hoàn hảo.', 
     GETDATE(), 
     N'Trihn'),

    (N'Cách Chăm Sóc và Nuôi Dưỡng Cá Koi Khỏe Mạnh', 
     N'Cá koi cần được chăm sóc kỹ lưỡng để giữ được màu sắc rực rỡ và sức khỏe tốt. Bài viết sẽ chia sẻ về các loại thức ăn phù hợp, lịch trình cho ăn, các loại bệnh thường gặp và cách phòng ngừa, cũng như bí quyết để cá koi phát triển toàn diện.', 
     GETDATE(), 
     N'Trihn'),

    (N'Lựa Chọn Cá Koi Theo Phong Thủy: Những Điều Cần Biết', 
     N'Trong phong thủy, cá koi được xem là biểu tượng của tài lộc, thịnh vượng và may mắn. Bài viết sẽ giới thiệu cách chọn cá koi theo màu sắc, hình dạng và số lượng phù hợp với phong thủy của gia chủ để mang lại nhiều điều tốt lành.', 
     GETDATE(), 
     N'Trihn'),

    (N'Top 5 Loại Cá Koi Đẹp Nhất Và Giá Trị Của Chúng', 
     N'Trên thị trường hiện nay có rất nhiều loại cá koi với màu sắc và hoa văn độc đáo. Bài viết này sẽ giới thiệu top 5 loại cá koi đẹp nhất như Kohaku, Taisho Sanke, Showa, và giải thích vì sao chúng được ưa chuộng cũng như mức giá trung bình của từng loại.', 
     GETDATE(), 
     N'Trihn');
-- Thêm dữ liệu vào bảng CompanyInfo
INSERT INTO CompanyInfo (CompanyName, Address, PhoneNumber, Email)
VALUES 
('Koi Pond Design', 'Ho Chi Minh City', '0909123456', 'KoiPondDesign@domain.com'),
('Aquatic Landscape', 'Hanoi', '0908765432', 'AquaticLandscape@domain.com'),
('Green Waters', 'Da Nang', '0901234567', 'GreenWaters@domain.com');

-- Thêm dữ liệu vào bảng Customer
INSERT INTO Customer (Address, PhoneNumber, FullName)
VALUES
('123 Main St, City A', '0900000002', 'John Doe'),
('456 Elm St, City B', '0900000003', 'Jane Smith');

-- Thêm dữ liệu vào bảng Feedback
INSERT INTO Feedback (OrderId, Rating, Comment, FeedbackDate)
VALUES
(1, 5, 'Excellent service!', '2024-07-05 00:00:00'),
(2, 4, 'Good but needs improvement', '2024-08-05 00:00:00');

-- Thêm dữ liệu vào bảng Invoice
INSERT INTO Invoice (OrderId, TotalAmount, PaymentMethod, PaymentDate, PaymentStatus)
VALUES
(1, 100.00, 'Credit Card', '2024-07-01 00:00:00', 'Paid'),
(2, 200.00, 'PayPal', '2024-08-02 00:00:00', 'Pending');

-- Thêm dữ liệu vào bảng MaintenanceResult
INSERT INTO MaintenanceResult (ServiceId, StaffId, ResultDescription, ResultDate)
VALUES
(1, 1, 'Standard maintenance completed', '2024-01-15 00:00:00'),
(2, 2, 'Deluxe maintenance completed', '2024-05-20 00:00:00');
-- Thêm dữ liệu vào bảng [Order]
INSERT INTO [Order] (CustomerId, ServiceId, OrderDate, Status, TotalCost, UserId, FullName)
VALUES
(1, 1, '2024-07-01 00:00:00', 'Completed', 100.00, 2, 'John Doe'),
(2, 2, '2024-08-01 00:00:00', 'Pending', 200.00, 3, 'Jane Smith');

-- Thêm dữ liệu vào bảng Project
INSERT INTO Project (CustomerId, DesignId, ProjectName, StaffId, StartDate, EndDate, Status, RequestDetails, UserId)
VALUES
(1, 1, 'John\'s Koi Pond', 1, '2024-01-01 00:00:00', '2024-03-01 00:00:00', 'Completed', 'hợp với vị trí nhà', 2),
(2, 2, 'Jane\'s Deluxe Pond', 2, '2024-04-01 00:00:00', '2024-06-01 00:00:00', 'In Progress', 'xanh mát', 3),
(1, 1, 'Yêu cầu thi công', 1, '2024-10-26 08:30:00', '2024-12-26 08:30:00', 'Pending', 'xanh mát', 2);

-- Thêm dữ liệu vào bảng Service
INSERT INTO Service (ServiceName, Description, Cost)
VALUES
('Standard Maintenance', 'Basic maintenance services', 100.00),
('Deluxe Maintenance', 'Comprehensive maintenance package', 200.00);

-- Thêm dữ liệu vào bảng ServiceStaff
INSERT INTO ServiceStaff (ServiceId, StaffId)
VALUES
(1, 1),
(2, 2);
INSERT INTO ProjectStaff (ProjectId, StaffId)
VALUES
(2, 1),
(3, 2);
-- Tạo bảng Promotion
CREATE TABLE Promotion (
    PromotionId INT PRIMARY KEY IDENTITY(1,1),
    PromotionName NVARCHAR(100),
    Discount DECIMAL(5, 2),
    StartDate DATETIME,
    EndDate DATETIME
);

-- Chèn dữ liệu vào bảng Promotion
INSERT INTO Promotion (PromotionName, Discount, StartDate, EndDate)
VALUES
('Summer Sale', 10.00, '2024-06-01 00:00:00', '2024-06-30 00:00:00'),
('New Year Discount', 15.00, '2024-12-01 00:00:00', '2024-12-31 00:00:00');
INSERT INTO Staff (UserId, Role, FullName)
VALUES
(4, 'Consulting Staff', 'Consultant One'),
(5, 'Design Staff', 'Designer One'),
(6, 'Construction Staff', 'Construction One');
INSERT INTO [User] (Username, PasswordHash, FullName, Email, PhoneNumber, Role)
VALUES
('guestuser', 'hash_guest', 'Guest User', 'guest@company.com', '0900000001', 'Guest'),
('john_doe', 'hash_customer1', 'John Doe', 'johndoe@example.com', '0900000002', 'Customer'),
('jane_smith', 'hash_customer2', 'Jane Smith', 'janesmith@example.com', '0900000003', 'Customer'),
('consultant1', 'hash_consultant1', 'Consultant One', 'consultant1@company.com', '0900000004', 'ConsultingStaff'),
('designer1', 'hash_designer1', 'Designer One', 'designer1@company.com', '0900000005', 'DesignStaff'),
('construction1', 'hash_construction1', 'Construction One', 'construction1@company.com', '0900000006', 'ConstructionStaff'),
('manager1', 'hash_manager1', 'Manager One', 'manager1@company.com', '0900000007', 'Manager'),
('phamquochuy', '123456789', 'pham_quoc_huy', 'quochuy15012000@gmail.com', '0900000008', 'Customer'),
('mmmm', '123456789', 'mmmm', 'mmmm@gmail.com', '1234567890', 'Customer'),
('qqqq', '123456789', 'qqqq', 'qqqq@gmail.com', '1234545678', 'Customer'),
('quockhanh', '123456789', 'khanh', 'khanh@gmail.com', '0147258369', 'Customer');