using System;
using System.Diagnostics.CodeAnalysis;

namespace Proximus
{
    public static class ExtensionMethods
    {
        public static string ToFixed(this double number, double decimals)
        {
            return number.ToString("N" + decimals);
        }
    }
}
