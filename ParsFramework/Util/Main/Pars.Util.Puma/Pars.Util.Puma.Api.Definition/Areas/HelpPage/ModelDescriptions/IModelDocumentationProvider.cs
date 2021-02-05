using System;
using System.Reflection;

namespace Pars.Util.Puma.Api.Definition.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}