using System.Reflection;
using ConsoleApp1.Enums;
using ConsoleApp1.Models;

internal class Program
{
    private static void Main(string[] args)
    { 
        // Initializes a new building with 7 floors.
        Building building = new Building(7);

        Console.WriteLine("Initial building status:");
        Console.WriteLine(building);

        // Main loop to handle user input and elevator operations.
        while (true)
        {

            Console.WriteLine("Type 'exit' to stop or press Enter to continue:");
            string input = Console.ReadLine(); 
            if (input.ToLower() == "exit")
            {
                break;
            }
            int callFloor = ReadFloorInput("Please enter the floor where you are calling the elevator:");
            Direction direction = ReadDirectionInput("Please enter the direction (up/down) where you want to go:");


            Console.WriteLine($"\nCalling elevator from floor {callFloor} in {direction} direction...");
            building.CallElevator(callFloor, direction);

            int elevatorIndex = building.Elevators.FindIndex(e => e.State == ElevatorState.Called);
            if (elevatorIndex != -1)
            {
                // Runs until the elevator reaches its destination floor.
                while (building.Elevators[elevatorIndex].State != ElevatorState.DestinationReached)
                {
                    building.UpdateElevators();
                    System.Threading.Thread.Sleep(1000);
                }
                
                int selectedFloor = ReadFloorInput("\nPlease enter the floor where you want to go:");

                Console.WriteLine($"\nSelecting new destination floor {selectedFloor} for {building.Elevators[elevatorIndex].Name}...");
                building.SelectDestination(elevatorIndex, selectedFloor);

                // Runs until the elevator becomes inactive after reaching its destination.

                while (building.Elevators[elevatorIndex].State != ElevatorState.Idle)
                {
                    building.UpdateElevators();
                    System.Threading.Thread.Sleep(1000);
                }


                Console.WriteLine($"\n{building.Elevators[elevatorIndex].Name} has reached its destination.");
            }
                Console.WriteLine("Current building status:");
            Console.WriteLine(building); 
        }
    }

    // Helper method to read and validate floor input from the user.
    public static int ReadFloorInput(string message)
    {
        int floor;
        while (true)
        {
            try
            {
                Console.WriteLine(message);
                floor = int.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
        }
        return floor;
    }

    // Helper method to read and validate direction input from the user.
    private static Direction ReadDirectionInput(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine().ToLower();

            if (input == "up")
            {
                return Direction.Up;
            }
            else if (input == "down")
            {
                return Direction.Down;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

}
