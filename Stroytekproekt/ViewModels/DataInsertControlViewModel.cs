using Stroytekproekt.Core;
using Stroytekproekt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.ViewModels
{
	public class DataInsertControlViewModel : BaseViewModel
	{
		public double CoordinateX { get; set; } = 0;
		public double CoordinateY { get; set; } = 0;
		public Point GetPoint()
		{
			return new Point(CoordinateX, CoordinateY);
		}

		public DataInsertControlViewModel()
		{

		}
	}
}
