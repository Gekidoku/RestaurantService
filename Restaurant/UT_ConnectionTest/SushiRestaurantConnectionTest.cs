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

            var expected = true; 
            var actualValue = request.HaveResponse;
            Assert.AreEqual(expected, actualValue);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var expectedValue = "OK";
            var actual = response.StatusCode;
            Assert.AreEqual(expectedValue, actual);
            
        }

        [TestMethod]
        public void UT_MakeRequest_Succeed() {

            //var service = new RestaurantService.RestaurantService();
            //var restaurantMethods = service.getRestaurantMethods();
            
           // var url = service.GetRestaurantByGoogleId("test");

            //service.GetType().is
            //var aa = service.GetRestaurantByGoogleId().GetType.doc;

            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            
            
            //var x = request;
            


            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            //XmlDocument doc = new XmlDocument();
            //doc.Load(response.GetResponseStream());
            //return doc;



        //var restaurantManager = new RestaurantService.RestaurantManager();

        //string url = "InvalidCommandName";
        //PrivateType pt = new PrivateType(typeof(MakeRequest));

        //bool actualresult = (bool)pt.InvokeStatic("ValidateCommand", new object[] { Command });
        //bool expectedresult = false;
        //Assert.AreEqual(actualresult, expectedresult);
    }

        [TestMethod]
        public void UT_Restaurants_List_Response_is_not_empty() {
            var lonG = "5.2533770500";
            var lat = "52.3809500";
            var rad = "2000";
            var type = "grieks";
            var restaurantManager = new RestaurantService.RestaurantManager().GetRestaurantsByGeo(lonG, lat, rad, type);


            var actualvalue = new RestaurantService.RestaurantService().GetRestaurantByGoogleId("GoogleID").City;


            //var actualValue = restaurantManager.
                //GetMethods().Where(p => p.Name.Contains("Restaurant")).ToList();
            //Assert.IsNotNull(actualValue);
            //Assert.AreEqual(testValue, actualValue);
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
