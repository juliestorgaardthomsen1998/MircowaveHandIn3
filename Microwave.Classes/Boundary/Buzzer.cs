using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;
        public bool isOn { get; set; } = false;
        //private bool isOn = false;

        public Buzzer(IOutput output)
        {
            myOutput = output; 
        }
        public void TurnOn()
        {
            if (!isOn)
            { 
                myOutput.OutputLine("beep beep beep"); 
                isOn = true; 
            }
        }

        public void TurnOff()
        {
            if (isOn)
            {
                myOutput.OutputLine("*silence*");
                isOn = false;
            }
        }
    }
}
