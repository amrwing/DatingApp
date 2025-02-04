using System.Net;
using System.Text;
using System.Text.Json;
using API.DTOs;
using API.UnitTests.Helpers;
using Xunit;

namespace API.UnitTests.Tests
{
    public class RegisterRequestTests
    {
        private readonly HttpClient _client;
        private HttpResponseMessage httpResponse;
        private string requestUrl;
        private string loginObject;
        private HttpContent httpContent;

        public RegisterRequestTests()
        {
            _client = TestHelper.Instance.Client;
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var expectedStatusCode = "OK";
            requestUrl = "api/account/register";
            var registerRequest = new RegisterRequest
            {
                Username = "newuser",
                Password = "password"
            };

            loginObject = JsonSerializer.Serialize(registerRequest);
            httpContent = GetHttpContent(loginObject);

            // Act
            httpResponse = await _client.PostAsync(requestUrl, httpContent);
            
            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(expectedStatusCode, true), httpResponse.StatusCode);
            Assert.Equal(expectedStatusCode, httpResponse.StatusCode.ToString());
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnBadRequest_WhenUsernameAlreadyInUse()
        {
            // Arrange
            var expectedStatusCode = "BadRequest";
            requestUrl = "api/account/register";
            var registerRequest = new RegisterRequest
            {
                Username = "arenita",
                Password = "password"
            };

            loginObject = JsonSerializer.Serialize(registerRequest);
            httpContent = GetHttpContent(loginObject);

            // Act
            httpResponse = await _client.PostAsync(requestUrl, httpContent);
            
            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(expectedStatusCode, true), httpResponse.StatusCode);
            Assert.Equal(expectedStatusCode, httpResponse.StatusCode.ToString());
        }
        

        #region Private methods

        private static StringContent GetHttpContent(string objectToCode) =>
            new(objectToCode, Encoding.UTF8, "application/json");

        #endregion
    }
}
