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
            manager.GetIconsPositions().ToList().ForEach(x => Console.WriteLine($"{x.Item1}, {x.Item2}"));
            Console.ReadKey();
        }
    }
}