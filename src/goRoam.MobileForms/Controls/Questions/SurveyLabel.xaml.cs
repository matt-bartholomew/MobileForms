using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class SurveyLabel : Grid
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SurveyLabel), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }

            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(SurveyLabel), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string HelperText
        {
            get
            {
                return (string)GetValue(HelperTextProperty);
            }

            set
            {
                SetValue(HelperTextProperty, value);
            }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SurveyLabel), default(string), BindingMode.OneWayToSource);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(SurveyLabel), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(SurveyLabel), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(SurveyLabel), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public SurveyLabel()
        {
            InitializeComponent();

            label.Text = Title;
            IsValid = true;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
                label.Text = Title;
        }
    }
}
