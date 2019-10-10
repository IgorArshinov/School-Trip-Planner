using SchoolTripPlannerUWP.Converters;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SchoolTripPlannerUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterToddlerPage : Page
    {
        public RegisterToddlerPage()
        {
            this.InitializeComponent();
        }

        private void ToddlerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ToTitleCaseConverter.StaticConvert(sender);
        }
    }
}
