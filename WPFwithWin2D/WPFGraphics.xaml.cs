using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace WPFwithWin2D
{
    public partial class WPFGraphics
    {
        readonly DrawingGroup drawingGroup = new DrawingGroup();

        public WPFGraphics()
        {
            InitializeComponent();
            var typeface = new Typeface(string.Empty);
            CompositionTarget.Rendering += (sender, e) =>
            {
                using var drawingContext = drawingGroup.Open();
                drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, 300.0, 300.0));
                drawingContext.DrawText(new FormattedText(@"WPF Graphics
Press any key
to switch graphics.", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16.0, Brushes.White, 96.0), new Point(50.0, 50.0));
            };
        }

        protected override void OnRender(DrawingContext drawingContext) => drawingContext.DrawDrawing(drawingGroup);
    }
}
