using System;
using Microwave.Classes;
using Microwave.Classes.Boundary;
using Microwave.Classes.Configuration;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;

namespace HandIn3Microwave
{
    class Program
    {
        static void Main(string[] args)
        {
            // tiljøjer noget her
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            IConfiguration config = new Configuration() //addition
            {
                MaxPower = 800
            };

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output,config); //addition

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, config, cooker); //addition

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();
        }
    }
}
