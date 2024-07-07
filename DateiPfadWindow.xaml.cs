using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReadAndWriteStreams
{
	/// <summary>
	/// Interaction logic for DateiPfad.xaml
	/// </summary>
	public partial class DateiPfadWindow : Window
	{
		
		public DateiPfadWindow()
		{
			InitializeComponent();
		}

		private void DateiPfadButton_Click(object sender, RoutedEventArgs e)
		{
			string path = string.Empty;
			string dirName = string.Empty;
			string fileName = "CopyFile.txt";
			byte[] buffer;
			int byteCount;

			#region TextBox
			//wenn Content ungleich null -> set path to content
			if (this.DateiPfadTextBlock.Text != null)
			{ 
				//Prüfen ob Pfad existiert und gelesen werden kann
				if (System.IO.Path.Exists(this.DateiPfadTextBlock.Text))
				{
					//Buffer erzeugen und Inhalt auslesen
					CreateFSByteBuffer(this.DateiPfadTextBlock.Text, out buffer, out byteCount);
					//Direcotry holen
					dirName = System.IO.Path.GetDirectoryName(this.DateiPfadTextBlock.Text)!;
					//Pfad zur neuen Datei erzeugen
					path = System.IO.Path.Combine(dirName, fileName);
					//neue Datei schreiben
					//wenn readSucces = 0 ist, wurde nichts eingelesen. 
					if (byteCount > 0)
					{
						//zum Schreiben der Datei muss das Byte Array in UTF8 encoded werden.   
						File.WriteAllText(path, Encoding.UTF8.GetString(buffer));
						return;
					}
					else
					{
						MessageBox.Show("Es konnte kein Inhalt gelesen werden.\r\nBitte prüfen Sie ob die Datei" +
							" leer ist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
						return;
					}
				}
			}
			#endregion

			////////////////////////////////////////////////////////////////////////
			
			#region FileDialog
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "All files (*.*)|*.*"; // Optional: Dateityp-Filter

			if (openFileDialog.ShowDialog() == true)
			{
				// FQ Path der ausgewählten Datei
				string selectedFilePath = openFileDialog.FileName;
				//Direcotry holen
				dirName = System.IO.Path.GetDirectoryName(selectedFilePath)!;

				//Pfad zur neuen Datei erzeugen
				path = System.IO.Path.Combine(dirName, fileName);

				//Vielleicht kann ich den Lese und Schriebzugriff in nur einem ReadWrite Stream erledigen
				//aber ich weiß nicht, ob ich dabei eine Datei erstellen kann.

				//Buffer erzeugen und Inhalt auslesen
				CreateFSByteBuffer(selectedFilePath, out buffer, out byteCount);

				//wenn readSucces = 0 ist, wurde nichts eingelesen. 
				if (byteCount > 0)
				{
					//zum Schreiben der Datei muss das Byte Array in UTF8 encoded werden.   
					File.WriteAllText(path, Encoding.UTF8.GetString(buffer));
				}
				else
				{
					MessageBox.Show("Es konnte kein Inhalt gelesen werden.\r\nBitte prüfen Sie ob die Datei" +
						" leer ist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			#endregion
		}

		/// <summary>
		/// Liest eine Datei ein und erzeugt daraus ein Byte-Array, inklusive Anzahl der gelesen Bytes.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="buffer"></param>
		/// <param name="byteCount"></param>
		private static void CreateFSByteBuffer(string path, out byte[] buffer, out int byteCount)
		{
			//Stream erzeugen
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);

			//zum Einlesen der Datei braucht fs.Read ein leeres byteArray, mit genügend Platz für den Dateiinhalt.
			//Array Länge ist die Länge des FileStreams.
			buffer = new byte[fs.Length];

			//Der Lesevorgang beginnt bei der (Zeiger-)Byte-Position 0 an und endet mit der Länge des Buffers
			//Der Rückgabewert von fs.Read ist die Länge der gelesenen Bytes in int.
			//wenn fs.Read > 0 zurückgibt, dann wurden alle Bytes ordnungsgemäß gelesen.
			//wenn 0 zurückgegeben wird, wurde nichts eingelesen. 
			byteCount = fs.Read(buffer, 0, buffer.Length);

			//Stream schließen
			fs.Close();
		}
	}
}
