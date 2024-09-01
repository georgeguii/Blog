using Blog.Api.Shared.Exceptions;

namespace Blog.Api.Domain.ValueObjects;

public enum DocumentType
{
    CPF,
    CNPJ
}

public sealed class Document
{
    public Document()
    {
    }

    public Document(string text)
    {
        var sanitizedText = string.Concat(text.Where(char.IsDigit)).Trim();

        (Text, Type) = sanitizedText.Length switch
        {
            11 => CpfValidator.IsValid(sanitizedText) ? (sanitizedText, DocumentType.CPF) : throw new DomainException($"{DocumentType.CPF} inválido"),
            14 => CnpjValidator.IsValid(sanitizedText) ? (sanitizedText, DocumentType.CNPJ) : throw new DomainException($"{DocumentType.CNPJ} inválido"),
            _ => throw new DomainException("Documento inválido")
        };
    }

    public DocumentType Type { get; }

    public string Text { get; }

    public static implicit operator string(Document text)
    {
        return text.ToString();
    }

    public static implicit operator Document(string text)
    {
        return new Document(text);
    }

    public override string ToString()
    {
        return Text.Trim();
    }
}

internal static class CpfValidator
{
    public static bool IsValid(string cpf)
    {
        var position = 0;
        var totalDigit1 = 0;
        var totalDigit2 = 0;
        var dv1 = 0;
        var dv2 = 0;

        var identicalDigits = true;
        var lastDigit = -1;

        foreach (var c in cpf)
        {
            if (!char.IsDigit(c)) continue;

            var digit = c - '0';
            if (position != 0 && lastDigit != digit) identicalDigits = false;

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

        if (position > 11 || identicalDigits) return false;

        var digit1 = totalDigit1 % 11;
        digit1 = digit1 < 2
            ? 0
            : 11 - digit1;

        if (dv1 != digit1) return false;

        totalDigit2 += digit1 * 2;
        var digit2 = totalDigit2 % 11;
        digit2 = digit2 < 2
            ? 0
            : 11 - digit2;

        return dv2 == digit2;
    }
}
internal static class CnpjValidator
{
    public static bool IsValid(string cnpj)
    {
        if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
            return false;

        var multipliers1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multipliers2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var digit1 = CalculateDigit(cnpj.Substring(0, 12), multipliers1);
        var digit2 = CalculateDigit(cnpj.Substring(0, 12) + digit1, multipliers2);

        return cnpj.EndsWith($"{digit1}{digit2}");
    }

    private static int CalculateDigit(string input, int[] multipliers)
    {
        var sum = input.Select((t, i) => (t - '0') * multipliers[i]).Sum();
        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }
}