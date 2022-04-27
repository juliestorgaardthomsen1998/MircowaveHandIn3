using System;
using Microwave.Classes;
using Microwave.Classes.Boundary;
using Microwave.Classes.Configuration;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;



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

            IConfiguration config = new Configuration() //addition
            {
                MaxPower = 800
            };

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output,config); //addition

            Light light = new Light(output);

            Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);


            UserInterface ui = new UserInterface(powerButton, minutesButton,secondsButton, startCancelButton, door, display, light, config, cooker);


            // Finish the double association
            cooker.UI = ui;

            Console.WriteLine("Press + to add 5 seconds");
            Console.WriteLine("Press - to substract 5 seconds");
            Console.WriteLine("When you press e, the program will stop");

            // Simulate a simple sequence

            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
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
                        timer.ChangeTime("+");
                        break;
                    case '-':
                        timer.ChangeTime("-");
                        break;
                    case 'e':
                        Environment.Exit(0);
                        break;
                }
            }

            
        }
    }
}
