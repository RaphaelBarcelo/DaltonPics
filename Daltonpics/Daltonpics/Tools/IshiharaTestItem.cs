using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Daltonpics.Tools
{
    /// <summary>
    /// Question item with possible answers
    /// </summary>
    public class IshiharaTestItem
    {
        public IshiharaTestItem(string imageResource, string option1, string option2, string option3, string option4, string correctAnswer,
                                string answerRed, string answerGreen, string answerBlue 
                                 )
        {
            
            UserAnswer = "";
            ImageResource = imageResource;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            Option4 = option4;
            CorrectAnswer = correctAnswer;
            AnswerRed = answerRed;
            AnswerGreen = answerGreen;
            AnswerBlue = answerBlue;
        }

        private bool _wrongAnswer;

        public bool WrongAnswer
        {
            get { return _wrongAnswer; }
            set { _wrongAnswer = value; }
        }


        private ImageSource _testDisk;

        public ImageSource TestDisk
        {
            get { return _testDisk; }
            set { _testDisk = value; }
        }


        private string _imageResource;

        public string ImageResource
        {
            get { return _imageResource; }
            set { _imageResource = value; }
        }

        private string _option1;

        public string Option1
        {
            get { return _option1; }
            set { _option1 = value; }
        }

        private string _option2;

        public string Option2
        {
            get { return _option2; }
            set { _option2 = value; }
        }
        private string _option3;

        public string Option3
        {
            get { return _option3; }
            set { _option3 = value; }
        }
        private string _option4;

        public string Option4
        {
            get { return _option4; }
            set { _option4 = value; }
        }

        private string _userAnswer;

        public string UserAnswer
        {
            get { return _userAnswer; }
            set { _userAnswer = value;
                WrongAnswer = !_userAnswer.Equals(CorrectAnswer);
            }
        }

        private string _correctAnswer;

        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }

        private string _answerRed;

        public string AnswerRed
        {
            get { return _answerRed; }
            set { _answerRed = value; }
        }

        private string _answerGreen;

        public string AnswerGreen
        {
            get { return _answerGreen; }
            set { _answerGreen = value; }
        }

        private string _answerBlue;

        public string AnswerBlue
        {
            get { return _answerBlue; }
            set { _answerBlue = value; }
        }

    }
}
