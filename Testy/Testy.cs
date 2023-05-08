using Time_TimePeriod;

namespace Testy
{
    [TestClass]
    public class TestyTime
    {
        [TestMethod]
        public void TestCreateValidTime()
        {
            // Test creating a valid time instance
            var time = new Time(12, 30, 45);
            Assert.AreEqual(12, time.Hours);
            Assert.AreEqual(30, time.Minutes);
            Assert.AreEqual(45, time.Seconds);
        }

        [TestMethod]
        public void TestCreateInvalidTimeWithNegativeHour()
        {
            // Test creating an invalid time instance with negative hour
            Assert.ThrowsException<ArgumentException>(() => new Time(70, 30, 45));
        }

        [TestMethod]
        public void TestCreateInvalidTimeWithInvalidMinute()
        {
            // Test creating an invalid time instance with invalid minute
            Assert.ThrowsException<ArgumentException>(() => new Time(12, 60, 45));
        }

        [TestMethod]
        public void TestCreateInvalidTimeWithInvalidSecond()
        {
            // Test creating an invalid time instance with invalid second
            Assert.ThrowsException<ArgumentException>(() => new Time(12, 30, 60));
        }

        [TestMethod]
        public void TestCreateInvalidTimeWithNegativeMinute()
        {
            // Test creating an invalid time instance with negative minute
            Assert.ThrowsException<ArgumentException>(() => new Time(12, 70, 45));
        }

