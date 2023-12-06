using Autodesk.Revit.UI;
using CsvHelper;
using CsvHelper.Configuration;
using Stroytekproekt.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroytekproekt.Controllers
{
	public class CSVController
	{
		public List<T> Read<T>(string pathToFile)
		{
			var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Encoding = Encoding.UTF8,
				Delimiter = ";"
			};

			try
			{
				using (var fs = File.Open(pathToFile, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					using (var textReader = new StreamReader(fs, Encoding.UTF8))
					using (var csv = new CsvReader(textReader, configuration))
					{
						return csv.GetRecords<T>().ToList();
					}
				}
			}
			catch (Exception e)
			{
				TaskDialog taskDialog = new TaskDialog("Ошибка при чтении CSV файла")
				{
					MainIcon = TaskDialogIcon.TaskDialogIconError,
					MainContent = e.Message,
				};
				taskDialog.Show();

				return null;
			}
		}
	}
}
