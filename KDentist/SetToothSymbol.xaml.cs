using KDentist.Model;
using KDentist.Service;
using KDentist.ViewModels;
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
using System.Windows.Shapes;

namespace KDentist
{
    /// <summary>
    /// Interaction logic for SetToothSymbol.xaml
    /// </summary>
    public partial class SetToothSymbol : Window
    {
        public SetToothSymbol()
        {
            InitializeComponent();
            var button = new Button();
            button.Click += ChooseType_Click;
            button.Height = 35;
            button.Margin = new Thickness(0, 2, 0, 2);
            button.FontSize = 16;
            button.Tag = 0;
            button.Content = "Нищо";
            this.Diagnoses.Children.Add(button);

            var items = new DiagnoseService().GetAll();
            foreach (var item in items)
            {
                var buttonRef = new Button();
                buttonRef.Click += ChooseType_Click;
                buttonRef.Height = 35;
                buttonRef.Margin = new Thickness(0, 2, 0, 2);
                buttonRef.FontSize = 16;
                buttonRef.Tag = item.Id;
                buttonRef.Content = item.Name + " - " + item.Code;
                this.Diagnoses.Children.Add(buttonRef);
            }
        }

        private void ChooseType_Click(object sender, RoutedEventArgs e)
        {
            var text = (sender as Button).Content.ToString();
            if (text != "Нищо")
            {
                var id = int.Parse((sender as Button).Tag.ToString());
                (this.DataContext as AddToothSymbolViewModel).TypeProcedure= new Diagnose{Id=id};
                var viewModel = (this.DataContext as AddToothSymbolViewModel);
                viewModel.Add.Execute(null);
                this.Close();
            }
            else
            {
                var viewModel = (this.DataContext as AddToothSymbolViewModel);
                (this.DataContext as AddToothSymbolViewModel).TypeProcedure = null;
                viewModel.Add.Execute(null);
                this.Close();
            }
        }
    }
}
