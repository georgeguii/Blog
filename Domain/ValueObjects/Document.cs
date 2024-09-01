using Blog.Shared.Exceptions;

namespace Blog.Domain.ValueObjects;

public sealed class Document
{
    public Document()
    {
    }

    public Document(string text)
    {
        if (text.Length.Equals(11))
        {
            Text = IsCpf(text) ? text.Trim() : throw new DomainException("CPF inválido");
        }
        else
        {
            Text = IsCnpj(text) ? text.Trim() : throw new DomainException("CNPJ inválido");
        }
    }

    public string Text { get; }

    public static implicit operator string(Document text) => text.ToString();

    public static implicit operator Document(string text) => new(text);
    public override string ToString() => Text.Trim();

    private bool IsCpf(string cpf)
    {
        var position = 0;
        var totalDigit1 = 0;
        var totalDigit2 = 0;
        var dv1 = 0;
        var dv2 = 0;

        bool identicalDigits = true;
        var lastDigit = -1;

        foreach (var c in cpf)
        {
            if (!char.IsDigit(c)) continue;
            
            var digit = c - '0';
            if (position != 0 && lastDigit != digit)
            {
                identicalDigits = false;
            }

            lastDigit = digit;
            switch (position)
            {
                case < 9:
                    totalDigit1 += digit * (10 - position);
                    totalDigit2 += digit * (11 - position);
                    break;
                case 9:
                    dv1 = digit;
                    break;
                case 10:
                    dv2 = digit;
                    break;
            }

            position++;
        }

        if (position > 11 || identicalDigits)
        {
            return false;
        }

        var digit1 = totalDigit1 % 11;
        digit1 = digit1 < 2 
            ? 0 
            : 11 - digit1;

        if (dv1 != digit1)
        {
            return false;
        }

        totalDigit2 += digit1 * 2;
        var digit2 = totalDigit2 % 11;
        digit2 = digit2 < 2 
            ? 0 
            : 11 - digit2;

        return dv2 == digit2;
    }


    private static bool IsCnpj(string cnpj)
    {
        int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int soma;
        int resto;
        string digito;
        string tempCnpj;
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;
        tempCnpj = cnpj.Substring(0, 12);
        soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito += resto.ToString();
        return cnpj.EndsWith(digito);
    }
}