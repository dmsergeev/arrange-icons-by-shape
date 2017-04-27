using System;
using System.Linq;
using ArrangeIconsByShape.DesktopIconsManager.Windows;

namespace ArrangeIconsByShape.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var manager = new WindowsDesktopIconsManager();
            Console.WriteLine($"Total number: {manager.GetNumberOfIcons()}");
            Console.WriteLine("Positions: ");
            manager.GetIconsPositions().ToList().ForEach(icon => Console.WriteLine($"{icon.x}, {icon.y}"));
            Console.ReadKey();
        }
    }
}