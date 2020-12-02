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
   public class HSVToRGBConverterTests
   {
      private const float HUE = 203.2f;
      private const float SATURATION = 0.34f;
      private const float VALUE = 0.63f;
      private const byte EXPECTED_R = 243;
      private const byte EXPECTED_G = 93;
      private const byte EXPECTED_B = 117;

      private Mock<IConverterHelper> _helperMock;
      private IHSVConverter _converter;

      [TestInitialize]
      public void Setup()
      {
         _helperMock = new Mock<IConverterHelper>();
         _helperMock.Setup(helper => helper.CalculateRed(HUE, SATURATION, VALUE)).Returns(EXPECTED_R);
         _helperMock.Setup(helper => helper.CalculateGreen(HUE, SATURATION, VALUE)).Returns(EXPECTED_G);
         _helperMock.Setup(helper => helper.CalculateBlue(HUE, SATURATION, VALUE)).Returns(EXPECTED_B);
         _helperMock.Setup(helper => helper.CalculateRGB(HUE, SATURATION, VALUE)).Returns((EXPECTED_R, EXPECTED_G, EXPECTED_B));
         _converter = new HSVConverter(_helperMock.Object);
      }

      [TestMethod]
      public void GetRed_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetRed(HUE, SATURATION, VALUE);
         Assert.AreEqual(EXPECTED_R, actual);
         _helperMock.Verify(helper => helper.CalculateRed(HUE, SATURATION, VALUE), Times.Once);
      }

      [TestMethod]
      public void GetGreen_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetGreen(HUE, SATURATION, VALUE);
         Assert.AreEqual(EXPECTED_G, actual);
         _helperMock.Verify(helper => helper.CalculateGreen(HUE, SATURATION, VALUE), Times.Once);
      }

      [TestMethod]
      public void GetBlue_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetBlue(HUE, SATURATION, VALUE);
         Assert.AreEqual(EXPECTED_B, actual);
         _helperMock.Verify(helper => helper.CalculateBlue(HUE, SATURATION, VALUE), Times.Once);
      }

      [TestMethod]
      public void GetRGB_CallsHelperMethodAndReturnsExpected()
      {
         var actual = _converter.GetRGB(HUE, SATURATION, VALUE);
         Assert.AreEqual(EXPECTED_R, actual.Red);
         Assert.AreEqual(EXPECTED_G, actual.Green);
         Assert.AreEqual(EXPECTED_B, actual.Blue);
         _helperMock.Verify(helper => helper.CalculateRGB(HUE, SATURATION, VALUE), Times.Once);
      }
   }
}
