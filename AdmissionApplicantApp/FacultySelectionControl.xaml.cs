using AdmissionApplicantApp.Data;
using AdmissionApplicantApp.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdmissionApplicantApp
{
    public partial class FacultySelectionControl : UserControl
    {
        private AppDbContext _context = new AppDbContext();
        public Faculty SelectedFaculty { get; private set; }

        public FacultySelectionControl()
        {
            InitializeComponent();
            LoadFaculties();
        }

        private void LoadFaculties()
        {
            FacultyComboBox.ItemsSource = _context.Faculties.ToList();
            FacultyComboBox.DisplayMemberPath = "FacultyName";
        }

        private void FacultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFaculty = (Faculty)FacultyComboBox.SelectedItem;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFaculty != null)
            {
                // Получаем ссылку на главное окно
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.NavigateToSpecializationSelection(SelectedFaculty);
                }
                else
                {
                    MessageBox.Show("Не удалось найти главное окно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите факультет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
