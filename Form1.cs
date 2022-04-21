using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_lab_wpf
{
    public partial class Form1 : Form
    {
        public static KeyValuePair<int, string>[] queueWithPriority = new KeyValuePair<int, string>[0];
        static string[] GetItemsQueue()
        {
            var tmp = new KeyValuePair<int, string>[queueWithPriority.Length];
            for (int i = 0; i < queueWithPriority.Length; i++)
            {
                tmp[i] = queueWithPriority[i];
            }
            string[] queue = new string[tmp.Length];
            for (int i = 0; i < queueWithPriority.Length; i++)
            {
                var tmpp = ExtractMax(ref tmp);
                queue[i] = $"{tmpp.Value.PadRight(25)}  {tmpp.Key}";
            }
            return queue;
        }
        static void Insert(int key, string value,ref KeyValuePair<int, string>[] queue)
        {
            Array.Resize(ref queue, queue.Length + 1);
            queue[queue.Length - 1] = new KeyValuePair<int, string>(key, value);
            SiftUp(queue.Length - 1, ref queue);
        }
        static KeyValuePair<int, string> ExtractMax(ref KeyValuePair<int, string>[] queue)
        {
            KeyValuePair<int, string> result = queue[0];
            queue[0] = queue[queue.Length - 1];
            Array.Resize(ref queue, queue.Length - 1);
            SiftDown(0,ref queue);
            return result;
        }
        static void Remove(int i, ref KeyValuePair<int, string>[] queue)
        {
            queue[i] = new KeyValuePair<int, string>(int.MaxValue, queue[i].Value);
            SiftUp(i, ref queue);
            ExtractMax(ref queue);
        }
        static void SiftUp(int i, ref KeyValuePair<int, string>[] queue)
        {
            while (i > 0 && queue[Parentt(i)].Key < queue[i].Key)
            {
                KeyValuePair<int, string> tmp = queue[Parentt(i)];
                queue[Parentt(i)] = queue[i];
                queue[i] = tmp;

                i = Parentt(i);
            }
        }
        static void SiftDown(int i,ref  KeyValuePair<int, string>[] queue)
        {

            int maxIndex = i;
            int left = LeftChild(i);
            if (left <= queue.Length - 1 && queue[left].Key > queue[maxIndex].Key)
            {
                maxIndex = left;
            }
            int right = RightChild(i);
            if (right <= queue.Length - 1 && queue[right].Key > queue[maxIndex].Key)
            {
                maxIndex = right;
            }
            if (i != maxIndex)
            {
                KeyValuePair<int, string> tmp = queue[i];
                queue[i] = queue[maxIndex];
                queue[maxIndex] = tmp;
                SiftDown(maxIndex,ref queue);
            }
        }
        static int Parentt(int i) => i / 2;
        static int LeftChild(int i) => 2 * i;
        static int RightChild(int i) => 2 * i + 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value = textBox1.Text;
            int key = int.Parse(textBox2.Text);
            Insert(key, value, ref queueWithPriority);
            textBox1.Clear();
            textBox2.Clear();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(GetItemsQueue());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{queueWithPriority[0].Value} {queueWithPriority[0].Key}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExtractMax(ref queueWithPriority);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(GetItemsQueue());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!(listBox1.SelectedItem is null))
            {
                string[] subs = listBox1.SelectedItem.ToString().Split();
                int indexRemove = 0;
                for (int i = 0; i < queueWithPriority.Length; i++)
                {
                    if (queueWithPriority[i].Value == subs[0]) indexRemove = i;
                }
                Remove(indexRemove, ref queueWithPriority);
                listBox1.Items.Clear();
                listBox1.Items.AddRange(GetItemsQueue());
            }
            
        }
        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
