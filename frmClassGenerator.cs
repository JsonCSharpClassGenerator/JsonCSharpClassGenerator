// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xamasoft.JsonClassGenerator.CodeWriters;



namespace Xamasoft.JsonClassGenerator.UI
{
    public partial class frmCSharpClassGeneration : Form
    {




        public frmCSharpClassGeneration()
        {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            Program.InitAppServices();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var b = new FolderBrowserDialog())
            {
                b.ShowNewFolderButton = true;
                b.SelectedPath = edtTargetFolder.Text;
                b.Description = "Please select a folder where to save the generated files.";
                if (b.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    edtTargetFolder.Text = b.SelectedPath;
                }

            }
        }

        private void frmCSharpClassGeneration_FormClosing(object sender, FormClosingEventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.UseProperties = radProperties.Checked;
            settings.InternalVisibility = radInternal.Checked;
            settings.SecondaryNamespace = edtSecondaryNamespace.Text;

            if (radNestedClasses.Checked) settings.NamespaceStrategy = 0;
            else if (radSameNamespace.Checked) settings.NamespaceStrategy = 1;
            else settings.NamespaceStrategy = 2;

            if (!settings.UseSeparateNamespace)
            {
                settings.SecondaryNamespace = string.Empty;
            }
            settings.Language = cmbLanguage.SelectedItem.GetType().Name;
            settings.SingleFile = chkSingleFile.Checked;
            settings.DocumentationExamples = chkDocumentationExamples.Checked;
            settings.Save();
        }

        private void frmCSharpClassGeneration_Load(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;
            (settings.UseProperties ? radProperties : radFields).Checked = true;
            (settings.InternalVisibility ? radInternal : radPublic).Checked = true;
            edtSecondaryNamespace.Text = settings.SecondaryNamespace;
            if (settings.NamespaceStrategy == 0) radNestedClasses.Checked = true;
            else if (settings.NamespaceStrategy == 1) radSameNamespace.Checked = true;
            else radDifferentNamespace.Checked = true;
            cmbLanguage.Items.AddRange(CodeWriters);
            var langIndex = CodeWriters.ToList().FindIndex(x => x.GetType().Name == settings.Language);
            cmbLanguage.SelectedIndex = langIndex != -1 ? langIndex : 0;
            chkSingleFile.Checked = settings.SingleFile;
            chkDocumentationExamples.Checked = settings.DocumentationExamples;
            UpdateStatus();
        }

        private readonly static ICodeWriter[] CodeWriters = new ICodeWriter[] {
            new CSharpCodeWriter(),
            new VisualBasicCodeWriter(),
            new TypeScriptCodeWriter(),
          //  new JavaCodeWriter()
        };

