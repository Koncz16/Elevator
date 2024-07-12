using ConsoleApp1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Models
{
    public class Elevator
    {
        public string Name { get; private set; }
        public ElevatorState State { get; private set; }
        public int CurrentFloor { get; private set; }
        public int DestinationFloor { get; private set; }
        public Direction Direction { get; private set; }


        public Elevator(string name, int initialFloor)
        {
            Name = name;
            CurrentFloor = initialFloor;
            DestinationFloor = initialFloor;
            Direction = Direction.idle;
            State = ElevatorState.Idle;
        }

        private void SetDestination(int floor)
        {
            DestinationFloor = floor;
        }

        public void Called(int floor)
        {
            State = ElevatorState.Called;
            SetDestination(floor);
        }

        public void SelectDestination(int floor)
        {
            State = ElevatorState.DestinationSelected;
            SetDestination(floor);
        }


        public void ResetState()
        {
            if (State == ElevatorState.DestinationReached)
            {
                State = ElevatorState.Idle;
            }
        }

        public void SetDirection()
        {
            if (CurrentFloor < DestinationFloor)
            {
                Direction = Direction.Up;
            }
            else if (CurrentFloor > DestinationFloor)
            {
                Direction = Direction.Down;
            }
            else
            {
                Direction = Direction.idle;
                State = ElevatorState.DestinationReached;
            }
        }

        public void Move()
        {
            if (CurrentFloor < DestinationFloor)
            {
                CurrentFloor++;
            }
            else if (CurrentFloor > DestinationFloor)
            {
                CurrentFloor--;
            }
            
        }

        public override string ToString()
        {
            return $"{Name} at floor {CurrentFloor} going {Direction}, state: {State}";
        }
    }
}
