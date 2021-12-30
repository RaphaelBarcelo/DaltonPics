using Daltonpics.Tools;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Daltonpics.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        private static int testPos = 0;



        public TestPage()
        {
            InitializeComponent();
            testPos = 0;

            // Create the SKCanvasView and set the PaintSurface handler
            // For images display

            //canvasView = new SKCanvasView();
            // canvasView.PaintSurface += OnCanvasViewPaintSurface;
            // skView.Content = canvasView;

        }


        /// <summary>
        /// Is called when the window appears . We are going to use it to remove
        /// the navigation bar and then have more space on the screen .
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, false);
            DisplayTest(testPos);
            // LoadDefaultImage();
        }

        private void DisplayTest(int testNum)
        {
            if (testPos >= 0 && testPos < viewModel.TotalTests())
            {

                viewModel.TestNum = testNum;
                viewModel.Progression = (float)testNum / (float)viewModel.TotalDisks;

            }
        }



        private void Btn_Start_Cliked(object sender, EventArgs e)
        {
            viewModel.TestInProgress = !viewModel.TestInProgress;
            testPos = viewModel.TestInProgress ? 1 : 0;
            DisplayTest(testPos);
        }

        private void Btn_Next_Cliked(object sender, EventArgs e)
        {

            // Register actual  answer
            viewModel.RegisterValue(testPos);

            if (testPos < (viewModel.TotalTests() - 1))
            {
                ++testPos;

            }
            else
            {
                viewModel.TestInProgress = false;
                testPos = 0;
                // Display result
                ShowResult();

            }
            DisplayTest(testPos);

        }


        async void ShowResult()
        {
            string result = String.Format("Vous avez {0} réponses correctes sur {1}", viewModel.GoodAnswers(), viewModel.TotalTests() - 1);
            bool answer = await DisplayAlert("Résultat", result, "Ok", "Détails");

            Debug.WriteLine("Answer: " + answer);

            if (!answer)
                await Shell.Current.GoToAsync("//ListViewPage", true);
        }

    }
}