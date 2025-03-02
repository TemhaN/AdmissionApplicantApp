using AdmissionApplicantApp.Models;
using System.Windows;

namespace AdmissionApplicantApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToFacultySelection();
        }

        public void NavigateToFacultySelection()
        {
            MainFrame.Navigate(new FacultySelectionControl());
        }

        public void NavigateToSpecializationSelection(Faculty selectedFaculty)
        {
            MainFrame.Navigate(new SpecializationSelectionControl(selectedFaculty));
        }

        public void NavigateToApplicantDataEntry(Specialization selectedSpecialization)
        {
            MainFrame.Navigate(new ApplicantDataEntryControl(selectedSpecialization));
        }
    }
}
