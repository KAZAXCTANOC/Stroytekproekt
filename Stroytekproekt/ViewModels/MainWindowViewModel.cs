using Autodesk.Revit.UI;
using Microsoft.Win32;
using Stroytekproekt.Controllers;
using Stroytekproekt.Core;
using Stroytekproekt.Entities;
using Stroytekproekt.RevitEvents;
using Stroytekproekt.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stroytekproekt.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public PolygonCreateEvent _calculateEvent { get; set; }
		public ExternalEvent _externalCalculateEvent { get; set; }

		public RemoveElementsEvent _removeElementsEvent { get; set; }
		public ExternalEvent _externalRemoveElementsEvent { get; set; }

		public PointCrossCreateEvent _pointCrossCreateEvent { get; set; }
		public ExternalEvent _externalPointCrossCreateEvent { get; set; }

		/// <summary>
		/// Список view-контролов с координатами точек
		/// </summary>
		public ObservableCollection<ContentControlWrapper<DataInsertControl>> DataInsertControls { get; set; }
			= new ObservableCollection<ContentControlWrapper<DataInsertControl>>();

		/// <summary>
		/// View-контрол проверяемой точки
		/// </summary>
		public ContentControlWrapper<DataInsertControl> LocationPointControll { get; set; }

		#region AddDataCommand
		public ICommand AddDataCommand { get; }
		private void AddDataCommandExecute(object p)
		{
			DataInsertControls.Add(
				new ContentControlWrapper<DataInsertControl>
				{
					ControlWindow = new DataInsertControl
					{
						DataContext = new DataInsertControlViewModel()
					}
				});

			OnPropertyChanged();
		}
		#endregion

		#region CreateLinesCommand
		public ICommand CreateLinesCommand { get; }
		private void CreateLinesCommandExecute(object p)
		{
			_externalRemoveElementsEvent.Raise();

			PolygonController polygonController = new PolygonController();

			foreach (ContentControlWrapper<DataInsertControl> dataInsertControl in DataInsertControls)
			{
				_calculateEvent.Points.Add(
					(dataInsertControl.ControlWindow.DataContext as DataInsertControlViewModel)
					.GetPoint());

				_pointCrossCreateEvent.Point = (LocationPointControll.ControlWindow.DataContext as DataInsertControlViewModel).GetPoint();
			}

			if (polygonController.IsPolygonPossible(_calculateEvent.Points))
			{
				this._externalCalculateEvent.Raise();

				this._externalPointCrossCreateEvent.Raise();

				if (polygonController.IsPointInPolygon(_calculateEvent.Points, _pointCrossCreateEvent.Point))
				{
					TaskDialog taskDialog = new TaskDialog("Расчёты")
					{
						MainIcon = TaskDialogIcon.TaskDialogIconNone,
						MainInstruction = "Точка внутри многоугольника!"
					};
					taskDialog.Show();
				}
				else
				{
					TaskDialog taskDialog = new TaskDialog("Расчёты")
					{
						MainIcon = TaskDialogIcon.TaskDialogIconNone,
						MainInstruction = "Точка вне многоугольника!"
					};
					taskDialog.Show();
				}
			}
			else
			{
				TaskDialog taskDialog = new TaskDialog("Внимание")
				{
					MainIcon = TaskDialogIcon.TaskDialogIconShield,
					MainInstruction = "Такого многогольника не может существовать!"
				};
				taskDialog.Show();

				_calculateEvent.Points = new List<Point>();
			}
		}
		#endregion

		public ICommand ReadCSVFileCommand { get; }
		private void ReadCSVFileCommandExecute(object p)
		{
			CSVController csvController = new CSVController();
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Multiselect = false,
				Filter = "(*.txt, *.csv)|*.txt;*.csv"
			};
			bool? result = openFileDialog.ShowDialog();

			if (result != null)
			{
				if (result == true)
				{
					try
					{
						List<Point> points = csvController.Read<Point>(openFileDialog.FileName);
						if (points != null)
						{
							foreach (Point point in points)
							{
								DataInsertControls.Add(
									new ContentControlWrapper<DataInsertControl>()
									{
										ControlWindow = new DataInsertControl()
										{
											DataContext = new DataInsertControlViewModel
											{
												CoordinateX = point.X,
												CoordinateY = point.Y
											}
										}
									});
							}

							OnPropertyChanged(nameof(DataInsertControls));
						}
					}
					catch (Exception E)
					{
						return;
					}
				}
			}
		}

		public MainWindowViewModel()
		{
			LocationPointControll = new ContentControlWrapper<DataInsertControl>()
			{
				ControlWindow = new DataInsertControl()
			};

			ReadCSVFileCommand = new LambdaCommand(ReadCSVFileCommandExecute);
			CreateLinesCommand = new LambdaCommand(CreateLinesCommandExecute);
			AddDataCommand = new LambdaCommand(AddDataCommandExecute);
		}
	}
}
