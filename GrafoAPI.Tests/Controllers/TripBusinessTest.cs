using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrafoAPI.Tests.Controllers
{
    [TestClass]
    public class TripBusinessTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.DistanceRoute("A-B-C"), 9);
        }

        [TestMethod]
        public void TestMethod2()
        {

            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.DistanceRoute("A-D"), 5);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.DistanceRoute("A-D-C"), 13);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var tripBusiness = new Business.TripBusiness();
            Assert.AreEqual(tripBusiness.DistanceRoute("A-E-B-C-D"), 22);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestMethod5()
        {
            var tripBusiness = new Business.TripBusiness();
            tripBusiness.DistanceRoute("A-E-D");
        }

        [TestMethod]
        public void TestMethod6()
        {
            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.SmallerRoute("A", "C"), 9);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.SmallerRoute("B", "B"), 9);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.MaxStopsRoutes("C", "C"), 2);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var tripBusiness = new Business.TripBusiness();

            Assert.AreEqual(tripBusiness.EqualStopsRoutes("A", "C"), 3);
        }

        [TestMethod]
        public void TestMethod10()
        {
            var tripBusiness = new Business.TripBusiness();

            //Assert.AreEqual(tripBusiness.MaxDistanceRoutes("C", "C"), 7);
        }

    }
}
