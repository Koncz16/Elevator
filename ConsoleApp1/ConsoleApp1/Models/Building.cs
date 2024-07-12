using ConsoleApp1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Building
    {
        public List<Elevator> Elevators { get; private set; }
        public List<Floor> Floors { get; }


        public Building(int numberOfFloors)
        {
            Elevators = new List<Elevator>
            {
                new Elevator("Lift A", 0),   
                new Elevator("Lift B", 6)    
            };
            Floors = new List<Floor>();
            for (int i = 0; i < numberOfFloors; i++)
            {
                Floors.Add(new Floor(i));
            }
        }

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
