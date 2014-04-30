using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using System.Windows.Forms;
using WpfApplication6.winformCtrl;


namespace WpfApplication6
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Background = Brushes.Gray;
        }
        
        private DrawingPanel m_Panel;
        private DrawingInBmp m_bitMap;
        private void windowsload(object sender, RoutedEventArgs e)
        {
            //m_Panel = new DrawingPanel(this);
            m_bitMap = new DrawingInBmp(this);
            Display.Background = Brushes.Gray;
            Command.Background = Brushes.Green;
        }

        private void Button_Begin_Click(object sender, RoutedEventArgs e)
        {
            if (m_Panel != null)
                m_Panel.BeginDrawing();

            if (m_bitMap != null)
                m_bitMap.BeginDrawing();

        }

        private void Button_End_Click(object sender, RoutedEventArgs e)
        {
            if (m_Panel != null)
                m_Panel.EndDrawing();

            if (m_bitMap != null)
                m_bitMap.EndDrawing();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                System.Windows.Forms.MessageBox.Show("del is processed by MainWindow");
                //e.Handled = true;
            }
        }
    }
}
