using RollAndKeep.Business;
using RollAndKeep.Models.Dto;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace RollAndKeep
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<RkConfigurationDto> oldConfigurations = new ObservableCollection<RkConfigurationDto>();
        public MainPage()
        {
            InitializeComponent();
            oldConfigurationList.ItemsSource = oldConfigurations;
        }

        private void OnRoll(object sender, EventArgs e)
        {
            var roll = Convert.ToInt32(nbOfRoll.Text);
            var keep = Convert.ToInt32(nbOfKeep.Text);
            var bonus = Convert.ToInt32(addToResult.Text);
            var rkLogic = new RkLogic(roll, keep, bonus);
            var result = rkLogic.RollAThrow();
            resultLabel.Text = result.ToString();
            AddToTopOfTheList(rkLogic.Configuration);
        }

        private void AddToTopOfTheList(RkConfigurationDto configuration)
        {
            if (!oldConfigurations.Any(x => x.Equals(configuration)))
            {
                //We keep only the ten last throws
                if (oldConfigurations.Count >= 10)
                {
                    foreach (var exceedingIndex in Enumerable.Range(10, oldConfigurations.Count()))
                    {
                        oldConfigurations.RemoveAt(exceedingIndex);
                    }
                }
                //Add it on the top of the list
                oldConfigurations.Insert(0, configuration);
                oldConfigurationList.SelectedItem = configuration;
            }
        }

        private void oldConfigurationList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var configuration = e.Item as RkConfigurationDto;
            if (configuration != null)
            {
                nbOfRoll.Text = configuration.OriginalRoll.ToString();
                nbOfKeep.Text = configuration.OriginalKeep.ToString();
                addToResult.Text = configuration.OriginalAddToResult.ToString();
                var rkLogic = new RkLogic(configuration);
                var result = rkLogic.RollAThrow();
                resultLabel.Text = result.ToString();
            }
        }
    }
}
