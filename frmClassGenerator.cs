// Copyright © 2010 Xamasoft

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Xamasoft.JsonClassGenerator.CodeWriters;
using Xamasoft.JsonClassGenerator.UI.Helpers;
using Xamasoft.JsonClassGenerator.UI.Properties;


namespace Xamasoft.JsonClassGenerator.UI
{
    public partial class frmCSharpClassGeneration : Form
    {
        public frmCSharpClassGeneration()
        {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            Program.InitAppServices();
            InitialiseSyntaxHighlightingEditor();
        }

        private void InitialiseSyntaxHighlightingEditor()
        {
            edtJson.Styler = new JsonStyler();              // The thing that sets Json syntax highlighting
            edtJson.Text = string.Empty;                    // Clear any developer entered dummy data
            edtJson.SetSavePoint();                         // Show the buffer has not been changed.
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
            if (SaveIfModified() == false)
            {
                e.Cancel = true;
                return;
            }

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
            settings.DeDuplicateClasses = chkDeduplicate.Checked;
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
            chkDeduplicate.Checked = settings.DeDuplicateClasses;
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

            var gen = new JsonClassGenerator
            {
                Example = edtJson.Text,
                InternalVisibility = radInternal.Checked,
                CodeWriter = (ICodeWriter) cmbLanguage.SelectedItem,
                DeduplicateClasses = chkDeduplicate.Checked,
                TargetFolder = edtTargetFolder.Text,
                UseProperties = radProperties.Checked,
                MainClass = edtMainClass.Text,
                SortMemberFields = chkSortMembers.Checked,
                UsePascalCase = chkPascalCase.Checked,
                UseNestedClasses = radNestedClasses.Checked,
                ApplyObfuscationAttributes = chkApplyObfuscationAttributes.Checked,
                SingleFile = chkSingleFile.Checked,
                ExamplesInDocumentation = chkDocumentationExamples.Checked,
                Namespace = string.IsNullOrEmpty(edtNamespace.Text) ? null : edtNamespace.Text,
                NoHelperClass = chkNoHelper.Checked,
                SecondaryNamespace = radDifferentNamespace.Checked && !string.IsNullOrEmpty(edtSecondaryNamespace.Text) ? edtSecondaryNamespace.Text : null,
            };
            gen.ExplicitDeserialization = chkExplicitDeserialization.Checked && gen.CodeWriter is CSharpCodeWriter;

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
                edtJson.SelectionEnd = edtJson.TextLength;
                e.Handled = true;
            }
        }

        private void btnPasteAndGo_Click(object sender, EventArgs e)
        {
            PasteAndGo();
        }

        #region Json file load/save handlers

        private void btnLoadJsonFile_Clicked(object sender, EventArgs e)
        {
            if (SaveIfModified() == false) return;
            if (openDlg.ShowDialog() == DialogResult.Cancel) return;

            var fileToRead = openDlg.FileName;

            try
            {
                edtJson.Text = File.ReadAllText(fileToRead);
                edtJson.Tag = fileToRead;
                edtJson.SetSavePoint();
                UpdateCursorPosition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.JsonFileLoadError, ex.Message),
                    Resources.UnableToLoadTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSaveJsonFile_Clicked(object sender, EventArgs e) { SaveJsonFile(); }

        private void SaveJsonFileAs_Clicked(object sender, EventArgs e) { SaveJsonFileAs(); }

        private void btnCloseJsonFile_Clicked(object sender, EventArgs e)
        {
            if (SaveIfModified() == false) return;

            edtJson.Text = string.Empty;
            edtJson.Tag = null;
            edtJson.SetSavePoint();
            UpdateCursorPosition();
        }

        private void SaveJsonFile()
        {
            if (edtJson.Tag == null)
            {
                SaveJsonFileAs();
                return;
            }

            SaveEditWindow();
        }

        private void SaveJsonFileAs()
        {
            if (saveDlg.ShowDialog() == DialogResult.Cancel) return;

            edtJson.Tag = saveDlg.FileName;
            SaveEditWindow();
        }

        private void SaveEditWindow()
        {
            try
            {
                File.WriteAllText(edtJson.Tag.ToString(), edtJson.Text);
                edtJson.SetSavePoint();
                UpdateCursorPosition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.UnableToSaveJsonFile, ex.Message),
                    Resources.JsonSaveFailedTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the edit buffer if it has been modified. 
        /// </summary>
        /// <returns>
        /// true if (a) the edit window has not been changed, (b) the file has been successfully saved, (c) the user
        /// says they do not want to save the file. Returns false if the user (a) cancels or (b) the save fails.
        /// </returns>
        private bool SaveIfModified()
        {
            if (!edtJson.Modified || string.IsNullOrWhiteSpace(edtJson.Text)) return true;

            var response = MessageBox.Show(Resources.YourFileHasChanged,
                Resources.SaveChangesTitle,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            switch (response)
            {
                case DialogResult.Cancel:
                    return false;

                case DialogResult.Yes:
                    SaveJsonFile();
                    // If the buffer is still modified, then the save failed.
                    if (edtJson.Modified)
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }

        #endregion

        #region Json validation

        private void btnValidate_Clicked(object sender, EventArgs e)
        {
            try
            {
                JObject.Parse(edtJson.Text);
                MessageBox.Show(Resources.JsonValidationOk, Resources.JsonValidationTitle, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.JsonValidationErrors, ex.Message), Resources.JsonValidationTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditorPositionChanged(object sender, EventArgs e) { UpdateCursorPosition(); }
        private void UpdateCursorPosition()
        {
            lblPosition.Text = $"{edtJson.CurrentLine + 1}/{edtJson.GetColumn(edtJson.CurrentPosition) + 1}";
        }


        #endregion

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
