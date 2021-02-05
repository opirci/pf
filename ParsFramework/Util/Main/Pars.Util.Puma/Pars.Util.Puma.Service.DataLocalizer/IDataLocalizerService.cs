using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [ServiceContract]
    public interface IDataLocalizerService
    {
        [OperationContract]
        GetLocalesByTableResponse GetLocalesByTable(GetLocalesByTableRequest request);

        [OperationContract]
        GetLocalesByRowResponse GetLocalesByRow(GetLocalesByRowRequest request);

        [OperationContract]
        SaveLocalesResponse SaveLocales(SaveLocalesRequest request);
    }
}
