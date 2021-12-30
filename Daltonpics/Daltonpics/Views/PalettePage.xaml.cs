using Daltonpics.BitmapManipulation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Daltonpics.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PalettePage : ContentPage
    {
        private TouchManipulationBitmap sKBitmap;
        readonly float xStart = 0, yStart = 0;
        float scale = 0;
        SKPoint touchedPoint = SKPoint.Empty;

        //readonly SKBitmap paletteBitmap;
        bool painting = false;
        // readonly string  resourceID = "Daltonpics.Images.ColorWheel_900.png";
        public PalettePage()
        {
            InitializeComponent();

            // Set the SKCanvasView view to skWiew  (ContentView in the XAML)
            // and set the PaintSurface handler
            skView.PaintSurface += OnCanvasViewPaintSurface;

            // Enable touche event
            skView.EnableTouchEvents = true;

            // Set touche event handler
            skView.Touch += SkiaOnTouchEffectAction;

            // Téléchargement de l'image

            DisplayColorWheel();

        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            // If we are already painting then return to avoid a stack overflow
            if (painting) return;
            painting = true;
            if (sKBitmap != null)
            {
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;

                canvas.Clear();


                // Scale bitmap to match full screen width
                scale = Math.Min((float)info.Width / sKBitmap.bitmap.Width,
                       (float)info.Height / sKBitmap.bitmap.Height);
                float xstart = (info.Width - scale * sKBitmap.bitmap.Width) / 2;
                float ystart = (info.Height - scale * sKBitmap.bitmap.Height) / 2;
                SKRect destRect = new SKRect(xstart, ystart, xstart + scale * sKBitmap.bitmap.Width,
                                                   ystart + scale * sKBitmap.bitmap.Height);

                // Paint bitmmap
                using (SKPaint paint = new SKPaint())
                {
                    //canvas.DrawBitmap(sKBitmap, hOfset, vOfset, paint: paint);
                    //canvas.DrawBitmap(sKBitmap, destRect);
                    sKBitmap.Paint(canvas, destRect, touchedPoint);
                }

            }
            painting = false;

        }


        #region Touch events

        /// <summary>
        /// Handler for touche events 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SkiaOnTouchEffectAction(object sender, SKTouchEventArgs args)
        {

            SKPoint pt = args.Location;
            SKPoint point = new SKPoint(xStart + (pt.X / scale), yStart + (pt.Y / scale));

            switch (args.ActionType)
            {
                case SKTouchAction.Pressed:
                case SKTouchAction.Moved:
                case SKTouchAction.Released:
                case SKTouchAction.Cancelled:
                    // On récupère la position

                    if (sKBitmap != null)
                    {
                        if (point.X >= 0 && point.X < sKBitmap.bitmap.Width & point.Y >= 0 && point.Y < sKBitmap.bitmap.Height)
                        {
                            SKColor pixel = sKBitmap.bitmap.GetPixel((int)point.X, (int)point.Y);

                            if (pixel != null)
                            {
                                if (pixel.Alpha != 0) // Si on est dans une zone avec couleura
                                {

                                    // Base color
                                    viewModel.CalculateBaseColors(pixel);

                                    // Complementary colors 
                                    viewModel.CalculateComplementaryColors(pixel);
                                    if (args.ActionType != SKTouchAction.Moved)
                                        touchedPoint = pt;
                                    else
                                        touchedPoint = SKPoint.Empty;
                                    skView.InvalidateSurface();
                                }

                            }
                        }




                        // Les OS know that we want to receive aither events 
                        args.Handled = true;
                    }

                    break;


                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Loads the color wheel
        /// </summary>

        private void DisplayColorWheel()
        {
            // Loading the image

            string resourceID = "Daltonpics.Images.ColorWheel_900.png";

            var assembly = this.GetType().GetTypeInfo().Assembly; // "this.GetType()"  can be replaced with "typeof(MyType)", where MyType is any type in the assembly.;
            byte[] buffer;
            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                long length = stream.Length;
                buffer = new byte[length];
                stream.Read(buffer, 0, (int)length);
                stream.Seek(0, SeekOrigin.Begin);
                //sKBitmap = SKBitmap.Decode(s);

                SKBitmap bitmap = SKBitmap.Decode(stream);
                sKBitmap = new TouchManipulationBitmap(bitmap);
                sKBitmap.TouchManager.Mode = TouchManipulationMode.ScaleRotate;
                // Remise à la taille de l'écran
                // var dstInfo = new SKImageInfo((int)skView.CanvasSize.Width, (int)skView.CanvasSize.Width);

                // sKBitmap = sKBitmap.Resize(dstInfo, SKFilterQuality.High);
            }
            skView.InvalidateSurface();
        }

        /// <summary>
        /// Est appelée lors de l'affichage de la fenêtre
        /// On va lutiliser pour enlever la barre de navigation
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, false);
        }

        private void ComplementSwitchOnTogle(object sender, ToggledEventArgs e)
        {
            titreSwitch.TextColor = (e.Value == true) ? Color.Black : Color.Gray;
        }




    }
}