namespace Blog.Unit.Tests.Domain.ValueObjects;

public class DocumentTestData
{
    public static IEnumerable<object[]> ValidCpfCases => new List<object[]>
    {
        new object[] { "745.425.380-60", "74542538060" },
        new object[] { "74542538060", "74542538060" },
        new object[] { "74542.-./538060", "74542538060" }
    };

    // CNPJ Test Cases
    public static IEnumerable<object[]> ValidCnpjCases => new List<object[]>
    {
        new object[] { "12.345.678/0001-95", "12345678000195" },
        new object[] { "12345678000195", "12345678000195" },
        new object[] { "1234567.-./8000195", "12345678000195" }
    };

    // Invalid Document Test Cases
    public static IEnumerable<object[]> InvalidDocumentCases => new List<object[]>
    {
        new object[] { "12345678900033300" },
        new object[] { "123456784" }
    };

    // Invalid CPF Test Cases
    public static IEnumerable<object[]> InvalidCpfCases => new List<object[]>
    {
        new object[] { "692.618.740-11" },
        new object[] { "69261874011" },
        new object[] { "00000000000" }
    };

    // Invalid CNPJ Test Cases
    public static IEnumerable<object[]> InvalidCnpjCases => new List<object[]>
    {
        new object[] { "57.241.960/0001-61" },
        new object[] { "57241960000161" },
        new object[] { "00000000000000" }
    };
}