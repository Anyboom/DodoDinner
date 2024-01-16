using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DodoDinner
{
    public class Dinner : INotifyPropertyChanged
    {

        private int _Id;
        public int Id
        {
            get {
                return _Id; 
            }
            set {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }

        private DateTime _StartAt;
        public DateTime StartAt
        {
            get
            {
                return _StartAt;
            }
            set
            {
                _StartAt = value;
                OnPropertyChanged("StartAt");
            }
        }

        private DateTime? _EndAt;
        public DateTime? EndAt
        {
            get
            {
                return _EndAt;
            }
            set
            {
                _EndAt = value;
                OnPropertyChanged("EndAt");
            }
        }

        public int Difference
        {
            get
            {
                return (EndAt.HasValue) ? Convert.ToInt32(EndAt.Value.Subtract(StartAt).TotalMinutes) : 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
