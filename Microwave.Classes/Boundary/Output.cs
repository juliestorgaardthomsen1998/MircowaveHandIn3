using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Output : IOutput
    {
        public void OutputLine(string line)
        {
            System.Console.WriteLine(line);
        }
    }
}
