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
using DodoDinner;

namespace DodoDinnerLibrary
{
    public class Dinner : BindableBase
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;

                OnPropertyChanged("Id");
            }
        }

        private DateTime _startedAt;
        public DateTime StartedAt
        {
            get
            {
                return _startedAt;
            }
            set
            {
                _startedAt = value;

                OnPropertyChanged(nameof(StartedAt));
            }
        }

        private DateTime? _closedAt;
        public DateTime? ClosedAt
        {
            get
            {
                return _closedAt;
            }
            set
            {
                _closedAt = value;

                OnPropertyChanged(nameof(ClosedAt));
                OnPropertyChanged(nameof(Difference));
            }
        }

        public DateTime Difference
        {
            get
            {
                return (ClosedAt.HasValue) ? new DateTime(ClosedAt.Value.Subtract(StartedAt).Ticks) : new DateTime(DateTime.Now.Subtract(StartedAt).Ticks);
            }
        }
    }
}
