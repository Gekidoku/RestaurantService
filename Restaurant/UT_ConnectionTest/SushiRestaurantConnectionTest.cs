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
            //arrange
            var restaurantManager = new RestaurantService.RestaurantService();
            var testValue = "GoogleID";

            var actualValue = restaurantManager.GetRestaurantByGoogleId("GoogleID").Name;
            Assert.IsNotNull(actualValue);
            Assert.AreEqual(testValue, actualValue);
            //act

            //assert
        }

        [TestMethod]
        public void UT_API_Call_is_not_empty() {

            var RestaurantService = new RestaurantService.RestaurantService();
            var x = RestaurantService.getRestaurantMethods();
            var restaurantManager = new RestaurantService.RestaurantManager();
            var getRestaurantByGoogleID = restaurantManager.GetRestaurantByGoogleID(null);


            x = null;
        }

        [TestMethod]
        public void UT_DelegatesVoltooidToKaartToevoegenProcessing() {
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
