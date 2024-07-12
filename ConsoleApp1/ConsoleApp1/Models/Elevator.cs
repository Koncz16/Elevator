using ConsoleApp1.Enums;
using System;

namespace ConsoleApp1.Models
{
    // Represents an elevator with basic functionalities.
    public class Elevator
    {
        public string Name { get; private set; }
        public ElevatorState State { get; private set; }
        public int CurrentFloor { get; private set; }
        public int DestinationFloor { get; private set; }
        public Direction Direction { get; private set; }

        // Constructor
        public Elevator(string name, int initialFloor)
        {
            Name = name;
            CurrentFloor = initialFloor;
            DestinationFloor = initialFloor;
            Direction = Direction.idle;
            State = ElevatorState.Idle;
        }

        // Sets the destination floor.
        private void SetDestination(int floor)
        {
            DestinationFloor = floor;
        }

        // Update the Elevator State and call SetDestination when the elevator is called from a floor.
        public void Called(int floor)
        {
            State = ElevatorState.Called;
            SetDestination(floor);
        }

        // Update the Elevator State and call SetDestination when a new floor is selected inside the elevator.
        public void SelectDestination(int floor)
        {
            State = ElevatorState.DestinationSelected;
            SetDestination(floor);
        }

        // Resets the elevator's state to idle when it reaches its destination floor.
        public void ResetState()
        {
            if (State == ElevatorState.DestinationReached)
            {
                State = ElevatorState.Idle;
            }
        }

        // Sets the direction of the elevator based on its current and destination floors.
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

        // Moves the elevator towards its destination floor.
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

        // Returns a string representation of the elevator's current state.
        public override string ToString()
        {
            return $"{Name} at floor {CurrentFloor} going {Direction}, state: {State}";
        }
    }
}
