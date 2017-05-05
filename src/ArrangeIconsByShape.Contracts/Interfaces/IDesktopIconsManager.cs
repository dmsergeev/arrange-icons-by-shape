namespace ArrangeIconsByShape.Contracts.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IDesktopIconsManager
    {
        int GetNumberOfIcons();
        IEnumerable<(int x , int y)> GetIconsPositions();
        IEnumerable<Monitor> GetDisplayMonitors();
        void RandomizeIconsPosition(Monitor monitor);
    }
}
