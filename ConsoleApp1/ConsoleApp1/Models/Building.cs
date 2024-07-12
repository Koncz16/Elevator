using ConsoleApp1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    // Represents a building with multiple floors and elevators.
    public class Building
    {
        public List<Elevator> Elevators { get; private set; }   // List of elevators in the building.
        public List<Floor> Floors { get; }                      // List of floors in the building.


        public Building(int numberOfFloors)
        {
            Elevators = new List<Elevator>
            {
                new Elevator("Lift A", 0),    // Creates Elevator A starting at ground floor.
                new Elevator("Lift B", 6)    // Creates Elevator B starting at top floor.
            };
            Floors = new List<Floor>();
            for (int i = 0; i < numberOfFloors; i++)
            {
                Floors.Add(new Floor(i));
            }
        }

        // Calls an elevator to the specified floor in the given direction.
        public void CallElevator(int floorNumber, Direction direction)
        {
            var floor = Floors.FirstOrDefault(f => f.FloorNumber == floorNumber);
            if (floor == null)
            {
                Console.WriteLine($"Invalid destination floor: {floor}");
                return;
            }

            floor.PressButton(direction);      
            Elevator closestElevator = GetClosestElevator(floorNumber);     

            closestElevator.Called(floorNumber);
        }

        // Selects a destination floor for the specified elevator index.
        public void SelectDestination(int elevatorIndex, int floorNumber)
        {
            var floor = Floors.FirstOrDefault(f => f.FloorNumber == floorNumber);
            if (floor == null)
            {
                Console.WriteLine($"Invalid destination floor: {floorNumber}");
                return;
            }

            if (elevatorIndex >= 0 && elevatorIndex < Elevators.Count)
            {
                Elevators[elevatorIndex].SelectDestination(floor.FloorNumber);
            }
            else
            {
                Console.WriteLine($"Invalid elevator index: {elevatorIndex}");
            }
        }

        // Define the closest elevator to floor.
        private Elevator GetClosestElevator(int floor)
        {
            Elevator elevatorA = Elevators[0];
            Elevator elevatorB = Elevators[1];

            int distanceA = Math.Abs(elevatorA.CurrentFloor - floor);
            int distanceB = Math.Abs(elevatorB.CurrentFloor - floor);

            if (distanceA < distanceB || (distanceA == distanceB && elevatorA.CurrentFloor < elevatorB.CurrentFloor))
            {
                return elevatorA;
            }
            else
            {
                return elevatorB;
            }
        }

        // Updates the state of each elevator in the building.
        public void UpdateElevators()
        {
            foreach (var elevator in Elevators)
            {
                if (elevator.State == ElevatorState.Called || elevator.State == ElevatorState.DestinationSelected)
                {
                    elevator.SetDirection();
                    Console.WriteLine(elevator);

                    elevator.Move();

                }else if(elevator.State == ElevatorState.DestinationReached)
                {
                    var floor = Floors.FirstOrDefault(f => f.FloorNumber == elevator.CurrentFloor);
                    if (floor != null)
                    {
                        floor.ResetButtons(); 
                    }
                    elevator.ResetState();
                }
            }
        }
        // Returns a string representation of the building's elevators.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var elevator in Elevators)
            {
                sb.AppendLine(elevator.ToString());
            }

            return sb.ToString();
        }
    }
}
