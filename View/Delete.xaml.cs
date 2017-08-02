using LocalDatabase.ViewModel;
using Microsoft.Phone.Controls;

namespace LocalDatabase
{
    public partial class Delete : PhoneApplicationPage
    {
        public Delete()
        {
            InitializeComponent();
			this.DataContext = new DBOperationsViewModel();
        }
    }
}