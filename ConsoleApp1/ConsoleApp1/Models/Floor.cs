using ConsoleApp1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Floor
    {
        public int FloorNumber { get; }
        public bool CallUp { get; private set; }
        public bool CallDown { get; private set; }

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
        }

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

        public void ResetButtons()
        {
            CallUp = false;
            CallDown = false;
        }
    }
}