using System;
using Xamarin.Forms;

namespace RollAndKeep.Behavior
{
    public class PositiveBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            int intValue;
            IsValid = Int32.TryParse(e.NewTextValue, out intValue) && intValue >= 0;
            var entry = sender as Entry;
            if (entry != null)
            {
                if (IsValid)
                {
                    entry.TextColor = Color.Default;
                }
                else
                {
                    entry.TextColor = Color.Red;
                }
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
