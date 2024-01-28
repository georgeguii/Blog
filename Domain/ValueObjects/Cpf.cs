using System.ComponentModel.DataAnnotations;
using Blog.Shared.Exceptions;

namespace Blog.ValueObjects.Entities;

public sealed class Cpf
{
    public Cpf() { }

    public Cpf(string text)
    {
        Text = IsValid(text) ? text.Trim() : throw new DomainException("CPF inválido");
    }

    [MaxLength(11)]
    public string Text { get; private set; }

    public static implicit operator string(Cpf text) => text.ToString();

    public static implicit operator Cpf(string text) => new Cpf(text);
    
    public override string ToString() => Text.Trim();

    private bool IsValid(string cpf)
    {
        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf = tempCpf + digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
}

