using ImgLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgLibTests.ColorConversions
{
   [TestClass]
   public class ConverterHelperTests
   {
      private const double TOLERANCE = 0.01;
      private ConverterHelper _helper = new ConverterHelper();

      [DataTestMethod]
      [DataRow((byte)0, 0f)]
      [DataRow((byte)173, 0.67843137f)]
      [DataRow((byte)255, 1f)]
      public void ConvertByteValueToFraction_GivenByteValues_ReturnExpectedFloat(byte input, float expectedOutput)
      {
         var actual = _helper.ConvertByteValueToFraction(input);
         Assert.AreEqual(expectedOutput, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow(0f, (byte)0)]
      [DataRow(0.3187f, (byte)81)]
      [DataRow(0.82315f, (byte)210)]
      [DataRow(1f, (byte)255)]
      public void ConvertFractionValueToByte_GivenFractionValues_ReturnExpectedByte(float input, byte expectedOutput)
      {
         var actual = _helper.ConvertFractionValueToByte(input);
         Assert.AreEqual(expectedOutput, actual);
      }

      [TestMethod]
      public void CMax_GivenFractionValues_ReturnsMaximum()
      {
         var actual = _helper.CMax(0.0f, 0.6231f, 0.3245f);
         Assert.AreEqual(0.6231f, actual, TOLERANCE);
      }

      [TestMethod]
      public void CMax_GivenByteValues_ReturnsMaximum()
      {
         var actual = _helper.CMax(212, 255, 79);
         Assert.AreEqual(255, actual);
      }

      [TestMethod]
      public void CMin_GivenFractionValues_ReturnsMinimum()
      {
         var actual = _helper.CMin(0.0f, 0.6231f, 0.3245f);
         Assert.AreEqual(0.0f, actual, TOLERANCE);
      }

      [TestMethod]
      public void CMin_GivenByteValues_ReturnsMinimum()
      {
         var actual = _helper.CMin(212, 254, 187);
         Assert.AreEqual(187, actual);
      }

      [DataTestMethod]
      [DataRow(0.69234f, 0.124f)]
      [DataRow(0.327f, 0.76124f)]
      public void Delta_GivenCMaxAndCMinFractionValues_ReturnsExpectedDelta(float cMin, float cMax)
      {
         var actual = _helper.Delta(cMin, cMax);
         var expected = cMax - cMin;
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow((byte)0, (byte)255)]
      [DataRow((byte)127, (byte)127)]
      public void Delta_GivenCMaxAndCMinByteValues_ReturnsExpectedDelta(byte cMin, byte cMax)
      {
         var actual = _helper.Delta(cMin, cMax);
         var expected = cMax - cMin;
         Assert.AreEqual(expected, actual);
      }

      [DataTestMethod]
      [DataRow((byte)255, (byte)0, (byte)39, (byte)255)]
      [DataRow((byte)77, (byte)88, (byte)155, (byte)78)]
      [DataRow((byte)113, (byte)113, (byte)113, (byte)0)]
      public void Delta_GivenRGBTripleByteValues_ReturnsExpectedDelta(byte r, byte g, byte b, byte expected)
      {
         var actual = _helper.Delta(r, g, b);
         Assert.AreEqual(expected, actual);
      }

      [DataTestMethod]
      [DataRow(0f, 0f)]
      [DataRow(360f, 0f)]
      [DataRow(1135f, 55f)]
      [DataRow(-1135f, 305f)]
      [DataRow(-60f, 300f)]
      public void NormalizeAngle_GivenAngles_ReturnsExpectedNormalizedAngle(float angle, float expected)
      {
         var actual = _helper.NormalizeAngle(angle);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow((byte)0, (byte)0, (byte)0, 0f)]
      [DataRow((byte)255, (byte)255, (byte)255, 0f)]
      [DataRow((byte)255, (byte)0, (byte)0, 0f)]
      [DataRow((byte)0, (byte)255, (byte)0, 120f)]
      [DataRow((byte)0, (byte)0, (byte)255, 240f)]
      [DataRow((byte)255, (byte)255, (byte)0, 60f)]
      [DataRow((byte)0, (byte)255, (byte)255, 180f)]
      [DataRow((byte)255, (byte)0, (byte)255, 300f)]
      [DataRow((byte)124, (byte)89, (byte)198, 259.26605224f)]
      public void CalculateHue_GivenRGBValues_ReturnsExpected(byte r, byte g, byte b, float expected)
      {
         var actual = _helper.CalculateHue(r, g, b);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow((byte)23, (byte)45, (byte)189, 0.878307f)]
      [DataRow((byte)124, (byte)89, (byte)198, 0.5505f)]
      [DataRow((byte)0, (byte)0, (byte)0, 0f)]
      public void CalculateSaturation_GivenRGBValues_ReturnsExpected(byte r, byte g, byte b, float expected)
      {
         var actual = _helper.CalculateSaturation(r, g, b);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow((byte)255, (byte)45, (byte)189, 1f)]
      [DataRow((byte)0, (byte)0, (byte)0, 0f)]
      [DataRow((byte)96, (byte)164, (byte)24, 0.643137f)]
      public void CalculateValue_GivenRGBValues_ReturnsExpected(byte r, byte g, byte b, float expected)
      {
         var actual = _helper.CalculateValue(r, g, b);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow((byte)0, (byte)0, (byte)0, 0f, 0f, 0f)]
      [DataRow((byte)255, (byte)255, (byte)255, 0f, 0f, 1f)]
      [DataRow((byte)255, (byte)0, (byte)0, 0f, 1f, 1f)]
      [DataRow((byte)0, (byte)255, (byte)0, 120f, 1f, 1f)]
      [DataRow((byte)0, (byte)0, (byte)255, 240f, 1f, 1f)]
      [DataRow((byte)255, (byte)255, (byte)0, 60f, 1f, 1f)]
      [DataRow((byte)0, (byte)255, (byte)255, 180f, 1f, 1f)]
      [DataRow((byte)255, (byte)0, (byte)255, 300f, 1f, 1f)]
      [DataRow((byte)191, (byte)191, (byte)191, 0f, 0f, 0.75f)]
      [DataRow((byte)128, (byte)128, (byte)128, 0f, 0f, 0.5f)]
      [DataRow((byte)128, (byte)0, (byte)0, 0f, 1f, 0.5f)]
      [DataRow((byte)128, (byte)128, (byte)0, 60f, 1f, 0.5f)]
      [DataRow((byte)0, (byte)128, (byte)0, 120f, 1f, 0.5f)]
      [DataRow((byte)128, (byte)0, (byte)128, 300f, 1f, 0.5f)]
      [DataRow((byte)0, (byte)128, (byte)128, 180f, 1f, 0.5f)]
      [DataRow((byte)0, (byte)0, (byte)128, 240f, 1f, 0.5f)]
      [DataRow((byte)28, (byte)142, (byte)63, 138.421051f, 0.8028f, 0.5569f)]
      public void CalculateHSV_GivenRGBValues_ReturnsExpected(byte r, byte g, byte b, float expectedHue, float expectedSaturation, float expectedValue)
      {
         var actual = _helper.CalculateHSV(r, g, b);
         Assert.AreEqual(expectedHue, actual.Hue, TOLERANCE);
         Assert.AreEqual(expectedSaturation, actual.Saturation, TOLERANCE);
         Assert.AreEqual(expectedValue, actual.Value, TOLERANCE);
      }

      [TestMethod]
      public void C_GivenValues_ReturnsExpected()
      {
         var s = 0.382f;
         var v = 0.81f;
         Assert.AreEqual(s * v, _helper.C(s, v));
      }

      [TestMethod]
      public void X_GivenValues_ReturnsExpected()
      {
         var h = 301.7f;
         var s = 0.478f;
         var v = 0.31f;
         Assert.AreEqual(0.143981567f, _helper.X(h, s, v), TOLERANCE);
      }

      [TestMethod]
      public void M_GivenValues_ReturnsExpected()
      {
         var s = 0.5f;
         var v = 0.5f;
         Assert.AreEqual(0.25f, _helper.M(s, v), TOLERANCE);
      }

      [DataTestMethod]
      [DataRow(27f, 0.45f, 0.11f, (byte)28)]
      [DataRow(218.9f, 0.602f, 0.719f, (byte)73)]
      public void CalculateRed_GivenHSVValues_ReturnsExpected(float hue, float saturation, float value, byte expected)
      {
         var actual = _helper.CalculateRed(hue, saturation, value);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow(27f, 0.45f, 0.11f, (byte)21)]
      [DataRow(218.9f, 0.602f, 0.719f, (byte)112)]
      public void CalculateGreen_GivenHSVValues_ReturnsExpected(float hue, float saturation, float value, byte expected)
      {
         var actual = _helper.CalculateGreen(hue, saturation, value);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow(27f, 0.45f, 0.11f, (byte)15)]
      [DataRow(218.9f, 0.602f, 0.719f, (byte)183)]
      public void CalculateBlue_GivenHSVValues_ReturnsExpected(float hue, float saturation, float value, byte expected)
      {
         var actual = _helper.CalculateBlue(hue, saturation, value);
         Assert.AreEqual(expected, actual, TOLERANCE);
      }

      [DataTestMethod]
      [DataRow(0f, 0f, 0f, (byte)0, (byte)0, (byte)0)]
      [DataRow(0f, 0f, 1f, (byte)255, (byte)255, (byte)255)]
      [DataRow(0f, 1f, 1f, (byte)255, (byte)0, (byte)0)]
      [DataRow(120f, 1f, 1f, (byte)0, (byte)255, (byte)0)]
      [DataRow(240f, 1f, 1f, (byte)0, (byte)0, (byte)255)]
      [DataRow(60f, 1f, 1f, (byte)255, (byte)255, (byte)0)]
      [DataRow(180f, 1f, 1f, (byte)0, (byte)255, (byte)255)]
      [DataRow(300f, 1f, 1f, (byte)255, (byte)0, (byte)255)]
      [DataRow(0f, 0f, 0.75f, (byte)191, (byte)191, (byte)191)]
      [DataRow(0f, 0f, 0.5f, (byte)128, (byte)128, (byte)128)]
      [DataRow(0f, 1f, 0.5f, (byte)128, (byte)0, (byte)0)]
      [DataRow(60f, 1f, 0.5f, (byte)128, (byte)128, (byte)0)]
      [DataRow(120f, 1f, 0.5f, (byte)0, (byte)128, (byte)0)]
      [DataRow(300f, 1f, 0.5f, (byte)128, (byte)0, (byte)128)]
      [DataRow(180f, 1f, 0.5f, (byte)0, (byte)128, (byte)128)]
      [DataRow(240f, 1f, 0.5f, (byte)0, (byte)0, (byte)128)]
      public void CalculateRGB_GivenHSVValues_ReturnsExpected(float hue, float saturation, float value, byte expectedRed, byte expectedGreen, byte expectedBlue)
      {
         var actual = _helper.CalculateRGB(hue, saturation, value);
         Assert.AreEqual(expectedRed, actual.Red);
         Assert.AreEqual(expectedGreen, actual.Green);
         Assert.AreEqual(expectedBlue, actual.Blue);
      }
   }
}
