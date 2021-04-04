using Microsoft.Win32;
using Spire.Pdf;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Spire.Doc;
using System.Net;
using System.IO;

namespace AdministratorMISiSmobile
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            
            InitializeComponent();

        }
        private string path;
        private string pathDOC;
        // Получение пути файла PDF
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                lbl.Content = dialog.FileName;
                path = dialog.FileName;
            }
            

        }
        // Сохранение файла PDF
        private void load_btn_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(path);
            pdf.SaveToFile("Full-TimeSchedule.html", Spire.Pdf.FileFormat.HTML);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://212.86.101.101/Full-TimeSchedule.html");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential("anonymous", "");
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(path))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Message.Content =($"Upload File Complete, status {response.StatusDescription}");
            }
            


        }
        //Получение пути файла DOC
        private void lblDOC_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                
                PathDoc.Content = dialog.FileName;
                pathDOC = dialog.FileName;
            }

            


        }
        // Cохранение файла DOC
        private void LoadDoc_Click(object sender, RoutedEventArgs e)
        {
            Spire.Doc.Document document = new Spire.Doc.Document();
            document.LoadFromFile(pathDOC);
            document.SaveToFile("ScheduleByCorrespondence.HTML", Spire.Doc.FileFormat.Html);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://212.86.101.101/ScheduleByCorrespondence.html");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential("anonymous", "");
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(pathDOC))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Message.Content = ($"Upload File Complete, status {response.StatusDescription}");
            }

        }
    }
}
