using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace hoc
{
    public partial class Form1 : Form
    {
        List<VCF_File> list_obj = new List<VCF_File>();
        List<String> list_of_header = new List<String>();
        List<String> list_of_new_header = new List<String>();
        List<String> final = new List<String>();
        String name_of_file = "";
        String name_of_file_with_type = "";
        int index_x;
        int index_y;
        Stream fileStream;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private List<VCF_File> get_value_from_vcf(String text)
        {
            List<VCF_File> arr = new List<VCF_File>();
            string[] words = text.Split('\n');
            int i = 0;
            Console.WriteLine("ko chay ak");
            foreach (var word in words)
            {
                VCF_File new_obj = new VCF_File();
                if (i == 0)
                {
                    String ww = (String)word;
                    string[] wwordss = ww.Split(',');
                    foreach (var q in wwordss)
                    {
                        list_of_header.Add(q);
                    }
                    i++;
                    
                    Console.WriteLine("lengh = " + list_of_new_header.Count);
                    continue;
                }
                String w = (String)word;
                string[] wordss = w.Split(',');
                int count = 0;
                foreach (var q in wordss)
                {
                    if(count == 0)
                    {
                        new_obj.set_ID((String)q);
                    }
                    else if(count == 1)
                    {
                        new_obj.set_name(q);
                    }
                    else
                    {
                        new_obj.set_age(Int32.Parse(q));
                    }
                    count++;
                }
                arr.Add(new_obj);
                i++;
            }
            return arr;
        }
        private void write_vcard(List<VCF_File> arr)
        {
            using (StreamWriter wr = new StreamWriter(fileStream))
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    wr.WriteLine("BEGIN:VCARD");
                    /*wr.WriteLine("VERSION:3.0");
                    wr.WriteLine("N:" + arr[i].get_name() + ";");
                    wr.WriteLine("END:VCARD");*/
                }
            }
        }
        private void update_data_grid_view()
        {
            for(int i = 0; i < list_of_header.Count; i++)
            {
                Console.WriteLine("co vao day ne");
                dataGridView1.Rows.Add(list_of_header[i]);
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                name_of_file = file;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    Console.Write(text);
                    list_obj = get_value_from_vcf(text);
                    update_data_grid_view();
                    dataGridView1.MouseClick += new MouseEventHandler(mouse_click_handle);
                    textBox1.Text = name_of_file;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow rw = new DataGridViewRow();
            DataGridViewColumn cl = dataGridView1.Columns[2];
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            for (int i = 0; i < list_of_header.Count; i++)
            {
                string s = dataGridView1.Rows[i].Cells[1].Value.ToString();
                ///Console.WriteLine(s);
                list_of_new_header.Add(s);
            }
            for (int i = 0; i < list_of_header.Count; i++)
            {
                Console.WriteLine(list_of_new_header[i]);
            }
            write_vcard(list_obj);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.MouseClick += new MouseEventHandler(mouse_click_handle);
            
        }
        private void menu_item_click(object sender, ToolStripItemClickedEventArgs e)
        {
            //list_of_new_header.Add(e.ClickedItem.Name.ToString());
            //list_of_new_header.Add(e.ClickedItem.Name.ToString());
            DataGridViewRow rw = dataGridView1.Rows[index_x];
            rw.Cells[1].Value = e.ClickedItem.Name.ToString();
       
        }
        private void mouse_click_handle(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
            int position_x_mouse_row = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            index_x = position_x_mouse_row;
            int position_y_mouse_row = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            index_y = position_y_mouse_row;
 
            if (e.Button == MouseButtons.Left && position_x_mouse_row >= 0 && position_y_mouse_row > 1)
            {
                menu.Items.Add("Name").Name = "Name";
                menu.Items.Add("ID").Name = "ID";
                menu.Items.Add("Age").Name = "Age";
                menu.Items.Add("Hu").Name = "A";
            }
            menu.Show(dataGridView1, new Point(e.X, e.Y));
            menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_item_click);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string[] wordss = name_of_file.Split('.');
            name_of_file_with_type = wordss[0];
            saveFileDialog1.InitialDirectory = wordss[0];
            Console.WriteLine(saveFileDialog1.InitialDirectory);
            saveFileDialog1.FileName = wordss[0] + ".vcf";
            saveFileDialog1.DefaultExt = "vcf";
            saveFileDialog1.Filter =  "(*.vcf)|*.vcf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileStream = saveFileDialog1.OpenFile();
            }
            textBox2.Text = name_of_file_with_type + ".vcf";
        }
    }
}
