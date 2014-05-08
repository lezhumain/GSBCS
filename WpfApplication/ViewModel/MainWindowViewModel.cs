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
using WPFFolderBrowser;
using GalaSoft.MvvmLight.Messaging;
using System.Data.Objects.DataClasses;
using WpfApplication.View;

namespace WpfApplication.ViewModel
{
    public struct FiltreStruct
    {
        private string valeur;
        public string Valeur
        {
            get
            {
                return valeur;
            }
            set
            {
                valeur = value;
            }
        }

        private string champ;
        public string Champ
        {
            get
            {
                return champ;
            }
            set
            {
                champ = value;
            }
        }


    }
    

    public class MainWindowViewModel : MainViewModel
    {
        private COLLABORATEUR sessionCol;
        public COLLABORATEUR SessionCol
        {
            get { return sessionCol; }
            set { NotifyPropertyChanged(ref sessionCol, value); }
        }

        private string visibleFormPrat;
        public string VisibleFormPrat
        {
            get { return visibleFormPrat; }
            set { NotifyPropertyChanged(ref visibleFormPrat, value); }
        }

        private string visibleFormRap;
        public string VisibleFormRap
        {
            get { return visibleFormRap; }
            set { NotifyPropertyChanged(ref visibleFormRap, value); }
        }

        private string visibleFormVis;
        public string VisibleFormVis
        {
            get { return visibleFormVis; }
            set { NotifyPropertyChanged(ref visibleFormVis, value); }
        }

        private string visibleDataGridRP;
        public string VisibleDataGridRP
        {
            get { return visibleDataGridRP; }
            set { NotifyPropertyChanged(ref visibleDataGridRP, value); }
        }


        private readonly IServiceClient serviceClient;
        private string currentList;

        private List<string> listeRegions;
        public List<string> ListeRegions
        {
            get { return listeRegions; }
            set { NotifyPropertyChanged(ref listeRegions, value); }
        }
        
        private FiltreStruct sfiltre;
        public FiltreStruct Sfiltre
        {
            get{ return sfiltre; }
            set { sfiltre = value; }
        }

        public Visibility CbVisible { get; set; }

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
                    testCommand = new DelegateCommand(RefreshLists);

