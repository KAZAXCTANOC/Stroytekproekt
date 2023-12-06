using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Stroytekproekt.Extentions;
using Stroytekproekt.StaticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.RevitEvents
{
	public class RemoveElementsEvent : IExternalEventHandler
	{
		public RemoveElementsEvent()
		{

		}
		public void Execute(UIApplication uiapp)
		{
			UIDocument uidoc = uiapp.ActiveUIDocument;
			Document doc = uidoc.Document;

			doc.DoInTransaction("Удаление прошлых линий", () =>
			{
				doc.Delete(WorldData.elementIds);
			});

			WorldData.elementIds = new List<ElementId>();
		}

		public string GetName()
		{
			return this.GetType().Name;
		}
	}
}
