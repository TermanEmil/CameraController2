#pragma warning disable CA1063 // Implement IDisposable Correctly

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GphotoCameraControl.Commands.AutoDetect;
using Moq;
using Processes;
using TestingUtils;
using Xunit;

namespace GphotoCameraControl.Tests.Commands.AutoDetect
{
    [Trait("Category", "Integration")]
    public class AutoDetectCommandHandlerTests : IDisposable
    {
        private readonly Mock<IProcess> processMock;
        private readonly Mock<IScriptRunner> scriptRunnerMock;
        private readonly AutoDetectCommandHandler sut;

        public AutoDetectCommandHandlerTests()
        {
            this.processMock = new Mock<IProcess>();
            this.scriptRunnerMock = new Mock<IScriptRunner>();
            this.sut = new AutoDetectCommandHandler(this.scriptRunnerMock.Object);
        }

        public void Dispose()
        {
            this.processMock.VerifyAll();
            this.scriptRunnerMock.VerifyAll();
        }

        [Fact]
        public async Task GivenTwoCameras_ShouldParseBothCameras()
        {
            const string procOut = "Foo1===Bar1\nFoo2===Bar2";

            this.processMock.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader(procOut));
            this.processMock.Setup(x => x.StandardError).Returns(StreamFactory.GenerateReader());
            this.scriptRunnerMock.Setup(x => x.RunAutoDetection()).Returns(this.processMock.Object);

            var actualEnum = await this.sut.Handle(new AutoDetectCommand());
            var actual = actualEnum.ToList();

            actual.Should().HaveCount(2);
            actual.Should()
                .Contain(x => x.Model == "Foo1" && x.Port == "Bar1")
                .And
                .Contain(x => x.Model == "Foo2" && x.Port == "Bar2");
        }

        [Fact]
        public async Task GivenEmptyInput_ShouldYieldNoCameras()
        {
            this.processMock.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader());
            this.processMock.Setup(x => x.StandardError).Returns(StreamFactory.GenerateReader());
            this.scriptRunnerMock.Setup(x => x.RunAutoDetection()).Returns(this.processMock.Object);

            var actual = await this.sut.Handle(new AutoDetectCommand());

            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenInvalidInput_ShouldThrow()
        {
            this.processMock.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader("Unexpected"));
            this.scriptRunnerMock.Setup(x => x.RunAutoDetection()).Returns(this.processMock.Object);

            await this.sut
                .Invoking(x => x.Handle(new AutoDetectCommand()))
                .Should()
                .ThrowAsync<Exception>();
        }
    }
}
