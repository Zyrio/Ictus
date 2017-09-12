using System;
using System.IO;
using Yio.Utilities.Interfaces;

namespace Yio.Utilities
{
    public class RandomGeneratorUtilities : IRandomGeneratorUtilities
    {
        public RandomGeneratorUtilities()
        {
        }

        public string GetRandomLowercaseAlphanumericString(int maxSize)
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path.Substring(0, maxSize);
        }

        public string GetRandomMixcaseAlphanumericString(int maxSize)
        {
            throw new NotImplementedException();
        }
    }
}