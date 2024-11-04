using System;
using System.Collections.Generic;
using System.Linq;
using goRoam.MobileForms.Resources;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class Textbox : Xamarin.Forms.Grid
    {
        bool _initing;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(Textbox), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(Textbox), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Textbox), default(string), BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(Textbox), default(string), BindingMode.OneTime);
        public string DefaultValue
        {
            get => (string)GetValue(DefaultValueProperty);
            set => SetValue(DefaultValueProperty, value);
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(Textbox), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(Textbox), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(Textbox), default(string), BindingMode.OneTime);
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(Textbox), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(Textbox), false, BindingMode.OneTime);
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public Textbox()
        {
            InitializeComponent();

            entry.TextChanged += OnTextChanged;
            entry.Completed += Entry_Completed;
            entry.Unfocused += Entry_Unfocused;

            helper.IsVisible = !String.IsNullOrEmpty(HelperText);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            SetIsValid();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            SetIsValid();
        }

        private void SetIsValid()
        {
            var validator = entry.Behaviors.FirstOrDefault(b => b is TextValidationBehavior) as TextValidationBehavior;
            validator?.ForceValidate();
            IsValid = validator?.IsValid ?? true;
            this.error.Text = IsValid ? "" : AppResources.RequiredMissing;
            this.error.IsVisible = !IsValid;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initing)
                Text = e.NewTextValue;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            _initing = true;

            base.OnPropertyChanged(propertyName);

            try
            {
                if (propertyName == TitleProperty.PropertyName)
                {
                    this.label.Text = Title;
                    return;
                }

                if (propertyName == HelperTextProperty.PropertyName)
                {
                    helper.Text = HelperText;
                    helper.IsVisible = !String.IsNullOrEmpty(HelperText);
                    return;
                }

                if (propertyName == IsRequiredProperty.PropertyName)
                {
                    if (IsRequired)
                    {
                        this.entry.Behaviors.Add(new TextValidationBehavior { MinimumLength = 1, InvalidStyle = this.Resources["InvalidEntryStyle"] as Style });
                        this.required.Text = $" ({AppResources.Required.ToLower()})";
                    }

                    SetIsValid();
                    return;
                }

                if (propertyName == IsReadOnlyProperty.PropertyName)
                {
                    entry.IsEnabled = !IsReadOnly;
                    return;
                }

                if (propertyName == DefaultValueProperty.PropertyName)
                {
                    if (!String.IsNullOrEmpty(DefaultValue))
                    {
                        entry.Text = DefaultValue;
                        SetIsValid();
                    }

                    return;
                }

                if (propertyName == TextProperty.PropertyName)
                {
                    if (!String.IsNullOrEmpty(Text))
                    {
                        entry.Text = Text;
                        SetIsValid();
                    }

                    return;
                }
            }
            finally
            {
                _initing = false;
            }
        }
    }
}
