using System.ComponentModel.DataAnnotations;

namespace Blog.ValueObjects.Entities;

public sealed class Description
{
    public Description() { }
    public Description(string text)
    {
        Text = text.Trim();
    }

    public string Text { get; private set; }

    public static implicit operator string(Description text) => text.ToString();
    
    public static implicit operator Description(string text) => new Description(text);
    public override string ToString() => Text.Trim();
}