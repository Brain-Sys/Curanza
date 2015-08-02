using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainSys.UWP.Curanza.SampleApp.Models;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class FeaturesViewModel : ApplicationViewModelBase
    {
        private ObservableCollection<Feature> feature;
        public ObservableCollection<Feature> Features
        {
            get { return feature; }
            set { feature = value; base.RaisePropertyChanged(); }
        }

        private Feature selectedFeature;
        public Feature SelectedFeature
        {
            get { return selectedFeature; }
            set { selectedFeature = value; base.RaisePropertyChanged(); }
        }

        public RelayCommand<string> LoadedCommand { get; set; }

        public FeaturesViewModel()
        {
            this.Features = new ObservableCollection<Feature>();
            this.Features.Add(new Feature("SWIPE", "Swipe"));
            this.Features.Add(new Feature("WHEEL", "Wheel"));
            this.Features.Add(new Feature("HOLD", "Hold"));
            this.Features.Add(new Feature("TEXTBOXES", "Text Boxes"));
            this.Features.Add(new Feature("DRAGDROP", "Drag'n'Drop"));
            this.Features.Add(new Feature("ITEMCLICK", "ItemClick in GridView/ListView"));
            this.Features.Add(new Feature("DOUBLETAPPED", "Double Tapped"));

            this.LoadedCommand = new RelayCommand<string>(loadedCommandExecute);
        }

        private void loadedCommandExecute(string parameter)
        {
            int value = Int32.Parse(parameter);
            this.SelectedFeature = this.Features[value];
        }
    }
}