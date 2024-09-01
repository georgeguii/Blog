namespace Blog.Api.Domain.ValueObjects;

public sealed class Description
{
    public Description()
    {
    }

    public Description(string text)
    {
        Text = text.Trim();
    }

    public string Text { get; }

    public static implicit operator string(Description text)
    {
        return text.ToString();
    }

    public static implicit operator Description(string text)
    {
        return new Description(text);
    }

    public override string ToString()
    {
        return Text.Trim();
    }
}