using MySql.Data.MySqlClient;
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

namespace MySQL_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MySqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            Query("SELECT * FROM `test`");
        }

        private static void Query(string sql)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "127.0.0.1";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "testing";
            connection = new MySqlConnection(builder.ToString());
            MySqlCommand command = new MySqlCommand(sql, connection);
            try { 
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if(!reader.HasRows)
                {
                    MessageBox.Show("Query successful");
                }
                else
                {
                    while (reader.Read())
                    {
                        
                        ($"{reader.GetString(0)} - {reader.GetString(1)}");
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show($"Query Error [{ex.Message}]");
            }

        }

        private void Add(object sender, RoutedEventArgs e)
        {
        }

        private void Update(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
