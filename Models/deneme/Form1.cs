namespace deneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
                listBox1.Items.Add(textBox1.Text);
            else
                MessageBox.Show("Lütfen bir metin giriniz.");
        }
    }
}
