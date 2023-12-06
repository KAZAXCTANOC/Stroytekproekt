using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.StaticObjects
{
	public static class WorldData
	{
		public static ExternalCommandData commandData { get; set; }
		public static List<ElementId> elementIds { get; set; } = new List<ElementId>();
	}
}
