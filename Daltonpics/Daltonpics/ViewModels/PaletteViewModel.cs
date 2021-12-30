using Xamarin.Forms;

namespace Daltonpics.ViewModels
{
    public class PaletteViewModel : BaseRGBViewModel
    {
        private bool _useComplementaryColor;
        private ImageSource _paletteImage;
        private string _imageResourceId;

        public string ImageResourceId
        {
            get { return _imageResourceId; }
            set
            {
                SetProperty(ref _imageResourceId, value);
                PaletteImage = ImageSource.FromResource(_imageResourceId);
            }
        }


        public ImageSource PaletteImage
        {
            get { return _paletteImage; }
            set { SetProperty(ref _paletteImage, value); }
        }


        public bool UseComplementaryColor
        {
            get { return _useComplementaryColor; }
            set { SetProperty(ref _useComplementaryColor, value); }
        }


        public PaletteViewModel()
        {
            Title = "Palette";

            // Default Image
            ImageResourceId = "Daltonpics.Images.ColorWheel_900.png";

            UseComplementaryColor = false;
        }



    }
}
