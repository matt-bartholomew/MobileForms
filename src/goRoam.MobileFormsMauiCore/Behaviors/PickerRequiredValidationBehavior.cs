using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace goRoam.MobileForms.Behaviors
{
    public class PickerRequiredValidationBehavior : ValidationBehavior
    {
        public PickerRequiredValidationBehavior()
        {
        }

        protected override string DefaultValuePropertyName => Picker.SelectedIndexProperty.PropertyName;

        protected override ValueTask<bool> ValidateAsync(object value, CancellationToken token)
        {
            var index = (int)(value ?? -1);
            return new ValueTask<bool>(index >= 0);
        }

        //protected override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnViewPropertyChanged(sender, e);
        //}

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}
