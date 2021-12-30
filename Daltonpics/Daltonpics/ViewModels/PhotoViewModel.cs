using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Daltonpics.ViewModels
{
    public class PhotoViewModel : BaseRGBViewModel
    {
        private bool _useSlider;

        public bool UseSlider
        {
            get { return _useSlider; }
            set { SetProperty(ref _useSlider, value); }
        }

        private int _filtreActif;

        public int ActiveFilter
        {
            get { return _filtreActif; }
            set { SetProperty(ref _filtreActif, value); }
        }

        public IList<string> ListeVisions
        {
            get
            {

                return new List<string> { "Vision normale",
                                          "Deuteranomlaie - Déficit du rouge",
                                          "Deuteranopie   - Absence du rouge",
                                          "Protoanomalie  - Déficit du vert",
                                          "Protoanopie    - Absence du vert",
                                          "Tritanomalie   - Déficit du bleue",
                                          "Tritanopie     - Absence du bleu",
                                          "Achromatopsie  - Absence de couleurs" };
            }
        }

        private int _percentColorPerception; // Percentage of the color perception

        public int PercentColorPerception
        {
            get { return _percentColorPerception; }
            set { SetProperty(ref _percentColorPerception, value); }
        }


        public PhotoViewModel()
        {
            Title = "Photos";
            PercentColorPerception = 100;
            ActiveFilter = 0;
            PercentColorPerception = 50;
            UseSlider = false;
        }


    }
}