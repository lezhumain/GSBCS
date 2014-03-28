using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApplication.Model;

namespace WpfApplication.ViewModel
{
    public class ListWindowViewModel : MainViewModel
    {
        private List<Client> listeClients;
        public List<Client> ListeClients
        {
            get { return listeClients; }
            set { NotifyPropertyChanged(ref listeClients, value); }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public ICommand QuiSuisJeCommand { get; set; }

        public ListWindowViewModel(IServiceClient service)
        {
            ListeClients = service.ChargerTout();

            QuiSuisJeCommand = new RelayCommand<Client>(QuiSuisJe);
        }

        private void QuiSuisJe(Client client)
        {
            MessageBox.Show("Je suis " + client.Prenom + " pélo.");
        }
    }
}
