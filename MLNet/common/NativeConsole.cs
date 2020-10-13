using System;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace MLNet.common
{
    /**
     * Pomocna klasa za redirekciju Console.Write/WriteLine/Flush metoda
     */

    public class NativeConsole : TextWriter
    {
        private TextBox console = null;

        public NativeConsole(TextBox output)
        {
            console = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            console.Dispatcher.BeginInvoke(new Action(() =>
            {
                console.AppendText(value.ToString());
            }));
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}