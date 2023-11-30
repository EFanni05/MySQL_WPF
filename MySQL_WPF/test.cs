using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySQL_WPF
{
    internal class Test
    {
        private int id;
        private string name;

        public Test(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name;
            set
            {
                string test = value;
                if (test.ToLower().Contains("drop") || test.ToLower().Contains("update") 
                    || test.ToLower().Contains("insert") || test.ToLower().Contains("select"))
                {
                    MessageBox.Show($"{value} is not correct");
                }
                else
                {
                    name = test;
                }
            } 
        }

        public override string? ToString()
        {
            return $"{id} - {name}";
        }
    }
}
