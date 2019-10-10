using System.Diagnostics;
using SchoolTripPlannerUWP.Converters;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SchoolTripPlannerUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlanSchoolTripPage : Page
    {
        public PlanSchoolTripPage()
        {
            this.InitializeComponent();
        }

        private void SchoolTripTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ToTitleCaseConverter.StaticConvert(sender);
        }

        private void TeachersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("text: " + (sender as ListView).SelectedValue);
            //(sender as ListView).SelectedValue = 0L;
        }

        private void TeachersListView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
//            (sender as ListView).SelectedValue = 1;
        }
    }
}
