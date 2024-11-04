using System;
using System.Collections.Generic;
using System.IO;
using goRoam.MobileForms.Resources;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class SurveyPicture : Grid
    {
        bool _initing = true;

        private byte[] _signatureBytes;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SurveyPicture), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(SurveyPicture), default(string), Xamarin.Forms.BindingMode.OneTime);
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SurveyPicture), default(string), BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty QuestionIdProperty = BindableProperty.Create(nameof(QuestionId), typeof(string), typeof(SurveyPicture), default(string), BindingMode.OneTime);
        public string QuestionId
        {
            get => (string)GetValue(QuestionIdProperty);
            set => SetValue(QuestionIdProperty, value);
        }

        public static readonly BindableProperty ReferenceNumberProperty = BindableProperty.Create(nameof(ReferenceNumber), typeof(string), typeof(SurveyPicture), default(string), BindingMode.OneTime);
        public string ReferenceNumber
        {
            get => (string)GetValue(ReferenceNumberProperty);
            set => SetValue(ReferenceNumberProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(SurveyPicture), true, BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(SurveyPicture), default(string), BindingMode.OneTime);
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(SurveyPicture), false, BindingMode.OneTime);
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public SurveyPicture()
        {
            InitializeComponent();

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() =>
                {
                    var popup = new PicturePopup();
                    popup.Dismissed += Popup_Dismissed;

                    Application.Current.MainPage.Navigation.ShowPopup(popup);
                })
            });

            label.Text = Title;
        }

        private void Popup_Dismissed(object sender, PopupDismissedEventArgs e)
        {
            _initing = false;
            var result = e.Result as byte[];

            if (result == null)
            {
                _signatureBytes = new byte[] { };
                this.picture.Source = ImageSource.FromStream(() => new MemoryStream(_signatureBytes));
                Text = Convert.ToBase64String(_signatureBytes);
                IsValid = IsRequired ? false : true;
                return;
            }

            IsValid = true;

            if (IsRequired)
                IsValid = (result.Length > 0 || (_signatureBytes?.Length ?? 0) > 0);

            if ((_signatureBytes?.Length ?? 0) > 0 && result.Length == 0)
                // don't overwrite what was already taken
                return;

            _signatureBytes = result;

            this.picture.Source = ImageSource.FromStream(() => new MemoryStream(_signatureBytes));
            Text = Convert.ToBase64String(_signatureBytes);
            _initing = true;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                this.label.Text = Title;
                return;
            }

            if (propertyName == IsRequiredProperty.PropertyName)
            {
                this.required.Text = $" ({AppResources.Required.ToLower()})";
                return;
            }

            if (propertyName == IsReadOnlyProperty.PropertyName)
            {
                if (IsReadOnly)
                {
                    this.GestureRecognizers.Clear();
                }

                this.instructions.IsVisible = !IsReadOnly;
                return;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                if (_initing && !String.IsNullOrEmpty(Text))
                {
                    _signatureBytes = Convert.FromBase64String(Text);
                    this.picture.Source = ImageSource.FromStream(() => new MemoryStream(_signatureBytes));
                }
            }
        }
    }
}
