using AdmissionApplicantApp.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using AdmissionApplicantApp.Data;

namespace AdmissionApplicantApp
{
    public partial class ApplicantDataEntryControl : UserControl
    {
        private Specialization _selectedSpecialization;
        private AppDbContext _context;

        public ApplicantDataEntryControl(Specialization specialization)
        {
            InitializeComponent();
            _selectedSpecialization = specialization;
            _context = new AppDbContext();  // Инициализация контекста
        }

        private void SubmitApplication_Click(object sender, RoutedEventArgs e)
        {
            // Валидация данных
            if (string.IsNullOrEmpty(FirstNameTextBox.Text) ||
                string.IsNullOrEmpty(LastNameTextBox.Text) ||
                DateOfBirthPicker.SelectedDate == null ||
                GenderComboBox.SelectedItem == null ||
                string.IsNullOrEmpty(AddressTextBox.Text) ||
                string.IsNullOrEmpty(PhoneNumberTextBox.Text) ||
                string.IsNullOrEmpty(EmailTextBox.Text) ||
                string.IsNullOrEmpty(PassportNumberTextBox.Text) ||
                string.IsNullOrEmpty(ExamScoreTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка балла аттестата
            if (!int.TryParse(ExamScoreTextBox.Text, out int examScore) || examScore < 0 || examScore > 100)
            {
                MessageBox.Show("Пожалуйста, введите корректный балл аттестата (от 0 до 100)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание нового объекта заявителя
            var applicant = new Applicant
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString(),
                Address = AddressTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Email = EmailTextBox.Text,
                PassportNumber = PassportNumberTextBox.Text,
                RegistrationDate = DateTime.Now  // Время подачи заявки
            };

            // Сохранение абитуриента в базе данных
            _context.Applicants.Add(applicant);
            _context.SaveChanges();  // Сохраняем абитуриента в базе данных

            // Создание заявки
            var application = new Application
            {
                ApplicantID = applicant.ApplicantID,  // ID абитуриента
                SpecializationID = _selectedSpecialization.SpecializationID,  // ID выбранной специальности
                SubmissionDate = DateTime.Now,  // Дата подачи
                Status = "На рассмотрении",  // Статус заявки
                FacultyID = _selectedSpecialization.FacultyID  // ID факультета
            };

            // Сохранение заявки в базе данных
            _context.Applications.Add(application);
            _context.SaveChanges();  // Сохраняем заявку в базе данных

            // Сохранение балла аттестата
            var examResult = new ExamResult
            {
                ApplicationID = application.ApplicationID,
                StageID = 5,  // Указываем StageID = 1 (так как этап только один)
                ExamScore = examScore
            };
            _context.ExamResults.Add(examResult);
            _context.SaveChanges();  // Сохраняем балл в базе данных

            // Уведомление о успешной подаче
            MessageBox.Show("Заявка подана успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очистка формы после успешной подачи заявки (по желанию)
            ClearForm();
        }

        private void ClearForm()
        {
            // Очистка всех полей формы
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            DateOfBirthPicker.SelectedDate = null;
            GenderComboBox.SelectedItem = null;
            AddressTextBox.Clear();
            PhoneNumberTextBox.Clear();
            EmailTextBox.Clear();
            PassportNumberTextBox.Clear();
            ExamScoreTextBox.Clear();
        }
    }
}