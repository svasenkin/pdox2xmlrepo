using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParadoxReader;
using System.Windows.Forms;
using System.IO;

namespace pdox2xml
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Paradox tables (*.db)|*.db|All files (*.*)|*.*";
            dlg.Multiselect = false;

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                ParadoxTable tbl = new ParadoxTable(dlg.FileName, "tbl");
              /*  int recIndex = 0;
                foreach(var rec in tbl.Enumerate())
                {
                    Console.WriteLine("Record #{0}", recIndex++);
                    for (int i = 0; i < tbl.FieldCount; i++)
                    {
                        Console.WriteLine("    {0} = {1}", tbl.FieldNames[i], rec.DataValues[i]);
                    }
                    if (recIndex > 10) break;
                }
                */
                //tbl.

                string newpath = Path.ChangeExtension(dlg.FileName, ".xml");
                Console.WriteLine(newpath);

                Console.ReadKey();
            }

        }
    }
}
