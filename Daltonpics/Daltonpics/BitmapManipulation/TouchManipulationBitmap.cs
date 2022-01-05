using System.Collections.Generic;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Daltonpics.BitmapManipulation
{
    class TouchManipulationBitmap
    {
        // Pointer circles radius
        private readonly float _radius1 = 26f, _radius2 = 32f, _radius3 = 38f;
        public readonly SKBitmap bitmap; 
        private bool processingEvent = false;
        private bool manipulating = false;
        private readonly SKPaint circlePaint1, circlePaint2, circlePaint3;
        private readonly Dictionary<long, TouchManipulationInfo> touchDictionary =
            new Dictionary<long, TouchManipulationInfo>();

        public TouchManipulationBitmap(SKBitmap bitmap)
        {
            this.bitmap = bitmap;
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


            // The three circles to pint where the user has cliked 
            
            // Properties of first circle
            circlePaint1 = new SKPaint
            {
                Color = SKColor.Parse("#FF000000"),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 6,
                IsAntialias = true
            };

            

            // Properties of second circle
            circlePaint2 = new SKPaint
            {
                Color = SKColor.Parse("#FFFFFFFF"),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 6,
                IsAntialias = true
            };


            circlePaint3 = new SKPaint
            {

                // Color = SKColor.Parse("#FFFF0000"),

                // No solid color for this one. Use a shader instead to create a gradient
                // so that it can bee seen on any pixel color
                Shader = SKShader.CreateSweepGradient(
                       new SKPoint(128, 128),
                       colors,
                       null),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 6,
                IsAntialias = true
            };

        }

        public TouchManipulationManager TouchManager { set; get; }

        public SKMatrix Matrix { set; get; }

        /// <summary>
        /// Paints the bitmap, not filter, not cursor
        /// </summary>
        /// <param name="canvas"></param>
        public void Paint(SKCanvas canvas)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);
            canvas.DrawBitmap(bitmap, 0, 0);
            canvas.Restore();
        }


        /// <summary>
        /// Paints th skBitmap applying a filter
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="filter"></param>
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


        /// <summary>
        ///  Paint he skBitmap appling a filter and displaying the cursor if point is not empty
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="filter"></param>
        /// <param name="point"></param>
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
                canvas.DrawCircle(point.X, point.Y, _radius1, circlePaint1);
                canvas.DrawCircle(point.X, point.Y, _radius2, circlePaint2);
                canvas.DrawCircle(point.X, point.Y, _radius3, circlePaint3);
            }

        }


        /// <summary>
        /// Paint he skBitmap within a given rectangle appling a filter and displaying the cursor if point is not empty
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="rect"></param>
        /// <param name="point"></param>
        public void Paint(SKCanvas canvas, SKRect rect, SKPoint point)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);
            canvas.DrawBitmap(bitmap, rect);
            if (!point.IsEmpty)
            {
                canvas.DrawCircle(point.X, point.Y, _radius1, circlePaint1);
                canvas.DrawCircle(point.X, point.Y, _radius2, circlePaint2);
                canvas.DrawCircle(point.X, point.Y, _radius3, circlePaint3);
            }
            canvas.Restore();


        }

        /// <summary>
        /// Tests the the location is within the bitmap
        /// to seen if the user pressed inside th bitmap.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Handles the touche event
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="location"></param>
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


        /// <summary>
        /// Manipulates the bitmpa within the canvas (Move, zoom, rotate, ...)
        /// </summary>
        void Manipulate()
        {
            if (manipulating) return;

            manipulating = true;
            SKMatrix touchMatrix = SKMatrix.CreateIdentity();
            TouchManipulationInfo[] infos = new TouchManipulationInfo[touchDictionary.Count];
            touchDictionary.Values.CopyTo(infos, 0);


            // On finger touch
            if (infos.Length == 1)
            {
                SKPoint prevPoint = infos[0].PreviousPoint;
                SKPoint newPoint = infos[0].NewPoint;
                SKPoint pivotPoint = Matrix.MapPoint(bitmap.Width / 2, bitmap.Height / 2);

                touchMatrix = TouchManager.OneFingerManipulate(prevPoint, newPoint, pivotPoint);
            }
            // Two fingers touch
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
