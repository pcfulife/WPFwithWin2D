
using Microsoft.Graphics.Canvas;
using System;
using System.Threading.Tasks;
using Windows.UI;

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
