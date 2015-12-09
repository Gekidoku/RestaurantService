using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RestaurantService
{
    public class RestaurantManager
    {
        private static RestaurantManager _instance = new RestaurantManager();

        public RestaurantManager() { }
        public static RestaurantManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #region Methods
        //
        ///<summary>
        ///Restaurant selected by googleid
        /// </summary>
        public Restaurant GetRestaurantByGoogleID(string GoogleID)
        {
            var key = Key();
            var url = "https://maps.googleapis.com/maps/api/place/details/xml?key="+key+"&placeid="+GoogleID;
            
            var result = MakeRequest(url);
            var rest = processRequest(result);
            return rest;
            //return new Restaurant() { Name = "GoogleID", GoogleID = "564654654", City = "Leiden", Rating="5 fucking stars m8", Street = "that one weird ally" };
        }
        public Restaurant GetRestaurantByGeo(string Long, string Lat)
        {
            //TODO google place call voor get place by long lat
            //return restaurant met google Id

            return new Restaurant()
            {
                Name = "test",
                GoogleID = new Guid().ToString(),
                City = "That one weird town",
                Rating = "5 bloody stars m8",
                Street = "TyranisaurusREKTM8 322",
                Lng = "322",
                Lat = "322"
            };
        }
        
        private string Key()
        {
            string[] googleapikeys = new string[2] { "AIzaSyDzVGDSyH_9cUC-VzbPikf_LD9MIcFhHWA", "AIzaSyDzVGDSyH_9cUC-VzbPikf_LD9MIcFhHWA" };
            int random = new Random().Next(0, googleapikeys.Count());
            return googleapikeys.GetValue(random).ToString();
        }
        private static XmlDocument MakeRequest(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            return doc;
        }
        private static Restaurant processRequest(XmlDocument doc)
        {
            var rest = new Restaurant();
            var res = doc.SelectNodes("/PlaceDetailsResponse/result");
            var xelement = XElement.Load(new XmlNodeReader(doc));

            rest.Name = xelement.Descendants("name").First().Value;
            rest.Rating = xelement.Descendants("rating").First().Value;
            rest.GoogleID = xelement.Descendants("place_id").First().Value;
            var citychild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "locality");
            rest.City = citychild.Parent.Element("long_name").Value;
            var streetchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "route");
            var housenrchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "street_number");
            rest.Street = streetchild.Parent.Element("long_name").Value + " " + housenrchild.Parent.Element("long_name").Value;
            var Geometry = xelement.Descendants("location");
            rest.Lng = xelement.Descendants("lng").First().Value;
            rest.Lat = xelement.Descendants("lat").First().Value;
            //rest.Rating = node["rating"].InnerText;
            //rest.GoogleID = node["id"].InnerText;

            //rest.City = node["address_component/long_name"].InnerText;



            return rest;
        }
        #endregion
    }
}