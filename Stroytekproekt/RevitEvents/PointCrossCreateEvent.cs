using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Stroytekproekt.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stroytekproekt.Entities;

using Point = Stroytekproekt.Entities.Point;
using Stroytekproekt.StaticObjects;

namespace Stroytekproekt.RevitEvents
{
	public class PointCrossCreateEvent : IExternalEventHandler
	{
		public Point Point { get; set; }
		public void Execute(UIApplication uiapp)
		{
			UIDocument uidoc = uiapp.ActiveUIDocument;
			Document doc = uidoc.Document;
			doc.DoInTransaction("Создание точки", () =>
			{
				//------X------
				WorldData.elementIds.Add(
					doc.Create.NewModelCurve(
						Line.CreateBound(
							new XYZ(-1000,Point.Y,0),
							new XYZ(1000, Point.Y, 0)),
						uiapp.ActiveUIDocument.ActiveView.SketchPlane).Id);

				//------Y------
				WorldData.elementIds.Add(
					doc.Create.NewModelCurve(
						Line.CreateBound(
							new XYZ(Point.X, 1000, 0),
							new XYZ(Point.X,-1000, 0)),
						uiapp.ActiveUIDocument.ActiveView.SketchPlane).Id);
			});
		}

		public string GetName()
		{
			return this.GetType().Name;
		}
	}
}
