using Daltonpics.Tools;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Daltonpics.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private ImageSource _testImage;
        private string _imageResourceId;
        private bool _testInProgress;
        private string _answer;
        private int _filtreActif;
        public static List<IshiharaTestItem> ishiharaTestItemList;

        private string _dontKnow;

        public string DontKnow
        {
            get { return _dontKnow; }
            set { SetProperty(ref _dontKnow, value); }
        }


        private string _sartButtonText;

        public string StartButtonText
        {
            get { return _sartButtonText; }
            set { SetProperty(ref _sartButtonText, value); }
        }



        private string _option1;

        public string Option1
        {
            get { return _option1; }
            set { SetProperty(ref _option1, value); }
        }

        private string _option2;

        public string Option2
        {
            get { return _option2; }
            set { SetProperty(ref _option2, value); }
        }
        private string _option3;

        public string Option3
        {
            get { return _option3; }
            set { SetProperty(ref _option3, value); }
        }
        private string _option4;

        public string Option4
        {
            get { return _option4; }
            set { SetProperty(ref _option4, value); }
        }



        private int _testNum;

        public int TestNum
        {
            get { return _testNum; }
            set { SetProperty(ref _testNum, value); SetTest(_testNum); }
        }
        private int _totalDisks;

        public int TotalDisks
        {
            get { return _totalDisks; }
            set { SetProperty(ref _totalDisks, value); }
        }

        private float _progression;

        public float Progression
        {
            get { return _progression; }
            set { SetProperty(ref _progression, value); }
        }




        public int TotalTests()
        {
            return ishiharaTestItemList.Count;
        }

        public void RegisterValue(int test)
        {
            if (test >= 0 && test < ishiharaTestItemList.Count)
            {
                ishiharaTestItemList[test].UserAnswer = Answer;
            }
        }

        public int GoodAnswers()
        {
            int result = 0;

            for (int i = 1; i < ishiharaTestItemList.Count; ++i)
            {
                if (ishiharaTestItemList[i].UserAnswer.Equals(ishiharaTestItemList[i].CorrectAnswer))
                    ++result;
            }

            return result;

        }

        private void SetTest(int test)
        {
            if (test >= 0 && test < ishiharaTestItemList.Count)
            {
                ImageResourceId = ishiharaTestItemList[test].ImageResource;

                Option1 = ishiharaTestItemList[test].Option1;
                Option2 = ishiharaTestItemList[test].Option2;
                Option3 = ishiharaTestItemList[test].Option3;
                Option4 = ishiharaTestItemList[test].Option4;

                Answer = DontKnow;
            }
        }

        public TestViewModel()
        {
            Title = "Test";
            ishiharaTestItemList = ResetIshiHaraList();
            ActiveFilter = 0;
            DontKnow = "Je ne sais pas";
            TestInProgress = false;
            Answer = DontKnow;
            TotalDisks = ishiharaTestItemList.Count-1;
            Progression = 0;

        }

        public int ActiveFilter
        {
            get { return _filtreActif; }
            set { SetProperty(ref _filtreActif, value); }
        }

        public string Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value); }
        }


        public bool TestInProgress
        {
            get { return _testInProgress; }
            set
            {
                SetProperty(ref _testInProgress, value);
                if (value)
                {
                    ishiharaTestItemList = ResetIshiHaraList();
                    StartButtonText = "Recommencer";
                }
                else
                {
                    StartButtonText = "Commencer";
                }
            }
        }


        public string ImageResourceId
        {
            get { return _imageResourceId; }
            set
            {
                SetProperty(ref _imageResourceId, value);
                TestImage = ImageSource.FromResource(_imageResourceId);
            }
        }


        public ImageSource TestImage
        {
            get { return _testImage; }
            set { SetProperty(ref _testImage, value); }
        }



        public List<IshiharaTestItem> ResetIshiHaraList()
        {

            List<IshiharaTestItem> result = new List<IshiharaTestItem> {

                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara0.png","X","X","X","X","","","",""), // SplashScreen image
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara1.png","12","72","17","77","12","12","12","12"),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara2.png","7","3","8","0","8","3","3",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara3.png","6","9","5","8","6","5","5",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara4.png","70","25","29","77","29","70","70",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara5.png","57","72","5","35","57","35","35",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara6.png","2","5","16","9","5","2","2",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara7.png","3","5","6","9","3","5","5",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara8.png","12","15","17","19","15","17","17","0"),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara9.png","21","72","74","77","74","21","21",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara10.png","3","2","8","9","2","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara11.png","5","9","4","6","6","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara12.png","81","61","37","97","97","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara13.png","42","45","61","25","45","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara14.png","5","6","3","7","5","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara15.png","5","2","7","3","7","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara16.png","16","15","75","19","16","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara17.png","18","73","13","23","73","","",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara18.png","3","5","1","7",DontKnow,"5","5",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara19.png","2","7","1","3",DontKnow,"2","2",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara20.png","45","72","67","75",DontKnow,"45","45",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara21.png","12","73","57","77",DontKnow,"73","73",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara22.png","6","26","2","72","26","6","2",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara23.png","2","42","17","4","42","2","4",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara24.png","35","86","5","3","35","5","3",""),
                new IshiharaTestItem("Daltonpics.Images.Ishihara.ishihara25.png","12","96","6","9","96","6","9","")
                };

            return result;

        }


    }


}
