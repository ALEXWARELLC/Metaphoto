using System;

namespace Altex.UI;

public class UIWindowHelper
{
    public static readonly UIWindowHelper Instance;

    static UIWindowHelper()
    {
        Instance = new UIWindowHelper();
    }

    public string SetTitle(string ProductName) { return $"{ProductName}"; }
    public string SetTitle(string ProductName, string FileName) { return $"{ProductName} | {FileName}"; }
}