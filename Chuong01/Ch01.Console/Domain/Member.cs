namespace EduHub.Ch01.Domain;

/// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
public class Member
{
    // TODO: Guid Id, string Name, string Email, string StudentCode, DateTime JoinedAt
    //       → tất cả dùng `private set` (hoặc init), KHÔNG public setter
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string StudentCode { get; private set; }
    public DateTime JoinedAt { get; private set; }

    // TODO: constructor PRIVATE tự sinh Id (Guid.NewGuid()) và JoinedAt (DateTime.UtcNow)
    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
    }

    // TODO: public static Member Create(string name, string email, string studentCode)
    //       → validate: name/studentCode không trống, email chứa '@'; sai thì throw ArgumentException
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name không được để trống.", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ.", nameof(email));

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("StudentCode không được để trống.", nameof(studentCode));

        return new Member(name.Trim(), email.Trim(), studentCode.Trim());
    }

    // TODO: public bool IsActive()  — thể hiện behavior của Member
    public bool IsActive()
    {
        return Id != Guid.Empty && JoinedAt != default;
    }

    // TODO: override ToString() → "{Name} ({StudentCode})"
    public override string ToString()
    {
        return $"{Name} ({StudentCode})";
    }
}
