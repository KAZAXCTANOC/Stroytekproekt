using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Stroytekproekt.RevitEvents;
using Stroytekproekt.StaticObjects;
using Stroytekproekt.ViewModels;
using Stroytekproekt.Windows;

namespace Stroytekproekt
{
	[Regeneration(RegenerationOption.Manual)]
	[Transaction(TransactionMode.Manual)]
	public class Command : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			WorldData.commandData = commandData;
			PolygonCreateEvent calculateEvent = new PolygonCreateEvent();
			ExternalEvent externalCalculateEvent = ExternalEvent.Create(calculateEvent);

			RemoveElementsEvent removeElementsEvent = new RemoveElementsEvent();
			ExternalEvent externalremoveElementsEvent = ExternalEvent.Create(removeElementsEvent);

			PointCrossCreateEvent pointCrossCreateEvent = new PointCrossCreateEvent();
			ExternalEvent externalPointCrossCreateEvent = ExternalEvent.Create(pointCrossCreateEvent);

			MainWindow window = new MainWindow()
			{
				DataContext = new MainWindowViewModel
				{
					_externalCalculateEvent = externalCalculateEvent,
					_calculateEvent = calculateEvent,

					_externalRemoveElementsEvent = externalremoveElementsEvent,
					_removeElementsEvent = removeElementsEvent,

					_pointCrossCreateEvent = pointCrossCreateEvent,
					_externalPointCrossCreateEvent = externalPointCrossCreateEvent
				},
				Height = 500,
				Width = 300
			};
			window.Show();

			return Result.Succeeded;
		}
	}
}
