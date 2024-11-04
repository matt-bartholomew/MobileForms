using System;
using System.Collections.Generic;
using System.Threading;
using goRoam.MobileForms.Models;
using goRoam.MobileForms.Resources;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls
{
    public partial class FormViewer : Grid
    {
        Dictionary<int, SurveyViewItem> formPages = new Dictionary<int, SurveyViewItem>();

        int _currentPageIndex = 0;

        public FormViewer()
        {
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
            Thread.Sleep(250);
            InitializeComponent();
        }

        void NextPageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (_currentPageIndex == (Survey.Pages.Count - 1))
                return;

            _currentPageIndex++;

            BuildUpPage(_currentPageIndex);
        }

        private void BuildUpPage(int currentPageIndex)
        {
            if (!formPages.TryGetValue(currentPageIndex, out SurveyViewItem newPage))
            {
                newPage = new SurveyViewItem { Page = Survey.Pages[currentPageIndex] };
                formPages.Add(currentPageIndex, newPage);
            }

            this.pageControls.Content = newPage;
            this.pageTitle.Text = Survey.Pages[currentPageIndex].Name;

            previousButton.IsEnabled = currentPageIndex > 0;
            (previousButton.Source as FontImageSource).Color = previousButton.IsEnabled ? Color.DimGray : Color.LightGray;
            nextButton.IsEnabled = currentPageIndex < (Survey.Pages.Count - 1);
            (nextButton.Source as FontImageSource).Color = nextButton.IsEnabled ? Color.DimGray : Color.LightGray;

            this.ForceLayout();
        }

        void PreviousPageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (_currentPageIndex > 0)
                _currentPageIndex--;

            BuildUpPage(_currentPageIndex);
        }

        public MobileForm Survey { get; set; }

        public static readonly BindableProperty SurveySourceProperty =
            BindableProperty.Create(nameof(SurveySource), typeof(MobileForm), typeof(FormViewer), null,
                propertyChanged: OnSurveySourceChanged);

        public MobileForm SurveySource
        {
            get => (MobileForm)GetValue(SurveySourceProperty);
            set => SetValue(SurveySourceProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
            BindableProperty.Create(nameof(Culture), typeof(string), typeof(FormViewer), "en-US", propertyChanged: OnCulturePropertyChanged);

        public string Culture
        {
            get => (string)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        static void OnSurveySourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is MobileForm survey)
                (bindable as FormViewer)?.UpdateSurveySource(survey);
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

            BuildUpPage(_currentPageIndex);
            // I think this is where we transform the pages in the survey into a collection of surveyviewitems so that the tabview can render them somehow

            BatchCommit();
        }
    }
}
