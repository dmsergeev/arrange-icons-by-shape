namespace ArrangeIconsByShape.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Models;
    using DesktopIconsManager.Windows;

    public class Program
    {
        public static void Main(string[] args)
        {
            var manager = new WindowsDesktopIconsManager();
            Console.WriteLine($"Total number: {manager.GetNumberOfIcons()}");
            Console.WriteLine("Positions: ");
            var icons = manager.GetIconsPositions().ToList();
            icons.ForEach(icon => Console.WriteLine($"{icon.x}, {icon.y}"));

            Console.WriteLine("Monitor info:");
            var monitors = manager.GetDisplayMonitors().ToList();
            monitors.ForEach(monitor => 
                Console.WriteLine($"Upper left ({monitor.UpperLeftCornerCoordinates.X};{monitor.UpperLeftCornerCoordinates.Y}) " +
                                  $"Lower right ({monitor.LowerRightCornerCoordinates.X};{monitor.LowerRightCornerCoordinates.Y})"));

            var numberOfIconsPerMonitor = monitors.Select(monitor => new
            {
                NumberOfIcons = GetNumberOfIcons(monitor, icons),
                Monitor = monitor
            });

            var monitorWithMostIcons = numberOfIconsPerMonitor.OrderByDescending(x => x.NumberOfIcons).FirstOrDefault().Monitor;

            manager.RandomizeIconsPosition(monitorWithMostIcons);
        }

        private static int GetNumberOfIcons(Monitor monitor, IEnumerable<(int x, int y)> icons) => icons.Count(icon => monitor.Contains(icon.x, icon.y));
    }
}