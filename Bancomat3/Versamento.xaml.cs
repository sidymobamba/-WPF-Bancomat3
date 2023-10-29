using System;
using System.Collections.Generic;
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
    /// Interaction logic for Versamento.xaml
    /// </summary>
    public partial class Versamento : Window
    {
        public Versamento()
        {
            InitializeComponent();
        }

        private void Versamento_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni l'importo da TextBox (assicurati che sia un valore valido)
            if (decimal.TryParse(importo.Text, out decimal versamentoAmount))
            {
                if (versamentoAmount > 0)
                {
                    // Connessione al database
                    string connectionString = "data source=B80MI-EMA119\\SQLEXPRESS;initial catalog=bancomat2;integrated security=True;";  
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            // Esegui il versamento nel database
                            string query = "UPDATE ContiCorrente SET Saldo = Saldo + @VersamentoAmount WHERE IdUtente = 1"; 

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@VersamentoAmount", versamentoAmount);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"Hai versato {versamentoAmount:C} con successo.");

                                    MenuPrincipale menu = new MenuPrincipale();
                                    menu.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Impossibile effettuare il versamento. Verifica i dettagli del conto.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Errore durante il versamento: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("L'importo del versamento deve essere superiore a zero.");
                }
            }
            else
            {
                MessageBox.Show("Inserisci un importo valido per il versamento.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipale menu = new MenuPrincipale();
            menu.Show();
            this.Close();
        }
    }
}
