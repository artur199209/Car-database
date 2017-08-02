using System;
using System.Data.Linq.Mapping;

namespace LocalDatabase
{
    [Table]
    public class Car
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID
        {
            get;
            set;
        }

        [Column(CanBeNull=false)]
        public string CarBrand
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string CarModel
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public int CarYear
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public int DoorsCount
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public double CarFuelUsage
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string CarType
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public double CarEngineCapacity
        {
            get;
            set;
        }

		public Car() { }

		public Car(string carBrand, string carModel, string carYear, string doorsCount, string carFuelUsage, string carType, string carEngineCapacity)
		{
			CarBrand = carBrand;
			CarModel = carModel;
			CarYear = Convert.ToInt32(carYear);
			DoorsCount = Convert.ToInt32(doorsCount);
			CarFuelUsage = Convert.ToDouble(carFuelUsage);
			CarType = carType;
			CarEngineCapacity = Convert.ToDouble(carEngineCapacity);
		}
    }
}