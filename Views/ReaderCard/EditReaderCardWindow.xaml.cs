using System.Windows;

namespace LibraryManagement.Views.ReaderCard
{
    public partial class EditReaderCardWindow : Window
    {
        public EditReaderCardWindow()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-Vn");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PackIcon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.Width == 1420)
            {
                this.Width -= 400;
                rightside.Visibility = Visibility.Collapsed;
                RotateIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowRightDropCircle;
                return;
            }
            this.Width += 400;
            rightside.Visibility = Visibility.Visible;
            RotateIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowLeftDropCircle;

        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
