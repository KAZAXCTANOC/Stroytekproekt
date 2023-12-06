using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Stroytekproekt.Extentions;
using Stroytekproekt.StaticObjects;
using System.Collections.Generic;

using Point = Stroytekproekt.Entities.Point;

namespace Stroytekproekt.RevitEvents
{
	public class PolygonCreateEvent : IExternalEventHandler
	{
		public PolygonCreateEvent()
		{

		}

		public List<Point> Points { get; set; } = new List<Point>();
		public void Execute(UIApplication uiapp)
		{
			UIDocument uidoc = uiapp.ActiveUIDocument;
			Document doc = uidoc.Document;
			doc.DoInTransaction("Создание кривых", () =>
			{
				for (int i = 0; i < Points.Count - 1; i += 1)
				{
					WorldData.elementIds.Add(
						doc.Create.NewModelCurve(
							Line.CreateBound(
								Points[i].ToXYZ(),
								Points[i + 1].ToXYZ()),
							uiapp.ActiveUIDocument.ActiveView.SketchPlane).Id);
				}

				WorldData.elementIds.Add(
					doc.Create.NewModelCurve(
						Line.CreateBound(
							Points[Points.Count - 1].ToXYZ(),
							Points[0].ToXYZ()),
						uiapp.ActiveUIDocument.ActiveView.SketchPlane).Id);
			});

			Points = new List<Point>();
		}

		public string GetName()
		{
			return this.GetType().Name;
		}
	}
}
