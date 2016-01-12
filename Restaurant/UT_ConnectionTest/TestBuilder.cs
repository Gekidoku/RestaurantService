namespace UT_ConnectionTest {
    public class TestBuilder {
        public TestBuilder() {
        }
        private Mock<TestprocessingService> connectionRestaurantMock = null;
       
        internal Mock<TestprocessingService> ConnectionRestaurantMock {
            get {
                return connectionRestaurantMock;
            }

            set {
                connectionRestaurantMock = value;
            }
        }

        //internal TestBuilder ConnectionRestaurantMock(Mock<TestprocessingService> connectionMock) {
        //    ConnectionRestaurantMock = connectionMock;
        //    return this;
        //}
    }
}