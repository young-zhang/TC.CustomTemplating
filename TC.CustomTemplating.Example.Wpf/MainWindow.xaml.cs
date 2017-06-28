
using System.Windows;

namespace TC.CustomTemplating.Example.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Controller controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            controller = new Controller();
            DataContext = controller;
        }

        /// <summary>
        /// Handles the Click event of the buttonTransform control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonTransform_Click(object sender, RoutedEventArgs e)
        {
            if (controller.Transform())
            {
                tabItemOutput.IsSelected = true;
            }
            else
            {
                tabItemErrors.IsSelected = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            controller.Clear();
        }

        /// <summary>
        /// Handles the Click event of the buttonTemplateDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonTemplateDomain_Click(object sender, RoutedEventArgs e)
        {
            controller.ToggleTemplateDomain();
        }

        /// <summary>
        /// Handles the Click event of the buttonTemplateDomainRecycle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonTemplateDomainRecycle_Click(object sender, RoutedEventArgs e)
        {
            controller.RecyleTemplateDomain();
        }
    }
}
