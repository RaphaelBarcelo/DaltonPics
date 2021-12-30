using System.Collections.Generic;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Daltonpics.BitmapManipulation
{
    class TouchManipulationBitmap
    {
        public readonly SKBitmap bitmap;
        private bool processingEvent = false;
        private bool manipulating = false;
        private readonly SKPaint circlePaint1, circlePaint2, circlePaint3;
        private readonly Dictionary<long, TouchManipulationInfo> touchDictionary =
            new Dictionary<long, TouchManipulationInfo>();

        public TouchManipulationBitmap(SKBitmap bitmap)
        {
            this.bitmap = bitmap;
            SKShader shader;
            SKColor[] colors;
            Matrix = SKMatrix.CreateIdentity();

            TouchManager = new TouchManipulationManager
            {
                Mode = TouchManipulationMode.ScaleRotate
            };

            colors = new SKColor[] {
                new SKColor(0, 255, 255),
                new SKColor(255, 0, 255),
                new SKColor(255, 255, 0),
                new SKColor(0, 255, 255)
            };

            shader = SKShader.CreateSweepGradient(
               new SKPoint(128, 128),
               colors,
               null);

            circlePaint1 = new SKPaint
            {
                Color = SKColor.Parse("#FF000000"),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 6,
                IsAntialias = true
            };

            circlePaint3 = new SKPaint
            {
                // Color = SKColor.Parse("#FFFF0000"),
                Style = SKPaintStyle.Stroke,
                Shader = shader,
                StrokeWidth = 6,
                IsAntialias = true
            };
            circlePaint2 = new SKPaint
            {
                Color = SKColor.Parse("#FFFFFFFF"),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 6,
                IsAntialias = true
            };

        }

        public TouchManipulationManager TouchManager { set; get; }

        public SKMatrix Matrix { set; get; }

        public void Paint(SKCanvas canvas)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);
            canvas.DrawBitmap(bitmap, 0, 0);
            canvas.Restore();
        }

        public void Paint(SKCanvas canvas, SKRect rect, SKPoint point)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);
            canvas.DrawBitmap(bitmap, rect);
            if (!point.IsEmpty)
            {
                canvas.DrawCircle(point.X, point.Y, 20, circlePaint1);
                canvas.DrawCircle(point.X, point.Y, 26, circlePaint2);
                canvas.DrawCircle(point.X, point.Y, 32, circlePaint3);
            }
            canvas.Restore();


        }

        public void Paint(SKCanvas canvas, float[] filter)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);

            using (SKPaint paint = new SKPaint())
            {
                paint.ColorFilter = SKColorFilter.CreateColorMatrix(filter);
                canvas.DrawBitmap(bitmap, 0, 0, paint: paint);
            }
            canvas.Restore();
        }

        public void Paint(SKCanvas canvas, float[] filter, SKPoint point)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);

            using (SKPaint paint = new SKPaint())
            {
                paint.ColorFilter = SKColorFilter.CreateColorMatrix(filter);
                canvas.DrawBitmap(bitmap, 0, 0, paint: paint);
            }
            canvas.Restore();
            if (!point.IsEmpty)
            {
                canvas.DrawCircle(point.X, point.Y, 15, circlePaint1);
                canvas.DrawCircle(point.X, point.Y, 18, circlePaint2);
                canvas.DrawCircle(point.X, point.Y, 21, circlePaint3);
            }

        }



        public bool HitTest(SKPoint location)
        {
            // Invert the matrix

            if (Matrix.TryInvert(out SKMatrix inverseMatrix))
            {
                // Transform the point using the inverted matrix
                SKPoint transformedPoint = inverseMatrix.MapPoint(location);

                // Check if it's in the untransformed bitmap rectangle
                SKRect rect = new SKRect(0, 0, bitmap.Width, bitmap.Height);
                return rect.Contains(transformedPoint);
            }
            return false;
        }

        public void ProcessTouchEvent(long id, SKTouchAction type, SKPoint location)
        {
            if (processingEvent) return;
            processingEvent = true;
            switch (type)
            {
                case SKTouchAction.Pressed:
                    if (touchDictionary.ContainsKey(id))
                        touchDictionary.Remove(id);
                    touchDictionary.Add(id, new TouchManipulationInfo
                    {
                        PreviousPoint = location,
                        NewPoint = location
                    });
                    break;

                case SKTouchAction.Moved:
                    TouchManipulationInfo info = touchDictionary[id];
                    info.NewPoint = location;
                    Manipulate();
                    info.PreviousPoint = info.NewPoint;
                    break;

                case SKTouchAction.Released:
                    touchDictionary[id].NewPoint = location;
                    Manipulate();
                    touchDictionary.Remove(id);
                    break;

                case SKTouchAction.Cancelled:
                    touchDictionary.Remove(id);
                    break;
            }
            processingEvent = false;
        }

        void Manipulate()
        {
            if (manipulating) return;

            manipulating = true;
            SKMatrix touchMatrix = SKMatrix.CreateIdentity();
            TouchManipulationInfo[] infos = new TouchManipulationInfo[touchDictionary.Count];
            touchDictionary.Values.CopyTo(infos, 0);


            if (infos.Length == 1)
            {
                SKPoint prevPoint = infos[0].PreviousPoint;
                SKPoint newPoint = infos[0].NewPoint;
                SKPoint pivotPoint = Matrix.MapPoint(bitmap.Width / 2, bitmap.Height / 2);

                touchMatrix = TouchManager.OneFingerManipulate(prevPoint, newPoint, pivotPoint);
            }
            else if (infos.Length >= 2)
            {
                int pivotIndex = infos[0].NewPoint == infos[0].PreviousPoint ? 0 : 1;
                SKPoint pivotPoint = infos[pivotIndex].NewPoint;
                SKPoint newPoint = infos[1 - pivotIndex].NewPoint;
                SKPoint prevPoint = infos[1 - pivotIndex].PreviousPoint;

                touchMatrix = TouchManager.TwoFingerManipulate(prevPoint, newPoint, pivotPoint);
            }

            Matrix = Matrix.PostConcat(touchMatrix);

            manipulating = false;
        }
    }
}
