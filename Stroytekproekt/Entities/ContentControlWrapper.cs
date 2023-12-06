using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Stroytekproekt.Entities
{
	public class ContentControlWrapper<T> where T : UserControl
	{
		public T ControlWindow { get; set; }
	}
}
