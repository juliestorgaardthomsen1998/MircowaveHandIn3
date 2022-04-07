using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    interface IPowerTube
    {
        void TurnOn(int power);

        void TurnOff();
    }
}