                return testCommand;
            }
        }

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

        private DelegateCommand statCommand;
        public ICommand StatCommand
        {
            get
            {
                if (statCommand == null)
                    statCommand = new DelegateCommand(Stat);

                return statCommand;
            }
        }

        private DelegateCommand filtreCommand;
        public ICommand FiltreCommand
        {
            get
            {
                if (filtreCommand == null)
                    filtreCommand = new DelegateCommand(Filtre);

                return filtreCommand;
            }
        }

        #endregion
        #region evenements

        private RelayCommand<KeyEventArgs> keyDownCommand;
        public ICommand KeyDownCommand
        {
            get
            {
                if (keyDownCommand == null)
                    keyDownCommand = new RelayCommand<KeyEventArgs>(KeyDown);

                return keyDownCommand;
            }
        }

        // lien de commande pour selectionChanged
        private RelayCommand<SelectionChangedEventArgs> selectedItemsCommand;
        public ICommand SelectedItemsCommand
        {
            get
            {
                if (selectedItemsCommand == null)
                    selectedItemsCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedItems);

                return selectedItemsCommand;
            }
        }

        private RelayCommand<SelectionChangedEventArgs> selectChangedCommand;
        public ICommand SelectChangedCommand
        {
            get
            {
                if (selectChangedCommand == null)
                    selectChangedCommand = new RelayCommand<SelectionChangedEventArgs>(Selected);

                return selectChangedCommand;
            }
        }

        private RelayCommand<EventArgs> doubleClickCommand;
        public ICommand DoubleClickCommand
        {
            get
            {
                if (doubleClickCommand == null)
                    doubleClickCommand = new RelayCommand<EventArgs>(DoubleClick);

                return doubleClickCommand;
            }
        }
        

        private RelayCommand<TextChangedEventArgs> textChangedCommand;
        public ICommand TextChangedCommand
        {
            get
            {
                if (textChangedCommand == null)
                    textChangedCommand = new RelayCommand<TextChangedEventArgs>(TextChanged);

                return textChangedCommand;
            }
        }

        private RelayCommand<SelectionChangedEventArgs> comboSelChangedCommand;
        public ICommand ComboSelChangedCommand
        {
            get
            {
                if (comboSelChangedCommand == null)
                    comboSelChangedCommand = new RelayCommand<SelectionChangedEventArgs>(ComboSelChanged);

                return comboSelChangedCommand;
            }
        }

        private RelayCommand<SelectionChangedEventArgs> comboSelChangedRegCommand;
        public ICommand ComboSelChangedRegCommand
        {
            get
            {
                if (comboSelChangedRegCommand == null)
                    comboSelChangedRegCommand = new RelayCommand<SelectionChangedEventArgs>(ComboSelChangedReg);

                return comboSelChangedRegCommand;
            }
        }

        #endregion
        #region Entites

        //attribut du formPrat
        private PRATICIEN objPratForm;
        public PRATICIEN ObjPratForm
        {
            get { return objPratForm; }
            set { NotifyPropertyChanged(ref objPratForm, value); }
        }

        private List<RapTrans> listeRapToBind;
        public List<RapTrans> ListeRapToBind
        {
            get { return listeRapToBind; }
            set { NotifyPropertyChanged(ref listeRapToBind, value); }
        }

        //attribut du formPrat
        private RAPPORT_DE_VISITE  objRapForm;
        public RAPPORT_DE_VISITE ObjRapForm
        {
            get { return objRapForm; }
            set { NotifyPropertyChanged(ref objRapForm, value); }
        }

        private List<object> listeSel;
        public List<object> ListeSel
        {
            get { return listeSel; }
            set { NotifyPropertyChanged(ref listeSel, value); }
        }

        private List<string> listeFiltres;
        public List<string> ListeFiltres
        {
            get { return listeFiltres; }
            set { NotifyPropertyChanged(ref listeFiltres, value); }
        }

        private List<ColTrans> listeCol;
        public List<ColTrans> ListeCol
        {
            get { return listeCol; }
            set { NotifyPropertyChanged(ref listeCol, value); }
        }
        
        private List<RapTrans> listeRap;
        public List<RapTrans> ListeRap
        {
            get { return listeRap; }
            set { NotifyPropertyChanged(ref listeRap, value); }
        }

        private List<PraTrans> listePrat;
        public List<PraTrans> ListePrat
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

        public List<RapTrans> convertRap(List<RAPPORT_DE_VISITE> list)
        {
            List<RapTrans> l = new List<RapTrans>();
            foreach (RAPPORT_DE_VISITE c in list)
            {

                PRATICIEN p = PraHelper.Current.getById( (int)c.matricule_praticien );
                l.Add(new RapTrans( c.num_rapport,
                                    p.prenom_praticien + " " + p.nom_praticien,
                                    c.date_rapport.ToString("dd/mm/yyyy"),
                                    c.COLLABORATEUR.prenom_col + " " + c.COLLABORATEUR.nom_col));
            }    
            return l;
        }

        public List<PraTrans> convertPrat(List<PRATICIEN> list)
        {
            List<PraTrans> l = new List<PraTrans>();
            foreach (PRATICIEN c in list)
            {
                l.Add(new PraTrans(c.matricule_praticien,
                                    c.nom_praticien,
                                    c.prenom_praticien));
            }
            return l;
        }

        #endregion



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

            ListeRegions = new List<string>();
            ListeSel = new List<object>();
            ListeFiltres = getHeaders(typeof(ColTrans));
            Sfiltre = new FiltreStruct();
            currentList = "Visiteurs";

            Messenger.Default.Register<COLLABORATEUR>(this, chargListes);
            //chargListes( ColHelper.Current.GetOneByUsername("Prenomrds.Nomrds") );

            //Details prat
            VisibleFormPrat     =   "Hidden";
            VisibleFormRap      =   "Hidden";
            VisibleFormVis      =   "Hidden";
            VisibleDataGridRP   =   "Hidden";
        }

        private void chargListes(COLLABORATEUR col)
        {
            List<String> lCodeReg = new List<string>();
            // on met le collab recup en "session"
            SessionCol = col;
            if (SessionCol.DIRECTEUR_REGIONAL != null)  // DIRECTEUR_REGIONAL
            {
                lCodeReg.Add( col.GERE.First<GERE>().code_region );
                chargListesForRegion(lCodeReg.First());
                CbVisible = Visibility.Hidden;
            }
            else                                        // Responsable de Secteur
            {
                // on recup la liste des regions a gerer
                ETRE_RESPONSABLE er = col.ETRE_RESPONSABLE.ToList<ETRE_RESPONSABLE>().First();
                List<REGION> query = (from q in er.SECTEUR.REGION
                                        select q).ToList<REGION>();

                // on met la liste dans celle bindee a la combo box
                ListeRegions = (from q in query select q.nom_region).ToList<string>();
                
                // puis on charge la premiere region par defaut
                chargListesForRegion( query.First() );
                CbVisible = Visibility.Visible;
            }

            //Details prat
            ObjPratForm = new PRATICIEN();

            // enfin on charge tous les praticiens
            ListePrat = convertPrat(PraHelper.Current.GetList());
        }

        private void chargListesForRegion(string regCode)
        {
            // On charge la liste des visiteurs de la region
            List<VISITEUR> lv = VisHelper.Current.getListByRegion(regCode);
            // on recup les COL associes
            List<COLLABORATEUR> lc = (from v in lv
                                      select v.COLLABORATEUR).ToList<COLLABORATEUR>();
            // puis on la met dans la liste affichee
            ListeCol = convertCol(lc);

            // charge la liste des rapports de la region
            List<RAPPORT_DE_VISITE> lr = RapHelper.Current.getListByRegion(regCode);
            // puis on la met dans la liste affichee
            ListeRap = convertRap(lr);
        }

        private void chargListesForRegion(REGION reg)
        {
            chargListesForRegion(reg.code_region);
        }

        /// <summary>
        /// Handler pour le clic sur le btn Quit
        /// </summary>
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Handler pour le clic sur le btn Refresh.
        /// Rempli les listes affichees dans la grid
        /// depuis le modele.
        /// </summary>
        private void RefreshLists()
        {
            ListeCol.Clear();
            ListeRap.Clear();
            ListePrat.Clear();

            chargListes(SessionCol);

            MessageBox.Show("Reloaded");
        }

        /// <summary>
        /// Ouvre un dialog permettant de selectionner
        /// un dossier.
        /// </summary>
        /// <returns>Le chemin vers le dossier selectionne ou une chaine vide</returns>
        private string chooseFolder()
        {
            WPFFolderBrowserDialog openFolder = new WPFFolderBrowserDialog();
            openFolder.InitialDirectory = Directory.GetCurrentDirectory();

            // Get the selected file name and display in a TextBox
            if (openFolder.ShowDialog() == true)
                return openFolder.FileName;
            else
                return ""; 
        }

        /// <summary>
        /// Handler pour l'appuie sur une touche.
        /// L'appuie sur la touche "Entree" lance le filtre
        /// </summary>
        /// <param name="args">Arguments de l'evnmt</param>
        private void KeyDown(KeyEventArgs args)
        {
            int i = 0;

            if (args.Key.ToString() == "Return")
                Filtre();
        }

        /// <summary>
        /// Handler pour la selection de ligne dans la datagrid
        /// Copie les SelectedItems de la grid dans ListeSel
        /// </summary>
        /// <param name="args">Les arguments de l'evnmt</param>
        private void SelectedItems(SelectionChangedEventArgs args)
        {
            string msg;

            if (args == null || (args.Source as DataGrid) == null)
                MessageBox.Show("args null");
            else
            {
                var lbi = ((args.Source as DataGrid).SelectedItems);

                if (lbi == null || lbi.Count == 0)
                    msg = "listeSel nule ou vide";
                else
                {
                    // on recup la classe des donnees selectionnees
                    string classe = (lbi[0].GetType().ToString()).Split('.').ToList<string>().Last();
                    
                    if (this.listeSel == null)
                        this.listeSel = new List<object>();
                    else
                        listeSel.Clear();
                    
                    switch (classe)
                    {
                        case "ColTrans":
                            foreach (ColTrans ct in lbi)
                            {
                                listeSel.Add(ct);
                            }
                            break;
                        case "RapTrans":
                            foreach (RapTrans ct in lbi)
                            {
                                listeSel.Add(ct);
                            }
                            break;
                        case "PraTrans":
                            foreach (PraTrans ct in lbi)
                            {
                                listeSel.Add(ct);
                            }
                            break;
                        default:
                            classe = "(L260)Défaut";
                            break;
                    }
                    msg = "class: " + classe;

                }
                //MessageBox.Show(msg);
            }
        }

        /// <summary>
        /// Handler pour le changement d'onglet
        /// Rempli la liste des filtres avec les nouvelles valeurs
        /// Met a jour this.currentList
        /// </summary>
        /// <param name="args">Les arguments de l'evnmt</param>
        private void Selected(SelectionChangedEventArgs args)
        {
            string name;
            //MessageBox.Show(((args.Source as TabControl).SelectedItem as TabItem).ToString());

            if (args.Source as TabControl != null)
            {
                TabItem ti = ((args.Source as TabControl).SelectedItem) as TabItem;
                string msg = "un tab a été select\narg type: ";

                // on determine dans quel onglet on est
                name = ti.Header.ToString();
                msg += name;

                if (this.ListeFiltres == null)
                    this.ListeFiltres = new List<string>();
                else
                    this.ListeFiltres.Clear();

                switch (name)
                {
                    // on recup les header de la classe corresp
                    case "Visiteurs":
                        this.currentList = "Visiteurs";
                        this.ListeFiltres = getHeaders(typeof(ColTrans));
                        break;
                    case "Rapports":
                        this.currentList = "Rapports";
                        this.ListeFiltres = getHeaders(typeof(RapTrans));
                        break;
                    case "Praticiens":
                        this.currentList = "Praticiens";
                        this.ListeFiltres = getHeaders(typeof(PraTrans));
                        break;
                }
            }

            //MessageBox.Show("Fin de Selected");
        }

        /// <summary>
        /// Handler pour selectionChanged de la comboBox
        /// Met la valeur selectionnee dans sfiltre.Champ
        /// </summary>
        /// <param name="args"></param>
        private void ComboSelChanged(SelectionChangedEventArgs args)
        {
            if (args.Source as ComboBox == null || (args.Source as ComboBox).SelectedItem == null)
                return;
            
            this.sfiltre.Champ = (args.Source as ComboBox).SelectedItem.ToString();
        }

        private void ComboSelChangedReg(SelectionChangedEventArgs args)
        {
            string regName = "";

            if (args.Source as ComboBox == null || (args.Source as ComboBox).SelectedItem == null)
                return;
            
            regName = (args.Source as ComboBox).SelectedItem.ToString();
            chargListesForRegion(RegHelper.Current.GetOneByName(regName));
        }

        private void DoubleClick(EventArgs args)
        {
            MouseButtonEventArgs mbea = args as MouseButtonEventArgs;

            if (mbea == null)
                return;

            switch (currentList)
            {
                case "Visiteurs":
                    COLLABORATEUR SelectCol = new COLLABORATEUR();
                    ColTrans SelectColTrans = (ColTrans)this.listeSel[0];
                    SelectCol = ColHelper.Current.GetOneById(SelectColTrans.matricule);
                    //Console.WriteLine(SelectCol.prenom_col);

                    VisibleFormPrat     =   "Hidden";
                    VisibleFormRap      =   "Hidden";
                    VisibleFormVis      =   "Visible";
                    VisibleDataGridRP   =   "Visible";

                    break;
                case "Praticiens":
                    if (this.listeSel[0] as PraTrans == null)
                        throw new System.ArgumentException("Parameter cannot be null", "original");

                    //Init attribut detail prat => objPratForm
                    PraTrans SelectPratTrans = this.listeSel[0] as PraTrans;
                    ObjPratForm = PraHelper.Current.getById(SelectPratTrans.matricule);

                    ListeRapToBind = convertRap(ObjPratForm.RAPPORT_DE_VISITE.ToList());
                    VisibleFormPrat     =   "Visible";
                    VisibleFormRap      =   "Hidden";
                    VisibleFormVis      =   "Hidden";
                    VisibleDataGridRP   =   "Visible";
                    break;
                case "Rapports":
                    if (this.listeSel[0] as PraTrans != null)
                        throw new System.ArgumentException("Parameter cannot be null", "original");
                    
                    //Init attribut detail prat => objPratForm
                    RapTrans SelectRapTrans = this.listeSel[0] as RapTrans;
                    ObjRapForm = RapHelper.Current.getById(SelectRapTrans.numero);

                    VisibleFormPrat     =   "Hidden";
                    VisibleFormRap      =   "Visible";
                    VisibleFormVis      =   "Hidden";
                    VisibleDataGridRP   =   "Hidden";
                    break;
                default:
                    break;
            }

            //MessageBox.Show( mbea.Source.GetType() + "\nDouble clicked biatch\n" + lToS(this.listeSel) );
        }
        
        /// <summary>
        /// Handler pour le bouton filtrer
        /// Algo a ameliorer
        /// </summary>
        private void Filtre()
        {
            string msg = "val: " + sfiltre.Valeur + "\nchamp: " + sfiltre.Champ;
            int lol; // num de matricule si renseigne

            if (sfiltre.Valeur == null || sfiltre.Valeur.Count() == 0)
                return;
                

            if (currentList == "Visiteurs")
            {
                switch (this.sfiltre.Champ.ToLower()) // pour les collabo
                {
                    case "matricule":
                        if (int.TryParse(sfiltre.Valeur, out lol))
                        {
                            List<ColTrans> l = ListeCol.Where(col => col.matricule == lol).ToList();
                            if (l.Count != 0)
                            {
                                ListeCol = l;
                                msg = "Matricule, fait.\n" + lToS(ListeCol);
                            }
                        }
                        else
                            msg = "Le matricule doit être un nombre";
                        break;
                    case "nom":
                        List<ColTrans> ll = ListeCol.Where(col => col.nom == sfiltre.Valeur).ToList();
                        if (ll.Count != 0)
                        {
                            ListeCol = ll;
                            msg = "Nom, fait.\n" + lToS(ListeCol);
                        }
                        break;
                    case "prenom":
                        List<ColTrans> lll = ListeCol.Where(col => col.prenom == sfiltre.Valeur).ToList();
                        if (lll.Count != 0)
                        {
                            ListeCol = lll;
                            msg = "Prénom, fait.\n" + lToS(ListeCol);
                        }
                        break;
                    default:
                        msg = "Defaut, a faire.";
                        break;
                }
            }
            else if (currentList == "Rapports") // pour les rapports
            {
                switch (this.sfiltre.Champ.ToLower()) // pour les collabo
                {
                    case "numero":
                        if (int.TryParse(sfiltre.Valeur, out lol))
                        {
                            List<RapTrans> l = ListeRap.Where(rap => rap.numero == lol).ToList();
                            if (l.Count != 0)
                            {
                                ListeRap = l;
                                msg = "Matricule, fait.\n" + lToS(ListeRap);
                            }
                        }
                        else
                            msg = "Le numéro doit être un nombre";
                        break;
                    case "praticien":
                        List<RapTrans> ll = ListeRap.Where(rap => rap.praticien == sfiltre.Valeur).ToList();
                        if (ll.Count != 0)
                        {
                            ListeRap = ll;
                            msg = "Nom fait.\n" + lToS(ListeRap);
                        }
                        break;
                    case "date":
                        List<RapTrans> lll = ListeRap.Where(rap => rap.date == sfiltre.Valeur).ToList();
                        if (lll.Count != 0)
                        {
                            ListeRap = lll;
                            msg = "Prénom fait.\n" + lToS(ListeRap);
                        }
                        break;
                    default:
                        msg = "Defaut, a faire.";
                        break;
                }
            }
            else if (currentList == "Praticiens") // pour les praticiens
            {
                switch (this.sfiltre.Champ.ToLower())
                {
                    case "matricule":
                        if (int.TryParse(sfiltre.Valeur, out lol))
                        {
                            List<PraTrans> l = ListePrat.Where(pra => pra.matricule == lol).ToList();
                            if (l.Count != 0)
                            {
                                ListePrat = l;
                                msg = "Matricule, fait.\n" + lToS(ListePrat);
                            }
                        }
                        else
                            msg = "Le matricule doit être un nombre";
                        break;
                    case "nom":
                        List<PraTrans> ll = ListePrat.Where(pra => pra.nom == sfiltre.Valeur).ToList();
                        if (ll.Count != 0)
                        {
                            ListePrat = ll;
                            msg = "Nom, fait.\n" + lToS(ListePrat);
                        }
                        break;
                    case "prenom":
                        List<PraTrans> lll = ListePrat.Where(pra => pra.prenom == sfiltre.Valeur).ToList();
                        if (lll.Count != 0)
                        {
                            ListePrat = lll;
                            msg = "Prénom, fait.\n" + lToS(ListeCol);
                        }
                        break;
                    default:
                        msg = "Defaut, a faire.";
                        break;
                }
            }
            else
                msg = "Erreur de valeur...";

            //MessageBox.Show( msg );
        }

        /// <summary>
        /// Handler pour le bouton excel
        /// Formate les informations necessaires dans une
        /// seule string puis la donne a manger a toFic()
        /// qui nous sort un csv pete sa mere
        /// </summary>
        private void Excel()
        {
            string content = "";
            bool skip = false; // tant que c'est pas fini

            if (listeSel == null || listeSel.Count() == 0)
                MessageBox.Show("Aucun élément sélectionné.");
            else
            {
                Type t = listeSel[0].GetType();
                string classe = t.Name;

                // on rempli les headers qui sont les noms des propriete
                foreach (MemberInfo mi in t.GetMembers())
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        content += UppercaseFirst(mi.Name) + ";";
                    }
                }
                content += "\n"; // fin des headers

                // Pour changer le type dans le foreach :
                switch(classe)
                {
                    case "ColTrans":
                        // on rempli les valeurs
                        foreach (ColTrans ct in listeSel)
                            content += ct.ToCsvRow();
                        break;
                    case "RapTrans":
                        // on rempli les valeurs
                        foreach (RapTrans ct in listeSel)
                            content += ct.ToCsvRow();
                        break;
                    case "PratTrans":
                        // on rempli les valeurs
                        foreach (PraTrans ct in listeSel)
                            content += ct.ToCsvRow();
                        break;
                    default:
                        MessageBox.Show("Default, a faire");
                        skip = true; // permet d'ecrire ou nn le fichier
                        break;
                }
                // on fait péter dans le fichier
                if(!skip) // enlever qd termine
                {
                    if(toFic(content))
                        MessageBox.Show("Données exportées avec succès !");
                    else
                        MessageBox.Show("Export interrompu");
                }
                else
                    MessageBox.Show("Rien n'a été fait.");
            }
        }

        private void Stat()
        {
            string content = "";

            barChartView bc = new barChartView(); // fenetre

            // envoie du COL
            //Messenger.Default.Send<COLLABORATEUR, MainWindowViewModel>(col);

            bc.Show();
        }

        /// <summary>
        /// Handler pour la valeur de la textbox des filtres
        /// Met a jour la valeur de la struct sfiltre.Valeur
        /// </summary>
        /// <param name="args"></param>
        private void TextChanged(TextChangedEventArgs args)
        {
            if (sfiltre.Valeur == null)
                sfiltre.Valeur = "";
            
            sfiltre.Valeur = (args.Source as TextBox).Text;

            //MessageBox.Show("cucu : " + sfiltre.Valeur);
        }

        /// <summary>
        /// Fait choisir un repertoire puis
        /// ecrit les donnees dans un fichier
        /// </summary>
        /// <param name="content">Le texte a ecrire dans le fichier</param>
        /// <returns>true si le fichier a ete ecrit, false sinon</returns>
        private bool toFic(string content)
        {
            string fold = chooseFolder();
            if (fold != "")
            {
                string ficPath = chooseFolder() + "\\test.csv";
                try
                {
                    StreamWriter sw = new StreamWriter(ficPath); // WpfApplication\bin\debug
                    sw.WriteLine(content);
                    sw.Close();
                    Process.Start(ficPath); // lance l'ouverture du fichier cree
                    return true;
                }
                catch (IOException e)
                {
                    string msg = "";

                    if (e.HResult == -2147024864) // code erreur si fichier deja ouvert
                        msg = "Erreur: le fichier de destination est déjà ouvert.\nVeuillez le fermer d'abord.";
                    else
                        msg = "IOException: erreur d'acces au fichier";

                    MessageBox.Show(msg);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Aucun répertoire sélectionné.");
                return false;
            }
        }

        /// <summary>
        /// Convertie une IList en string
        /// Forme : 
        ///     {obj1, obj2, obj3} =>
        ///     "obj1.ToString()
        ///     obj2.ToString()
        ///     obj3.ToString()"
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

        /// <summary>
        /// Retourne les proprietes du type passde en parametre
        /// </summary>
        /// <param name="type">Le type dont on veut recuperer les props</param>
        /// <returns>Une liste de string</returns>
        private List<string> getHeaders(Type type)
        {
            List<string> h = new List<string>();
                
            // on rempli les headers qui sont les noms des propriete
            foreach (MemberInfo mi in type.GetMembers())
            {
                if (mi.MemberType == MemberTypes.Property)
                {
                    h.Add( UppercaseFirst(mi.Name) );
                }
            }

            return h;
        }
    }
}