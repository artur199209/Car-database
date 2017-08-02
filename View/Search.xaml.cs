using LocalDatabase.ViewModel;
using Microsoft.Phone.Controls;

namespace LocalDatabase
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
			this.DataContext = new DBOperationsViewModel();
        }        
    }
}