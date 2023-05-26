﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Numerics;
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

        void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            var viewbox = (sender as WindowsXamlHost).Child as Viewbox;
            if (viewbox == null)
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
                while (true)
                {
                    using (var canvasDrawingSession = canvasSwapChain.CreateDrawingSession(Colors.White))
                    {
                        canvasDrawingSession.FillRectangle(new Rect(0.0, 0.0, 300.0, 300.0), Colors.Black);
                        canvasDrawingSession.DrawText(GC.CollectionCount(0).ToString(), new Vector2(50F, 50F), Colors.White);
                        canvasDrawingSession.DrawText(GC.CollectionCount(1).ToString(), new Vector2(50F, 100F), Colors.White);
                        canvasDrawingSession.DrawText(GC.CollectionCount(2).ToString(), new Vector2(50F, 150F), Colors.White);
                    }
                    canvasSwapChain.Present(0);
                }
            });
        }
    }
}