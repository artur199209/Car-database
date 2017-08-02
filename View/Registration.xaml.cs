using LocalDatabase.ViewModel;
using Microsoft.Phone.Controls;

namespace LocalDatabase
{
    public partial class Registration : PhoneApplicationPage
    {
		public Registration()
        {
            InitializeComponent();
			this.DataContext = new DBOperationsViewModel();
        }
    }
}