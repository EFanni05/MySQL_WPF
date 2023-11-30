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
        List<Test> tests = new List<Test>();

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
                        Test x = new Test(int.Parse(reader.GetString(0)), reader.GetString(1));
                        tests.Add(x);
                    }
                    for (int i = 0; i < tests.Count; i++)
                    {
                        Lista.Items.Add(tests[i].ToString());
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
            if (NameText.Text.Trim() == "")
            {
                MessageBox.Show("Error: name");
            }
            else
            {
                Test x = new Test(tests.Count, NameText.Text);
                if(x.Name != null)
                {
                    tests.Add(x);
                    string sql = $"INSERT INTO `test` (`id`, `name`) VALUES (null, '{x.Name}')";
                    MySqlCommand command = new MySqlCommand( sql, connection);
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        Test v = tests.Find(y => y.Id == x.Id);
                        tests[v.Id].Id = int.Parse(reader.GetString(0));
                        command = new MySqlCommand($"SELECT * FROM `test`", connection);
                        connection.Close();
                        connection.Open();
                        reader = command.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Query unsuccessful");
                        }
                        else
                        {
                            Lista.Items.Clear();
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
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            string item = Lista.SelectedItems.ToString();
            if(item != "")
            {
                string v = item.Substring(0, 3).Replace('.', ' ').Trim();
                int id = int.Parse(v);
                Test x = new Test(id, item.Replace($"{v}.", " ").Trim());
                tests.Add(x);
                if(x.Name != null)
                {

                    string sql = $"UPDATE `test` SET `name`= '{x.Name}' WHERE id = {id}";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        command = new MySqlCommand("SELECT * FROM `test`", connection);
                        connection.Close();
                        connection.Open();
                        reader = command.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Query unsuccessful");
                        }
                        else
                        {
                            Lista.Items.Clear();
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
            else
            {
                MessageBox.Show("Error: no select");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string item = Lista.SelectedItem.ToString();
            if (item.Trim() == "")
            {
                MessageBox.Show("Error: no select");
            }
            else
            {
                string v = item.Substring(0, 3).Replace('.', ' ').Trim();
                int id = int.Parse(v);
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
