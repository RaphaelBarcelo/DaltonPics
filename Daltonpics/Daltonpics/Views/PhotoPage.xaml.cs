using System;
using Xamarin.Forms;
using System.IO;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Daltonpics.Tools;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Daltonpics.BitmapManipulation;
using System.Collections.Generic;

namespace Daltonpics.Views
{
    /// <summary>
    ///  Take photos or pick photos from the galerry. Show color of a specific point (pixel) add filters
    ///  and minipulate image (Zoom, scroll)
    /// </summary>
    public partial class PhotoPage : ContentPage
    {
        TouchManipulationBitmap sKBitmap;
        private SKBitmap surfaceBitmap = null;
        private readonly List<long> touchIds = new List<long>();
        int color_coef_index = 0;
        static bool painting = false;
        private readonly ColorFilters visionFilter = new ColorFilters();
        private string PhotoPath;
        SKPoint cursorPpoint;
        bool bitmapMoving = false;


        public object TouchManager { get; private set; }

        public PhotoPage()
        {
            InitializeComponent();

            // Set the SKCanvasView view to skWiew  (ContentView in the XAML)
            // and set the PaintSurface handler
            skView.PaintSurface += OnCanvasViewPaintSurface;

            // Enable touche event
            skView.EnableTouchEvents = true;

            // Set touche event handler
            skView.Touch += SkiaOnTouchEffectAction;

            // Load the default image
            LoadDefaultImage();
        }


