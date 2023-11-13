using System;
using System.Diagnostics;

namespace Altex
{
    public class Core
    {
        public static bool Initialize()
        {
            try
            {
                new Altex.Licensing.ProductManager();
                new Altex.UI.UIWindowHelper();
                return true;
            } catch
            { 
                return false;
            }
        }
    }
}
