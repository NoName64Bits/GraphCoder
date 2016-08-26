using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;
using System.IO;

namespace GraphCoder
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textEditorControl1_Load(object sender, EventArgs e)
        {
            string dirc = Application.StartupPath + "/lang";
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(dirc))
            {
                fsmp = new FileSyntaxModeProvider(dirc);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                textEditorControl1.SetHighlighting("C#");
            }

            textEditorControl1.Text = Data.code;
        }
    }
}
