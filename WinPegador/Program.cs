using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Windows.Interop;
using System.Diagnostics;


public class MiForm : Form
{
        private const int WH_MOUSE_LL = 14;
    private const int WM_LBUTTONDOWN = 0x0201;
        private static LowLevelMouseProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
         private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && (wParam == (IntPtr)WM_LBUTTONDOWN))
        {
            Console.WriteLine("Detected click");
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    public MiForm() {
        // Form's properties
        this.Text = "Super pegador FG";
        this.Width = 400;
        this.Height = 300;

        // Creating label and button
        Label tituloLabel = new Label();
        tituloLabel.Text = "FG";
        tituloLabel.Font = new Font("Arial", 16, FontStyle.Bold);
        tituloLabel.Location = new Point(50, 50);
        tituloLabel.TextAlign = ContentAlignment.MiddleCenter;

        Button boton = new Button();
        boton.Text = "Pegadeitor";
        boton.Location = new Point(50, 100);

        // Adding label and button to the form
        this.Controls.Add(tituloLabel);
        this.Controls.Add(boton);
        this.MouseClick += new MouseEventHandler(Form_MouseClick);
    }

    private void Form_MouseClick(object sender, MouseEventArgs e) {
        MessageBox.Show($"Clic detectado en posición: {e.Location} + sender {sender} + args {e}");
    }
    

    public static void Main()
    {
        _hookID = SetHook(_proc);
        Application.Run( new MiForm());
        UnhookWindowsHookEx(_hookID);
    }

    private static IntPtr SetHook(LowLevelMouseProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_MOUSE_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}
