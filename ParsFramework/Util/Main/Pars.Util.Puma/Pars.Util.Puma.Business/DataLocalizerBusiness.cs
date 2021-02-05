using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Business
{
    public class DataLocalizerBusiness : IDataLocalizerBusiness
    {
        private readonly IDataLocalizerRepository _repository;

        public DataLocalizerBusiness(IDataLocalizerRepository repository)
        {
            _repository = repository;
        }

        public DataLocalizerBusiness() : this(Pars.Core.Container.Instance.Resolve<IDataLocalizerRepository>())
        {

        }

        public DataLocalization GetLocalesByTable(DataLocalization dataLocalization, List<int> languages)
        {
            DataLocalization result = _repository.GetLocalesByTable(dataLocalization, languages);
            return result;
        }

        public DataLocalization GetLocalesByRow(string guid, int objectRef, List<int> languages)
        {
            DataLocalization result = _repository.GetLocalesByRow(guid, objectRef, languages);
            return result;
        }

        public bool SaveLocales(DataLocalization dataLocalization)
        {
            bool result = _repository.SaveLocales(dataLocalization);
            return result;
        }
    }
}
