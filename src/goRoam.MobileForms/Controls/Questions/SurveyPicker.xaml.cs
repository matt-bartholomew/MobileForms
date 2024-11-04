using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using goRoam.MobileForms.Behaviors;
using goRoam.MobileForms.Resources;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class SurveyPicker : Xamarin.Forms.Grid
    {
        private bool _initing = true;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SurveyPicker), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(SurveyPicker), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SurveyPicker), default(string), BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(SurveyPicker), default(string), BindingMode.OneTime);
        public string DefaultValue
        {
            get => (string)GetValue(DefaultValueProperty);
            set => SetValue(DefaultValueProperty, value);
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(SurveyPicker), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(SurveyPicker), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<string>), typeof(SurveyPicker), default(string), BindingMode.OneTime);
        public List<string> Items
        {
            get => (List<string>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(SurveyPicker), false, BindingMode.OneTime);
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(SurveyPicker), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(SurveyPicker), false, BindingMode.OneTime);
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public SurveyPicker()
        {
            InitializeComponent();

            label.Text = Title;
            picker.Title = HelperText;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

            SetIsValid();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _initing = false;
            Text = picker.SelectedItem?.ToString();

            SetIsValid();
            _initing = true;
        }

        void SetIsValid()
        {
            var behavior = picker.Behaviors?.FirstOrDefault(b => b is MultiValidationBehavior) as MultiValidationBehavior;
            behavior?.ForceValidate();
            IsValid = behavior?.IsValid ?? true;
            this.error.Text = IsValid ? "" : AppResources.RequiredMissing;
            this.error.IsVisible = !IsValid;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                this.label.Text = Title;
                return;
            }

            if (propertyName == HelperTextProperty.PropertyName)
            {
                picker.Title = HelperText;
                return;
            }

            if (propertyName == ItemsProperty.PropertyName)
            {
                picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;

                picker.Items.Clear();

                Items.ForEach(i =>
                {
                    picker.Items.Add(i);
                });

                picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

                if (Items.Count > 1)
                {
                    if (!String.IsNullOrEmpty(Text))
                    {
                        if (picker.Items.Contains(Text, StringComparer.Create(CultureInfo.CurrentCulture, true)))
                        {
                            picker.SelectedIndex = picker.Items.IndexOf(Text);
                            SetIsValid();
                        }
                    }

                    return;
                }

                picker.SelectedIndex = 0;

                return;
            }

            if (propertyName == IsRequiredProperty.PropertyName)
            {
                if (IsRequired)
                {
                    if (picker.Behaviors[0] is MultiValidationBehavior validation)
                    {
                        var charBehavior = new PickerRequiredValidationBehavior
                        {
                            ValuePropertyName = Picker.SelectedIndexProperty.PropertyName
                        };
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
                picker.IsEnabled = !IsReadOnly;
                return;
            }

            if (propertyName == DefaultValueProperty.PropertyName)
            {
                if (!String.IsNullOrEmpty(DefaultValue))
                {
                    if (picker.Items.Contains(DefaultValue, StringComparer.Create(CultureInfo.CurrentCulture, true)))
                    {
                        picker.SelectedIndex = picker.Items.IndexOf(DefaultValue);
                        SetIsValid();
                    }
                }

                return;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                if (_initing && !String.IsNullOrEmpty(Text))
                {
                    if (picker.Items.Contains(Text, StringComparer.Create(CultureInfo.CurrentCulture, true)))
                    {
                        picker.SelectedIndex = picker.Items.IndexOf(Text);
                        SetIsValid();
                    }
                }

                return;
            }
        }
    }
}
