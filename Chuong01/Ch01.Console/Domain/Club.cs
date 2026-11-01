namespace EduHub.Ch01.Domain;

/// <summary>Câu lạc bộ sinh viên.</summary>
public class Club
{
    // TODO: Guid Id, string Name, string Description, DateTime CreatedAt  (private set)
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // TODO: danh sách thành viên: field private List<Member> + property IReadOnlyList<Member> Members
    //       (bên ngoài không được .Add() trực tiếp — phải đi qua AddMember)
    private readonly List<Member> _members = new();
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    // TODO: constructor PRIVATE + public static Club Create(string name, string description)
    private Club(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name không được để trống.", nameof(name));

        return new Club(name.Trim(), string.IsNullOrWhiteSpace(description) ? string.Empty : description.Trim());
    }

    // TODO: public void AddMember(Member member) — chặn null và thành viên trùng
    public void AddMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));

        if (_members.Any(existingMember => existingMember.Id == member.Id))
            throw new InvalidOperationException("Thành viên đã tồn tại trong câu lạc bộ.");

        _members.Add(member);
    }

    // TODO: public int MemberCount()
    public int MemberCount()
    {
        return _members.Count;
    }
}
