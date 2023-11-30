using Microsoft.VisualBasic;
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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

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

        private void Query(string sql)
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
                        
                        Lista.Items.Add($"{reader.GetString(0)} - {reader.GetString(1)}");
                    }
                }
                connection.Close();
            }catch (Exception ex)
            {
                MessageBox.Show($"Query Error [{ex.Message}]");
            }

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IDText.Text.Trim(), out int id))
            {
                MessageBox.Show("Error: ID");
            }
            else if (NameText.Text.Trim() == "")
            {
                MessageBox.Show("Error: name");
            }
            else
            {
                string sql = $"INSERT INTO `test` (`id`, `name`) VALUES ('{id}', '{NameText.Text}')";
                MySqlCommand command = new MySqlCommand( sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    command = new MySqlCommand($"SELECT * FROM `test` WHERE id = {id}", connection);
                    connection.Close();
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Query unsuccessful");
                    }
                    else
                    {
                        while (reader.Read())
                        {

                            Lista.Items.Add($"{reader.GetString(0)} - {reader.GetString(1)}");
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Query Error [{ex.Message}]");
                }
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IDText.Text.Trim(), out int id))
            {
                MessageBox.Show("Error: ID");
            }
            else if (NameText.Text.Trim() == "")
            {
                MessageBox.Show("Error: name");
            }
            else
            {
                string sql = $"UPDATE `test` SET `name`= '{NameText.Text}' WHERE id = {id}";
                MySqlCommand command = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    command = new MySqlCommand($"SELECT * FROM `test` WHERE id = {id}", connection);
                    connection.Close();
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Query unsuccessful");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Lista.Items.Add($"{reader.GetString(0)} - {reader.GetString(1)}");
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Query Error [{ex.Message}]");
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IDText.Text.Trim(), out int id))
            {
                MessageBox.Show("Error: ID");
            }
            else
            {
                string sql = $"DELETE FROM `test` WHERE id = {id}";
                MySqlCommand command = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    MessageBox.Show("Successful deletion");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Query Error [{ex.Message}]");
                }
            }
        }
    }
}
