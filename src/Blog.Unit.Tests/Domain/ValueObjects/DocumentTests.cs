using Blog.Api.Domain.ValueObjects;
using Blog.Api.Shared.Exceptions;
using FluentAssertions;

namespace Blog.Unit.Tests.Domain.ValueObjects;

public class DocumentTests
{

    [Theory]
    [MemberData(nameof(DocumentTestData.ValidCpfCases), MemberType = typeof(DocumentTestData))]
    public void ShouldCreateDocumentWithValidCpf(string input, string expectedText)
    {
        // Act
        var document = (Document)input;

        // Assert
        document.Type.Should().Be(DocumentType.CPF);
        document.Text.Should().Be(expectedText);
    }
    
    [Theory]
    [MemberData(nameof(DocumentTestData.ValidCnpjCases), MemberType = typeof(DocumentTestData))]
    public void ShouldCreateDocumentWithValidCnpj(string input, string expectedText)
    {
        // Act
        var document = (Document)input;

        // Assert
        document.Type.Should().Be(DocumentType.CNPJ);
        document.Text.Should().Be(expectedText);
    }

    [Theory]
    [MemberData(nameof(DocumentTestData.InvalidDocumentCases), MemberType = typeof(DocumentTestData))]
    public void ShouldThrowExceptionForInvalidDocument(string input)
    {
        // Act
        var action = () => (Document)input;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Documento inválido");
    }
    
    [Theory]
    [MemberData(nameof(DocumentTestData.InvalidCpfCases), MemberType = typeof(DocumentTestData))]
    public void ShouldThrowExceptionForInvalidCpf(string input)
    {
        // Act
        var action = () => (Document)input;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage($"{DocumentType.CPF} inválido");
    }

    [Theory]
    [MemberData(nameof(DocumentTestData.InvalidCnpjCases), MemberType = typeof(DocumentTestData))]
    public void ShouldThrowExceptionForInvalidCnpj(string input)
    {
        // Act
        var action = () => (Document)input;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage($"{DocumentType.CNPJ} inválido");
    }
}