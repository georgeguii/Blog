using Blog.Api.Shared.Extensions;
using FluentAssertions;

namespace Blog.Unit.Tests.Shared.Extensions;

public class StringExtensionTests
{
    [Theory]
    [InlineData("Çalışkan", "Caliskan")]
    [InlineData("São Paulo", "Sao Paulo")]
    [InlineData("Tête-à-tête", "Tete-a-tete")]
    [InlineData("Jalapeño", "Jalapeno")]
    [InlineData("ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýÿ", "AAAAAAAECEEEEIIIIDNOOOOOOUUUUYTHssaaaaaaaeceeeeiiiidnoooooouuuuyy")]
    public void RemoveDiacritics_FromString_ShouldReturnExpectedString(string input, string expectedText)
    {
        // Act
        var inputWithoutDiacritics = input.RemoveDiacritics();

        // Assert
        inputWithoutDiacritics.Should().Be(expectedText);
    }

}