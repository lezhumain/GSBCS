﻿using GalaSoft.MvvmLight.Command;
using Model;
using Model.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApplication.Helpers;
using WpfApplication.Model;

namespace WpfApplication.ViewModel
{
    public struct LoginInfosStruct
    {
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        private string mdp;
        public string Mdp
        {
            get
            {
                return mdp;
            }
            set
            {
                mdp = value;
            }
        }
    }

    public class LoginViewModel : MainViewModel
    {
        private LoginInfosStruct sInfos;

        #region Evnmt
            private RelayCommand<TextChangedEventArgs> loginTextChangedCommand;
            public ICommand LoginTextChangedCommand
            {
                get
                {
                    if (loginTextChangedCommand == null)
                        loginTextChangedCommand = new RelayCommand<TextChangedEventArgs>(LoginTextChanged);

                    return loginTextChangedCommand;
                }
            }

            private RelayCommand<RoutedEventArgs> mdpTextChangedCommand;
            public ICommand MdpTextChangedCommand
            {
                get
                {
                    if (mdpTextChangedCommand == null)
                        mdpTextChangedCommand = new RelayCommand<RoutedEventArgs>(MdpTextChanged);

                    return mdpTextChangedCommand;
                }
            }
        #endregion

        #region Evnmt methodes
            /// <summary>
            /// Handler pour la valeur de la textbox du login
            /// Met a jour la valeur de la structr
            /// </summary>
            /// <param name="args"></param>
            private void LoginTextChanged(TextChangedEventArgs args)
            {
                if (sInfos.Username == null)
                    sInfos.Username = "";

                sInfos.Username = (args.Source as TextBox).Text;

                //MessageBox.Show("cucu : " + sfiltre.Valeur);
            }

            /// <summary>
            /// Handler pour la valeur de la textbox du mdp
            /// Met a jour la valeur de la struct
            /// </summary>
            /// <param name="args"></param>
            private void MdpTextChanged(RoutedEventArgs args)
            {
                string msg = "pass: ";
                if(sInfos.Mdp == null)
                    sInfos.Mdp = "";

                PasswordBox p = args.Source as PasswordBox;

                if (p != null)
                {
                    sInfos.Mdp = p.Password;
                    msg += sInfos.Mdp;
                }
                else
                    msg += "<null>";
                //MessageBox.Show(sInfos.Mdp);
            }
        #endregion
        
        #region Commands
            // attribut binde au clic sur btn exit
            private DelegateCommand signInCommand;
            public ICommand SignInCommand
            {
                get
                {
                    if (signInCommand == null)
                        signInCommand = new DelegateCommand(SignIn);

                    return signInCommand;
                }
            }
        #endregion

        #region Commands methodes
            /// <summary>
            /// Handler pour le clic sur le btn Quit
            /// </summary>
            private void SignIn()
            {
                string msg = "";
                COLLABORATEUR col = ColHelper.Current.GetOneByUsername(sInfos.Username);

                if (col == null)
                    msg = "Aucun collabo";
                else
                {
                    if (sInfos.Mdp != col.mdp_col)
                        msg = "login: " + sInfos.Username + "\nmdp: " + sInfos.Mdp + "\n\nMot de passe erroné.";
                    else
                        msg = "Bienvenue";
                }
                MessageBox.Show(msg);
                /*
                string mdp = "fEPMcDj4cOneuOgSR/KDcni4xD14MY4NJuRcIhk9KUuiDH8EtE0u+qsTRBThX8S0fQtt9cpmrln5emyrBt2Hqg==";
                string salt_col = "0d360b479f16a1a2a93c22603354e09e2";
                */
                //COLLABORATEUR c = ColHelper.Current.GetOneByUsername(sInfos.Username); 
                //PasswordEncoder p = new PasswordEncoder();
               
                /* if (c == null)
                    MessageBox.Show("null");
                else*/
                    //MessageBox.Show("login: " + sInfos.Username + "\nmdp: " + sInfos.Mdp + " - " + p.encodePassword(sInfos.Mdp, c.salt_col));
                
                //MessageBox.Show("login: " + sInfos.Username + "\nmdp: " + sInfos.Mdp + "\n" + p.encodePassword(sInfos.Mdp, salt_col) + "\n" + mdp);
            }
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        public LoginViewModel(IServiceClient service)
        {
            sInfos = new LoginInfosStruct();
        }
    }
}