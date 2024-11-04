using System;
using System.Collections.Generic;
using goRoam.MobileForms.Models;
using goRoam.MobileForms.Resources;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls
{
#if false
    public class SurveyView : View
    {
        public SurveyView() : base()
        {
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
        }

        public MobileForm Survey { get; set; }

        public static readonly BindableProperty SurveySourceProperty =
            BindableProperty.Create(nameof(SurveySource), typeof(MobileForm), typeof(SurveyView), null,
                propertyChanged: OnSurveySourceChanged);

        public MobileForm SurveySource
        {
            get => (MobileForm)GetValue(SurveySourceProperty);
            set => SetValue(SurveySourceProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
            BindableProperty.Create(nameof(Culture), typeof(string), typeof(SurveyView), "en-US", propertyChanged: OnCulturePropertyChanged);

        public string Culture
        {
            get => (string)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        static void OnSurveySourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is MobileForm survey)
                (bindable as SurveyView)?.UpdateSurveySource(survey);
        }

        private static void OnCulturePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newCulture = (newValue ?? "").ToString();

            if (!String.IsNullOrEmpty(newCulture))
                AppResources.Culture = System.Globalization.CultureInfo.GetCultureInfo(newCulture);
        }

        private void UpdateSurveySource(MobileForm survey)
        {
            BatchBegin();

            Survey = survey;
            // I think this is where we transform the pages in the survey into a collection of surveyviewitems so that the tabview can render them somehow
            this.
            this.TabItems.Clear();

            foreach (var item in survey.Pages)
            {
                this.TabItems.Add(new SurveyViewItem { Page = item });
            }

            BatchCommit();
        }
    }
#endif
}

