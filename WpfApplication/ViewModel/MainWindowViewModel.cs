using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model.helpers;
using WpfApplication.ViewModel;
using WpfApplication.ViewModel.Trans;
using WpfApplication.Model;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using WpfApplication.Helpers;
using System.Windows.Input;
using Model;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace WpfApplication.ViewModel
{
    public class MainWindowViewModel : MainViewModel
    {
        private readonly IServiceClient serviceClient;
        
        #region commandes

        // attribut binde au clic sur btn exit
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

        // attribut binde au clic sur btn refresh
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

        // lien de commande pour selectionChanged
        public ICommand SelectedItemsCommand { get; set; }
        /*
        private DelegateCommand selectedItemsCommand;
        public ICommand SelectedItemsCommand
        {
            get
            {
                if (selectedItemsCommand == null)
                    selectedItemsCommand = new DelegateCommand(SelectedItems);

                return selectedItemsCommand;
            }
        }
        */

        private DelegateCommand excelCommand;
        public ICommand ExcelCommand
        {
            get
            {
                if (excelCommand == null)
                    excelCommand = new DelegateCommand(Excel);

                return excelCommand;
            }
        }

        #endregion

        private List<ColTrans> listeSel;
        public List<ColTrans> ListeSel
        {
            get { return listeSel; }
            set { NotifyPropertyChanged(ref listeSel, value); }
        }

        #region Entites
        private List<ColTrans> listeCol;
        public List<ColTrans> ListeCol
        {
            get { return listeCol; }
            set { NotifyPropertyChanged(ref listeCol, value); }
        }
        /*
        private List<COLLABORATEUR> listeCole;
        public List<COLLABORATEUR> ListeCole
        {
            get { return listeCole; }
            set { NotifyPropertyChanged(ref listeCole, value); }
        }

        private List<RAPPORT_DE_VISITE> listeRape;
        public List<RAPPORT_DE_VISITE> ListeRape
        {
            get { return listeRape; }
            set { NotifyPropertyChanged(ref listeRape, value); }
        }
        */
        private List<RapTrans> listeRap;
        public List<RapTrans> ListeRap
        {
            get { return listeRap; }
            set { NotifyPropertyChanged(ref listeRap, value); }
        }

        private List<PratTrans> listePrat;
        public List<PratTrans> ListePrat
        {
            get { return listePrat; }
            set { NotifyPropertyChanged(ref listePrat, value); }
        }

        public List<ColTrans> convertCol(List<COLLABORATEUR> list)
        {
            List<ColTrans> l = new List<ColTrans>();
            foreach(COLLABORATEUR c in list)
            {
                l.Add(new ColTrans(c.matricule_col, c.nom_col, c.prenom_col));
            }
            return l;
        }
        #endregion

        public List<RapTrans> convertRap(List<RAPPORT_DE_VISITE> list)
        {
            List<RapTrans> l = new List<RapTrans>();
            foreach (RAPPORT_DE_VISITE c in list)
            {
                PRATICIEN p = PraHelper.Current.getById( (int)c.matricule_praticien );
                l.Add(new RapTrans( c.num_rapport,
                                    p.prenom_praticien + " " + p.nom_praticien,
                                    c.date_rapport.ToString("dd/mm/yyyy") ));
            }    
            return l;
        }

        public List<PratTrans> convertPrat(List<PRATICIEN> list)
        {
            List<PratTrans> l = new List<PratTrans>();
            foreach (PRATICIEN c in list)
            {
                l.Add(new PratTrans(c.matricule_praticien,
                                    c.nom_praticien,
                                    c.prenom_praticien));
            }
            return l;
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        /// <summary>
        /// Constructeur du View-Model
        /// </summary>
        /// <param name="service">???</param>
        public MainWindowViewModel(IServiceClient service)
        {
            serviceClient = service;

            ListeCol = convertCol( ColHelper.Current.GetList() );            
            ListeRap = convertRap( RapHelper.Current.GetList() );
            ListePrat = convertPrat(PraHelper.Current.GetList());
            ListeSel = new List<ColTrans>();

            SelectedItemsCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedItems);
        }

        /// <summary>
        /// Handler pour le clic sur le btn Quit
        /// </summary>
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Handler pour le clic sur le btn Refresh
        /// </summary>
        private void Test()
        {
            string msg = "aucun test";


            MessageBox.Show(msg);
        }

        /// <summary>
        /// Handler pour l'evnmt SelectionChanged
        /// Copie les SelectedItems de la grid dans ListeSel
        /// </summary>
        /// <param name="args">Les arguments de l'evnmt</param>
        private void SelectedItems(SelectionChangedEventArgs args)
        {
            string msg;

            if (args == null)
                MessageBox.Show("args null");
            else
            {
                var lbi = ((args.Source as DataGrid).SelectedItems);
                if (lbi == null || lbi.Count == 0)
                {
                    msg = "listeSel nule ou vide";
                }
                else
                {
                    if (this.listeSel == null)
                        this.listeSel = new List<ColTrans>();
                    else
                        listeSel.Clear();

                    foreach (ColTrans ct in lbi)
                    {
                        listeSel.Add(ct);
                    }
                    msg = "You selected " + lbi.ToString() + ".";   
                }        
                //MessageBox.Show(msg);
            }
        }

        /// <summary>
        /// Formate les informations necessaires dans une
        /// seule string puis la donne a manger a toFic()
        /// qui nous sort un csv pete sa mere
        /// </summary>
        private void Excel()
        {
            string content = "";

            if (listeSel == null || listeSel.Count() == 0)
                MessageBox.Show("vide");
            else
            {
                Type t = listeSel[0].GetType();
                
                // on rempli les headers qui sont les noms des propriete
                foreach (MemberInfo mi in t.GetMembers())
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        content += UppercaseFirst(mi.Name) + ";";
                    }
                }
                content += "\n"; // fin des headers

                // on rempli les valeurs
                foreach (ColTrans ct in listeSel)
                {
                    content += ct.matricule + ";" + ct.nom + ";" + ct.prenom + "\n";
                }

                // on fait péter dans le fichier
                if( toFic(content) )
                    MessageBox.Show("Données exportées avec succès !");
                else
                    MessageBox.Show("Export interrompu");
            }
        }

        /// <summary>
        /// Ecrit dans un fichier
        /// </summary>
        /// <param name="content">Le texte a ecrire dans le fichier</param>
        /// <returns>true si reussi, false sinon</returns>
        private bool toFic(string content)
        {
            string ficPath = Directory.GetCurrentDirectory() + "\\test.csv";
            try
            {
                StreamWriter sw = new StreamWriter(ficPath); // WpfApplication\bin\debug
                sw.WriteLine(content);
                sw.Close();
                //Process.Start(ficPath); // lance l'ouverture du fichier cree
                return true;
            }
            catch (IOException e)
            {
                string msg = "";

                if (e.HResult == -2147024864) // code si fichier deja ouvert
                    msg = "Erreur: le fichier de destination est déjà ouvert.\nVeuillez le fermer d'abord.";
                else
                    msg = "IOException: erreur d'acces au fichier";
                
                MessageBox.Show(msg);
                return false;
            }
        }

        /// <summary>
        /// Convertie une IList en string
        /// Forme : 
        ///     {obj1, obj2, obj3} =>
        ///     obj1.ToString()\n
        ///     obj2.ToString()\n
        ///     obj3.ToString()\n
        /// </summary>
        /// <param name="liste">La liste a representer en string</param>
        /// <returns>La string qui represente la liste</returns>
        private string lToS(IList liste)
        {
            string str = "";
            foreach (object o in liste)
            {
                str += o.ToString() + "\n";
            }
            return str;
        }

        /// <summary>
        /// Met la 1ere lettre d'une string en majuscule
        /// </summary>
        /// <param name="s">La string cible</param>
        /// <returns>La chaine majusculee</returns>
        private string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}