namespace Blog.Api.Domain.ValueObjects;

public sealed class Email
{
    public Email()
    {
    }

    public Email(string address)
    {
        Address = address.Trim();
    }

    public string Address { get; }

    public static implicit operator string(Email email)
    {
        return email.ToString();
    }

    public static implicit operator Email(string endereco)
    {
        return new Email(endereco);
    }

    public override string ToString()
    {
        return Address.Trim();
    }
}