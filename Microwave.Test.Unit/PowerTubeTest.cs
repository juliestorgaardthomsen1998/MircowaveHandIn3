using Microwave.Classes.Boundary;
using Microwave.Classes.Configuration;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;
        private IConfiguration config;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            config = Substitute.For<IConfiguration>();
            uut = new PowerTube(output, config);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(499)]
        [TestCase(500)]
        public void TurnOn_WasOffCorrectPower500_CorrectOutput(int power)
        {
            config.MaxPower = 500; //

            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(801)]
        [TestCase(850)]
        public void TurnOn_WasOffOutOfRangePower800_ThrowsException(int power)
        {
            config.MaxPower = 800;
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn700_CorrectOutput()
        {
            config.MaxPower = 700;
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOn1000_CorrectOutput()
        {
            config.MaxPower = 1000;
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            config.MaxPower = 700; 
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }
    }
}