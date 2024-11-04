using System;
using System.Collections.Generic;
using System.Globalization;
using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;
using goRoam.MobileFormsMauiCore.Resources;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class NumericTextbox : Grid
    {
        bool _initing;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NumericTextbox), default(string), BindingMode.OneTime);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(NumericTextbox), default(string), BindingMode.OneTime);
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(NumericTextbox), default(string), BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(NumericTextbox), default(string), BindingMode.OneTime);
        public string DefaultValue
        {
            get => (string)GetValue(DefaultValueProperty);
            set => SetValue(DefaultValueProperty, value);
        }

        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(NumericTextbox), double.MinValue, BindingMode.OneTime);
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(NumericTextbox), double.MaxValue, BindingMode.OneTime);
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(NumericTextbox), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(NumericTextbox), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(NumericTextbox), false, BindingMode.OneTime);
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(NumericTextbox), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(NumericTextbox), false, BindingMode.OneTime);
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public NumericTextbox()
        {
            InitializeComponent();

            entry.TextChanged += Entry_TextChanged;
            entry.Completed += Entry_Completed;
            entry.Unfocused += Entry_Unfocused;

            helper.IsVisible = !String.IsNullOrEmpty(HelperText);
            SetIsValid();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initing)
            {
                try
                {
                    _initing = true;

                    var addingText = e.NewTextValue.Length > (e.OldTextValue?.Length ?? 0);

                    if (!addingText && (e.NewTextValue == "-" || e.NewTextValue == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                    {
                        Text = "";
                        _initing = true;
                        entry.Text = "";
                        return;
                    }

                    if (!String.IsNullOrEmpty(e.NewTextValue))
                    {
                        if (!Double.TryParse(e.NewTextValue, out double _))
                        {
                            Text = e.OldTextValue;
                            return;
                        }
                    }

                    Text = e.NewTextValue;
                }
                finally
                {
                    _initing = false;
                }
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            SetIsValid();
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            SetIsValid();
        }

        private void SetIsValid()
        {
            var validation = entry.Behaviors[0] as MultiValidationBehavior;
            validation?.ForceValidate();
            IsValid = validation?.IsValid ?? true;
            this.error.Text = IsValid ? "" : String.Join(',', validation?.Errors ?? new List<object>());
            this.error.IsVisible = !IsValid;
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
                        if (entry.Behaviors[0] is MultiValidationBehavior validation)
                        {
                            var charBehavior = new TextValidationBehavior { MinimumLength = 1 };
                            MultiValidationBehavior.SetError(charBehavior, AppResources.RequiredMissing);
                            validation.Children.Add(charBehavior);
                        }

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
                    if (!String.IsNullOrEmpty(DefaultValue) && Double.TryParse(DefaultValue, out double val))
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
