using Microsoft.EntityFrameworkCore;
using piedteam_net1_2_hocmienphi.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

// Kiến trúc 3 Layer (Tầng).

// Tầng API
    // Chịu trách nhiệm khai báo các endpoint,
        // nhận request, trả response
    // Config hệ thống
    // Tầng API gọi tới Service
// Tầng Service
    // Chịu trách nhiệm xử lý nghiệp vụ
    // Tương tác với tầng Repository để lấy dữ liệu
    // Tầng Service gọi tới Repository
// Tầng Repository
    // Chịu trách nhiệm tương tác với Database
    // Cấu hình những thứ liên quan tới Database
    
// A có 1 cái Req làád đăng nhập vào hệ thống
    // Tầng API: Muốn đăng nhập vào hệ thống á
        // Chui vô đây nè: POST /api/auth/login
            // Nhận request body {email: "tan", password: "123"}
    // Tầng API lúc này gọi xuống tầng Service có cái hàm là
        // Xử lí login: LoginHandler(email, password)
        // Lúc này hàm login trong Service hãy chạy như sau
            // Kiểm tra email | người có tồn tại trong database hay không
            // người dùng này có bị banned hay không
            // Nếu có lỗi thì trả về lỗi
            // Nếu không có lỗi thì trả về Token đăng nhập
    // Tầng Service lúc này gọi xuống tầng Repository có cái hàm là
        // GetUserByEmail(email)
        // Hàm này sẽ chạy câu lệnh SQL để
            // lấy thông tin người dùng ra khỏi database

// Một quá trình phát triển phần mềm thường sẽ trải qua:
    // Đi tìm hiểu nhu cầu của khách hàng
        // Hiểu được nhu cầu rồi -> Phân tích ra các Requirement
            // Tìm hiểu thêm ở trên mạng hoặc học gì đó
        // Tính tiền
    // Sau đó, dựa vào các Requirement, chúng ta sẽ thiết kế ra hệ thống
        // Thiết kế ra kiến trúc của hệ thống
        // Thiết kế ra database (ERD)
   // Implement các Requirement lên Code
   // Test các Requirement đã được Implement
   // Đóng gói - deplou - giao khách hàng
   // Maintain

            
// Nơi cho các học sinh đi vào nền tảng tìm kiếm các Mentor
    // để học tập, trao đổi kiến thức, kinh nghiệm với nhau
    // Khi mà Mentor muốn apply nào nền tảng, thì phải điền thông tin
        // Sau đó Admin sẽ duyệt
// Mentor thì đi vào nền tảng, tạo những lịch rảnh và
    // và các học sinh sẽ book lịch rảnh đó.

// Để 1 User có thể trở thành 1 Mentor
    // User sẽ điền thông tin để apply trở thành Mentor
        // 1 User sẽ có yêu cầu (ApplyRequest) để trở thành Mentor
            // Khi mà người dùng đưa cho mình file Cv, chúng ta sẽ upload file đó lên Cloud
        // Mối quan hệ giữa User và ApplyRequest
        // => MỐi quan hệ 1 - N: Có thể có đơn bị từ chối hoặc được duyệt, nhưng chỉ có 1 đơn được duyệt
            // Khi mà có User nộp đơn apply, thì hệ thống phải thông báo cho Admin
            // Khi mà Admin duyệt (Từ chối, Chấp nhận) thì phải thông báo cho User
    // Admin sẽ duyệt thông tin đó, nếu thông tin hợp lệ thì sẽ duyệt
    // Nếu được duyệt, thì User đó sẽ trở thành Mentor/
    // Chỉ có User nào có quyền Admin thì mới được sử dụng các API như lấy đơn hệ thống/phê duyệt đơn

    // API:
        // Tạo đơn
            // (Dành cho User)
            // POST /api/applyRequest
                // Để gọi được API này, cần CV và mô tả bản thân
        // Lấy các đơn apply của tôi
            // GET /api/applyRequest/me
            // Mentor cần api này để kiểm tra xem tiến độ | tình trạng của đơn
        // Lấy tất cả các đơn apply
            // (Dành cho Admin)
            // GET /api/applyRequest
            // Admin cần api này để duyệt đơn apply của người dùng
        // Lấy thông tin chi tiết của đơn này
            // GET /api/applyRequest/{id}
            // (Dành cho Admin và User)
        // Duyệt đơn apply 
            // (Dành cho Admin)
            // POST /api/applyRequest/{id}/review
            // Khi duyệt đơn, thì Admin có thể chọn duyệt hoặc từ chối
            // Nếu từ chối thì phải có lý do từ chối
            // Khi mà duyệt dơn xong, thì role của User phải được đổi thành Mentor
            // Khi duyệt đơn, thì hệ thống phải thông báo cho User về kết quả của đơn apply đó

// Kĩ thuật snapshot:
    // Đầu tiên a có bản
    // Trong năm 2026, anh Tân bán 1 cái Áo với giá 1000
    // Sau do, Bao thay "Ao dep qua, muon mua cho crush"
        // Bao mua don hang (voi ID O1) tong la 3000,
            // trong don hang co 2 san pham, P1 va P2
    // Thời gian đưa trôi thấm thoá
    // Bây giờ là năm 2027, anh Tân đổi giá Áo (Id 1) thành 2000
    // Sau do bao chia tay ny, va bao doi lai qua, luc do
        // bao lay hoa don voi san pham P1,
        // Crush thi keu la ngay xua a mua em co 1000 ah
        // Sao gio anh Bao doi lai 2000
    
// App này gồm bao nhiều ngừi xài:
    // Admin:
        // Quản lí User (Học sinh và Mentor) của nền tảng
        // Quản lí và phê duyệt đơn để trở thành Mentor
    // Mentor
        // Tạo lịch rảnh để học sinh book
        // Quản lí lịch Book (Dời Lịch, Huỷ Lịch)
        // Quản li Profile
    // Học sinh 
        // Book lịch rảnh của Mentor
        // Quản lí lịch Book (Dời Lịch, Huỷ Lịch)
        // Quản li Profile

// 4W 1H
// WHAT
    // Vấn đề | Sự Việc này là cái gì
// WHY
    // Tại sao lại có vấn đề này, tại sao lại cần giải quyết vấn đề này
// WHEN
    // Khi nào thì vấn đề này xảy ra, khi nào thì cần giải quyết vấn đề này
// WHERE
    // Ở đâu thì vấn đề này xảy ra, ở đâu thì cần giải quyết vấn đề này
// HOW
    // Giải quyết như thế nào