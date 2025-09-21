using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocabulary
{
    public partial class Form1: Form
    {
        private Dictionary<string, string> internetSlangDict = new Dictionary<string, string>();
        private string filePath = "slang.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // dictionary[Key] = Value

            string word = txtWord.Text.Trim();
            string meaning = txtMeaning.Text;

            if (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(meaning))
            {
                internetSlangDict[word] = meaning; // o kak
                txtWord.Clear();
                txtMeaning.Clear();
                UpdateList();
            }
            else
            {
                MessageBox.Show("Ti debil?");
            }
        }

        private void UpdateList()
        {
            listBox1.Items.Clear();
            foreach(var pair in internetSlangDict)
            {
                // метод інтерполяції
                listBox1.Items.Add($"{pair.Key} - {pair.Value}");// (KeyEx) - (ValueEx)
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {                                                 //(сепаратор)[ітератор листа].Trim
                string word = listBox1.SelectedItem.ToString().Split('-')[0].Trim();//list ["KeyEx "," ValueEx"]
                internetSlangDict.Remove(word); // if(dict.Remove(word)
                UpdateList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            
            if (search != "")
            {
                listBox1.Items.Clear();

                foreach (var pair in internetSlangDict)
                {
                    if (pair.Key.StartsWith(search))
                    {
                        listBox1.Items.Add($"{pair.Key} - {pair.Value}");
                    }
                }
                if(listBox1.Items.Count == 0)
                {
                    MessageBox.Show("404 not found");
                    UpdateList();
                }
            }
            else if (search == "")
            {
                UpdateList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach(var pair in internetSlangDict)
                {
                    // cout << stroing;
                    // file << string
                    // Console.WriteLine == cout 
                    sw.WriteLine($"{pair.Key}:{pair.Value}");  // "wearher":"sunny"
                }
            } // 800kb

            //600kb
            MessageBox.Show("Dictionary saved");

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists(filePath))
            {
                internetSlangDict.Clear();

                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(':'); 
                    if(parts.Length == 2)
                    {
                        internetSlangDict[parts[0]] = parts[1];  // додаєш або оновлюєш існуючий
                    }
                }
                UpdateList();

                MessageBox.Show("List has been loaded");
            }
        }
    }
}
