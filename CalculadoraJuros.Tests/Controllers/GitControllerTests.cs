using CalculadoraJuros.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraJuros.Tests.Controllers
{
    public class GitControllerTests
    {
        private readonly GitController _gitController;

        public GitControllerTests()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.SetupGet(x => x["GITHUB_CODE_URL"]).Returns("https://github.com/LouriJr/teste-granito");
            _gitController = new GitController(configuration.Object);
        }

        [Fact]
        public void ShowMeTheCode_DeveRetornarOkComUrlDoGithub()
        {
            // Act
            var response = _gitController.ShowMeTheCode();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var githubUrl = Assert.IsType<string>(okResult.Value);
            Assert.Equal("https://github.com/LouriJr/teste-granito", githubUrl);
        }
    }
}
