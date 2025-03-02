using AdmissionApplicantApp.Data;
using AdmissionApplicantApp.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdmissionApplicantApp
{
    public partial class SpecializationSelectionControl : UserControl
    {
        private AppDbContext _context = new AppDbContext();
        private Faculty _faculty;
        public Specialization SelectedSpecialization { get; private set; }

        public SpecializationSelectionControl(Faculty faculty)
        {
            InitializeComponent();
            _faculty = faculty;
            LoadSpecializations();
        }

        private void LoadSpecializations()
        {
            var specializations = _context.Specializations
                .Where(s => s.FacultyID == _faculty.FacultyID)
                .ToList();

            // Если не найдены специализации, сообщим об этом
            if (!specializations.Any())
            {
                Console.WriteLine("Не найдены специализации для данного факультета.");
            }

            // Устанавливаем источник данных для ComboBox
            SpecializationComboBox.ItemsSource = specializations;
            SpecializationComboBox.DisplayMemberPath = "SpecializationName"; // Отображаем имя специальности
            SpecializationComboBox.SelectedValuePath = "SpecializationID"; // Используем SpecializationID как значение
        }

        private void SpecializationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecializationComboBox.SelectedItem != null)
            {
                // Получаем выбранную специальность
                SelectedSpecialization = (Specialization)SpecializationComboBox.SelectedItem;
                Console.WriteLine($"Выбрана специальность: {SelectedSpecialization.SpecializationName}");  // Отладка
            }
            else
            {
                // Если ничего не выбрано, сбрасываем SelectedSpecialization
                SelectedSpecialization = null;
                Console.WriteLine("Специальность не выбрана");  // Отладка
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSpecialization != null)
            {
                // Если специальность выбрана, переходим дальше
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.NavigateToApplicantDataEntry(SelectedSpecialization);
                }
                else
                {
                    MessageBox.Show("Не удалось найти главное окно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Если специальность не выбрана, выводим предупреждение
                MessageBox.Show("Выберите специальность.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
