using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace DodoDinnerLibrary
{
    public class Dinner : INotifyPropertyChanged
    {
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
                OnPropertyChanged("Difference");
                OnPropertyChanged("ClosedLate");
            }
        }

        public DateTime Difference
        {
            get
            {
                return (EndAt.HasValue) ? new DateTime(EndAt.Value.Subtract(StartAt).Ticks) : new DateTime(DateTime.Now.Subtract(StartAt).Ticks);
            }
        }

        public bool ClosedLate
        {
            get
            {
                return TimeSpan.FromTicks(Difference.Ticks).TotalHours > 4;
            }
        }

        public Dinner()
        {
            if(EndAt.HasValue == false)
            {
                DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render)
                {
                    Interval = TimeSpan.FromMilliseconds(1),
                };

                timer.Tick += (s, e) =>
                {
                    OnPropertyChanged("Difference");
                    OnPropertyChanged("ClosedLate");
                };

                timer.Start();
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
