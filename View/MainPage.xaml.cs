using Microsoft.Phone.Controls;
using LocalDatabase.ViewModel;

namespace LocalDatabase
{
    public partial class MainPage : PhoneApplicationPage
    {
		public MainPage()
        {
            InitializeComponent();
			this.DataContext = new DBOperationsViewModel();
        }
    }
}