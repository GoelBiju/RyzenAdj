using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

using RyzenAdjustApi;


namespace RyzenAdjUI
{
    internal enum AccentState
    {
        ACCENT_DISABLED = 1,
        ACCENT_ENABLE_GRADIENT = 0,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19
        // ...
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdjustApi adjustApi;
        ryzen_access apiAccess;

        private string dir = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();

            // Initialise the adjust api for use.
            InitialiseApi();
        }


        private void InitialiseApi()
        {
            adjustApi = new AdjustApi();

            //
            apiAccess = adjustApi.GetRyzenAccess();
            if (!apiAccess.use)
            {
                MessageBoxResult result = MessageBox.Show("Unable to initialise an API access object. Quiting application.", 
                    "Ryzen Mobile - Adjustment Tool", MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    QuitApp();
                }
            }
        }


        private void QuitApp()
        {
            // Call to cleanup the api.
            adjustApi.Cleanup();
            Application.Current.Shutdown();
        }

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void RcheckBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (stapmLimitCheckbox.Fill != Brushes.Green)
            {
                stapmLimitCheckbox.Fill = Brushes.Green;
                cube1.Visibility = Visibility.Visible;
                rect1s.Visibility = Visibility.Visible;
                stapmLimitSlider.Visibility = Visibility.Visible;
                lab1.Visibility = Visibility.Visible;
            }
            else
            {
                stapmLimitCheckbox.Fill = Brushes.Transparent;
                cube1.Visibility = Visibility.Hidden;
                rect1s.Visibility = Visibility.Hidden;
                stapmLimitSlider.Visibility = Visibility.Hidden;
                lab1.Visibility = Visibility.Hidden;
            }

        }

        public void RcheckBox2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (fastLimitCheckbox.Fill != Brushes.Green)
            {
                fastLimitCheckbox.Fill = Brushes.Green;
                cube2.Visibility = Visibility.Visible;
                rect2s.Visibility = Visibility.Visible;
                fastLimitSlider.Visibility = Visibility.Visible;
                lab2.Visibility = Visibility.Visible;
            }
            else
            {
                fastLimitCheckbox.Fill = Brushes.Transparent;
                cube2.Visibility = Visibility.Hidden;
                rect2s.Visibility = Visibility.Hidden;
                fastLimitSlider.Visibility = Visibility.Hidden;
                lab2.Visibility = Visibility.Hidden;
            }
        }

        public void RcheckBox3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (slowLimitCheckbox.Fill != Brushes.Green)
            {
                slowLimitCheckbox.Fill = Brushes.Green;
                cube3.Visibility = Visibility.Visible;
                rect3s.Visibility = Visibility.Visible;
                slowLimitSlider.Visibility = Visibility.Visible;
                lab3.Visibility = Visibility.Visible;
            }
            else
            {
                slowLimitCheckbox.Fill = Brushes.Transparent;
                cube3.Visibility = Visibility.Hidden;
                rect3s.Visibility = Visibility.Hidden;
                slowLimitSlider.Visibility = Visibility.Hidden;
                lab3.Visibility = Visibility.Hidden;
            }
        }

        public void RcheckBox4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (temperatureLimitCheckbox.Fill != Brushes.Green)
            {
                temperatureLimitCheckbox.Fill = Brushes.Green;
                cube4.Visibility = Visibility.Visible;
                rect4s.Visibility = Visibility.Visible;
                temperatureLimitSlider.Visibility = Visibility.Visible;
                lab4.Visibility = Visibility.Visible;
            }
            else
            {
                temperatureLimitCheckbox.Fill = Brushes.Transparent;
                cube4.Visibility = Visibility.Hidden;
                rect4s.Visibility = Visibility.Hidden;
                temperatureLimitSlider.Visibility = Visibility.Hidden;
                lab4.Visibility = Visibility.Hidden;
            }
        }

        public void RcheckBox5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (maxVrmCurrentCheckbox.Fill != Brushes.Green)
            {
                maxVrmCurrentCheckbox.Fill = Brushes.Green;
                cube5.Visibility = Visibility.Visible;
                rect5s.Visibility = Visibility.Visible;
                maxVrmCurrentSlider.Visibility = Visibility.Visible;
                lab5.Visibility = Visibility.Visible;
            }
            else
            {
                maxVrmCurrentCheckbox.Fill = Brushes.Transparent;
                cube5.Visibility = Visibility.Hidden;
                rect5s.Visibility = Visibility.Hidden;
                maxVrmCurrentSlider.Visibility = Visibility.Hidden;
                lab5.Visibility = Visibility.Hidden;
            }
        }

        //public void RcheckBox6_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (rcheckBox6.Fill != Brushes.Green)
        //    {
        //        rcheckBox6.Fill = Brushes.Green;
        //    }
        //    else
        //    {
        //        rcheckBox6.Fill = Brushes.Transparent;
        //    }
        //}

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //RunExe();
            //ApplySettings();
            //MessageBox.Show("Success!");
            //Application.Current.Shutdown();

            Console.WriteLine(adjustApi.SetStapmLimit(apiAccess, 20000));
        }

        private void ApplySettings()
        {
            int stapmLimit = 25;
            //int fastLimit = 25;
            //int slowLimit = 25;
            //int temperatureLimit = 80;
            //int maxVrmCurrent = 30000;

            if (stapmLimitCheckbox.Fill == Brushes.Green)
            {
                stapmLimit = int.Parse(fastLimitSlider.Value.ToString()) * 1000;
                //bool set = adjustApi.SetStapmLimit(stapmLimit);
            }   

            //if (fastLimitCheckbox.Fill == Brushes.Green)
            //{
            //    fastLimit = int.Parse(fastLimitSlider.Value.ToString()) * 1000;
            //    //bool set = adjustApi.SetFastLimit(fastLimit);
            //}
                

            //if (slowLimitCheckbox.Fill == Brushes.Green)
            //    slowLimit = int.Parse(slowLimitSlider.Value.ToString()) * 1000;

            //if (temperatureLimitCheckbox.Fill == Brushes.Green)
            //    temperatureLimit = int.Parse(temperatureLimitSlider.Value.ToString());

            //if (maxVrmCurrentCheckbox.Fill == Brushes.Green)
            //    maxVrmCurrent = int.Parse(maxVrmCurrentSlider.Value.ToString());

            //string vrmhex = vrm.ToString("X");

            //string args = $"--stapm-limit={spm}000 --fast-limit={sfl}000 --slow-limit={ssl}000 --tctl-temp={tmp} --vrmmax-current=0x{vrmhex}";

            //try
            //{
            //    Process.Start(new ProcessStartInfo
            //    {
            //        FileName = $"{dir}\\ryzenadj.exe",
            //        UseShellExecute = false,
            //        CreateNoWindow = true,
            //        Arguments = args,
            //        RedirectStandardOutput = true
            //    });
            //}

            //catch
            //{
            //    MessageBox.Show("Can't find ryzenadj.exe folder");
            //}

            //if (rcheckBox6.Fill == Brushes.Green)
            //{
            //    AutoStart();
            //}
            //else
            //{
            //    ClearAutoStart();
            //}

        }

        //public void AutoStart()
        //{
        //    var batFile = CreateBat();
        //    RegistryKey rkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        //    rkey.SetValue("RyzenAdj", batFile);
        //}

        public void ClearAutoStart()
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if ((string)rkey.GetValue("RyzenAdj") != null)
            {
                rkey.DeleteValue("RyzenAdj");
            }
        }

        //public string CreateBat()
        //{
        //    int spm = 15; int sfl = 30; int ssl = 25; int tmp = 80; int vrm = 30000;

        //    if (stapmLimitCheckbox.Fill == Brushes.Green)
        //        spm = int.Parse(slider1.Value.ToString());

        //    if (fastLimitCheckbox.Fill == Brushes.Green)
        //        sfl = int.Parse(slider2.Value.ToString());

        //    if (slowLimitCheckbox.Fill == Brushes.Green)
        //        ssl = int.Parse(slider3.Value.ToString());

        //    if (temperatureLimitCheckbox.Fill == Brushes.Green)
        //        tmp = int.Parse(slider4.Value.ToString());  

        //    if (maxVrmCurrentCheckbox.Fill == Brushes.Green)
        //        vrm = int.Parse(slider5.Value.ToString());

        //    string vrmhex = vrm.ToString("X");

        //    string batFilePath = $"{dir}\\autostart.bat";
        //    string bat = $"%~dp0\\ryzenadj.exe --stapm-limit={spm}000 --fast-limit={sfl}000 --slow-limit={ssl}000 --tctl-temp={tmp} --vrmmax-current=0x{vrmhex}";

        //    if (!File.Exists(batFilePath))
        //    {
        //        using (FileStream fs = File.Create(batFilePath))
        //        {
        //            fs.Close();
        //        }
        //    }

        //    using (StreamWriter sw = new StreamWriter(batFilePath))
        //    {
        //        sw.WriteLine(bat);
        //    }

        //    return batFilePath;


        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            QuitApp();
        }
    }
}
