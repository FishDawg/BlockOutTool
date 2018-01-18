using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FishDawg.BlockOut
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            try
            {
                string text = ReadConfigFile();
                var stream = CreateConfigStream(text);

                using (var reader = JsonReaderWriterFactory.CreateJsonReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    if (reader.Read())
                    {
                        var rootElement = XElement.Load(reader);
                        var blockElements = rootElement.XPathSelectElements("//blocks/item");

                        int defaultPosition = 100;
                        foreach (var blockElement in blockElements)
                        {
                            var x = ReadElementValue(blockElement, "x", defaultPosition);
                            var y = ReadElementValue(blockElement, "y", defaultPosition);
                            var width = ReadElementValue(blockElement, "width", 100);
                            var height = ReadElementValue(blockElement, "height", 100);
                            var color = ReadElementValue(blockElement, "color");
                            var opacity = ReadElementValue(blockElement, "opacity", 100);

                            BlockForm form = new BlockForm();
                            form.Location = new Point(x, y);
                            form.Size = new Size(width, height);
                            form.BackColor = color;
                            form.Opacity = opacity / 100.0;
                            form.Show(this);

                            defaultPosition += 100;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }
        }

        private int ReadElementValue(XElement parentElement, string name, int defaultValue)
        {
            var element = parentElement.XPathSelectElement(name);

            int result;
            if (int.TryParse(element.Value, out result))
            {
                return result;
            }
            return defaultValue;
        }

        private Color ReadElementValue(XElement parentElement, string name)
        {
            var element = parentElement.XPathSelectElement(name);
            var color = Color.FromName(element.Value);
            if (!color.IsKnownColor || color.A < 255)
            {
                color = Color.Black;
            }
            return color;
        }

        private static string ReadConfigFile()
        {
            using (var stream = File.Open("BlockOutToolConfig.json", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static Stream CreateConfigStream(string text)
        {
            var stream = new MemoryStream(text.Length);
            var writer = new StreamWriter(stream, new UTF8Encoding(false));
            writer.Write(text);
            writer.Flush();
            stream.Seek(0L, SeekOrigin.Begin);

            return stream;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
