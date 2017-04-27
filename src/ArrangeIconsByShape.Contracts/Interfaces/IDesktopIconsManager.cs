using System;
using System.Collections.Generic;

namespace ArrangeIconsByShape.Contracts.Interfaces
{
    public interface IDesktopIconsManager
    {
        int GetNumberOfIcons();
        IEnumerable<Tuple<int, int>> GetIconsPositions();

    }
}
