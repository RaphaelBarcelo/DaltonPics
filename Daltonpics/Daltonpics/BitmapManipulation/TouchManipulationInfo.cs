using System;

using SkiaSharp;

namespace Daltonpics.BitmapManipulation
{
    class TouchManipulationInfo
    {
        public SKPoint PreviousPoint { set; get; }

        public SKPoint NewPoint { set; get; }
    }
}
