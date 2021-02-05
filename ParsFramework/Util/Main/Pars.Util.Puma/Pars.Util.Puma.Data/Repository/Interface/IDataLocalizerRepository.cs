using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository
{
    public interface IDataLocalizerRepository
    {
        DataLocalization GetLocalesByTable(DataLocalization dataLocalization, List<int> languages);

        DataLocalization GetLocalesByRow(string guid, int objectRef, List<int> languages);

        bool SaveLocales(DataLocalization dataLocalization);
    }
}
