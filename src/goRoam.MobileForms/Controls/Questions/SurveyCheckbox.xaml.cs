using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class SurveyCheckbox : Grid
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SurveyCheckbox), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(SurveyCheckbox), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SurveyCheckbox), default(string), BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(SurveyCheckbox), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(SurveyCheckbox), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(SurveyCheckbox), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(SurveyCheckbox), false, BindingMode.OneTime);
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public SurveyCheckbox()
        {
            InitializeComponent();
            label.Text = Title;
            helper.Text = HelperText;

            checkBox.IsToggled = false;

            if (bool.TryParse(Text, out bool toggled))
                checkBox.IsToggled = toggled;

            checkBox.Toggled += CheckBox_Toggled;

            IsValid = true;

            helper.IsVisible = (helper.Text?.Length ?? 0) > 0;
        }

        private void CheckBox_Toggled(object sender, ToggledEventArgs e)
        {
            Text = e.Value.ToString();
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
                helper.IsVisible = (helper.Text?.Length ?? 0) > 0;
                return;
            }

            if (propertyName == IsReadOnlyProperty.PropertyName)
            {
                checkBox.IsEnabled = !IsReadOnly;
                return;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                if (bool.TryParse(Text, out bool isChecked))
                    checkBox.IsToggled = isChecked;

                return;
            }
        }
    }
}
