using BudgetManagement.Core.Helpers;

namespace BudgetManagement.Tests.Helpers;

public class StringHelperTests
{
    [Theory]
    [InlineData("password", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")]
    [InlineData("123456", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92")]
    [InlineData("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b8")]
    public void GetPasswordHash_WhenCalled_ReturnsExpectedHash(string password, string expectedHashStart)
    {
        // Act
        var result = password.GetPasswordHash();

        // Assert
        Assert.StartsWith(expectedHashStart, result);
    }
}
