using Pars.Util.Puma.Api.DataLocalizer.Models;
using Pars.Util.Puma.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Pars.Core;

namespace Pars.Util.Puma.Api.DataLocalizer.Controllers
{
    [Authorize]
    public class DataLocalizerController : ApiController
    {
        DataLocalizerBusiness business = new DataLocalizerBusiness();

        [HttpPost]
        [Route("api/localizer/table")]
        public GetLocalesByTableResponse GetLocalesByTable([FromBody]GetLocalesByTableRequest request)
        {
            var result = new GetLocalesByTableResponse();

            try
            {
                result.Value = business.GetLocalesByTable(request.Value, request.Languages);
            }
            catch (Exception ex)
            {
                result.AddError(ex.GetMessageDeep());
            }


            return result;
        }

        [HttpPost]
        [Route("api/localizer/row")]
        public GetLocalesByRowResponse GetLocalesByRow([FromBody]GetLocalesByRowRequest request)
        {
            var result = new GetLocalesByRowResponse();

            try
            {
                result.Value = business.GetLocalesByRow(request.Guid, request.ObjectRef, request.Languages);
            }
            catch (Exception ex)
            {
                result.AddError(ex.GetMessageDeep());
            }


            return result;
        }

        [HttpPost]
        [Route("api/localizer/save")]
        public SaveLocalesResponse SaveLocales([FromBody]SaveLocalesRequest request)
        {
            var result = new SaveLocalesResponse();

            try
            {
                result.IsSuccess = business.SaveLocales(request.Value);
            }
            catch (Exception ex)
            {
                result.AddError(ex.GetMessageDeep());
            }

            return result;
        }
    }
}