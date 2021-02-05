using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DDataLocalizerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DDataLocalizerService.svc or DDataLocalizerService.svc.cs at the Solution Explorer and start debugging.
    public class DataLocalizerService : IDataLocalizerService
    {
        readonly IDataLocalizerBusiness _dataLocalizerBusiness;

        public DataLocalizerService(IDataLocalizerBusiness dataLocalizerBusiness)
        {
            _dataLocalizerBusiness = dataLocalizerBusiness;
        }

        public DataLocalizerService() : this(Pars.Core.Container.Instance.Resolve<IDataLocalizerBusiness>())
        {

        }

        public GetLocalesByTableResponse GetLocalesByTable(GetLocalesByTableRequest request)
        {
            var response = new GetLocalesByTableResponse();

            try
            {
                DataLocalization result = _dataLocalizerBusiness.GetLocalesByTable(request.Value, request.Languages);
                response.Value = result;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetLocalesByRowResponse GetLocalesByRow(GetLocalesByRowRequest request)
        {
            var response = new GetLocalesByRowResponse();

            try
            {
                DataLocalization result = _dataLocalizerBusiness.GetLocalesByRow(request.Guid, request.ObjectRef, request.Languages);
                response.Value = result;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public SaveLocalesResponse SaveLocales(SaveLocalesRequest request)
        {
            var response = new SaveLocalesResponse();

            try
            {
                bool isSuccess = _dataLocalizerBusiness.SaveLocales(request.Value);
                response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }
    }
}
