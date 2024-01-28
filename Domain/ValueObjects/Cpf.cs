using System.ComponentModel.DataAnnotations;

namespace Blog.ValueObjects.Entities;

public sealed class Cpf
{
    public Cpf() { }
    public Cpf(string text)
    {
        Text = IsValid(text) ? text.Trim() : throw new Exception("bla");
    }

    [MaxLength(11)]
    public string Text { get; private set; }

    public static implicit operator string(Cpf text) => text.ToString();
    
    public static implicit operator Cpf(string text) => new Cpf(text);
    public override string ToString() => Text.Trim();

    private bool IsValid(string cpf)
    {
        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for(int i=0; i<9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if ( resto < 2 )
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for(int i=0; i<10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }
}