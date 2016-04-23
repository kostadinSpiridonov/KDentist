using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using KDentist.Model;
using KDentist.ViewModels.CalculatorViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDentist.Views.CalculatorViews
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Page
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }

        private void ReCalculateRowPrice(object sender, TextChangedEventArgs e)
        {
            var guid = (sender as TextBox).Tag.ToString();
            (this.DataContext as CalculatorHomeViewModel).Rows.Where(x => x.Guid == guid).FirstOrDefault().CalculateRowPrice();
        }

        private void CalculateTotal(object sender, DataTransferEventArgs e)
        {

            (this.DataContext as CalculatorHomeViewModel).CalculateTotal();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Document (.pdf)|*.pdf";
            saveFileDialog.FileName = Guid.NewGuid().ToString();

            var context = (this.DataContext as CalculatorHomeViewModel);
            if(context.Diagnose==null)
            {
                context.Diagnose = "";
            }
            if (context.FirstName == null)
            {
                context.FirstName = "";
            }
            if (context.LastName == null)
            {
                context.LastName = "";
            }
            if (context.Doctor == null)
            {
                context.Doctor = "";
            }
            if (context.Note == null)
            {
                context.Note = "";
            }
            if (saveFileDialog.ShowDialog() == true)
            {
                var path = saveFileDialog.FileName;

                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    Document myDocument = new Document();


                    PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);

                    Assembly ass = Assembly.GetExecutingAssembly();
                    string arPath = System.IO.Path.GetDirectoryName(ass.Location);
                    string resourcesFolderPath = System.IO.Path.Combine(
                            Directory.GetParent(arPath).Parent.FullName, "Resources");
                    string ARIALUNI_TFF = System.IO.Path.Combine(resourcesFolderPath, "ARIALUNI.TTF");

                    //Create a base font object making sure to specify IDENTITY-H
                    BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                    //Create a specific font object
                    Font f = new Font(bf, 12, Font.NORMAL);

                    myDocument.Open();

                    Font titleFont = new Font(bf, 22, Font.NORMAL);
                    var title = new iTextSharp.text.Paragraph("План за лечение", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;

                    myDocument.Add(title);
                    myDocument.Add(new Chunk("\n"));

                    iTextSharp.text.Paragraph par = new iTextSharp.text.Paragraph("Пациент: " + context.FirstName + " " + context.LastName,f);
                    iTextSharp.text.Paragraph par1 = new iTextSharp.text.Paragraph("Доктор: " + context.Doctor,f);
                    iTextSharp.text.Paragraph par2 = new iTextSharp.text.Paragraph("Диагноза: " + context.Diagnose,f);
                    iTextSharp.text.Paragraph par3 = new iTextSharp.text.Paragraph("Бележка: " + context.Note,f);
                  
                    myDocument.Add(par);
                    myDocument.Add(par1);
                    myDocument.Add(par2);
                    myDocument.Add(par3);
                    myDocument.Add(new Chunk("\n"));

                    var tableTit = new PdfPTable(4);
                    Font titB = new Font(bf, 12, Font.BOLD);
                    var firstTCell = new PdfPCell((new iTextSharp.text.Paragraph("Дейност", titB)));
                    firstTCell.BorderColor = BaseColor.WHITE;
                    var secondTCell = new PdfPCell((new iTextSharp.text.Paragraph("Брой", titB)));
                    secondTCell.BorderColor = BaseColor.WHITE;
                    var thirdTCell = new PdfPCell((new iTextSharp.text.Paragraph("Цена", titB)));
                    thirdTCell.BorderColor = BaseColor.WHITE;

                    var fourthTCell = new PdfPCell((new iTextSharp.text.Paragraph("Ед.цена", titB)));
                    fourthTCell.BorderColor = BaseColor.WHITE;
                    tableTit.AddCell(firstTCell);
                    tableTit.AddCell(fourthTCell);
                    tableTit.AddCell(secondTCell);
                    tableTit.AddCell(thirdTCell);
                    myDocument.Add(tableTit);

                    var table1 = new PdfPTable(4);
                    foreach (var item in context.Rows)
                    {
                        if (item.SelectedActivity != null)
                        {
                            var f1 = new PdfPCell(new iTextSharp.text.Paragraph(item.SelectedActivity.Name.ToString(), f));
                            f1.Border = 0;
                            f1.BorderWidthTop = 1;
                            var s1 = new PdfPCell(new iTextSharp.text.Paragraph(item.CountProcedures.ToString(), f));
                            s1.Border = 0;
                            s1.BorderWidthTop = 1;
                            var t1 = new PdfPCell(new iTextSharp.text.Paragraph(item.RowPrice.ToString() + " лв.", f));
                            t1.Border = 0;
                            t1.BorderWidthTop = 1;
                            var fo1 = new PdfPCell(new iTextSharp.text.Paragraph(item.SelectedActivity.Price.ToString() + " лв.", f));
                            fo1.Border = 0;
                            fo1.BorderWidthTop = 1;
                            table1.AddCell(f1);
                            table1.AddCell(fo1);
                            table1.AddCell(s1);
                            table1.AddCell(t1);
                        }
                    }
                    myDocument.Add(table1);


                    myDocument.Add(new Chunk("\n"));

                    Font totalFont = new Font(bf, 14, Font.BOLD);
                    var table3 = new PdfPTable(4);

                    var totalCell4 = new PdfPCell((new iTextSharp.text.Paragraph(" ", totalFont)));
                    totalCell4.BorderColor = BaseColor.WHITE;
                    totalCell4.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table3.AddCell(totalCell4);

                    var totalCell3 = new PdfPCell((new iTextSharp.text.Paragraph(" ", totalFont)));
                    totalCell3.BorderColor = BaseColor.WHITE;
                    totalCell3.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table3.AddCell(totalCell3);

                    var totalCell = new PdfPCell((new iTextSharp.text.Paragraph("Общо: ", totalFont)));
                    totalCell.BorderColor = BaseColor.WHITE;
                    totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table3.AddCell(totalCell);


                    var totalCell2 = new PdfPCell((new iTextSharp.text.Paragraph(context.Total.ToString() + " лв.", totalFont)));
                    totalCell2.BorderColor = BaseColor.WHITE;
                    totalCell2.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(totalCell2);
                    myDocument.Add(table3);

                    myDocument.Add(new Chunk("\n"));
                    myDocument.Add(new Chunk("\n"));
                    myDocument.Add(new Chunk("\n")); 
                    Chunk glue = new Chunk(new VerticalPositionMark());
               
                    var datePar = new iTextSharp.text.Paragraph(DateTime.Now.ToShortDateString(), f);
                    datePar.Add(new Chunk(glue));
                   
                   
                    datePar.Add("Подпис                                       ");
                   
                    myDocument.Add(datePar);
                    // We're done adding stuff to our PDF.
                    myDocument.Close();

                    myPDFWriter.Close();
                    byte[] content = myMemoryStream.ToArray();

                    // Write out PDF from memory stream.
                    using (FileStream fs = File.Create(path))
                    {
                        fs.Write(content, 0, (int)content.Length);
                    }
                }

            }
        }



    }
}
