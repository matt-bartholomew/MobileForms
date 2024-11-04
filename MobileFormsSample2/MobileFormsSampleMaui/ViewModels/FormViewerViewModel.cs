using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using goRoam.MobileForms.Models;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MobileFormsSample
{
    public class FormViewerViewModel : INotifyPropertyChanged
    {
        public FormViewerViewModel(string formName, bool completed = false)
        {
            var assembly = this.GetType().Assembly;
            var stream = assembly.GetManifestResourceStream($"MobileFormsSampleMauiCore.Resources.{formName}");

            using (var reader = new StreamReader(stream))
            {
                var formJson = reader.ReadToEnd();

                if (!completed)
                {
                    var context = new
                    {
                        CustomerName = "Some Customer",
                        OrderType = "Option 1"
                    };

                    var form = MobileForm.Create(formJson, context);

                    Form = form;
                }
                else
                {
                    var form = JsonConvert.DeserializeObject<MobileForm>(formJson);
                    form.IsReadonly = true;
                    Form = form;
                }
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
