using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WPFwithWin2D
{
    public partial class UWPGraphics
    {
        public UWPGraphics()
        {
            InitializeComponent();
        }

        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint timeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint timeEndPeriod(uint uMilliseconds);

        void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            timeBeginPeriod(1);
            if (!((sender as WindowsXamlHost).Child is Viewbox viewbox))
            {
                return;
            }
            viewbox.Stretch = Stretch.Fill;
            var canvasSwapChain = new CanvasSwapChain(CanvasDevice.GetSharedDevice(), 300F, 300F, 96F);
            var canvasSwapChainPanel = new CanvasSwapChainPanel
            {
                Width = 300.0,
                Height = 300.0,
                SwapChain = canvasSwapChain
            };
            viewbox.Child = canvasSwapChainPanel;
            Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                var totalFrame = 0;
                var lastElapsedMilliseconds = 0.0;
                var frametimes = new List<double>();
                var stopwatch = Stopwatch.StartNew();
                while (true)
                {
                    using (var canvasDrawingSession = canvasSwapChain.CreateDrawingSession(Colors.White))
                    {
                        canvasDrawingSession.FillRectangle(new Rect(0.0, 0.0, 300.0, 300.0), Colors.Black);
                        canvasDrawingSession.DrawText(@"UWP (Win2D)
Graphics with
Xaml Islands", new Vector2(50F, 50F), Colors.White);
                    }
                    canvasSwapChain.Present(1);
                    ++totalFrame;
                    var elapsedMilliseconds = 1000.0 * stopwatch.ElapsedTicks / Stopwatch.Frequency;
                    frametimes.Add(elapsedMilliseconds - lastElapsedMilliseconds);
                    lastElapsedMilliseconds = elapsedMilliseconds;
                    if (elapsedMilliseconds >= 1000)
                    {
                        Console.WriteLine("fps: " + totalFrame * 1000.0 / elapsedMilliseconds);
                        Console.WriteLine("Max: " + 1000.0 / frametimes.Max());
                        lastElapsedMilliseconds = 0.0;
                        totalFrame = 0;
                        frametimes.Clear();
                        stopwatch.Restart();
                    }
                }
            });
        }
    }
}
