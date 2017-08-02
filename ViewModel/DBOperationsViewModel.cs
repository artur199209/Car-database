using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using LocalDatabase.Model;

namespace LocalDatabase.ViewModel
{
	public class DBOperationsViewModel : NotifyPropertyChanged
	{
		private const string strConnection = @"isostore:/cars.sdf";
		
		private static ObservableCollection<Car> searchCars = new ObservableCollection<Car>();
		public static ObservableCollection<Car> SearchCars
		{
			get { return searchCars; }
			set
			{
				if (searchCars != value)
				{
					searchCars = value;
				}
			}
		}

		Car car;

		public DBOperationsViewModel() 
		{
			car = new Car();
		}

		public string CBrand
		{
			get { return car.CarBrand; }
			set
			{
				if (value != car.CarBrand)
				{
					car.CarBrand = value;
					OnPropertyChanged();
				}
			}
		}

		public string CModel
		{
			get { return car.CarModel; }
			set
			{
				if (value != car.CarModel)
				{
					car.CarModel = value;
					OnPropertyChanged();
				}
			}
		}

		public int CYear
		{
			get { return car.CarYear; }
			set
			{
				if (value != car.CarYear)
				{
					car.CarYear = value;
					OnPropertyChanged();
				}
			}
		}

		public int CDoorsCount
		{
			get { return car.DoorsCount; }
			set
			{
				if (value != car.DoorsCount)
				{
					car.DoorsCount = value;
					OnPropertyChanged();
				}
			}
		}

		public double CFuelUsage
		{
			get { return car.CarFuelUsage; }
			set
			{
				if (value != car.CarFuelUsage)
				{
					car.CarFuelUsage = value;
					OnPropertyChanged();
				}
			}
		}

		public string CType
		{
			get { return car.CarType; }
			set
			{
				if (value != car.CarType)
				{
					car.CarType = value;
					OnPropertyChanged();
				}
			}
		}

		public double CEngineCapacity
		{
			get { return car.CarEngineCapacity; }
			set
			{
				if (value != car.CarEngineCapacity)
				{
					car.CarEngineCapacity = value;
					OnPropertyChanged();
				}
			}
		}

		private string password;
		public string Password
		{
			get { return password; }
			set
			{
				if (password != value)
				{
					password = value;
					OnPropertyChanged();
				}
			}
		}

		private RelayCommand deleteCarCommand;
		public RelayCommand DeleteCarCommand
		{
			get
			{
				return deleteCarCommand ?? (deleteCarCommand = 
					new RelayCommand(() => DeleteCars(CBrand, CModel, CYear.ToString())));
			}
		}

		private RelayCommand addCarCommand;
		public RelayCommand AddCarCommand
		{
			get
			{
				return addCarCommand ?? (addCarCommand =
					new RelayCommand(() => AddCar(CBrand, CModel, CYear.ToString(), CDoorsCount.ToString(),
						CFuelUsage.ToString(), CType, CEngineCapacity.ToString())));
			}
		}

		private RelayCommand searchCarCommand;
		public RelayCommand SearchCarCommand
		{
			get
			{
				return searchCarCommand ?? (searchCarCommand =
					new RelayCommand(() => SearchCar(CBrand, CModel, CYear.ToString())));
			}
		}

		private RelayCommand viewCarCommand;
		public RelayCommand ViewCarCommand
		{
			get
			{
				return viewCarCommand ?? (viewCarCommand =
					new RelayCommand(() => ViewCar()));
			}
		}

		private RelayCommand navigateToMenuCommand;
		public RelayCommand NavigateToMenuCommand
		{
			get
			{
				return navigateToMenuCommand ?? (navigateToMenuCommand =
					new RelayCommand(() => GoTo("Registration")));
			}
		}

		private RelayCommand navigateToSearchCommand;
		public RelayCommand NavigateToSearchCommand
		{
			get
			{
				return navigateToSearchCommand ?? (navigateToSearchCommand =
					new RelayCommand(() => GoTo("Search")));
			}
		}

		private RelayCommand navigateToDeleteCommand;
		public RelayCommand NavigateToDeleteCommand
		{
			get
			{
				return navigateToDeleteCommand ?? (navigateToDeleteCommand =
					new RelayCommand(() => GoTo("Delete")));
			}
		}

		private RelayCommand deleteAllCarsCommand;
		public RelayCommand DeleteAllCarsCommand
		{
			get
			{
				return deleteAllCarsCommand ?? (deleteAllCarsCommand =
					new RelayCommand(() => DeleteAllCars()));
			}
		}

