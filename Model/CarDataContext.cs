using System.Data.Linq;

namespace LocalDatabase
{
    public class CarDataContext:DataContext
    {
        public CarDataContext(string connection): base(connection)
        {
            if (!DatabaseExists())
            {
                this.CreateDatabase();
            }
        }

        public Table<Car> Cars
        {
            get
            {
                return this.GetTable<Car>();
            }
        }
    }
}