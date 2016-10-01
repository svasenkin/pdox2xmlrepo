using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParadoxReader;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

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
                ParadoxTable tbl = new ParadoxTable(Path.GetDirectoryName(dlg.FileName), Path.GetFileNameWithoutExtension(dlg.FileName));
                XElement root = new XElement("Data");
                string[] fnames = tbl.FieldNames;
                ParadoxRecord r = tbl.Enumerate().First<ParadoxRecord>();
                Type[] types = (from c in r.DataValues.AsEnumerable() select c.GetType()).ToArray();
                List<XAttribute> lst = new List<XAttribute>();

                for (int i = 0;i != fnames.Length; i++)
                {
                    lst.Add(new XAttribute(fnames[i], types[i].ToString()));
                }
                XElement hdr = new XElement("Header", null);
                hdr.Add(lst);
                root.Add(hdr);

                foreach(var rec in tbl.Enumerate())
                {
                    int i = 0;
                    XElement trec = new XElement("Record", from c in rec.DataValues.AsEnumerable()
                                                           select new XElement(fnames[i++], c));
                    root.Add(trec);
                }
                
                string newpath = Path.ChangeExtension(dlg.FileName, ".xml");
                root.Save(newpath);
                Console.WriteLine(newpath);

                Console.ReadKey();
            }

        }
    }
}
