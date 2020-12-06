using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAPI_UI
{
    public class Service
    {
        private bool _loggedIn = false;
        public event Action OnChange;
        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
