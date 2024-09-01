namespace Blog.Api.Shared.Exceptions;

public sealed class InvalidParametersException : Exception
{
    private const string _message = "Dado inválido.";

    public InvalidParametersException(string message = _message) : base(message)
    {
    }

    public static void ThrowIfNull(string? item, string message = _message)
    {
        if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
            throw new InvalidParametersException(message);
    }

    public static void ThrowIfNull(
        string?[] items,
        string message = _message)
    {
        foreach (var item in items)
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                throw new InvalidParametersException(message);
    }

    public static void ThrowIfNull<T>(T obj, string message = _message) where T : class
    {
        if (obj == null) throw new InvalidParametersException(message);
    }

    public static void ThrowIfNull<T>(List<T> obj, string message = _message) where T : class
    {
        if (obj == null) throw new InvalidParametersException(message);
    }
}