/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WpfApplication"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WpfApplication.Model;
using WpfApplication.Model.Design;

namespace WpfApplication.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
                SimpleIoc.Default.Register<IServiceClient, DesignServiceClient>();
            else
                SimpleIoc.Default.Register<IServiceClient, ServiceClient>();

            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public MainWindowViewModel MainWindowVM
        {
            get { return ServiceLocator.Current.GetInstance<MainWindowViewModel>(); }
        }

        public ListWindowViewModel ListWindowVM
        {
            get { return ServiceLocator.Current.GetInstance<ListWindowViewModel>(); }
        }

        public LoginViewModel LoginVM
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }
    }
}