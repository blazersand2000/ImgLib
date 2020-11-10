using ImgLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgLibTests
{
   [TestClass]
   public class RGBToHSVConverterTests
   {
      private const double TOLERANCE = 0.00001;
      private RGBToHSVConverter converter = new RGBToHSVConverter();
      
      [DataTestMethod]
      [DataRow((byte)0, 0f)]
      [DataRow((byte)173, 0.67843137f)]
      [DataRow((byte)255, 1f)]
      public void ConvertByteValueToFraction_ByteValues_ReturnExpectedFloat(byte input, float expectedOutput)
      {
         var actual = converter.ConvertByteValueToFraction(input);
         Assert.AreEqual(expectedOutput, actual, TOLERANCE);
      }

      [TestMethod]
      public void CMax_GivenFractionValues_ReturnsMaximum()
      {
         var actual = converter.CMax(0.0f, 0.6231f, 0.3245f);
         Assert.AreEqual(0.6231f, actual, TOLERANCE);
      }

      [TestMethod]
      public void CMin_GivenFractionValues_ReturnsMinimum()
      {
         var actual = converter.CMin(0.0f, 0.6231f, 0.3245f);
         Assert.AreEqual(0.0f, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow(0.69234f, 0.124f)]
      [DataRow(0.327f, 0.76124f)]
      public void Delta_GivenCMaxAndCMinValues_ReturnsExpectedDelta(float cMin, float cMax)
      {
         var actual = converter.Delta(cMin, cMax);
         var expected = cMax - cMin;
         Assert.AreEqual(expected, actual, TOLERANCE);
      }
   }
}
