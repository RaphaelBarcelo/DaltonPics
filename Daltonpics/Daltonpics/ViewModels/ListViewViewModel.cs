using Daltonpics.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Daltonpics.ViewModels
{
    internal class ListViewViewModel : BaseRGBViewModel
    {
        private List<IshiharaTestItem> _ishiharaTestItemList;


        private bool _displayPopup;

        public bool DisplayPopup
        {
            get { return _displayPopup; }
            set { SetProperty(ref _displayPopup, value); }
        }


        private IshiharaTestItem _activeTest;

        public IshiharaTestItem ActiveTest
        {
            get { return _activeTest; }
            set { SetProperty(ref _activeTest, value); }
        }


        public List<IshiharaTestItem> IshiharaTestItemList
        {
            get { return _ishiharaTestItemList; }
            set { SetProperty(ref _ishiharaTestItemList, value); }
        }

        public ListViewViewModel()
        {

            // Copy of list
        }

        public void FillList()
        {
            IshiharaTestItem item;
            IshiharaTestItemList = new List<IshiharaTestItem>();

            for (int i = 1; i < TestViewModel.ishiharaTestItemList.Count; ++i)
            {
                item = TestViewModel.ishiharaTestItemList[i];
                item.TestDisk = ImageSource.FromResource(item.ImageResource);
                IshiharaTestItemList.Add(item);
            }

            ActiveTest = IshiharaTestItemList[0];

        }



    }
}
