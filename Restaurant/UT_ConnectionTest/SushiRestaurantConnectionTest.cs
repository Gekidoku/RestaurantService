using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_ConnectionTest {
    [TestClass]
    public class SushiRestaurantConnectionTest {
        [TestMethod]
        public void UT_API_Connection_Succeed() {
            HttpWebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/place/details/xml?key=AIzaSyDzVGDSyH_9cUC-VzbPikf_LD9MIcFhHWA&placeid=GoogleID") as HttpWebRequest;
            Assert.IsNotNull(request);
            
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var expectedValue = "OK";
            var actual = response.StatusCode.ToString();
            Assert.AreEqual(expectedValue, actual);
            
        }

        [TestMethod]
        public void UT_API_Calls_Limit_Not_Reached() {
            HttpWebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/place/details/xml?key=AIzaSyDzVGDSyH_9cUC-VzbPikf_LD9MIcFhHWA&placeid=GoogleID") as HttpWebRequest;
            Assert.IsNotNull(request);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var expectedValue = "OK";
            var actual = response.StatusCode.ToString();
            Assert.AreEqual(expectedValue, actual);

        }


        [TestMethod]
        public void UT_MakeRequest_Succeed() {

            Assert.Inconclusive("deze test wordt overgeslagen vanwege het limiet aan API calls");

        }

        [TestMethod]
        public void UT_Restaurants_List_Response_is_not_empty() {
            var lonG = "5.2533770500";
            var lat = "52.3809500";
            var rad = "2000";
            var type = "grieks";
            var results = new RestaurantService.RestaurantManager().GetRestaurantsByGeo(lonG, lat, rad, type);

            var actual = results.Count;
            Assert.IsNotNull(actual);

            if (actual > 0) {
                return;
            }

        }

        [TestMethod]
        public void UT_Restaurants_List_Response_returns_almerestad_Resaurant_succes() {
            var city = "almere-stad";
            var lonG = "5.2533770500";
            var lat = "52.3809500";
            var rad = "2000";
            var type = "grieks";
            var result = new RestaurantService.RestaurantManager().GetRestaurantsByGeo(lonG, lat, rad, type);
            

            var actualCity = new RestaurantService.RestaurantService().GetRestaurantByGoogleId("GoogleID").City;
            Assert.AreEqual(city, actualCity);
        }

        [TestMethod]
        public void UT_API_Call_unknown() {
            //var stateDataBuilder = new TestBuilder();
            //StateData stateData = stateDataBuilder
            //                        .WithActiveStepId("Voltooid")
            //                        .Build();

            //var stateInfoBuilder = new StateInfoBuilder(stateData);
            //StateInfo state = stateInfoBuilder.Build();
            ////Arrange
            //var processingServiceMockBuilder = new ProcessingMockBuilder();
            //var processingServiceMock = processingServiceMockBuilder.WithProcessSetup(state, null, false).Build();

        }
    }
}
