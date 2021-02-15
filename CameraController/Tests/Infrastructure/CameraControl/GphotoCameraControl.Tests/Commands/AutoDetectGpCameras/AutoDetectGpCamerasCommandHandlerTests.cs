using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GphotoCameraControl.Commands.AutoDetectGpCameras;
using GphotoCameraControl.Exceptions;
using GphotoCameraControl.ScriptRunning;
using MediatR;
using Moq;
using Processes;
using TestingUtils;
using Xunit;

namespace GphotoCameraControl.Tests.Commands.AutoDetectGpCameras
{
    [Trait("Category", "Integration")]
    public class AutoDetectGpCamerasCommandHandlerTests : IDisposable
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<IProcess> process;
        private readonly Mock<IScriptRunner> scriptRunner;
        private readonly AutoDetectGpCamerasCommandHandler sut;

        public AutoDetectGpCamerasCommandHandlerTests()
        {
            this.mediator = new Mock<IMediator>();
            this.process = new Mock<IProcess>();
            this.scriptRunner = new Mock<IScriptRunner>();
            this.sut = new AutoDetectGpCamerasCommandHandler(this.mediator.Object, this.scriptRunner.Object);
        }

        public void Dispose()
        {
            this.mediator.VerifyAll();
            this.process.VerifyAll();
            this.scriptRunner.VerifyAll();
        }

        [Fact]
        public async Task GivenTwoCameras_ShouldParseBothCameras()
        {
            const string procOut = "Foo1===Bar1\nFoo2===Bar2";

            this.process.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader(procOut));
            this.process.Setup(x => x.StandardError).Returns(StreamFactory.GenerateReader());
            this.scriptRunner.Setup(x => x.RunAutoDetection()).Returns(this.process.Object);

            var actualEnum = await this.sut.Handle(new AutoDetectGpCamerasCommand());
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
            this.process.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader());
            this.process.Setup(x => x.StandardError).Returns(StreamFactory.GenerateReader());
            this.scriptRunner.Setup(x => x.RunAutoDetection()).Returns(this.process.Object);

            var actual = await this.sut.Handle(new AutoDetectGpCamerasCommand());

            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenInvalidInput_ShouldThrow()
        {
            this.process.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader("Unexpected"));
            this.scriptRunner.Setup(x => x.RunAutoDetection()).Returns(this.process.Object);

            await this.sut
                .Invoking(x => x.Handle(new AutoDetectGpCamerasCommand()))
                .Should()
                .ThrowAsync<UnexpectedProcessOutputException>();
        }

        [Fact]
        public async Task ProcessOutputsErrors_ShouldThrow()
        {
            this.process.Setup(x => x.StandardOutput).Returns(StreamFactory.GenerateReader());
            this.process.Setup(x => x.StandardError).Returns(StreamFactory.GenerateReader("Error"));
            this.scriptRunner.Setup(x => x.RunAutoDetection()).Returns(this.process.Object);

            await this.sut
                .Invoking(x => x.Handle(new AutoDetectGpCamerasCommand()))
                .Should()
                .ThrowAsync<GphotoException>();
        }
    }
}