        [TestMethod]
        public void TestCreateInvalidTimeWithNegativeSecond()
        {
            // Test creating an invalid time instance with negative second
            Assert.ThrowsException<ArgumentException>(() => new Time(12, 30, 70));
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedTime()
        {
            // Arrange
            Time time = new Time(14, 30, 0);

            // Act
            string result = time.ToString();

            // Assert
            Assert.AreEqual("14:30:00", result);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedTimeWithLeadingZero()
        {
            // Arrange
            Time time = new Time(9, 5, 0);

            // Act
            string result = time.ToString();

            // Assert
            Assert.AreEqual("09:05:00", result);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedTime2()
        {
            // Arrange
            Time time = new Time(5, 30, 0);

            // Act
            string result = time.ToString();

            // Assert
            Assert.AreEqual("05:30:00", result);
        }

        [TestMethod]
        public void TestTimeEqualsWithEqualValues()
        {
            Time time1 = new Time(12, 30);
            Time time2 = new Time(12, 30);
            Assert.IsTrue(time1 == time2);
        }

        [TestMethod]
        public void TestTimeEqualsWithDifferentValues()
        {
            Time time1 = new Time(12, 30);
            Time time2 = new Time(11, 45);
            Assert.IsFalse(time1 == time2);
        }



        [TestMethod]
        public void TestCompareTo_SameTime_ReturnsZero()
        {
            // Arrange
            Time time1 = new Time(9, 30);
            Time time2 = new Time(9, 30);

            // Act
            int result = time1.CompareTo(time2);

            // Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestLessThanOperator_EarlierTime_ReturnsTrue()
        {
            // Arrange
            Time earlierTime = new Time(8, 30);
            Time laterTime = new Time(10, 0);

            // Act
            bool result = earlierTime < laterTime;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGreaterThanOperator_LaterTime_ReturnsTrue()
        {
            // Arrange
            Time earlierTime = new Time(8, 30);
            Time laterTime = new Time(10, 0);

            // Act
            bool result = laterTime > earlierTime;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestLessThanOrEqualOperator_EarlierTime_ReturnsTrue()
        {
            // Arrange
            Time earlierTime = new Time(8, 30);
            Time laterTime = new Time(10, 0);

            // Act
            bool result1 = earlierTime <= laterTime;
            bool result2 = earlierTime <= earlierTime;

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void TestGreaterThanOrEqualOperator_LaterTime_ReturnsTrue()
        {
            // Arrange
            Time earlierTime = new Time(8, 30);
            Time laterTime = new Time(10, 0);

            // Act
            bool result1 = laterTime >= earlierTime;
            bool result2 = laterTime >= laterTime;

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void TestEqualityOperator_EqualTime_ReturnsTrue()
        {
            // Arrange
            Time time1 = new Time(9, 30);
            Time time2 = new Time(9, 30);

            // Act
            bool result = time1 == time2;

            // Assert
            Assert.IsTrue(result);
        }

    }
    [TestClass]
    public class TestyTimePeriod
    {
        [TestMethod]
        public void Create_TimePeriodWithNegativeValues_ThrowsArgumentException()
        {
            // Arrange
            Action action = () => new TimePeriod(-1, 2, -3);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void TryParse_NullOrEmptyString_ReturnsFalse()
        {
            // Arrange
            TimePeriod result;

            // Act
            var success1 = TimePeriod.TryParse(null, out result);
            var success2 = TimePeriod.TryParse("", out result);

            // Assert
            Assert.IsFalse(success1);
            Assert.IsFalse(success2);
        }

        [TestMethod]
        public void TryParse_InvalidString_ReturnsFalse()
        {
            // Arrange
            TimePeriod result;

            // Act
            var success1 = TimePeriod.TryParse("1:2:3:4", out result);
            var success2 = TimePeriod.TryParse("-1:2:3", out result);
            var success3 = TimePeriod.TryParse("1:-2:3", out result);
            var success4 = TimePeriod.TryParse("1:2:-3", out result);

            // Assert
            Assert.IsFalse(success1);
            Assert.IsFalse(success2);
            Assert.IsFalse(success3);
            Assert.IsFalse(success4);
        }

        [TestMethod]
        public void Plus_AddingTwoTimePeriods_ReturnsCorrectResult()
        {
            // Arrange
            var t1 = new TimePeriod(1, 30, 0);
            var t2 = new TimePeriod(2, 45, 0);

            // Act
            var result = TimePeriod.Plus(t1, t2);

            // Assert
            Assert.AreEqual(new TimePeriod(4, 15, 0), result);
        }

        [TestMethod]
        public void Minus_SubtractingTwoTimePeriods_ReturnsCorrectResult()
        {
            // Arrange
            var t1 = new TimePeriod(3, 0, 0);
            var t2 = new TimePeriod(2, 30, 0);

            // Act
            var result = TimePeriod.Minus(t1, t2);

            // Assert
            Assert.AreEqual(new TimePeriod(0, 30, 0), result);
        }

        [TestMethod]
        public void Multiply_MultiplyingTimePeriod_ReturnsCorrectResult()
        {
            // Arrange
            var t1 = new TimePeriod(1, 30, 0);
            var multiplier = 2.5;

            // Act
            var result = TimePeriod.Multiply(t1, multiplier);

            // Assert
            Assert.AreEqual(new TimePeriod(3, 45, 0), result);
        }

        [TestMethod]
        public void Divide_DividingTimePeriod_ReturnsCorrectResult()
        {
            // Arrange
            var t1 = new TimePeriod(2, 30, 0);
            var divisor = 2;

            // Act
            var result = TimePeriod.Divide(t1, divisor);

            // Assert
            Assert.AreEqual(new TimePeriod(1, 15, 0), result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectTimeForZeroPeriod()
        {
            // Arrange
            var period = new TimePeriod(0, 0, 0);

            // Act
            var result = period.ToString();

            // Assert
            Assert.AreEqual("00:00:00", result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectTimeForOneHourTwoMinutesThreeSecondsPeriod()
        {
            // Arrange
            var period = new TimePeriod(1, 2, 3);

            // Act
            var result = period.ToString();

            // Assert
            Assert.AreEqual("01:02:03", result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectTimeForTenHoursFiftyNineMinutesFiftyNineSecondsPeriod()
        {
            // Arrange
            var period = new TimePeriod(10, 59, 59);

            // Act
            var result = period.ToString();

            // Assert
            Assert.AreEqual("10:59:59", result);
        }

        [TestMethod]
        public void TestEqualsReturnsTrueForEqualValues()
        {
            var t1 = new TimePeriod(1, 23, 45);
            var t2 = new TimePeriod(1, 23, 45);

            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void TestEqualsReturnsFalseForDifferentValues()
        {
            var t1 = new TimePeriod(1, 23, 45);
            var t2 = new TimePeriod(2, 34, 56);

            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod]
        public void TestOperatorEqualsReturnsTrueForEqualValues()
        {
            var t1 = new TimePeriod(1, 23, 45);
            var t2 = new TimePeriod(1, 23, 45);

            Assert.IsTrue(t1 == t2);
        }

        [TestMethod]
        public void Constructor_NegativeValues_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(-1, 2, 3));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(1, -2, 3));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(1, 2, -3));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(-1, -2, -3));
        }

        [TestMethod]
        public void CompareTo_TwoTimePeriods_ReturnsCorrectResult()
        {
            TimePeriod t1 = new TimePeriod(1, 30, 0);
            TimePeriod t2 = new TimePeriod(2, 0, 0);
            TimePeriod t3 = new TimePeriod(0, 45, 0);

            Assert.IsTrue(t1.CompareTo(t2) < 0);
            Assert.IsTrue(t2.CompareTo(t1) > 0);
            Assert.IsTrue(t1.CompareTo(t3) > 0);
            Assert.IsTrue(t3.CompareTo(t1) < 0);
            Assert.IsTrue(t1.CompareTo(t1) == 0);
        }

        [TestMethod]
        public void LessThanOperator_TwoTimePeriods_ReturnsCorrectResult()
        {
            TimePeriod t1 = new TimePeriod(1, 30, 0);
            TimePeriod t2 = new TimePeriod(2, 0, 0);
            TimePeriod t3 = new TimePeriod(0, 45, 0);

            Assert.IsTrue(t1 < t2);
            Assert.IsFalse(t2 < t1);
            Assert.IsFalse(t1 < t3);
            Assert.IsTrue(t3 < t1);
        }

        [TestMethod]
        public void TestTimePlusTimePeriod()
        {
            Time t1 = new Time(12, 30, 45);
            TimePeriod tp1 = new TimePeriod(2, 15, 30);

            Time t2 = t1.Plus(tp1);

            Assert.AreEqual(t2.Hours, 14);
            Assert.AreEqual(t2.Minutes, 46);
            Assert.AreEqual(t2.Seconds, 15);
        }

        [TestMethod]
        public void TestStaticTimePlusTimePeriod()
        {
            Time t1 = new Time(22, 45, 30);
            TimePeriod tp1 = new TimePeriod(1, 30, 15);

            Time t2 = Time.Plus(t1, tp1);

            Assert.AreEqual(t2.Hours, 0);
            Assert.AreEqual(t2.Minutes, 15);
            Assert.AreEqual(t2.Seconds, 45);
        }

        [TestMethod]
        public void TestTimePlusTimePeriodModulo24Hours()
        {
            Time t1 = new Time(23, 30, 0);
            TimePeriod tp1 = new TimePeriod(1, 30, 0);

            Time t2 = t1.Plus(tp1);

            Assert.AreEqual(t2.Hours, 1);
            Assert.AreEqual(t2.Minutes, 0);
            Assert.AreEqual(t2.Seconds, 0);
        }

        [TestMethod]
        public void TestTimeMinusTimePeriod()
        {
            Time t1 = new Time(10, 15, 30);
            TimePeriod tp1 = new TimePeriod(2, 30, 15);

            Time t2 = t1.Minus(tp1);

            Assert.AreEqual(t2.Hours, 7);
            Assert.AreEqual(t2.Minutes, 45);
            Assert.AreEqual(t2.Seconds, 15);
        }

        [TestMethod]
        public void TestTimeMinusTimePeriodModulo24Hours()
        {
            Time t1 = new Time(0, 30, 0);
            TimePeriod tp1 = new TimePeriod(2, 30, 0);

            Time t2 = t1.Minus(tp1);

            Assert.AreEqual(t2.Hours, 22);
            Assert.AreEqual(t2.Minutes, 0);
            Assert.AreEqual(t2.Seconds, 0);
        }

        [TestMethod]
        public void OperatorMultiply_MultipliesTimePeriodByMultiplier_ReturnsCorrectTimePeriod()
        {
            // Arrange
            var timePeriod = new TimePeriod(2, 30, 0);
            double multiplier = 2.5;

            // Act
            var result = timePeriod * multiplier;

            // Assert
            Assert.AreEqual(6, result.Hours);
            Assert.AreEqual(15, result.Minutes);
            Assert.AreEqual(0, result.Seconds);
        }

        [TestMethod]
        public void TestTimePeriodMultiplication()
        {
            TimePeriod tp1 = new TimePeriod(1, 30, 0);

            TimePeriod tp2 = tp1 * 2;

            Assert.AreEqual(tp2.Hours, 3);
            Assert.AreEqual(tp2.Minutes, 0);
            Assert.AreEqual(tp2.Seconds, 0);
        }
    }
}