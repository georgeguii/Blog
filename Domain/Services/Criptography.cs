namespace Blog.Domain.Services;

public static class Criptography
{
    public static string Encrypt(this string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return senhaCriptografada;
    }

    public static bool CompareHash(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}