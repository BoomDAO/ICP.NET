using System;

namespace ICP.Candid.Models.Values
{
    public static class StringUtil
    {
        public static string PascalToCamelCase(string name)
        {
            return name.Substring(0, 1).ToUpperInvariant() + name.Substring(1);
        }
    }
}