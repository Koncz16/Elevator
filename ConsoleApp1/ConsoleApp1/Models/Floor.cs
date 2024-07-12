using ConsoleApp1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{    
    // Represents a floor in a building with call buttons for elevators.
    public class Floor
    {
        public int FloorNumber { get; }
        public bool CallUp { get; private set; }
        public bool CallDown { get; private set; }

        // Constructor
        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
        }

        // Simulates pressing the call button for the specified direction (Up/Down).
        public void PressButton(Direction direction)
        {
            if (direction == Direction.Up)
            {
                CallUp = true;
            }
            else if (direction == Direction.Down)
            {
                CallDown = true;
            }
        }

        // Resets the call buttons on the floor
        public void ResetButtons()
        {
            CallUp = false;
            CallDown = false;
        }
    }
}
