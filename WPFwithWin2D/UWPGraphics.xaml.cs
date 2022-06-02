using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WPFwithWin2D
{
    public partial class UWPGraphics
    {
        // Copy NativeDll.dll from NativeDll project to WPFwithWin2D project output.
        [DllImport("NativeDll")]
        public static extern IntPtr GetD3D11Device(IntPtr dxgi);

        public UWPGraphics()
        {
            InitializeComponent();
        }

        void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            var viewbox = (sender as WindowsXamlHost).Child as Viewbox;
            if (viewbox == null)
            {
                return;
            }
            viewbox.Stretch = Stretch.Fill;
            var canvasDevice = new CanvasDevice();
            var canvasSwapChain = new CanvasSwapChain(canvasDevice, 300F, 300F, 96F);
            var canvasSwapChainPanel = new CanvasSwapChainPanel
            {
                Width = 300.0,
                Height = 300.0,
                SwapChain = canvasSwapChain
            };

            // dxgi is NOT IntPtr.Zero but valid pointer
            var dxgi = Marshal.GetComInterfaceForObject(canvasDevice, typeof(IDirect3DDevice));
            // d3d11 is IntPtr.Zero
            var d3d11 = GetD3D11Device(dxgi);

            viewbox.Child = canvasSwapChainPanel;
            Task.Run(() =>
            {
                while (true)
                {
                    using (var canvasDrawingSession = canvasSwapChain.CreateDrawingSession(Colors.White))
                    {
                        canvasDrawingSession.FillRectangle(new Rect(0.0, 0.0, 300.0, 300.0), Colors.Black);
                        canvasDrawingSession.DrawText(@"UWP (Win2D)
Graphics with
Xaml Islands", new Vector2(50F, 50F), Colors.White);
                    }
                   canvasSwapChain.Present();
                }
            });
        }
    }
}
