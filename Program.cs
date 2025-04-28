using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example from the problem statement
            List<ColorChip> exampleChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            RunTest("Example Test", exampleChips);

            // Additional test case with no solution
            List<ColorChip> noSolutionChips = new List<ColorChip>
            {
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange)
            };

            RunTest("No Solution Test", noSolutionChips);

            // Additional test case with multiple possible paths
            List<ColorChip> multiplePathsChips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Green),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Red, Color.Yellow)
            };

            RunTest("Multiple Paths Test", multiplePathsChips);

            // Wait for user input before closing
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void RunTest(string testName, List<ColorChip> chips)
        {
            Console.WriteLine($"\n=== {testName} ===");
            Console.WriteLine("Available Chips:");
            foreach (var chip in chips)
            {
                Console.WriteLine($"[{chip.StartColor}, {chip.EndColor}]");
            }

            SecuritySystem securitySystem = new SecuritySystem();
            List<ColorChip> sequence = securitySystem.FindLongestValidSequence(chips);

            Console.WriteLine("\nResult:");
            if (sequence != null && sequence.Count > 0)
            {
                Console.WriteLine(securitySystem.FormatSequence(sequence));
                Console.WriteLine($"Used {sequence.Count} out of {chips.Count} chips");
                Console.WriteLine($"Valid sequence: {securitySystem.ValidateSequence(sequence)}");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }
        }
    }
}
