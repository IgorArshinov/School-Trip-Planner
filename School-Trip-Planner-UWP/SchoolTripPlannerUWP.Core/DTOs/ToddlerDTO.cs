using GalaSoft.MvvmLight;
using System;
using Windows.UI.Xaml.Media.Imaging;

namespace SchoolTripPlannerUWP.Core.DTOs
{
    public class ToddlerDTO : ObservableObject, ICloneable
    {
        private string _surname;
        private string _name;
        private ClassDTO _class;
        private long _id;
        private string _classId;
        private WriteableBitmap _qrCodeImageSource;

        public long Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public ClassDTO Class
        {
            get => _class;
            set => Set(ref _class, value);
        }

        public string ClassId
        {
            get => _classId;
            set => Set(ref _classId, value);
        }

        public string QrCode => Surname + Name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("QrCode");
                }
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("QrCode");
                }
            }
        }

        public WriteableBitmap QrCodeImageSource
        {
            get => _qrCodeImageSource;
            set => Set(ref _qrCodeImageSource, value);
        }

        public string FullName => Surname + " " + Name;

        public object Clone()
        {
            var toddler = (ToddlerDTO) MemberwiseClone();
            toddler.Class = (ClassDTO) Class.Clone();

            return toddler;
        }
    }
}