		private void DeleteAllCars()
		{
			IList<Car> CarList = null;
			using (var context = new CarDataContext(strConnection))
			{
				IQueryable<Car> CarQuery = from c in context.Cars select c;
				CarList = CarQuery.ToList();

				if (CarList.Count() == 0)
				{
					MessageBox.Show(LocalDatabase.Resources.Resources.NoItems.ToString());
				}
				else
				{
					context.Cars.DeleteAllOnSubmit(CarList);
					context.SubmitChanges();
					MessageBox.Show(LocalDatabase.Resources.Resources.CarsDeleted.ToString());
				}
			}
		}

		private void GoTo(string key)
		{
			var navigationService = new NavigationService();
			navigationService.Configure("ViewAll", new Uri("/View/ViewAll.xaml?reload=true", UriKind.Relative));
			navigationService.Configure("Registration", new Uri("/View/Registration.xaml?reload=true", UriKind.Relative));
			navigationService.Configure("Search", new Uri("/View/Search.xaml?reload=true", UriKind.Relative));
			navigationService.Configure("Delete", new Uri("/View/Delete.xaml?reload=true", UriKind.Relative));
			navigationService.Configure("Menu", new Uri("/View/Menu.xaml?reload=true", UriKind.Relative));
			navigationService.NavigateTo(key);
		}

		public void ViewCar()
		{
			IList<Car> CarList = null;

			using (var context = new CarDataContext(strConnection))
			{
				IQueryable<Car> CarQuery = from c in context.Cars select c;
				CarList = CarQuery.ToList();
			}

			SearchCars.Clear();

			foreach(var item in CarList)
			{
				SearchCars.Add(item);
			}

			GoTo("ViewAll");
		}

		public void DeleteCars(string carBrand, string carModel, string carYear)
		{
			IList<Car> ToDel = null;

			if (string.IsNullOrEmpty(carBrand) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(carYear))
			{
				MessageBox.Show(LocalDatabase.Resources.Resources.FillAllGaps.ToString());
			}
			else
			{
				using (CarDataContext context = new CarDataContext(strConnection))
				{
					IQueryable<Car> CarQuery = from c in context.Cars
											   where c.CarBrand == carBrand && c.CarModel == carModel
												   && c.CarYear == Convert.ToInt32(carYear)
											   select c;
					ToDel = CarQuery.ToList();

					if (ToDel.Count() == 0)
					{
						MessageBox.Show(LocalDatabase.Resources.Resources.NoItems.ToString());
					}
					else
					{
						context.Cars.DeleteAllOnSubmit(ToDel);
						context.SubmitChanges();
						MessageBox.Show(LocalDatabase.Resources.Resources.CarsDeleted.ToString());
					}
				}
			}
		}

		public void AddCar(string carBrand, string carModel, string carYear, string doorsCount, string carFuelUsage, string carType, string carEngineCapacity)
		{
			if (string.IsNullOrEmpty(carBrand) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(carYear)
				|| string.IsNullOrEmpty(doorsCount) || string.IsNullOrEmpty(carFuelUsage) || string.IsNullOrEmpty(carType) || string.IsNullOrEmpty(carEngineCapacity))
			{
				MessageBox.Show(LocalDatabase.Resources.Resources.FillAllGaps.ToString());
			}
			else
			{
				using (var context = new CarDataContext(strConnection))
				{
					var car = new Car(carBrand, carModel, carYear, doorsCount, carFuelUsage, carType, carEngineCapacity);
					context.Cars.InsertOnSubmit(car);
					context.SubmitChanges();
					MessageBox.Show(LocalDatabase.Resources.Resources.CarsDeleted.ToString());
				}
			}  
		}

		public void SearchCar(string carBrand, string carModel, string carYear)
		{
			if (string.IsNullOrEmpty(carBrand) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(carYear))
			{
				MessageBox.Show(LocalDatabase.Resources.Resources.FillAllGaps.ToString());
			}
			else
			{
				IList<Car> searchCarList = null;
				using (CarDataContext context = new CarDataContext(strConnection))
				{
					IQueryable<Car> QueryCar = from c in context.Cars
											   where c.CarBrand == carBrand &&
												   c.CarModel == carModel && c.CarYear == Convert.ToInt32(carYear)
											   select c;
					searchCarList = QueryCar.ToList();

				}

				if (searchCarList.Count() == 0)
				{
					MessageBox.Show(LocalDatabase.Resources.Resources.NoItems.ToString());
				}
				else
				{
					SearchCars.Clear();

					foreach(var item in searchCarList)
					{
						SearchCars.Add(item);
					}
				}
			}
		}
	}
}