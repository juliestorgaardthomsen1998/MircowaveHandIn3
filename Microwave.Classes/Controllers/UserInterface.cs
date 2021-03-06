    using System;
using System.Runtime.Serialization;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class UserInterface : IUserInterface
    {
        private enum States
        {
            READY, SETPOWER, SETTIME, COOKING, DOOROPEN
        }

        private States myState = States.READY;

        private ICookController myCooker;
        private ILight myLight;
        private IDisplay myDisplay;
        private IConfiguration myConfig; //addition

        private int powerLevel = 50;
        private int minutes = 1;
        private int seconds = 1;

        public UserInterface(
            IButton powerButton,
            IButton minutesButton,
            IButton secondsButton,
            IButton startCancelButton,
            IDoor door,
            IDisplay display,
            ILight light,
            IConfiguration config, // addition
            ICookController cooker)
        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            minutesButton.Pressed += new EventHandler(OnMinutesPressed);
            secondsButton.Pressed += new EventHandler(OnSecondsPressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);

            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
            myConfig = config;
            //config.MaxPower = 700;
        }

        private void ResetValues()
        {
            powerLevel = 50;
            minutes = 1;
            seconds = 1;
        }

        public void OnPowerPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myDisplay.ShowPower(powerLevel);
                    myState = States.SETPOWER;
                    break;
                case States.SETPOWER:
                    powerLevel = (powerLevel >= myConfig.MaxPower ? 50 : powerLevel+50); //700 changed to myConfig.MaxPower
                    myDisplay.ShowPower(powerLevel);
                    break;
            }
        }

        public void OnMinutesPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(minutes, seconds);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    minutes += 1;
                    myDisplay.ShowTime(minutes, seconds);
                    break;
            }
        }
        public void OnSecondsPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(minutes, seconds);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    seconds += 1;
                    myDisplay.ShowTime(minutes, seconds);
                    break;
            }
        }

        public void OnStartCancelPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    ResetValues();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
                case States.SETTIME:
                    myLight.TurnOn();
                    myCooker.StartCooking(powerLevel, minutes,seconds); //Skal jeg ændre her??
                    myState = States.COOKING;
                    break;
                case States.COOKING:
                    ResetValues();
                    myCooker.Stop();
                    myLight.TurnOff();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
            }
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myLight.TurnOn();
                    myState = States.DOOROPEN;
                    break;
                case States.SETPOWER:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.SETTIME:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.COOKING:
                    myCooker.Stop();
                    myDisplay.Clear();
                    ResetValues();
                    myState = States.DOOROPEN;
                    break;
            }
        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.DOOROPEN:
                    myLight.TurnOff();
                    myState = States.READY;
                    break;
            }
        }

        public void CookingIsDone()
        {
            switch (myState)
            {
                case States.COOKING:
                    ResetValues();
                    myDisplay.Clear();
                    myLight.TurnOff();
                    // Beep 3 times
                    myState = States.READY;
                    break;
            }
        }
    }
}