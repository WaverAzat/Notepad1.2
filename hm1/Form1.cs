using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using hm1;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public int fontSize = 0;
        public System.Drawing.FontStyle fs = FontStyle.Regular; 
        public string filename;
        public bool isFileChanged;
        public FontSettings fontSetts;
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
        }
        public void CreateNewDocument(object sender,EventArgs e )
        {
            SaveUnsavedFile();
            textBox1.Text = "";
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
        }
        public void OpenFile(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                    isFileChanged = false; 
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл!");
                }
            }
            UpdateTextWithTitle();
        }
        public void SaveFile(string _filename)
        {
            if (_filename == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw = new StreamWriter(_filename + ".txt");
                sw.Write(textBox1.Text);
                sw.Close(); 
                filename = _filename;
                isFileChanged = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл!");
            }
            UpdateTextWithTitle();
        }
        public void Save(object sender, EventArgs e)
        {
            SaveFile(filename);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }

        private void OnTextChanged(object sender, EventArgs e)
        { 
            if (!isFileChanged)
            {
                this.Text = this.Text.Replace('*', ' ');
                isFileChanged = true;
                this.Text = "*" + this.Text;
            }
        }
        public void UpdateTextWithTitle()
        {
            if (filename != "")
            this.Text = filename + " - Блокнот";
            else this.Text = "Безымянный  - Блокнот";

        }
        public void SaveUnsavedFile()
        {
            if (isFileChanged)
            {
              DialogResult result = MessageBox.Show("Сохранить изменения в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
              if (result == DialogResult.Yes)
              {
                    SaveFile(filename);
              }
            }
        }
        public void CopyText()
        {
            if (textBox1.SelectedText.Length != 0)
            {
                Clipboard.SetText(textBox1.SelectedText);
            }
        }
        public void CutText()
        {
            if (textBox1.SelectedText.Length != 0)
            {
                Clipboard.SetText(textBox1.SelectedText);
                textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
            }
        }
        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length- textBox1.SelectionStart);
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            CutText();
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
             PasteText();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUnsavedFile();
        }

        private void OnFontClick(object sender, EventArgs e)
        {
            fontSetts = new FontSettings();
            fontSetts.Show();
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if (fontSetts != null)
            {
                fontSize = fontSetts.fontSize;
                fs = fontSetts.fs;
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize, fs);
                fontSetts.Close();
            }
        }

        private void OnStatusProg(object sender, EventArgs e)
        {
            MessageBox.Show("Программа была создана недопрограммистом.", "Об этом блокноте", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnWhiteStyle(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.BackColor = Color.White;
        }

        private void OnDarkStyle(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.White;
            textBox1.BackColor = Color.Black;
        }

        private void WhiteRed(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Red;
            textBox1.BackColor = Color.White;
        }

        private void WhiteBlue(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Blue;
            textBox1.BackColor = Color.White;
        }

        private void WhiteGreen(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Green;
            textBox1.BackColor = Color.White;
        }

        private void DarkPink(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Pink;
            textBox1.BackColor = Color.Black;
        }

        private void DarkRed(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Red;
            textBox1.BackColor = Color.Black;
        }

        private void DarkBlue(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Blue;
            textBox1.BackColor = Color.Black;
        }
    }
}
