using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Model
{
    public interface IServiceClient
    {
        Client Charger();

        List<Client> ChargerTout();
    }
}
