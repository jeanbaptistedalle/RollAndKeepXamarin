using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace RollAndKeep.Helper.Test
{
    [TestFixture]
    public class EnumerableHelperTest
    {
        static IEnumerable<TestCaseData> GenerateNumbersTestCaseSource()
        {
            yield return new TestCaseData(1, 1, new List<int> { 1 }); //Same number case
            yield return new TestCaseData(1, 2, new List<int> { 1, 2 }); // Different number case
            yield return new TestCaseData(2, 9, new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 }); // Different number case
            yield return new TestCaseData(-2, 2, new List<int> { -2, -1, 0, 1, 2 });//Positive/Negative numbers case
            yield return new TestCaseData(-5, -1, new List<int> { -5, -4, -3, -2, -1 });//Negative numbers case
        }
        [TestCaseSource(nameof(GenerateNumbersTestCaseSource))]
        public void GenerateNumbersTest(int from, int to, List<int> expectedNumbers)
        {
            var result = EnumerableHelper.GenerateNumbers(from, to).ToList();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(expectedNumbers.Count()));
            for (var idx = 0; idx < expectedNumbers.Count; idx++)
            {
                Assert.That(result[idx], Is.EqualTo(expectedNumbers[idx]));
            }
        }

        static IEnumerable<TestCaseData> GenerateNumbersArgumentExceptionTestCaseSource()
        {
            yield return new TestCaseData(10, 0);
            yield return new TestCaseData(5, -5);
            yield return new TestCaseData(-5, -10);
        }
        [TestCaseSource(nameof(GenerateNumbersArgumentExceptionTestCaseSource))]
        public void GenerateNumbersArgumentExceptionTest(int from, int to)
        {
            Assert.That(() => EnumerableHelper.GenerateNumbers(from, to), Throws.ArgumentException);
        }
    }
}
