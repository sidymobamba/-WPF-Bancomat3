using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Bancomat3
{
    /// <summary>
    /// Interaction logic for AutenticaUtenteDialog.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly int idBanca;

        public Login(int idBanca)
        {
            InitializeComponent();
            this.idBanca = idBanca;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = password.Password;
            passwordTextBox.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Visibility = Visibility.Collapsed;
            password.Visibility = Visibility.Visible;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "" || password.Password == "")
            {
                MessageBox.Show("Inserisci sia il nome utente che la password.");
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("data source=B80MI-EMA119\\SQLEXPRESS;initial catalog=bancomat2;integrated security=True;");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Utenti WHERE NomeUtente = @username AND Password = @password AND IdBanca = @idBanca", conn);
                    cmd.Parameters.AddWithValue("username", username.Text);
                    cmd.Parameters.AddWithValue("password", password.Password);
                    cmd.Parameters.AddWithValue("idBanca", idBanca);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login riuscito!");
                        MenuPrincipale menu = new MenuPrincipale();
                        menu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nome utente, password o banca non validi.");
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore durante il tentativo di accesso: " + ex.Message);
                }
            }
        }

    }
}
