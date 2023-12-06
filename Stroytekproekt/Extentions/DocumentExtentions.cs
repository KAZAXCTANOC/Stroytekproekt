using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.Extentions
{
	public static class DocumentExtentions
	{
		public static void DoInTransaction(this Document doc, string name, Action action)
		{
			using (Transaction trans = new Transaction(doc, name))
			{
				trans.Start();
				action();
				trans.Commit();
			}
		}
	}
}
