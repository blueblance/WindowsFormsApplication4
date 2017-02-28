using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace delegate_practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public delegate string testhandler1(string a);
        public delegate void tbstringhandler(string a, Settextboxcallback sb, TextBox tb);
        public delegate void testhand();
        public delegate void loophander(TextBox tb, int i);
        public delegate void loophandler2();
        public delegate void mipihander(mipi mipi);
        BackgroundWorker bb = null;
        Thread workThread = null;
        private void button1_Click(object sender, EventArgs e)
        {
            List<Thread> threadlist = new List<Thread>();
            mipi m1 = new mipi(500, textBox1);
            mipi m2 = new mipi(50, textBox2);
            //testhand tt = test1;
            //tt += test2;
            //tt();
            //Settextboxcallback sb = Settextbox;
            //sb(textBox1, "hello 1");
            //Thread t1 = new Thread(delegate () { mipiloop(m1); });
            //threadlist.Add(t1);
            //Thread t2 = new Thread(delegate () { mipiloop2(m2); });
            //threadlist.Add(t2);
            //foreach(Thread tt in threadlist )
            //{
            //    tt.Start();
            //}
            //mipihander tt = mipiloop2;
            //tt += mipiloop;
            //tt(m1);
            //Thread t1 = new Thread(delegate () { mipiloop(m1); });
            ThreadStart tstar = new ThreadStart(nofunc);
            tstar += nofunc;
            Thread t2 = new Thread(tstar);


            //t1.Start();
            //t2.Start();
            //t1.Join();
            //loophander lp = loop_test1;
            //lp += loop_test2;

            //lp(textBox1, 50);

        }

        public void nofunc()
        {
            for (int i = 0; i < 100; i++)
            {
                Settextbox(textBox1, i.ToString());
                Thread.Sleep(100);
            }

        }
        public void mipiloop(mipi mipi)
        {
            for (int i = 0; i < mipi.bitrate; i++)
            {
                Settextbox(mipi.tb, i.ToString());
                Thread.Sleep(100);
            }
        }
        public void mipiloop2(mipi mipi)
        {
            for (int i = 0; i < mipi.bitrate; i++)
            {
                Settextbox(mipi.tb, (i * 2).ToString());
                Thread.Sleep(100);
            }
        }

        public void loop_test1(TextBox tb, int i)
        {
            for (int a = 0; a < i; a++)
            {
                Settextbox(tb, i.ToString());
                Thread.Sleep(100);
            }
        }

        public void loop_test2(TextBox tb, int i)
        {
            for (int a = 0; a < i; a++)
                Settextbox(tb, (i * 2).ToString());
        }

        public void test1()
        {
            Settextbox(textBox1, "test1");
        }

        public void test2()
        {
            Settextbox(textBox2, "test2");
        }

        public delegate void Settextboxcallback(TextBox tb, String data);
        public void Settextbox(TextBox tb, String data)
        {
            if (tb.InvokeRequired)
            {
                Settextboxcallback Sb = new Settextboxcallback(Settextbox);
                tb.BeginInvoke(Sb, new object[] { tb, data });
            }
            else
            {
                tb.Text = data;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            workThread.Abort();
            workThread = null;

            bb.Dispose();
            bb = null;

        }

        void stop_thread(object sender, EventArgs e)
        {
            if (e is Form1)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bb == null && workThread == null)
            {
                mipihander mm = mipiloop;
                mipi m1 = new mipi(500, textBox1);
                bb = new BackgroundWorker();
                bb.DoWork += (delegate (object workObject, DoWorkEventArgs workEvent) {
                    workThread = Thread.CurrentThread;
                    mipiloop(m1);

                });
                bb.RunWorkerAsync();
            }
        }
    }

    public class mipi
    {
        public int bitrate;
        public TextBox tb;

        public mipi(int a, TextBox tb)
        {
            bitrate = a;
            this.tb = tb;
        }
    }
}
