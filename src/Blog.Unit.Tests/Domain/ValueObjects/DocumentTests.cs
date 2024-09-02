using Blog.Api.Domain.ValueObjects;
using Blog.Api.Shared.Exceptions;
using FluentAssertions;

namespace Blog.Unit.Tests.Domain.ValueObjects;

public class DocumentTests
{
    [Theory]
    [MemberData(nameof(DocumentTestData.ValidCpfCases), MemberType = typeof(DocumentTestData))]
    public void CreateDocument_WithValidCpf_ShouldSetTypeAsCpfAndTextAsExpected(string input, string expectedText)
    {
        // Act
        var document = (Document)input;

        // Assert
        document.Type.Should().Be(DocumentType.CPF);
        document.Text.Should().Be(expectedText);
    }
    
    [Theory]
    [MemberData(nameof(DocumentTestData.ValidCnpjCases), MemberType = typeof(DocumentTestData))]
    public void CreateDocument_WithValidCnpj_ShouldSetTypeAsCnpjAndTextAsExpected(string input, string expectedText)
    {
        // Act
        var document = (Document)input;

        // Assert
        document.Type.Should().Be(DocumentType.CNPJ);
        document.Text.Should().Be(expectedText);
    }

    [Theory]
    [MemberData(nameof(DocumentTestData.InvalidDocumentCases), MemberType = typeof(DocumentTestData))]
    public void CreateDocument_WithInvalidDocument_ShouldThrowDomainException(string input)
    {
        // Act
        var action = () => (Document)input;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Documento inválido");
    }
    
    [Theory]
    [MemberData(nameof(DocumentTestData.InvalidCpfCases), MemberType = typeof(DocumentTestData))]
    public void CreateDocument_WithInvalidCpf_ShouldThrowDomainExceptionWithCpfMessage(string input)
    {
        // Act
        var action = () => (Document)input;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage($"{DocumentType.CPF} inválido");
    }

    [Theory]
    [MemberData(nameof(DocumentTestData.InvalidCnpjCases), MemberType = typeof(DocumentTestData))]
    public void CreateDocument_WithInvalidCnpj_ShouldThrowDomainExceptionWithCnpjMessage(string input)
    {
        // Act
        var action = () => (Document)input;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage($"{DocumentType.CNPJ} inválido");
    }
}