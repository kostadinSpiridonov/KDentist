using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class ProceduresTableViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public int PatientId { get; set; }

        private ObservableCollection<ProcedureTableRow> rows;

        public ObservableCollection<ProcedureTableRow> Rows
        {
            get
            {
                return this.rows;
            }
        }

        public void LoadData(int id)
        {
            this.PatientId = id;
            rows = new ObservableCollection<ProcedureTableRow>();

            var cells = this.proceduresTableService.GetById(id).Cells;

            foreach (var item in cells)
            {
                if (rows.Any(x => x.Year == item.Date.Year))
                {
                    var cell = rows.Where(x => x.Year == item.Date.Year).FirstOrDefault()
                         .Cells.Where(x => x.Tooth == item.Tooth).FirstOrDefault();
                    cell.ProcedureTypeLeft = item.ProcedureTypeLeft != null ? item.ProcedureTypeLeft.Code.ToString() : "";
                    cell.ProcedureTypeTop = item.ProcedureTypeTop != null ? item.ProcedureTypeTop.Code.ToString() : "";
                    cell.ProcedureTypeRight = item.ProcedureTypeRight != null ? item.ProcedureTypeRight.Code.ToString() : "";
                    cell.ProcedureTypeBottom = item.ProcedureTypeBottom != null ? item.ProcedureTypeBottom.Code.ToString() : "";
                    cell.ProcedureTypeCenter = item.ProcedureTypeCenter != null ? item.ProcedureTypeCenter.Code.ToString() : "";
                    cell.Id = item.Id;
                    cell.Year = item.Date.Year;
                }
                else
                {
                    rows.Add(new ProcedureTableRow
                        {
                            Year = item.Date.Year
                        });
                    var cell = rows.Where(x => x.Year == item.Date.Year).FirstOrDefault()
                        .Cells.Where(x => x.Tooth == item.Tooth).FirstOrDefault();
                    cell.ProcedureTypeLeft = item.ProcedureTypeLeft != null ? item.ProcedureTypeLeft.Code.ToString() : "";
                    cell.ProcedureTypeTop = item.ProcedureTypeTop != null ? item.ProcedureTypeTop.Code.ToString() : "";
                    cell.ProcedureTypeRight = item.ProcedureTypeRight != null ? item.ProcedureTypeRight.Code.ToString() : "";
                    cell.ProcedureTypeBottom = item.ProcedureTypeBottom != null ? item.ProcedureTypeBottom.Code.ToString() : "";
                    cell.ProcedureTypeCenter = item.ProcedureTypeCenter != null ? item.ProcedureTypeCenter.Code.ToString() : "";
                    cell.Id = item.Id;
                    cell.Year = item.Date.Year;
                }
            }
            foreach (var item in rows)
            {
                var year = item.Year;
                foreach (var item1 in item.Cells)
                {
                    item1.Year = year;
                }
            }

            rows = new ObservableCollection<ProcedureTableRow>(rows.OrderByDescending(x => x.Year));

            if (rows.Count() == 0)
            {
                rows.Add(new ProcedureTableRow
                {
                    Year = DateTime.Now.Year
                });
                foreach (var item in rows.First().Cells)
                {
                    item.Year = rows.First().Year;
                }
            }
            NotifyAll();
        }

        private ICommand addRow;
        public ICommand AddRow
        {
            get
            {
                if (this.addRow == null)
                {
                    this.addRow = new RelayCommand(this.AddRowC);
                }
                return this.addRow;
            }
        }
        private void AddRowC(object obj)
        {
            rows = new ObservableCollection<ProcedureTableRow>(rows.Reverse());
            var year = rows.Last().Year + 1;

            var r = new ProcedureTableRow
            {
                Year = rows.Last().Year + 1
            };

            foreach (var item in r.Cells)
            {
                item.Year = year;
            }
            rows.Add(r);
            rows = new ObservableCollection<ProcedureTableRow>(rows.Reverse());
            NotifyAll();
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (this.delete == null)
                {
                    this.delete = new RelayCommand(this.DeleteC);
                }
                return this.delete;
            }
        }
        private void DeleteC(object obj)
        {

            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тази година?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                var year = (int)obj;
                if (year <= DateTime.Now.Year)
                {
                    MessageBox.Show("Не може да изтриете настояща или минала година", "Проблем", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var row = this.rows.Where(x => x.Year == year).FirstOrDefault();

                foreach (var item in row.Cells)
                {
                    if (item.Id != 0)
                    {
                        this.procedureCellService.Remove(item.Id);
                    }
                }
                this.rows.Remove(row);
                NotifyAll();
            }
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("PatientId");
            NotifyPropertyChanged("Rows");
        }

        public void UpdateRowCell(AddToothSymbolViewModel model)
        {
            var cell = rows.Where(x => x.Year == model.Year).FirstOrDefault()
                 .Cells.Where(x => x.Tooth == model.Tooth).FirstOrDefault();

            if (model.TypeProcedure != null)
            {
                model.TypeProcedure = this.diagnoseService.GetById(model.TypeProcedure.Id);
            }

            if (model.CellId != 0)
            {
                switch (model.ToothOrient)
                {
                    case 1:
                        {
                            cell.ProcedureTypeLeft = model.TypeProcedure != null ? model.TypeProcedure.Code.ToString() : "";
                            break;
                        }
                    case 2:
                        {
                            cell.ProcedureTypeTop = model.TypeProcedure != null ? model.TypeProcedure.Code.ToString() : "";
                            break;
                        }
                    case 3:
                        {
                            cell.ProcedureTypeRight = model.TypeProcedure != null ? model.TypeProcedure.Code.ToString() : "";
                            break;
                        }
                    case 4:
                        {
                            cell.ProcedureTypeBottom = model.TypeProcedure != null ? model.TypeProcedure.Code.ToString() : "";
                            break;
                        }
                    case 5:
                        {
                            cell.ProcedureTypeCenter = model.TypeProcedure != null ? model.TypeProcedure.Code.ToString() : "";
                            break;
                        }
                }
            }
            else if (model.TypeProcedure != null)
            {
                var updated = this.procedureCellService.GetForTable(this.PatientId).Where(x => x.Date.Year == model.Year)
                     .Where(x => x.Tooth == model.Tooth)
                     .FirstOrDefault();

                cell.Id = updated.Id;
                cell.ProcedureTypeLeft = updated.ProcedureTypeLeft != null ? updated.ProcedureTypeLeft.Code.ToString() : "";
                cell.ProcedureTypeTop = updated.ProcedureTypeTop != null ? updated.ProcedureTypeTop.Code.ToString() : "";
                cell.ProcedureTypeRight = updated.ProcedureTypeRight != null ? updated.ProcedureTypeRight.Code.ToString() : "";
                cell.ProcedureTypeBottom = updated.ProcedureTypeBottom != null ? updated.ProcedureTypeBottom.Code.ToString() : "";
                cell.ProcedureTypeCenter = updated.ProcedureTypeCenter != null ? updated.ProcedureTypeCenter.Code.ToString() : "";
            }
            cell.NotifyPType();
            NotifyAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public class ProcedureTableRow
    {
        public ProcedureTableRow()
        {
            cells = new ObservableCollection<ListCellViewModel>();
            for (int i = 11; i <= 18; i++)
            {
                cells.Add(
                    new ListCellViewModel
                    {
                        Tooth = i
                    });
            }
            for (int i = 21; i <= 28; i++)
            {
                cells.Add(
                    new ListCellViewModel
                    {
                        Tooth = i
                    });
            }
            for (int i = 31; i <= 38; i++)
            {
                cells.Add(
                    new ListCellViewModel
                    {
                        Tooth = i
                    });
            }
            for (int i = 41; i <= 48; i++)
            {
                cells.Add(
                    new ListCellViewModel
                    {
                        Tooth = i
                    });
            }
        }

        public int Year { get; set; }

        private ObservableCollection<ListCellViewModel> cells;

        public ObservableCollection<ListCellViewModel> Cells
        {
            get
            {
                return this.cells;
            }
        }


    }

    public class ListCellViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }


        public string ProcedureTypeLeft { get; set; }
        public string ProcedureTypeTop { get; set; }
        public string ProcedureTypeRight { get; set; }
        public string ProcedureTypeBottom { get; set; }
        public string ProcedureTypeCenter { get; set; }

        public int Tooth { get; set; }

        public int Year { get; set; }

        public void NotifyPType()
        {
            NotifyPropertyChanged("ProcedureTypeLeft");
            NotifyPropertyChanged("ProcedureTypeTop");
            NotifyPropertyChanged("ProcedureTypeRight");
            NotifyPropertyChanged("ProcedureTypeBottom");
            NotifyPropertyChanged("ProcedureTypeCenter");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
