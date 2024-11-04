using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using goRoam.MobileForms.Models;
using HandlebarsDotNet;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MobileFormsSample
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel(string formName)
        {
            ResultsCommand = new Command(() =>
            {
                var surveyJson = JsonConvert.SerializeObject(Form);
                Console.WriteLine(surveyJson);
            });

            var assembly = this.GetType().Assembly;
            var stream = assembly.GetManifestResourceStream($"MobileFormsSample.Resources.{formName}");

            using (var reader = new StreamReader(stream))
            {
                var formJson = reader.ReadToEnd();

                var context = new
                {
                    CustomerName = "Some Customer",
                    OrderType = "Option 1"
                };

                var form = MobileForm.Create(formJson, context);

                Form = form;
            }
        }

        private MobileForm _form;

        public event PropertyChangedEventHandler PropertyChanged;

        public MobileForm Form
        {
            get => _form;
            set
            {
                _form = value;
                OnPropertyChanged();
            }
        }

        public string Culture
        {
            get => CultureInfo.CurrentCulture.Name;
        }

        public ICommand ResultsCommand
        {
            get;
            private set;
        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
