using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace Xamarin_Movie.Class
{
    public class NetworkState
    {
        public NetworkState()
        {
            bool state = false;
            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Cellular) || profiles.Contains(ConnectionProfile.Ethernet))
            {
                state = true;
            }
            Constantes.IsConnect = state;

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            new NetworkState();
        }
    }
}
