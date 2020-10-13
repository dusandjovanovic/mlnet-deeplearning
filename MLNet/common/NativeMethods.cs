using System;
using System.Runtime.InteropServices;

namespace MLNet.common
{
    /**
     * Pomocna klasa za instanciranje odvojene konzole
     */

    public static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
    }
}