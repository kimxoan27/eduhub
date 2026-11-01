namespace EduHub.Ch01.Domain;

/// <summary>Ghi nhận sự hiện diện thực tế của một Registration (quan hệ 1–1).</summary>
public class CheckIn
{
    // TODO: Guid Id, Guid RegistrationId, DateTime CheckedInAt, string Method ("Manual" hoặc "QrCode") (private set)
    public Guid Id { get; private set; }
    public Guid RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }
    public string Method { get; private set; }

    // TODO: constructor PRIVATE + public static CheckIn Create(Registration registration, string method)
    //       → chỉ cho phép khi registration.Status == Confirmed
    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    public static CheckIn Create(Registration registration, string method)
    {
        if (registration is null)
            throw new ArgumentNullException(nameof(registration));

        if (registration.Status != Registration.RegistrationStatus.Confirmed)
            throw new InvalidOperationException("Chỉ phiếu đăng ký đã xác nhận mới được check-in.");

        if (string.IsNullOrWhiteSpace(method))
            throw new ArgumentException("Method không được để trống.", nameof(method));

        return new CheckIn(registration.Id, method.Trim());
    }

    // TODO: public string Summary() 
    public string Summary()
    {
        return $"Check-in lúc {CheckedInAt:HH:mm:ss} bằng {Method}";
    }
}
