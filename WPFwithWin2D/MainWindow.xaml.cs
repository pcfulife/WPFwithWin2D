
using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using System;
using System.Runtime;
using System.Threading.Tasks;

namespace WPFwithWin2D
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var canvasSwapChain = new CanvasSwapChain(CanvasDevice.GetSharedDevice(), 300F, 300F, 96F);
            Task.Run(() =>
            {
                GCSettings.LatencyMode = GCLatencyMode.LowLatency;
                while (true)
                {
                    using (var canvasDrawingSession = canvasSwapChain.CreateDrawingSession(Colors.White))
                    {
                    }
                    canvasSwapChain.Present(0);
                    Console.WriteLine(GC.CollectionCount(0).ToString() + " " + GC.CollectionCount(1).ToString() + " " + GC.CollectionCount(2).ToString());
                }
            });
        }
    }
}
