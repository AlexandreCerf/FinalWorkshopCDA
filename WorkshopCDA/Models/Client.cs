namespace WorkshopCDA.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Client(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public static Client CreateUser(string email, string password)
    {
        return new Client(email, HashPassword(password));
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password,13);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

}
