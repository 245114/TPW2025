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
        public ICommand StopCommand { get; set; }
        private Model.Model _model;

        public int CanvasWidth { get; private set; } = 1300;
        public int CanvasHeight { get; private set; } = 800;

        public ViewModel()
        {
            _model = new Model.Model();
            StartCommand = new Commands(Start);
            StopCommand = new Commands(Stop);
            _model.SetCanvasSize(CanvasWidth, CanvasHeight);
            
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

        private void Stop()
        {
            _model.StopBalls();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}