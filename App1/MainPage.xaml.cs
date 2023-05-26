using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Runtime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace App1
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var canvasSwapChain = new CanvasSwapChain(CanvasDevice.GetSharedDevice(), 300F, 300F, 96F);
            MyCanvasSwapChainPanel.SwapChain = canvasSwapChain;
            Task.Run(() =>
            {
                GCSettings.LatencyMode = GCLatencyMode.LowLatency;
                while (true)
                {
                    using (var canvasDrawingSession = canvasSwapChain.CreateDrawingSession(Colors.White))
                    {
                        canvasDrawingSession.FillRectangle(new Rect(0.0, 0.0, 300.0, 300.0), Colors.Black);
                        canvasDrawingSession.DrawText(GC.CollectionCount(2).ToString(), new Vector2(50F, 50F), Colors.White);
                    }
                    canvasSwapChain.Present(0);
                }
            });
        }
    }
}
