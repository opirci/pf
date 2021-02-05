${
    using Typewriter.Extensions.WebApi;

    string ReturnType(Method m) => m.Type.Name == "IHttpActionResult" ? "void" : m.Type.Name;
    string ServiceName(Class c) => c.Name.Replace("Controller", "Service");
    
}
module App { $Classes(:ApiControllerBase)[

    export class $ServiceName {

        constructor(private $http: ng.IHttpService) { 
        } $Methods[
        
        public $name = ($Parameters[$name: $Type][, ]) : ng.IHttpPromise<$ReturnType> => {
            
            return this.$http<$ReturnType>({
                url: `$Url`, 
                method: "$HttpMethod", 
                data: $RequestData
            });
        };]
    }
    
    angular.module("App").service("$ServiceName", ["$http", $Name]);]
}