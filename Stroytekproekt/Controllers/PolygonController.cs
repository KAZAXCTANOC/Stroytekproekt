using Stroytekproekt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.Controllers
{
	public class PolygonController
	{
		public bool IsPolygonPossible(List<Point> tops)
		{
			int countOfTops = tops.Count;

			//Больше ли трёх вершин
			if (countOfTops < 3)
			{
				return false;
			}

			//Лежат ли они на одной прямой?
			//collinear - проверка на преднадлежность одной прямой
			bool collinear = false;
			for (int i = 0; i < countOfTops; i++)
			{
				int orientation = Orientation(tops[i], tops[(i + 1) % countOfTops], tops[(i + 2) % countOfTops]);
				if (orientation == 0)
				{
					collinear = true;
					break;
				}
			}

			if (collinear)
				return false;

			return true;
		}

		public bool IsPointInPolygon(IList<Point> polygons, Point point)
		{
			int count = polygons.Count;
			bool inside = false;

			for (int i = 0; i < polygons.Count - 1; i++)
			{
				if (i + 1 > polygons.Count - 1)
				{
					if (IsPointOnLine(point, polygons[i], polygons[0]))
					{
						return false;
					}
				}
				else
				{
					if (IsPointOnLine(point, polygons[i], polygons[i + 1]))
					{
						return false;
					}
				}
			}

			for (int i = 0, j = count - 1; i < count; j = i++)
			{
				if (((polygons[i].Y > point.Y) != (polygons[j].Y > point.Y)) &&
					(point.X < (polygons[j].X - polygons[i].X) * (point.Y - polygons[i].Y) / (polygons[j].Y - polygons[i].Y) + polygons[i].X))
				{
					inside = !inside;
				}
			}

			return inside;
		}

		#region Доп.вычесления
		private int Orientation(Point p, Point q, Point r)
		{
			// Вычисление ориентации между тремя точками
			double val =
				(q.Y - p.Y)
				* (r.X - q.X)
				- (q.X - p.X)
				* (r.Y - q.Y);

			if (val == 0)
			{
				return 0; // Коллинеарные???
			}
			return (val > 0) ? 1 : 2; //наравление не сильно важно?
		}
		public bool IsPointOnLine(Point point, Point startPoint, Point endPoint)
		{
			double lineLength = Distance(startPoint, endPoint);
			double distanceToStart = Distance(point, startPoint);
			double distanceToEnd = Distance(point, endPoint);

			// Проверка, равна ли сумма расстояний от точки до концов линии длине линии
			return Math.Abs(distanceToStart + distanceToEnd - lineLength) < 1e-25;
		}

		public static double Distance(Point p1, Point p2)
		{
			return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
		}
		#endregion

	}
}
