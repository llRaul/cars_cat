using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace cars
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private List<string> _manufacturers;
        public List<string> Manufacturers
        {
            get { return _manufacturers; }
            set
            {
                _manufacturers = value;
                OnPropertyChanged("Manufacturers");
            }
        }
        private List<string> _models;
        public List<string> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                OnPropertyChanged("Models");
            }
        }
        private ImageSource _photo;
        public ImageSource Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
            }
        }
        private string _carInfo;
        public string CarInfo
        {
            get { return _carInfo; }
            set
            {
                _carInfo = value;
                OnPropertyChanged("CarInfo");
            }
        }
        private string _selectedManufacturer;
        public string SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {
                _selectedManufacturer = value;
                Models = new List<string>(SQLManager.GetStingListByQuery($"select model from Cars where manufacturer = '{value}'"));
            }
        }
        private string _selectedModel;
        public string SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                  if (value != null)
                {
                    CarInfo = SQLManager.GetStingListByQuery($"select top 1 info from Cars where model = '{value}'")[0];
                    try
                    {
                        Photo = null;
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.CacheOption = BitmapCacheOption.OnLoad;
                        img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        img.UriSource = new Uri($"Images/{value}.jpg", UriKind.Relative);
                        img.EndInit();
                        Photo = img;
                    }
                    catch
                    {
                        MessageBox.Show("Изображения нет");
                    }
                }
            }
        }
        private SQL SQLManager = new SQL();

        public ViewModel()
        {
            Manufacturers = new List<string>(SQLManager.GetStingListByQuery("select distinct manufacturer from Cars"));
        }

        public event PropertyChangedEventHandler PropertyChanged; 
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
