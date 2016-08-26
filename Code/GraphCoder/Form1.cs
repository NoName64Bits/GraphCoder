using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.IO;
using System.Diagnostics;

namespace GraphCoder
{
    public partial class Form1 : Form
    {
        Compiler compiler = new Compiler();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            compiler.compile(codeEdit.Text, "C:\\Users\\NoName\\Desktop\\plot1.exe");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void codeEdit_Load(object sender, EventArgs e)
        {
            string dirc = Application.StartupPath + "/lang";
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(dirc))
            {
                fsmp = new FileSyntaxModeProvider(dirc);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                codeEdit.SetHighlighting("C#");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog1.FileName, codeEdit.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            codeEdit.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Data.code = "";
            File.Delete(Application.StartupPath + "\\temp.exe");

            Data.code += File.ReadAllText(Application.StartupPath + "\\templates\\one.par");
            Data.code += "\n";
            Data.code += ("\t\tpublic static int g = " + Info.scale.ToString() + ";\n");
            Data.code += File.ReadAllText(Application.StartupPath + "\\templates\\three.par");
            Data.code += "\n";
            Data.code += ("\t\t\tthis.Size = new Size(" + Info.size.Height.ToString() + ", " +
                Info.size.Width.ToString() + ");\n\t\t\tthis.Text = \"" + Info.name + "\";\n");
            Data.code += File.ReadAllText(Application.StartupPath + "\\templates\\for.par");
            Data.code += "\n";
            Data.code += ("\t\t\tpicGraph.Size = new Size(" + Info.size.Height.ToString() + ", " +
                Info.size.Width.ToString() + ");");
            Data.code += File.ReadAllText(Application.StartupPath + "\\templates\\five.par");
            Data.code += "\n";
            foreach(string line in codeEdit.Text.Split('\n'))
            {
                Data.code += ("\t\t" + line + "\n");
            }
            Data.code += File.ReadAllText(Application.StartupPath + "\\templates\\two.par");
            Data.code += "\n";
            
            if(toolStripComboBox1.Text == "Build")
            {
                compiler.compile(Data.code, Application.StartupPath + "\\temp.exe");
            }
            else if(toolStripComboBox1.Text == "Generate template code")
            {
                Form2 frm = new Form2();
                frm.Show();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\temp.exe");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            File.Copy(Application.StartupPath + "\\temp.exe", saveFileDialog2.FileName);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            fr.Show();
        }
    }

    public class Data
    {

        public static string code = "";

        public Data()
        {

        }
    }

    public class Compiler
    {
        public Compiler()
        {

        }

        public void compile(String input, String output)
        {
            MessageBox.Show(output);
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll", "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll", "System.Xml.dll", "System.Xml.Linq.dll", "System.Data.dll", "System.Data.DataSetExtensions.dll" }, output, true);
            parameters.GenerateExecutable = true;
            CompilerResults results = csc.CompileAssemblyFromSource(parameters, input);
            results.Errors.Cast<CompilerError>().ToList().ForEach((error) => { MessageBox.Show(error.ToString()); });
        }

    }
}
