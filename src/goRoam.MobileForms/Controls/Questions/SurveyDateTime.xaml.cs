using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class SurveyDateTime : Grid
    {
        private bool _initing = true;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SurveyDateTime), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(SurveyDateTime), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SurveyDateTime), default(string), BindingMode.OneWayToSource);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(SurveyDateTime), default(string), BindingMode.OneTime);
        public string DefaultValue
        {
            get => (string)GetValue(DefaultValueProperty);
            set => SetValue(DefaultValueProperty, value);
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(SurveyDateTime), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(SurveyDateTime), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(SurveyDateTime), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(SurveyDateTime), false, BindingMode.OneTime);
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public SurveyDateTime()
        {
            InitializeComponent();

            date.Date = DateTime.Now;
            date.DateSelected += OnDateSelected;
            time.Time = DateTime.Now.TimeOfDay;
            time.PropertyChanged += OnTimePropertyChanged;

            Text = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, time.Time.Hours, time.Time.Minutes, time.Time.Seconds, DateTimeKind.Local).ToString("O");

            label.Text = Title;
            helper.IsVisible = !String.IsNullOrEmpty(HelperText);
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _initing = false;
            Text = new DateTime(e.NewDate.Year, e.NewDate.Month, e.NewDate.Day, time.Time.Hours, time.Time.Minutes, time.Time.Seconds, DateTimeKind.Local).ToString("O");
            _initing = true;
        }

        private void OnTimePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _initing = false;
            if (e.PropertyName.Equals(TimePicker.TimeProperty.PropertyName))
            {
                Text = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, time.Time.Hours, time.Time.Minutes, time.Time.Seconds, DateTimeKind.Local).ToString("O");
            }
            _initing = true;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                label.Text = Title;
                return;
            }

            if (propertyName == HelperTextProperty.PropertyName)
            {
                helper.Text = HelperText;
                helper.IsVisible = !String.IsNullOrEmpty(HelperText);
                return;
            }

            if (propertyName == IsReadOnlyProperty.PropertyName)
            {
                date.IsEnabled = !IsReadOnly;
                time.IsEnabled = !IsReadOnly;
                return;
            }

            if (propertyName == DefaultValueProperty.PropertyName)
            {
                if (!String.IsNullOrEmpty(DefaultValue))
                {
                    if (DateTimeOffset.TryParse(DefaultValue, out DateTimeOffset dt))
                    {
                        var localDate = dt.LocalDateTime;

                        date.Date = dt.Date;
                        time.Time = dt.TimeOfDay;
                    }
                }

                return;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                if (_initing && !String.IsNullOrEmpty(Text))
                {
                    if (DateTimeOffset.TryParse(Text, out DateTimeOffset dt))
                    {
                        var localDate = dt.LocalDateTime;

                        date.Date = dt.Date;
                        time.Time = dt.TimeOfDay;
                    }
                }

                return;
            }
        }
    }
}
