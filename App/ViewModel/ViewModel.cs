using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using Data;
using Logic;
using Model;
using System.Windows;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand StartCommand { get; set; }
        private Model.Model _model;

        public int CanvasWidth { get; } = 800;
        public int CanvasHeight { get; } = 450;

        public ViewModel()
        {
            _model = new Model.Model();
            StartCommand = new Commands(Start);
            CanvasHeight = _model.canvasHeight;
            CanvasWidth = _model.canvasWidth;
            
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Ball> Balls => _model.Balls;

        private void Start()
        {
            _model.DrawBalls(Count);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}