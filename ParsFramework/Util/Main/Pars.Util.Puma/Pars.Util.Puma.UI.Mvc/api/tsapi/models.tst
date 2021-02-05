${
    using Typewriter.Extensions.WebApi;

    string ReturnType(Method m) => m.Type.Name == "IHttpActionResult" ? "void" : m.Type.Name;
    string ServiceName(Class c) => c.Name.Replace("Controller", "Service");

    string[] services =new string[] {"AuthSvc", "DataLocalizerSvc","","","",""};

    Template(Settings settings)    {     
        settings.OutputFilenameFactory = (f)=> {
        string ns =  f.Classes.FirstOrDefault().Namespace;
        return  ns.Substring(ns.LastIndexOf(".")+1) + ".ts";    };    
        }

     string Extending(Class @class)
     {
          return (@class.BaseClass != null ? "extends "+ @class.BaseClass.Name:"").Replace("DomainBaseOfint","DomainBase");
     }

}

module App { $Classes(c => c.Interfaces.Any( i => i.Name =="IExtensibleDataObject") || c.BaseClass != null && (c.BaseClass.Name.Contains("DomainBase")))[

    export class $Name $Extending {$Properties(p => p.Type.Name != "ExtensionDataObject")[
        $name: $Type;]    
    }]  
}