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
       //     var imagetest = "https://lh4.googleusercontent.com/-1wzlVdxiW14/USSFZnhNqxI/AAAAAAAABGw/YpdANqaoGh4/s1600-w400/Google%2BSydney";
       
            var result = MakeRequest(url);
            var rest = processRequest(result);
            return rest;
            //return new Restaurant() { Name = "GoogleID", GoogleID = "564654654", City = "Leiden", Rating="5 fucking stars m8", Street = "that one weird ally" };
        }
        public List<Restaurant> GetRestaurantsByGeo(string Long, string Lat, string Rad, string Type)
        {
            var key = Key();
            var url = "https://maps.googleapis.com/maps/api/place/textsearch/xml?query=" + Type + "&key=" + key + "&location=" + Lat + "," + Long + "&radius=" + Rad + "&type=restaurant";
            var result = MakeRequest(url);
            var xelement = XElement.Load(new XmlNodeReader(result));
            var restaurantlist = new List<Restaurant>();
            foreach (var item in xelement.Descendants("result"))
            {
                restaurantlist.Add(GetRestaurantByGoogleID(item.Descendants("place_id").First().Value));
            }
            //TODO google place call voor get place by long lat
            //return restaurant met google Id
            
         
            return restaurantlist;
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
      
        public string getPhoto(string reference)
        {
            var key = Key();
            var photourl = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference="+reference+"&key="+key;
            return photourl;
        }
        public Restaurant processRequest(XmlDocument doc)
        {
            var rest = new Restaurant();
            var res = doc.SelectNodes("/PlaceDetailsResponse/result");
            var xelement = XElement.Load(new XmlNodeReader(doc));
            
            rest.Name = xelement.Descendants("name").First().Value;
            var ratingcheck = xelement.Descendants("rating").FirstOrDefault();
            if (ratingcheck == null)
            {
                rest.Rating = "0";
            }
            else
            {
                rest.Rating = ratingcheck.Value;
            }
            rest.GoogleID = xelement.Descendants("place_id").First().Value;
            var citychild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "locality");
            rest.City = citychild.Parent.Element("long_name").Value;
            var streetchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "route");
            var housenrchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "street_number");
            if (streetchild == null)
            {
                rest.Street = "";
                rest.StreetNr = "";
            }
            else
            {
                rest.Street = streetchild.Parent.Element("long_name").Value;
                rest.StreetNr = housenrchild.Parent.Element("long_name").Value;
            }
            var postalchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "postal_code");
            rest.PostalCode = postalchild.Parent.Element("long_name").Value;
            var Geometry = xelement.Descendants("location");
            var photocheck = xelement.Descendants("photo_reference").FirstOrDefault();
            if(photocheck == null)
            {
                rest.image = "";
            }
            else
            { 
                var photo = photocheck.Value;
                rest.image = getPhoto(photo);
            }
            
            rest.Lng = xelement.Descendants("lng").First().Value;
            rest.Lat = xelement.Descendants("lat").First().Value;
    


            return rest;
        }
        public Restaurant processSingle(XElement xelement)
        {
            var rest = new Restaurant();
            var photo = xelement.Descendants("photo_reference").First().Value;
            rest.image = getPhoto(photo);

            rest.Name = xelement.Descendants("name").First().Value;
            var ratingcheck = xelement.Descendants("rating").First();
            if(ratingcheck == null)
            {
                rest.Rating = "0";
            }
            else
            {
                rest.Rating = ratingcheck.Value;
            }
            rest.GoogleID = xelement.Descendants("place_id").First().Value;
            var citychild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "locality");
            rest.City = citychild.Parent.Element("long_name").Value;
            var streetchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "route");
            var housenrchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "street_number");
            rest.Street = streetchild.Parent.Element("long_name").Value;
            rest.StreetNr = housenrchild.Parent.Element("long_name").Value;
            var postalchild = xelement.Descendants("type").FirstOrDefault(x => x.Value == "postal_code");
            rest.PostalCode = postalchild.Parent.Element("long_name").Value;
            var Geometry = xelement.Descendants("location");
            rest.Lng = xelement.Descendants("lng").First().Value;
            rest.Lat = xelement.Descendants("lat").First().Value;



            return rest;
        }
        #endregion
    }
}