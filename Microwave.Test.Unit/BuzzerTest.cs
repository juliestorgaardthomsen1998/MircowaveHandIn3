using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private IOutput output;

            [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            uut = new Buzzer(output);
        }

        [Test]
        public void TurnOn_BuzzerIsTurnedOn_ReceivedOutputIsCorrectAndIsOnIsTrue()
        {
            //Act
            uut.TurnOn();

            //Assert
            output.Received(1).OutputLine("beep beep beep");
            Assert.That(uut.isOn,Is.EqualTo(true));
        }

        [Test]
        public void TurnOff_BuzzerIsTurnedOff_ReceivedOutputIsCorrectAndIsOnIsFalse()
        {
            //Act
            uut.TurnOff();

            //Assert
            output.Received(1).OutputLine("*silence*");
            Assert.That(uut.isOn, Is.EqualTo(false));
        }

    }
}
