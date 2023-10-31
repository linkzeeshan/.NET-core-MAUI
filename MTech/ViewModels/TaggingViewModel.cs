using MTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MTech.ViewModels
{
    public partial class TaggingViewModel : INotifyPropertyChanged
    {
        private TaggingModel myTaggingModel = new TaggingModel();

        public TaggingModel MyTaggingModel
        {
            get { return myTaggingModel; }
            set 
            {
                myTaggingModel = value;
                OnPropertyChanged(nameof(myTaggingModel));
            }
        }
        public ICommand NextCommand { get; }
        public ICommand FinishCommand { get; }

        public ICommand CloseCommand { get; }
        
        public TaggingViewModel()
        {
            NextCommand = new Command(PerformNextAction);
            FinishCommand = new Command(PerformFinishAction);
            CloseCommand = new Command(PerformCloseAction);
        }

        private void PerformCloseAction()
        {
            Shell.Current.DisplayAlert("Close", "Closing Message", "OK");
        }

        private void PerformFinishAction()
        {
            Shell.Current.DisplayAlert("Finish", "Finish Message", "OK");

        }

        private void PerformNextAction()
        {
            Shell.Current.DisplayAlert("Next", "Next Message", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
