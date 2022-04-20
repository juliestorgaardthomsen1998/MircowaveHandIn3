using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput; // addition
        private IConfiguration myConfig; // addition

        private bool IsOn = false;

        public PowerTube(IOutput output, IConfiguration config) // addition
        {
            myOutput = output;
            myConfig = config; // addition
        }

        public void TurnOn(int power)
        {
            if (power < 1 || myConfig.MaxPower < power) // 700 changed to myConfig.MaxPower
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and " + myConfig.MaxPower + " (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}
