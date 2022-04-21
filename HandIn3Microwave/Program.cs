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

            Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, minutesButton,secondsButton, startCancelButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            Console.WriteLine("Press + to add 5 seconds");
            Console.WriteLine("Press - to substract 5 seconds");
            Console.WriteLine("When you press enter, the program will stop");

            // Simulate a simple sequence

            powerButton.Press();

            minutesButton.Press();

            secondsButton.Press();

            secondsButton.Press();

            startCancelButton.Press();

            while (true)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '+':

                        break;
                    case '-':

                        break;
                    //case '':
                    //    break;
                }
            }

            Console.ReadLine();
        }
    }
}
