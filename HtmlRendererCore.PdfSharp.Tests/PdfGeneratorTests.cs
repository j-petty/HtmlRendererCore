using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace HtmlRendererCore.PdfSharp.Tests
{
    [Trait("Category", "UnitTest")]
    public class PdfGeneratorTests
    {
        [Fact]
        public void GeneratePdf_Success()
        {
            // Arrange
            var html = @"
                <html>
                    <body>
                        <p>Test document</p>
                    </body>
                </html>
            ";

            // Act
            var result = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void GeneratePdfBase64_Success()
        {
            // Arrange
            var html = @"
                <html>
                    <body>
                        <p>Test document</p>
                    </body>
                </html>
            ";

            // Act
            var result = string.Empty;

            using (var stream = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);

                pdf.Save(stream);

                result = Convert.ToBase64String(stream.ToArray());
            }

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}
