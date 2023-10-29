using System;
using System.Data.SqlClient;
using System.Windows;

namespace Bancomat3
{
    /// <summary>
    /// Interaction logic for Saldo.xaml
    /// </summary>
    public partial class Saldo : Window
    {
        public Saldo()
        {
            InitializeComponent();
            var saldoInfo = GetSaldoAndUltimaDataVersamentoFromDatabase();

            saldoLabel.Content = $"Saldo: {saldoInfo.Saldo:C}";
            ultimoVersamentoLabel.Content = $"Data dell'ultimo versamento: {saldoInfo.UltimaDataVersamento:d}";
            dataAttualeLabel.Content = $"Data e ora attuali: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
        }

        private SaldoInfo GetSaldoAndUltimaDataVersamentoFromDatabase()
        {
            SaldoInfo saldoInfo = new SaldoInfo();

            string connectionString = "data source=B80MI-EMA119\\SQLEXPRESS;initial catalog=bancomat2;integrated security=True;";
            string query = "SELECT Saldo, DataUltimaOperazione FROM ContiCorrente WHERE IdUtente = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            saldoInfo.Saldo = reader.GetInt32(0);
                            saldoInfo.UltimaDataVersamento = reader.GetDateTime(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante il recupero del saldo: {ex.Message}");
                }
            }

            return saldoInfo;
        }

        private void IndietroButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipale menu = new MenuPrincipale();
            menu.Show();
            this.Close();
        }

        public class SaldoInfo
        {
            public int Saldo { get; set; }
            public DateTime UltimaDataVersamento { get; set; }
        }
    }
}
