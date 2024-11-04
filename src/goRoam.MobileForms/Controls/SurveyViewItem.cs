using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using goRoam.MobileForms.Controls.Questions;
using goRoam.MobileForms.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls
{
    public class SurveyViewItem : ScrollView
    {
        public SurveyViewItem()
        {
        }

        public static readonly BindableProperty PageProperty =
            BindableProperty.Create(nameof(Page), typeof(MobileFormPage), typeof(SurveyViewItem), null, BindingMode.TwoWay, propertyChanged: OnPagePropertyChanged);

        public MobileFormPage Page
        {
            get => (MobileFormPage)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        static void OnPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as SurveyViewItem)?.SetupPage(newValue as MobileFormPage);
        }

        void SetupPage(MobileFormPage page)
        {
            BatchBegin();

            var pageControls = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(4, 10),
                Spacing = 0
            };

            this.Content = pageControls;

            foreach (var question in page.Questions)
            {
                switch (question.QuestionType)
                {
                    case QuestionTypes.Checkbox:
                        var checkbox = new SurveyCheckbox();
                        checkbox.SetBinding(SurveyCheckbox.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        checkbox.SetBinding(SurveyCheckbox.TextProperty, nameof(MobileFormQuestion.Value));
                        checkbox.SetBinding(SurveyCheckbox.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        checkbox.SetBinding(SurveyCheckbox.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        checkbox.SetBinding(SurveyCheckbox.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        checkbox.SetBinding(SurveyCheckbox.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        checkbox.SetBinding(SurveyCheckbox.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        checkbox.BindingContext = question;
                        checkbox.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(checkbox);
                        break;
                    case QuestionTypes.Picker:
                        var picker = new SurveyPicker();
                        picker.SetBinding(SurveyPicker.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        picker.SetBinding(SurveyPicker.TextProperty, nameof(MobileFormQuestion.Value));
                        picker.SetBinding(SurveyPicker.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        picker.SetBinding(SurveyPicker.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        picker.SetBinding(SurveyPicker.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        picker.SetBinding(SurveyPicker.ItemsProperty, nameof(MobileFormQuestion.Options));
                        picker.SetBinding(SurveyPicker.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        picker.SetBinding(SurveyPicker.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        picker.SetBinding(SurveyPicker.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        picker.SetBinding(SurveyPicker.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        picker.BindingContext = question;
                        picker.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(picker);
                        break;
                    case QuestionTypes.Text:
                        var entry = new Textbox();
                        entry.SetBinding(Textbox.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        entry.SetBinding(Textbox.TextProperty, nameof(MobileFormQuestion.Value));
                        entry.SetBinding(Textbox.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        entry.SetBinding(Textbox.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        entry.SetBinding(Textbox.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        entry.SetBinding(Textbox.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        entry.SetBinding(Textbox.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        entry.SetBinding(Textbox.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        entry.SetBinding(Textbox.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        entry.BindingContext = question;
                        entry.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(entry);
                        break;
                    case QuestionTypes.Email:
                        var emailEntry = new EmailTextbox();
                        emailEntry.SetBinding(EmailTextbox.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        emailEntry.SetBinding(EmailTextbox.TextProperty, nameof(MobileFormQuestion.Value));
                        emailEntry.SetBinding(EmailTextbox.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        emailEntry.SetBinding(EmailTextbox.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        emailEntry.SetBinding(EmailTextbox.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        emailEntry.SetBinding(EmailTextbox.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        emailEntry.SetBinding(EmailTextbox.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        emailEntry.SetBinding(EmailTextbox.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        emailEntry.SetBinding(EmailTextbox.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        emailEntry.BindingContext = question;
                        emailEntry.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(emailEntry);
                        break;
                    case QuestionTypes.Website:
                        var urlEntry = new UrlTextbox();
                        urlEntry.SetBinding(UrlTextbox.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        urlEntry.SetBinding(UrlTextbox.TextProperty, nameof(MobileFormQuestion.Value));
                        urlEntry.SetBinding(UrlTextbox.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        urlEntry.SetBinding(UrlTextbox.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        urlEntry.SetBinding(UrlTextbox.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        urlEntry.SetBinding(UrlTextbox.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        urlEntry.SetBinding(UrlTextbox.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        urlEntry.SetBinding(UrlTextbox.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        urlEntry.SetBinding(UrlTextbox.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        urlEntry.BindingContext = question;
                        urlEntry.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(urlEntry);
                        break;
                    case QuestionTypes.PhoneNumber:
                        var phoneEntry = new PhoneTextbox();
                        phoneEntry.SetBinding(PhoneTextbox.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        phoneEntry.SetBinding(PhoneTextbox.TextProperty, nameof(MobileFormQuestion.Value));
                        phoneEntry.SetBinding(PhoneTextbox.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        phoneEntry.SetBinding(PhoneTextbox.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        phoneEntry.SetBinding(PhoneTextbox.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        phoneEntry.SetBinding(PhoneTextbox.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        phoneEntry.SetBinding(PhoneTextbox.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        phoneEntry.SetBinding(PhoneTextbox.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        phoneEntry.SetBinding(PhoneTextbox.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        phoneEntry.BindingContext = question;
                        phoneEntry.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(phoneEntry);
                        break;
                    case QuestionTypes.Number:
                        var numericEntry = new NumericTextbox();
                        numericEntry.SetBinding(NumericTextbox.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        numericEntry.SetBinding(NumericTextbox.TextProperty, nameof(MobileFormQuestion.Value));
                        numericEntry.SetBinding(NumericTextbox.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        numericEntry.SetBinding(NumericTextbox.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        numericEntry.SetBinding(NumericTextbox.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        numericEntry.SetBinding(NumericTextbox.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        numericEntry.SetBinding(NumericTextbox.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        numericEntry.SetBinding(NumericTextbox.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        numericEntry.SetBinding(NumericTextbox.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        numericEntry.SetBinding(NumericTextbox.MinValueProperty, nameof(MobileFormQuestion.MinValue));
                        numericEntry.SetBinding(NumericTextbox.MaxValueProperty, nameof(MobileFormQuestion.MaxValue));
                        numericEntry.BindingContext = question;
                        numericEntry.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(numericEntry);
                        break;
                    case QuestionTypes.Picture:
                        var picture = new SurveyPicture();
                        picture.SetBinding(SurveyPicture.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        picture.SetBinding(SurveyPicture.TextProperty, nameof(MobileFormQuestion.Value));
                        picture.SetBinding(SurveyPicture.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        picture.SetBinding(SurveyPicture.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        picture.SetBinding(SurveyPicture.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        picture.SetBinding(SurveyPicture.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        picture.SetBinding(SurveyPicture.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        picture.SetBinding(SurveyPicture.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        picture.BindingContext = question;
                        picture.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(picture);
                        break;
                    case QuestionTypes.Signature:
                        var signature = new SurveySignature();
                        signature.SetBinding(SurveySignature.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        signature.SetBinding(SurveySignature.TextProperty, nameof(MobileFormQuestion.Value));
                        signature.SetBinding(SurveySignature.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        signature.SetBinding(SurveySignature.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        signature.SetBinding(SurveySignature.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        signature.SetBinding(SurveySignature.IsRequiredProperty, nameof(MobileFormQuestion.IsRequired));
                        signature.SetBinding(SurveySignature.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        signature.SetBinding(SurveySignature.IsReadOnlyProperty, nameof(MobileFormQuestion.IsReadOnly));
                        signature.BindingContext = question;
                        signature.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(signature);
                        break;
                    case QuestionTypes.Label:
                        var label = new SurveyLabel();
                        label.SetBinding(SurveyLabel.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        label.SetBinding(SurveyLabel.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        label.SetBinding(SurveyLabel.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        label.BindingContext = question;
                        label.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(label);
                        break;
                    case QuestionTypes.DateTime:
                        var dateTime = new SurveyDateTime();
                        dateTime.SetBinding(SurveyDateTime.TitleProperty, nameof(MobileFormQuestion.Prompt));
                        dateTime.SetBinding(SurveyDateTime.TextProperty, nameof(MobileFormQuestion.Value));
                        dateTime.SetBinding(SurveyDateTime.HelperTextProperty, nameof(MobileFormQuestion.HelperText));
                        dateTime.SetBinding(SurveyDateTime.QuestionIdProperty, nameof(MobileFormQuestion.ID));
                        dateTime.SetBinding(SurveyDateTime.ReferenceNumberProperty, nameof(MobileFormQuestion.ReferenceNumber));
                        dateTime.SetBinding(SurveyDateTime.IsValidProperty, nameof(MobileFormQuestion.IsValid));
                        dateTime.SetBinding(SurveyDateTime.DefaultValueProperty, nameof(MobileFormQuestion.DefaultValue));
                        dateTime.BindingContext = question;
                        dateTime.VerticalOptions = LayoutOptions.Start;
                        pageControls.Children.Add(dateTime);
                        break;
                }
            }

            BatchCommit();
        }
    }
}

