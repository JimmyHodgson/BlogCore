using System;

namespace BlogCore.Common
{
    public static class StringToByteParser
    {
        public static long Parse(string value)
        {
            string unit = value.Substring(value.Length - 2);
            long val = Convert.ToInt32(value.Substring(0, value.Length - 2));
            switch (unit.ToLower())
            {
                case "kb":
                    return val * 1024;
                case "mb":
                    return val * 1024 * 1024;
                case "gb":
                    return val * 1024 * 1024 * 1024;
                default:
                    return 0;
            }
        }
    }
}