        private void edtNamespace_TextChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            HideCompletionMessage();
            if (edtTargetFolder.Text == string.Empty)
            {
                MessageBox.Show(this, "Please specify an output directory.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (edtNamespace.Text == string.Empty)
            {
                MessageBox.Show(this, "Please specify a namespace.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var gen = Prepare();
            if (gen == null) return;
            try
            {
                gen.GenerateClasses();
                messageTimer.Stop();
                lblDone.Visible = true;
                lnkOpenFolder.Visible = true;
                messageTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to generate the code: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string lastGeneratedString;

        private void PasteAndGo()
        {
            HideCompletionMessage();
            var jsonClipboard = Clipboard.GetText(TextDataFormat.UnicodeText | TextDataFormat.Text);
            if (jsonClipboard != lastGeneratedString)
            {
                var jsonClipboardTrimmed = jsonClipboard.Trim();
                var jsonTextboxTrimmed = edtJson.Text.Trim();
                if (
                    (jsonClipboardTrimmed.StartsWith("{") || jsonClipboardTrimmed.StartsWith("[")) ||
                    !(jsonTextboxTrimmed.StartsWith("{") || jsonTextboxTrimmed.StartsWith("[")))
                    edtJson.Text = jsonClipboard;
            }
            var gen = Prepare();
            if (gen == null) return;
            try
            {
                gen.TargetFolder = null;
                gen.SingleFile = true;
                using (var sw = new StringWriter())
                {
                    gen.OutputStream = sw;
                    gen.GenerateClasses();
                    sw.Flush();
                    lastGeneratedString = sw.ToString();
                    Clipboard.SetText(lastGeneratedString);
                }
                messageTimer.Stop();
                lblDoneClipboard.Visible = true;
                messageTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to generate the code: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private JsonClassGenerator Prepare()
        {
            if (edtJson.Text == string.Empty)
            {
                MessageBox.Show(this, "Please insert some sample JSON.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                edtJson.Focus();
                return null;
            }


            if (edtMainClass.Text == string.Empty)
            {
                MessageBox.Show(this, "Please specify a main class name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var gen = new JsonClassGenerator();
            gen.Example = edtJson.Text;
            gen.InternalVisibility = radInternal.Checked;
            gen.CodeWriter = (ICodeWriter)cmbLanguage.SelectedItem;
            gen.ExplicitDeserialization = chkExplicitDeserialization.Checked && gen.CodeWriter is CSharpCodeWriter;
            gen.Namespace = string.IsNullOrEmpty(edtNamespace.Text) ? null : edtNamespace.Text;
            gen.NoHelperClass = chkNoHelper.Checked;
            gen.SecondaryNamespace = radDifferentNamespace.Checked && !string.IsNullOrEmpty(edtSecondaryNamespace.Text) ? edtSecondaryNamespace.Text : null;
            gen.TargetFolder = edtTargetFolder.Text;
            gen.UseProperties = radProperties.Checked;
            gen.MainClass = edtMainClass.Text;
            gen.UsePascalCase = chkPascalCase.Checked;
            gen.UseNestedClasses = radNestedClasses.Checked;
            gen.ApplyObfuscationAttributes = chkApplyObfuscationAttributes.Checked;
            gen.SingleFile = chkSingleFile.Checked;
            gen.ExamplesInDocumentation = chkDocumentationExamples.Checked;
            return gen;
        }



        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (var w = new frmAbout())
            {
                w.ShowDialog(this);
            }
        }

        private void chkExplicitDeserialization_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {


            if (edtSecondaryNamespace.Text.Contains("JsonTypes") || edtSecondaryNamespace.Text == string.Empty)
            {
                edtSecondaryNamespace.Text = edtNamespace.Text == string.Empty ? string.Empty : edtNamespace.Text + "." + edtMainClass.Text + "JsonTypes";
            }

            edtSecondaryNamespace.Enabled = radDifferentNamespace.Checked;

            var writer = (ICodeWriter)cmbLanguage.SelectedItem;

            chkPascalCase.Enabled = !(writer is TypeScriptCodeWriter);
            chkExplicitDeserialization.Enabled = writer is CSharpCodeWriter;
            chkNoHelper.Enabled = chkExplicitDeserialization.Enabled && chkExplicitDeserialization.Checked;
        }

        private void radNestedClasses_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void radSameNamespace_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void radDifferentNamespace_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void edtMainClass_TextChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                using (var f = new frmAbout())
                {
                    f.ShowDialog(this);
                }
            }
            else if (e.KeyCode == Keys.F9)
            {
                e.Handled = true;
                PasteAndGo();
            }
            base.OnKeyDown(e);
        }

        private void messageTimer_Tick(object sender, EventArgs e)
        {
            messageTimer.Stop();
            HideCompletionMessage();
        }

        private void HideCompletionMessage()
        {

            lnkOpenFolder.Visible = false;
            lblDone.Visible = false;
            lblDoneClipboard.Visible = false;
        }

        private void lnkOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(edtTargetFolder.Text);
            }
            catch (Exception)
            {
            }

        }

        private void edtJson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                edtJson.SelectionStart = 0;
                edtJson.SelectionLength = edtJson.TextLength;
                e.Handled = true;
            }
        }

        private void btnPasteAndGo_Click(object sender, EventArgs e)
        {
            PasteAndGo();
        }

        //private void edtMainClass_Enter(object sender, EventArgs e)
        //{

        //    edtMainClass.Focus();
        //    edtMainClass.SelectAll();
        //}

        //private void edtTargetFolder_Enter(object sender, EventArgs e)
        //{
        //    edtTargetFolder.SelectAll();
        //}

        //private void edtNamespace_Enter(object sender, EventArgs e)
        //{
        //    edtNamespace.SelectAll();
        //}

        //private void edtJson_Enter(object sender, EventArgs e)
        //{
        //    edtJson.SelectAll();
        //}








    }
}
