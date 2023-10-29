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

namespace Bancomat3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bancomatEntities context;
        private Utenti utente;
        private bool loggedOut = true;

        public MainWindow()
        {
            InitializeComponent();
            context = new bancomatEntities();
        }

        private void BPM_btn(object sender, RoutedEventArgs e)
        {
            GoToLogin(1);
            this.Close();
        }
        private void CheBanca_btn(object sender, RoutedEventArgs e)
        {
            GoToLogin(2);
            this.Close();
        }
        private void CreditAgricole_btn(object sender, RoutedEventArgs e)
        {
            GoToLogin(3);
            this.Close();
        }
        private void Fineco_btn(object sender, RoutedEventArgs e)
        {
            GoToLogin(5);
            this.Close();
        }
        private void IntesaSanPaolo_btn(object sender, RoutedEventArgs e)
        {
            GoToLogin(6);
            this.Close();
        }

        private void GoToLogin(int idBanca)
        {
            Login login = new Login(idBanca);
            login.ShowDialog(); // Usa ShowDialog() per bloccare l'esecuzione fino a quando la finestra di login non viene chiusa
        }

       
    }
}
