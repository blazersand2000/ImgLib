using System;
using System.Collections.Generic;
using System.Text;

namespace ImgLib.ColorConversions
{
   public static class ColorConversionsFactory
   {
      public static IConverterHelper MakeColorConverter()
      {
         return new ConverterHelper();
      }
   }
}
