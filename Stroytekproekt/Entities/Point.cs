using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.Entities
{
	public struct Point
	{
		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; private set; }
		public double Y { get; private set; }

		public XYZ ToXYZ()
		{
			return new XYZ(X, Y, 0);
		}
	}
}
