using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using goRoam.MobileForms.Controls.Questions;
using goRoam.MobileForms.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.ImageSources;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel.Communication;

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
                }
            }

            BatchCommit();
        }
    }
}

