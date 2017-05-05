namespace ArrangeIconsByShape.Contracts.Models
{
    using System;

    public class Monitor
    {
        public (int X, int Y) UpperLeftCornerCoordinates;
        public (int X, int Y) LowerRightCornerCoordinates;

        public int Width => Math.Abs(LowerRightCornerCoordinates.X - UpperLeftCornerCoordinates.X);
        public int Height => Math.Abs(LowerRightCornerCoordinates.Y - UpperLeftCornerCoordinates.Y);

        public bool Contains(int x, int y) => UpperLeftCornerCoordinates.X <= x &&
                                              x < UpperLeftCornerCoordinates.X + Width &&
                                              UpperLeftCornerCoordinates.Y <= y &&
                                              y < UpperLeftCornerCoordinates.Y + Height;
    }
}