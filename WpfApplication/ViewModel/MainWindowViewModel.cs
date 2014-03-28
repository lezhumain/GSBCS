using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model.helpers;
using WpfApplication.ViewModel;
using WpfApplication.Model;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using WpfApplication.Helpers;
using System.Windows.Input;

namespace WpfApplication.ViewModel
{
    public class MainWindowViewModel : MainViewModel
    {
        private readonly IServiceClient serviceClient;
        private DelegateCommand exitCommand;

        private string prenom;
        public string Prenom
        {
            get { return prenom; }
            set { NotifyPropertyChanged(ref prenom, value); }
        }


        private int age;
        public int Age
        {
            get { return age; }
            set { NotifyPropertyChanged(ref age, value); }
        }

        private List<Client> listeClients;
        public List<Client> ListeClients
        {
            get { return listeClients; }
            set { NotifyPropertyChanged(ref listeClients, value); }
        }

        private SolidColorBrush bonClient;
        public SolidColorBrush BonClient
        {
            get { return bonClient; }
            set { NotifyPropertyChanged(ref bonClient, value); }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public MainWindowViewModel(IServiceClient service)
        {
            serviceClient = service;

            Client client = serviceClient.Charger();
            ListeClients = service.ChargerTout();

            Prenom = client.Prenom;
            Age = client.Age;
            if (client.EstBonClient)
                BonClient = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            else
                BonClient = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new DelegateCommand(Exit);

                return exitCommand;
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}