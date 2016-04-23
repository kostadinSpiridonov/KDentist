using KDentist.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.ViewModels
{
    public class BaseViewModel
    {
        public IPatientService patientService;
        public IDentalProceduresService dentalProceduresService;
        public IMDTService mdtService;
        public IProceduresTableService proceduresTableService;
        public IProcedureCellService procedureCellService;
        public IDentistAppointmentService dentistAppointmentsService;
        public IDoctorService doctorService;
        public IActivityService activityService;
        public IReminderService reminderService;
        public IVideoService videoService;
        public IDiseaseService diseaseService;
        public IDiagnoseService diagnoseService;

        public BaseViewModel()
        {
            patientService = new PatientService();
            mdtService = new MDTService();
            dentalProceduresService = new DentalProceduresService();
            procedureCellService = new ProcedureCellService();
            proceduresTableService = new ProceduresTableService();
            dentistAppointmentsService = new DentistAppointmentService();
            doctorService = new DoctorService();
            activityService = new ActivityService();
            reminderService = new ReminderService();
            videoService = new VideoService();
            diseaseService = new DiseaseService();
            diagnoseService = new DiagnoseService();
        }
    }
}