        /// <summary>
        /// Is called when the window appears . We are going to use it to remove
        /// the navigation bar and then have more space on the screen .
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, false);
            // LoadDefaultImage();
        }



        #region Take and pick photos


        private async void Browse_gallery(object sender, EventArgs e)
        {
            (sender as ImageButton).IsEnabled = false;
            await PickPhotoAsync();

            (sender as ImageButton).IsEnabled = true;
        }

        private async void Take_picture(object sender, EventArgs e)
        {
            (sender as ImageButton).IsEnabled = false;
            await TakePhotoAsync();
            (sender as ImageButton).IsEnabled = true;

        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                Console.WriteLine($"Feature not supported on the device: {fnsEx.Message}");
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                Console.WriteLine($"Permission not granted: {pEx.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task PickPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                Console.WriteLine($"Feature not supported on the device: {fnsEx.Message}");
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                Console.WriteLine($"Permission not granted: {pEx.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {

            // Reset touched point position  not to draw it
            cursorPpoint = SKPoint.Empty;
            // Operation was canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                SKBitmap bitmap = SKBitmap.Decode(stream);
                sKBitmap = new TouchManipulationBitmap(bitmap);
                sKBitmap.TouchManager.Mode = TouchManipulationMode.ScaleRotate;

                skView.InvalidateSurface();
            }
            PhotoPath = newFile;
        }
        #endregion


        #region Image displaying
        /// <summary>
        /// Called when the canvas needs to be repainted
        /// filters and zoom are applied here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            // If we are already painting then return to avoid a stack overflow
            if (painting) return;
            painting = true;
            if (sKBitmap != null)
            {
                SKSurface paintSurface = args.Surface;
                SKCanvas canvas = paintSurface.Canvas;

                canvas.Clear();


                // If filer type exists in the dictionary
                if (visionFilter.ColorBlindnessFilters.ContainsKey((ColorBlindnessType)viewModel.ActiveFilter))
                {
                    // Set the filter
                    float[] filter = visionFilter.ColorBlindnessFilters[(ColorBlindnessType)viewModel.ActiveFilter];

                    // Modify involved the color coef if slider in use 
                    if (viewModel.UseSlider)
                    {
                        filter[color_coef_index] = ((float)viewModel.PercentColorPerception) / 100f;
                    }

                    // Display the bitmap
                    // If bitmap is not moving draw position circle
                    // else reset point 
                    if (bitmapMoving)
                        cursorPpoint = SKPoint.Empty;
          
                    sKBitmap.Paint(canvas, filter, cursorPpoint);
      
                    // Get the resulting image from the view 
                    if (surfaceBitmap == null)
                        surfaceBitmap = SKBitmap.FromImage(paintSurface.Snapshot());
                }

            }
            painting = false;

        }



        /// <summary>
        /// Loads a default image from resource to avoid a blank screen
        /// </summary>
        public void LoadDefaultImage()
        {
            // Loading the image

            string resourceID = "Daltonpics.Images.daltonpics_logo.png";

            var assembly = this.GetType().GetTypeInfo().Assembly; // "this.GetType()"  can be replaced with "typeof(MyType)", where MyType is any type in the assembly.;
            byte[] buffer;
            using (Stream s = assembly.GetManifestResourceStream(resourceID))
            {
                long length = s.Length;
                buffer = new byte[length];
                s.Read(buffer, 0, (int)length);
                s.Seek(0, SeekOrigin.Begin);
                SKBitmap bitmap = SKBitmap.Decode(s);
                sKBitmap = new TouchManipulationBitmap(bitmap);
                sKBitmap.TouchManager.Mode = TouchManipulationMode.IsotropicScale;

            }
            skView.InvalidateSurface();
        }
        #endregion


        #region Image manipulation

        /// <summary>
        /// Changes filter when filter picker selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedrawImage(object sender, EventArgs e)
        {
            if (sKBitmap != null)
            {
                // Get the selected filter 
                viewModel.ActiveFilter = FilterPicker.SelectedIndex;
                // Set visibility of the slider y necessary
                switch ((ColorBlindnessType)viewModel.ActiveFilter)
                {
                    case ColorBlindnessType.VISION_NORMALE:
                    case ColorBlindnessType.DEUTERANOPIE:
                    case ColorBlindnessType.PROTOANOPIE:
                    case ColorBlindnessType.TRITANOPIE:
                    case ColorBlindnessType.ACHROMATOPSIE:
                        viewModel.UseSlider = false;

                        break;



                    case ColorBlindnessType.DEUTERANOMALIE:
                        color_coef_index = 0;
                        viewModel.UseSlider = true;
                        viewModel.PercentColorPerception = 50;
                        break;
                    case ColorBlindnessType.PROTOANOMALIE:
                        color_coef_index = 6;
                        viewModel.UseSlider = true;
                        viewModel.PercentColorPerception = 50;
                        break;
                    case ColorBlindnessType.TRITANOMALIE:
                        color_coef_index = 12;
                        viewModel.UseSlider = true;
                        viewModel.PercentColorPerception = 50;
                        break;
                    default:
                        viewModel.UseSlider = false;
                        break;
                }
                // Invalidate the canvas surface so that th bitmap is painted 
                // with the new parameters
                skView.InvalidateSurface();
            }
        }


        /// <summary>
        /// Changes Color filter value when slider moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorIndexValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (skView != null && viewModel.UseSlider)
                skView.InvalidateSurface();
        }

        #endregion


        #region Touch events

        /// <summary>
        /// Handler for touche events 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SkiaOnTouchEffectAction(object sender, SKTouchEventArgs args)
        {
            SKPoint point = args.Location;

            switch (args.ActionType)
            {
                case SKTouchAction.Pressed:
                    // On récupère la position

                    if (sKBitmap != null &&
                        surfaceBitmap != null &&
                        sKBitmap.HitTest(point) &&
                        point.X >= 0 && point.X < surfaceBitmap.Width &&
                        point.Y >= 0 && point.Y < surfaceBitmap.Height)
                    {
                        try
                        {
                            SKColor pixel = surfaceBitmap.GetPixel((int)point.X, (int)point.Y);

                            if (pixel != null)
                            {
                                if (pixel.Alpha != 0) // Si on est dans une zone avec couleura
                                {

                                    // Base color
                                    viewModel.CalculateBaseColors(pixel);

                                    // Complementary colors 
                                    viewModel.CalculateComplementaryColors(pixel);
                                }
                            }
                            if (!touchIds.Contains(args.Id))
                                touchIds.Add(args.Id);
                            sKBitmap.ProcessTouchEvent(args.Id, args.ActionType, point);
                            bitmapMoving = false;
                            cursorPpoint = point;
                            //skView.InvalidateSurface();
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        // Les OS know that we want to receive aither events 
                        args.Handled = true;
                    }

                    break;

                case SKTouchAction.Moved:
                    if (sKBitmap != null && touchIds.Contains(args.Id))
                    {
                        try
                        {
                            bitmapMoving = true;
                            sKBitmap.ProcessTouchEvent(args.Id, args.ActionType, point);
                            skView.InvalidateSurface();
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        args.Handled = true;
                    }
                    break;

                case SKTouchAction.Released:
                case SKTouchAction.Cancelled:
                    //_ = viewModel.TellColorAsync();

                    // Set surfaceBitmap to null so that it is reinitialized during painting
                    surfaceBitmap = null;

                    // Get the resulting image on the view 

                    if (sKBitmap != null && touchIds.Contains(args.Id))
                    {
                        touchIds.Remove(args.Id);
                        try
                        {
                            bitmapMoving = false;
                            cursorPpoint = point;
                            sKBitmap.ProcessTouchEvent(args.Id, args.ActionType, point);
                            skView.InvalidateSurface();
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }

                        args.Handled = true;
                    }
                    break;

                default:
                    break;
            }


        }


        #endregion


    }
}