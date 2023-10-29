using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Bancomat3
{
    /// <summary>
    /// Interaction logic for RegistroOperazioni.xaml
    /// </summary>
    public partial class RegistroOperazioni : Window
    {
        public RegistroOperazioni(string nomeUtente)
        {
            InitializeComponent();
            string nomeUtenteDesiderato = "dario";
            int idUtente = GetUserIdFromNomeUtente(nomeUtenteDesiderato);
            if (idUtente != -1)
            {
                LoadRegistroOperazioni(1);
            }
            else
            {
                MessageBox.Show("Utente non trovato nel database.");
                Close();
            }
        }

        private int GetUserIdFromNomeUtente(string nomeUtente)
        {
            // Sostituisci con la tua stringa di connessione al database
            string connectionString = "data source=B80MI-EMA119\\SQLEXPRESS;initial catalog=bancomat2;integrated security=True;";

            string getUserIdQuery = "SELECT IdUtente FROM Utenti WHERE NomeUtente = @NomeUtente";

            int idUtente = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                {
                    getUserIdCommand.Parameters.AddWithValue("@NomeUtente", nomeUtente);
                    connection.Open();

                    object result = getUserIdCommand.ExecuteScalar();
                    if (result != null)
                    {
                        idUtente = (int)result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il recupero dell'IdUtente: {ex.Message}");
            }

            return idUtente;
        }

        private void LoadRegistroOperazioni(int idUtente)
        {
            string connectionString = "data source=B80MI-EMA119\\SQLEXPRESS;initial catalog=bancomat2;integrated security=True;";

            string query = "SELECT DISTINCT BF.IdBanca, F.Nome, M.Quantita, M.DataOperazione " +
                "FROM Utenti AS U " +
                "JOIN Banche_Funzionalita AS BF ON U.IdBanca = BF.IdBanca " +
                "JOIN Funzionalita AS F ON BF.IdFunzionalita = F.Id " +
                "JOIN Movimenti AS M ON M.NomeUtente = U.NomeUtente " +
                "WHERE U.Id = 1;";


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUtente", idUtente);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nomeBanca = reader.GetString(0);
                            string funzionalita = reader.GetString(1);
                            int quantita = reader.GetInt32(2);
                            DateTime dataOperazione = reader.GetDateTime(3);

                            // Aggiungi l'operazione alla ListView
                            registroListView.Items.Add(new
                            {
                                NomeBanca = nomeBanca,
                                Funzionalita = funzionalita,
                                Quantità = quantita,
                                DataOperazione = dataOperazione
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il recupero dei registri delle operazioni: {ex.Message}");
            }
        }

        private void IndietroButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipale menu = new MenuPrincipale();
            menu.Show();
            Close();
        }
    }
}
