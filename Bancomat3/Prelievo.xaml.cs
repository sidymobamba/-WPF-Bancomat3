using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Bancomat3
{
    public partial class Prelievo : Window
    {
        public Prelievo()
        {
            InitializeComponent();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipale menu = new MenuPrincipale();
            menu.Show();
            this.Close(); 
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Ottieni l'importo da TextBox (assicurati che sia un valore valido)
            if (decimal.TryParse(importo.Text, out decimal prelievoAmount))
            {
                // Connessione al database
                string connectionString = "data source=B80MI-EMA119\\SQLEXPRESS;initial catalog=bancomat2;integrated security=True;"; 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "UPDATE ContiCorrente SET Saldo = Saldo - @PrelievoAmount WHERE IdUtente = 1"; 

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PrelievoAmount", prelievoAmount);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Hai prelevato {prelievoAmount:C} con successo."); 
                                MenuPrincipale menu = new MenuPrincipale();
                                menu.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Impossibile effettuare il prelievo. Verifica il saldo disponibile.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore durante il prelievo: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Inserisci un importo valido per il prelievo.");
            }
        }
        private void importo_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
