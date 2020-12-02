using ImgLib;
using ImgLib.ColorConversions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImgLibTests.ColorConversions
{
   [TestClass]
   public class RGBToHSVConverterTests
   {
      private const byte R = 243;
      private const byte G = 93;
      private const byte B = 117;
      private const float EXPECTED_HUE = 203.2f;
      private const float EXPECTED_SATURATION = 0.34f;
      private const float EXPECTED_VALUE = 0.63f;
      private const double TOLERANCE = 0.001;

      private Mock<IConverterHelper> _helperMock;
      private IHSVConverter _converter;

      [TestInitialize]
      public void Setup()
      {
         _helperMock = new Mock<IConverterHelper>();
         _helperMock.Setup(helper => helper.CalculateHue(R, G, B)).Returns(EXPECTED_HUE);
         _helperMock.Setup(helper => helper.CalculateSaturation(R, G, B)).Returns(EXPECTED_SATURATION);
         _helperMock.Setup(helper => helper.CalculateValue(R, G, B)).Returns(EXPECTED_VALUE);
         _helperMock.Setup(helper => helper.CalculateHSV(R, G, B)).Returns((EXPECTED_HUE, EXPECTED_SATURATION, EXPECTED_VALUE));

         _converter = new HSVConverter(_helperMock.Object);
      }

      [TestMethod]
      public void GetHue_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetHue(R, G, B);
         Assert.AreEqual(EXPECTED_HUE, actual, TOLERANCE);
         _helperMock.Verify(helper => helper.CalculateHue(R, G, B), Times.Once);
      }

      [TestMethod]
      public void GetSaturation_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetSaturation(R, G, B);
         Assert.AreEqual(EXPECTED_SATURATION, actual, TOLERANCE);
         _helperMock.Verify(helper => helper.CalculateSaturation(R, G, B), Times.Once);
      }

      [TestMethod]
      public void GetValue_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetValue(R, G, B);
         Assert.AreEqual(EXPECTED_VALUE, actual, TOLERANCE);
         _helperMock.Verify(helper => helper.CalculateValue(R, G, B), Times.Once);
      }

      [TestMethod]
      public void GetHSV_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetHSV(R, G, B);
         Assert.AreEqual(EXPECTED_HUE, actual.Hue, TOLERANCE);
         Assert.AreEqual(EXPECTED_SATURATION, actual.Saturation, TOLERANCE);
         Assert.AreEqual(EXPECTED_VALUE, actual.Value, TOLERANCE);
         _helperMock.Verify(helper => helper.CalculateHSV(R, G, B), Times.Once);
      }
   }
}
