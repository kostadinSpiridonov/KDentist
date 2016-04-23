using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class AddToothSymbolViewModel:BaseViewModel
    {
        private int cellId;
        public int CellId
        {
            get
            {
                return this.cellId;
            }
            set
            {
                this.cellId = value;
            }
        }

        private int toothOrient;
        public int ToothOrient
        {
            get
            {
                return this.toothOrient;
            }
            set
            {
                this.toothOrient = value;
            }
        }
        private bool hasSymbol;
        public bool HasSymbol
        {
            get
            {
                return this.hasSymbol; 
            }
            set
            {
                this.hasSymbol = value;
            }
        }

        private int year;
        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
            }
        }

        private int patientId;
        public int PatientId
        {
            get
            {
                return this.patientId;
            }
            set
            {
                this.patientId = value;
            }
        }

        private int tooth;
        public int Tooth
        {
            get
            {
                return this.tooth;
            }
            set
            {
                this.tooth = value;
            }
        }


        private Diagnose typeProcedure;
        public Diagnose TypeProcedure
        {
            get
            {
                return this.typeProcedure;
            }
            set
            {
                this.typeProcedure = value;
            }
        }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new RelayCommand(this.AddC);
                }
                return this.add;
            }
        }
        private void AddC(object obj)
        {
            if (this.cellId != 0)
            {
                if (this.TypeProcedure != null)
                {
                    var toot = this.procedureCellService.GetById(this.cellId);
                    switch (ToothOrient)
                    {
                        case 1:
                            {
                                toot.ProcedureTypeLeftId = this.TypeProcedure.Id;
                                break;
                            }
                        case 2:
                            {
                                toot.ProcedureTypeTopId = this.TypeProcedure.Id;
                                break;
                            }
                        case 3:
                            {
                                toot.ProcedureTypeRightId = this.TypeProcedure.Id;
                                break;
                            }
                        case 4:
                            {
                                toot.ProcedureTypeBottomId = this.TypeProcedure.Id;
                                break;
                            }
                        case 5:
                            {
                                toot.ProcedureTypeCenterId = this.TypeProcedure.Id;
                                break;
                            }
                    }
                    this.procedureCellService.Update(toot);
                }
                else
                {
                    var toot = this.procedureCellService.GetById(this.cellId);
                    switch (ToothOrient)
                    {
                        case 1:
                            {
                                toot.ProcedureTypeLeftId = null;
                                break;
                            }
                        case 2:
                            {
                                toot.ProcedureTypeTopId = null;
                                break;
                            }
                        case 3:
                            {
                                toot.ProcedureTypeRightId = null;
                                break;
                            }
                        case 4:
                            {
                                toot.ProcedureTypeBottomId = null;
                                break;
                            }
                        case 5:
                            {
                                toot.ProcedureTypeCenterId = null;
                                break;
                            }
                    }
                    this.procedureCellService.Update(toot);

                }
            }
            else if (this.typeProcedure != null)
            {
                var toot = new ProcedureCell
                    {
                        Date = new DateTime(this.year, 1, 1),
                        TableId = this.patientId,
                        Tooth = this.tooth
                    };
                switch (ToothOrient)
                {
                    case 1:
                        {
                            toot.ProcedureTypeLeftId = this.TypeProcedure.Id;
                            break;
                        }
                    case 2:
                        {
                            toot.ProcedureTypeTopId = this.TypeProcedure.Id;
                            break;
                        }
                    case 3:
                        {
                            toot.ProcedureTypeRightId = this.TypeProcedure.Id;
                            break;
                        }
                    case 4:
                        {
                            toot.ProcedureTypeBottomId = this.TypeProcedure.Id;
                            break;
                        }
                    case 5:
                        {
                            toot.ProcedureTypeCenterId = this.TypeProcedure.Id;
                            break;
                        }
                }
                this.procedureCellService.Add(toot);
            }
        }
    }
}
