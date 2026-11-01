using EduHub.Ch01.Domain;

// ═══════════════════════════════════════════════════════════════
//  EduHub Ch01 — Console demo (luồng nghiệp vụ đầy đủ)
//  Dùng hằng số cho separator, KHÔNG hardcode chuỗi lặp lại.
// ═══════════════════════════════════════════════════════════════

const string Separator = "────────────────────────────────────────";

Console.WriteLine("=== EduHub — Club & Event Management ===");

// TODO 1: Tạo CLB "Tech Club" với mô tả phù hợp         → Club.Create(...)
Club club = Club.Create(
    "Tech Club",
    "Câu lạc bộ công nghệ dành cho sinh viên yêu thích lập trình và AI.");

// TODO 2: Tạo 3 Member, thêm vào CLB                      → Member.Create(...), club.AddMember(...)
Member member1 = Member.Create("Nguyễn Văn An", "an@example.com", "SV001");
Member member2 = Member.Create("Trần Thị Bình", "binh@example.com", "SV002");
Member member3 = Member.Create("Lê Minh Châu", "chau@example.com", "SV003");

club.AddMember(member1);
club.AddMember(member2);
club.AddMember(member3);

// TODO 3: Tạo sự kiện "Workshop AI", capacity = 50        → Event.Create(club.Id, ...)
Event workshop = Event.Create(
    club.Id,
    "Workshop AI",
    DateTime.Now.AddDays(7),
    50,
    false);

// TODO 4: 2 Member đăng ký (kiểm tra HasCapacity) → Pending → gọi Confirm()
var registrations = new List<Registration>();

Registration registration1 = Registration.Create(member1.Id, workshop.Id);
Registration registration2 = Registration.Create(member2.Id, workshop.Id);

if (workshop.HasCapacity(registrations.Count))
{
    registration1.Confirm();
    registrations.Add(registration1);
}

if (workshop.HasCapacity(registrations.Count))
{
    registration2.Confirm();
    registrations.Add(registration2);
}

// TODO 5: 1 Member check-in                               → CheckIn.Create(registration, ...)
CheckIn firstCheckIn = CheckIn.Create(registration1, "QrCode");
List<CheckIn> checkIns = new() { firstCheckIn };

// TODO 6: In báo cáo tham dự: số đăng ký, số check-in, danh sách tên
Console.WriteLine($"\n{Separator}");
Console.WriteLine("THÔNG TIN CÂU LẠC BỘ");
Console.WriteLine($"Tên CLB: {club.Name}");
Console.WriteLine($"Mô tả: {club.Description}");
Console.WriteLine($"Số thành viên: {club.MemberCount()}");

Console.WriteLine($"\n{Separator}");
Console.WriteLine("DANH SÁCH THÀNH VIÊN");
foreach (Member member in club.Members)
{
    Console.WriteLine($"- {member}");
}

Console.WriteLine($"\n{Separator}");
Console.WriteLine("THÔNG TIN SỰ KIỆN");
Console.WriteLine($"Tên sự kiện: {workshop.Title}");
Console.WriteLine($"Ngày tổ chức: {workshop.Date:dd/MM/yyyy HH:mm}");
Console.WriteLine($"Hình thức: {(workshop.IsOnline ? "Online" : "Offline")}");
Console.WriteLine($"Số đăng ký / sức chứa: {registrations.Count} / {workshop.Capacity}");

Console.WriteLine($"\n{Separator}");
Console.WriteLine("THÔNG TIN CHECK-IN");
Console.WriteLine(firstCheckIn.Summary());

Console.WriteLine($"\n{Separator}");
Console.WriteLine("BÁO CÁO THAM DỰ");
Console.WriteLine($"Số đăng ký: {registrations.Count}");
Console.WriteLine("Danh sách thành viên đã đăng ký:");

foreach (Registration registration in registrations)
{
    Member? registeredMember = club.Members.FirstOrDefault(
        member => member.Id == registration.MemberId);

    Console.WriteLine(
        $"- {registeredMember?.Name ?? "Không rõ"} - {registration.Status}");
}

Console.WriteLine($"Số check-in: {checkIns.Count}");

Console.WriteLine("\n✅ Demo complete!");
