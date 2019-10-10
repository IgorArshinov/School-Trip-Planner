using Windows.UI.Xaml.Controls;

namespace SchoolTripPlannerUWP.Views.Printing
{
    /// <summary>
    /// Page content to send to the printer
    /// </summary>
    public sealed partial class PrintRegisteredToddler : Page
    {
        public RichTextBlock TextContentBlock { get; set; }

        public PrintRegisteredToddler()
        {
            this.InitializeComponent();
            TextContentBlock = TextContent;
        }
    }
}
