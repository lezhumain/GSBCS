﻿using Model;
using Model.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.ViewModel
{
    public class BarChartViewModel : MainViewModel
    {
        private List<KeyValuePair<string, int>> context;
        public List<KeyValuePair<string, int>> Context
        {
            get { return context; }
            set { NotifyPropertyChanged(ref context, value); }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public BarChartViewModel()
        {
            Context = new List<KeyValuePair<string, int>>();

            List<COLLABORATEUR> lc = ColHelper.Current.GetListForChart();
            showColumnChart( lc );
        }

        /// 
        /// \summary  Cree un histogramme a partir d'une
        ///         liste cree en dur
        /// 
        private void showColumnChart()
        {
            Context.Add(new KeyValuePair<string, int>("Mahak", 300));
            Context.Add(new KeyValuePair<string, int>("Pihu", 250));
            Context.Add(new KeyValuePair<string, int>("Rahul", 289));
            Context.Add(new KeyValuePair<string, int>("Raj", 256));
            Context.Add(new KeyValuePair<string, int>("Vikas", 140));
            Context.Add(new KeyValuePair<string, int>("alMahak", 300));
            Context.Add(new KeyValuePair<string, int>("Pibdhu", 250));
            Context.Add(new KeyValuePair<string, int>("Ravshul", 289));
            Context.Add(new KeyValuePair<string, int>("Ravvdwj", 256));
            Context.Add(new KeyValuePair<string, int>("Vikawwbnzs", 140));
        }

        /// 
        /// \summary  Cree un histogramme a partir des parametres (surcharge)
        /// 
        /// \param  keys Liste de String qui identifieront les valeurs (affiches sur l'axe x)
        ///         values Liste de int représentant les valeurs à afficher dans le graph
        /// 
        private void showColumnChart(List<string> keys, List<int> values)
        {
            Context.Clear();

            int cnt = keys.Count();

            for (int i = 0; i < cnt; ++i)
                Context.Add(new KeyValuePair<string, int>(keys[i], values[i]));
        }

        /// 
        /// \summary  Cree un histogramme a partir des parametres (surcharge)
        ///         Appelle ensuite 'showColumnChart(keys, values)'
        ///         avec    keys : liste des noms de chaque Entite
        ///                 values : liste des nombres de rapports de chaque Entite
        /// \param  
        ///     collabs Liste d'entites COLLABORATEUR
        /// 
        private void showColumnChart(List<COLLABORATEUR> collabs)
        {
            List<string> keys = new List<string>();
            List<int> values = new List<int>();

            foreach (COLLABORATEUR cols in collabs)
            {
                keys.Add(cols.nom_col);
                values.Add(cols.RAPPORT_DE_VISITE.Count());
            }

            showColumnChart(keys, values);
        }
    }
}
