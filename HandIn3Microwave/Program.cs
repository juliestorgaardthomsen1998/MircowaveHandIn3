using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;


namespace HandIn3Microwave
{
    public class Program
    {
        static void Main(string[] args)
        {
            // tiljøjer noget her
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button minutesButton = new Button();
            Button secondsButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, minutesButton,secondsButton, startCancelButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            minutesButton.Press();

            secondsButton.Press();

            secondsButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();
        }
    }
}
