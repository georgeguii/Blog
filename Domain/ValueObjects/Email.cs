namespace Blog.Domain.ValueObjects;

public sealed class Email
{
    public Email()
    {
    }

    public Email(string address)
    {
        Address = address.Trim();
    }

    public string Address { get; private set; }

    public static implicit operator string(Email email) => email.ToString();

    public static implicit operator Email(string endereco) => new Email(endereco);
    public override string ToString() => Address.Trim();
}