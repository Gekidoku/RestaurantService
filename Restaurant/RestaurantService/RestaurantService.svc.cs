using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;


namespace RestaurantService
{
   
    
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RestaurantService
    {
      
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public List<Method> Methods()
        {
            return this.getRestaurantMethods();
        }
        
        [OperationContract]
        [WebGet(UriTemplate = "/GetRestaurantMethods", ResponseFormat = WebMessageFormat.Json)]
        public List<Method> getRestaurantMethods()
        {
            var methods = new RestaurantManager();
            
            var methodlist = methods.GetType().GetMethods().Where(p => p.Name.Contains("Restaurant")).ToList();
            var methodreturn = new List<Method>();
            foreach (var method in methodlist)
            {
                methodreturn.Add(new Method() { Name = method.Name, Returns = method.ReturnParameter.ToString() });

            }
            // Add your operation implementation here
            return methodreturn;
        }
        [OperationContract]
        [WebGet(UriTemplate = "/GetRestaurantByGoogleId/{GoogleId}", ResponseFormat = WebMessageFormat.Json)]
        public Restaurant GetRestaurantByGoogleId(string GoogleId)
        {
            var Restaurant = RestaurantManager.Instance.GetRestaurantByGoogleID(GoogleId);
            
            return Restaurant;
        }

        [OperationContract]
        [WebGet(UriTemplate = "/GetRestaurantsByGeo/{Long}/{Lat}/{Rad}/{Type}", ResponseFormat = WebMessageFormat.Json)]
        public List<Restaurant> GetRestaurantsByGeo(string Long, string Lat, string Rad, string Type)
        {
            return RestaurantManager.Instance.GetRestaurantsByGeo(Long, Lat, Rad, Type);
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
