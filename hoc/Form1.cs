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
        List<String> list_of_header = new List<String>();
        List<String> list_of_new_header = new List<String>();
        List<String> final = new List<String>();
        String name_of_file = "";
        String name_of_file_with_type = "";
        List<String> name_file = new List<String>();
        String name_of_saved_file = "";
        int index_x;
        int index_y;
        String text_in_file;
        Stream fileStream;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void get_header_for_table(String text)
        {
            string[] words = text.Split('\n');
            int i = 0;
            Console.WriteLine("ko chay.....");
            foreach (var word in words)
            {
                if (i == 0)
                {
                    String ww = (String)word;
                    string[] wwordss = ww.Split(',');
                    foreach (var q in wwordss)
                    {
                        list_of_header.Add(q);
                    }
                    i++;
                    Console.WriteLine("lengh = " + list_of_header.Count);
                    return;
                }
            }
        }
        private void write_vcard()
        {
            using (StreamWriter wr = new StreamWriter(fileStream))
            {
                string[] words = text_in_file.Split('\n');
                int length_of_obj = words.Length;
                int i = 0;
                foreach (var word in words)
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    if(i == length_of_obj - 1)
                    {
                        Console.WriteLine("end of obj");
                        return;
                    }
                    wr.WriteLine("BEGIN:VCARD");
                    wr.WriteLine("VERSION:3.0");
                    String w = (String)word;
                    string[] wordss = w.Split(',');
                    int count = 0;
                    foreach (var q in wordss)
                    {
                    
                        Console.WriteLine("head"+list_of_new_header[count]);
                        if (list_of_new_header[count] == "Full Name")
                        {
                            wr.Write("FN:"+q);
                            if(count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Gender")
                        {
                            if(q == "Male")
                            {
                                wr.Write("GENDER:M");
                                if (count != wordss.Length - 1)
                                {
                                    wr.Write("\r");
                                }
                            }
                            else if(q == "Female")
                            {
                                wr.Write("GENDER:F");
                                if (count != wordss.Length - 1)
                                {
                                    wr.Write("\r");
                                }
                            }
                        }
                        else if (list_of_new_header[count] == "Birthday")
                        {
                            wr.Write("BDAY:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Nickname")
                        {
                            wr.Write("NICKNAME:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Email")
                        {
                            wr.Write("EMAIL:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Mobile phone")
                        {
                            wr.Write("TEL;TYPE=cell:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Home Address")
                        {
                            wr.Write("ADR;TYPE = home:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Business address")
                        {
                            wr.Write("ADR;TYPE = business:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        else if (list_of_new_header[count] == "Job title")
                        {
                            wr.Write("TITLE:" + q);
                            if (count != wordss.Length - 1)
                            {
                                wr.Write("\r");
                            }
                        }
                        count++;
                    }
                    wr.WriteLine("END:VCARD");
                    i++;
                }
                
            }
            
        }
        private void update_data_grid_view()
        {
            for(int i = 0; i < list_of_header.Count; i++)
            {
                Console.WriteLine("co vao day ne");
                properties_table.Rows.Add(list_of_header[i]);
                
            }
        }
        private void browse_button(object sender, EventArgs e)
        {
            int size = -1;
            properties_table.Rows.Clear();
            list_of_new_header.Clear();
            list_of_header.Clear();
            for(int i = 0; i < list_of_new_header.Count; i++)
            {
                Console.WriteLine("saU" + list_of_new_header[i]);
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                name_of_file = file;
                string[] name = name_of_file.Split('.');
                try
                {
                    text_in_file = File.ReadAllText(file);
                    size = text_in_file.Length;
                    Console.Write(text_in_file);
                    //list_obj = get_value_from_vcf(text_in_file);
                    get_header_for_table(text_in_file);
                    update_data_grid_view();
                    properties_table.MouseClick += new MouseEventHandler(mouse_click_handle);
                    csv_address.Text = name_of_file;
                    vcard_address.Text = name[0] + ".vcf";
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }
        private void create_save_file()
        {
            string[] wordss = name_of_file.Split('.');
            name_of_file_with_type = wordss[0];
            vcard_address.Text = name_of_file_with_type + ".vcf";
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = wordss[0];
            Console.WriteLine(saveFileDialog1.InitialDirectory);
            Console.WriteLine("file :" + vcard_address.Text);
            saveFileDialog1.FileName = vcard_address.Text;
            name_of_saved_file = saveFileDialog1.FileName;

            saveFileDialog1.DefaultExt = "vcf";
            saveFileDialog1.Filter = "(*.vcf)|*.vcf";

            fileStream = saveFileDialog1.OpenFile();

            vcard_address.Text = name_of_file_with_type + ".vcf";
        }
        private void convert_button(object sender, EventArgs e)
        {
            DataGridViewRow rw = new DataGridViewRow();
            DataGridViewColumn cl = properties_table.Columns[2];
            DataGridViewRow row = properties_table.Rows[properties_table.SelectedCells[0].RowIndex];
            Console.WriteLine("kich thuoc cua list_of_header: " + list_of_header.Count);
            for (int i = 0; i < list_of_header.Count; i++)
            {
                if (properties_table.Rows[i].Cells[1].Value == null)
                {
                    Console.WriteLine("bi null ne");
                    MessageBox.Show("Not set some attributes", "Alert");
                    return;
                }
                string s = properties_table.Rows[i].Cells[1].Value.ToString();
                ///Console.WriteLine(s);
                list_of_new_header.Add(s);
            }
            for (int i = 0; i < list_of_header.Count; i++)
            {
                Console.WriteLine(list_of_new_header[i]);
            }
            create_save_file();
            Console.WriteLine("textbox = " + vcard_address.Text);
            if (vcard_address.Text == "")
            {
                MessageBox.Show("Not set address of the file you want to save", "Alert");
                return;
            }
            
            write_vcard();
            Console.WriteLine("Done!!!");
            MessageBox.Show("Done!!!!", "Alert");
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
            DataGridViewRow rw = properties_table.Rows[index_x];
            rw.Cells[1].Value = e.ClickedItem.Name.ToString();
        }
        private void mouse_click_handle(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
            int position_x_mouse_row = properties_table.HitTest(e.X, e.Y).RowIndex;
            index_x = position_x_mouse_row;
            int position_y_mouse_row = properties_table.HitTest(e.X, e.Y).ColumnIndex;
            index_y = position_y_mouse_row;
 
            if (e.Button == MouseButtons.Left && position_x_mouse_row >= 0 && position_y_mouse_row > 1)
            {
                menu.Items.Add("Full Name").Name = "Full Name";
                menu.Items.Add("Nickname").Name = "Nickname";
                menu.Items.Add("Birthday").Name = "Birthday";
                menu.Items.Add("Gender").Name = "Gender";
                menu.Items.Add("Job title").Name = "Job title";
                menu.Items.Add("Mobile phone").Name = "Mobile phone";
                menu.Items.Add("Email").Name = "Email";
                menu.Items.Add("Business Address").Name = "Business Address";
                menu.Items.Add("Home Address").Name = "Home Address";
                
            }
            menu.Show(properties_table, new Point(e.X, e.Y));
            menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_item_click);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void save_as_button(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string[] wordss = name_of_file.Split('.');
            saveFileDialog1.InitialDirectory = wordss[0];
            Console.WriteLine(saveFileDialog1.InitialDirectory);
            Console.WriteLine("file :" + vcard_address.Text);
            //saveFileDialog1.FileName = vcard_address.Text;
            name_of_saved_file = saveFileDialog1.FileName;
            Console.WriteLine(name_of_saved_file);
            saveFileDialog1.DefaultExt = "vcf";
            saveFileDialog1.Filter =  "(*.vcf)|*.vcf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileStream = saveFileDialog1.OpenFile();
            }
            vcard_address.Text = name_of_file_with_type + ".vcf";
        }

        private void close_button(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
