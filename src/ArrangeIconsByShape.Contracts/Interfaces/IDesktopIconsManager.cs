using System.Collections.Generic;

namespace ArrangeIconsByShape.Contracts.Interfaces
{
    public interface IDesktopIconsManager
    {
        int GetNumberOfIcons();
        IEnumerable<(int x , int y)> GetIconsPositions();

    }
}
