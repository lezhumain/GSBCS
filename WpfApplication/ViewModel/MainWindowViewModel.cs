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
using Model;

namespace WpfApplication.ViewModel
{
    public class MainWindowViewModel : MainViewModel
    {
        private readonly IServiceClient serviceClient;
        
        /* Commandes */
        private DelegateCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new DelegateCommand(Exit);

                return exitCommand;
            }
        }
        
        private DelegateCommand testCommand;
        public ICommand TestCommand
        {
            get
            {
                if (testCommand == null)
                    testCommand = new DelegateCommand(Test);

                return testCommand;
            }
        }

        /*
        private DelegateCommand voirPratCommand;
        public ICommand VoirPratCommand
        {
            get
            {
                if (voirPratCommand == null)
                    voirPratCommand = new DelegateCommand(VoirPrat);

                return voirPratCommand;
            }
        }
         */
        /*************/

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

        /* Entites */
        private List<COLLABORATEUR> listeCol;
        public List<COLLABORATEUR> ListeCol
        {
            get { return listeCol; }
            set { NotifyPropertyChanged(ref listeCol, value); }
        }
        /*
        private List<RAPPORT_DE_VISITE> listeRap;
        public List<RAPPORT_DE_VISITE> ListeRap
        {
            get { return listeRap; }
            set { NotifyPropertyChanged(ref listeRap, value); }
        }

        private List<VISITEUR> listeVis;
        public List<VISITEUR> ListeVis
        {
            get { return listeVis; }
            set { NotifyPropertyChanged(ref listeVis, value); }
        }

        private List<PRATICIEN> listePra;
        public List<PRATICIEN> ListePra
        {
            get { return listePra; }
            set { NotifyPropertyChanged(ref listePra, value); }
        }
         */
        /***********/

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
            ListeCol = ColHelper.Current.GetList().ToList<COLLABORATEUR>();
            /*
            ListeVis = null;
            ListeRap = null;
            ListePra = null;
             */

            Prenom = client.Prenom;
            Age = client.Age;
            if (client.EstBonClient)
                BonClient = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            else
                BonClient = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void Test()
        {
            COLLABORATEUR c = listeCol[0];

            MessageBox.Show(c.ToString());
        }

        /*
        private void VoirPrat()
        {

        }
         */
    }
}