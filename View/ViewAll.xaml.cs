using Microsoft.Phone.Controls;
using LocalDatabase.ViewModel;

namespace LocalDatabase
{
    public partial class ViewAll : PhoneApplicationPage
    {
        public ViewAll()
        {
            InitializeComponent();
			this.DataContext = new DBOperationsViewModel();
        }
    }
}