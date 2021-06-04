using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Third
{
    public class Bus : INotifyPropertyChanged
    {
        private ObservableCollection<Passanger> 
            _sitting = new ObservableCollection<Passanger>(), 
            _standing = new ObservableCollection<Passanger>();
        private string _left, _come;
        private Random r = new Random();
        public string Total => (Sitting.Count + Standing.Count).ToString(); 
        public string Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged(nameof(Left));
                OnPropertyChanged(nameof(Total));
            }
        }
        public string Come
        {
            get => _come;
            set
            {
                _come = value;
                OnPropertyChanged(nameof(Come));
                OnPropertyChanged(nameof(Total));
            }
        }
        public ObservableCollection<Passanger> Sitting
        {
            get => _sitting;
            set
            {
                _sitting = value;
                OnPropertyChanged(nameof(Sitting));
            }
        }
        public ObservableCollection<Passanger> Standing
        {
            get => _standing;
            set
            {
                _standing = value;
                OnPropertyChanged(nameof(Standing));
            }
        }
        public void TryToSeat(Passanger p)
        {
            if (Sitting.Count < 24) Sitting.Add(p);
            else if (Standing.Count < 12) Standing.Add(p);
        }
        public void BusStop()
        {
            Come = "0";
            int cnt = 0, 
                cnt2 = 0,
                to_left = r.Next(1, 5), 
                to_come = r.Next(2, 7);
            
            while (Sitting.Count < 24 && Sitting.Count > 0 && Standing.Count > 0 && cnt2 < to_left)
            {
                Sitting.RemoveAt(0);
                Left = cnt2++.ToString();
                return;
            }
            if(Standing.Count + to_come <= 12)
            {
                for (int i = 0; i < to_come; i++) Standing.Add(new Passanger());
                Come = to_come.ToString();
                return;
            }
            if (Sitting.Count < 24) {
                int left = 0;
                while(Sitting.Count < 24 && Standing.Count > 0)
                {
                    Sitting.Add(Standing[0]);
                    Standing.RemoveAt(0);
                    left++;
                }
                Left = left.ToString();
                return;
            }
            while (Sitting.Count > 0 && Standing.Count > 0 && cnt < to_left)
            {
                Sitting.RemoveAt(0);
                Sitting.Add(Standing[0]);
                Standing.RemoveAt(0);
                Left = cnt++.ToString();
            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
