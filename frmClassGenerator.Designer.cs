namespace Xamasoft.JsonClassGenerator.UI
{
    partial class frmCSharpClassGeneration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCSharpClassGeneration));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.edtJson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radFields = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radPublic = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radProperties = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radInternal = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.edtSecondaryNamespace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radSameNamespace = new System.Windows.Forms.RadioButton();
            this.radDifferentNamespace = new System.Windows.Forms.RadioButton();
            this.radNestedClasses = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.chkApplyObfuscationAttributes = new System.Windows.Forms.CheckBox();
            this.chkExplicitDeserialization = new System.Windows.Forms.CheckBox();
            this.chkPascalCase = new System.Windows.Forms.CheckBox();
            this.edtMainClass = new System.Windows.Forms.TextBox();
            this.edtTargetFolder = new System.Windows.Forms.TextBox();
            this.chkNoHelper = new System.Windows.Forms.CheckBox();
            this.edtNamespace = new System.Windows.Forms.TextBox();
            this.chkSingleFile = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lblDone = new System.Windows.Forms.Label();
            this.lnkOpenFolder = new Xamasoft.Controls.BetterLinkLabel();
            this.messageTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.lblDoneClipboard = new System.Windows.Forms.Label();
            this.chkDocumentationExamples = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(539, 529);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 17;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // edtJson
            // 
            this.edtJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edtJson.Location = new System.Drawing.Point(15, 253);
            this.edtJson.MaxLength = 10000000;
            this.edtJson.Multiline = true;
            this.edtJson.Name = "edtJson";
            this.edtJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtJson.Size = new System.Drawing.Size(680, 271);
            this.edtJson.TabIndex = 14;
            this.edtJson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edtJson_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Generate C# classes from sample JSON:";
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Location = new System.Drawing.Point(12, 12);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(67, 13);
            this.lblNamespace.TabIndex = 4;
            this.lblNamespace.Text = "Namespace:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(620, 529);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Members generation:";
            // 
            // radFields
            // 
            this.radFields.AutoSize = true;
            this.radFields.Location = new System.Drawing.Point(81, 3);
            this.radFields.Name = "radFields";
            this.radFields.Size = new System.Drawing.Size(52, 17);
            this.radFields.TabIndex = 1;
            this.radFields.Text = "Fields";
            this.radFields.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Visibility:";
            // 
            // radPublic
            // 
            this.radPublic.AutoSize = true;
            this.radPublic.Location = new System.Drawing.Point(69, 3);
            this.radPublic.Name = "radPublic";
            this.radPublic.Size = new System.Drawing.Size(54, 17);
            this.radPublic.TabIndex = 1;
            this.radPublic.Text = "Public";
            this.radPublic.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.radProperties);
            this.flowLayoutPanel1.Controls.Add(this.radFields);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(458, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(136, 23);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // radProperties
            // 
            this.radProperties.AutoSize = true;
            this.radProperties.Checked = true;
            this.radProperties.Location = new System.Drawing.Point(3, 3);
            this.radProperties.Name = "radProperties";
            this.radProperties.Size = new System.Drawing.Size(72, 17);
            this.radProperties.TabIndex = 0;
            this.radProperties.TabStop = true;
            this.radProperties.Text = "Properties";
            this.radProperties.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.radInternal);
            this.flowLayoutPanel2.Controls.Add(this.radPublic);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(458, 33);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(126, 23);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // radInternal
            // 
            this.radInternal.AutoSize = true;
            this.radInternal.Checked = true;
            this.radInternal.Location = new System.Drawing.Point(3, 3);
            this.radInternal.Name = "radInternal";
            this.radInternal.Size = new System.Drawing.Size(60, 17);
            this.radInternal.TabIndex = 0;
            this.radInternal.TabStop = true;
            this.radInternal.Text = "Internal";
            this.radInternal.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(276, 63);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Target folder:";
            // 
            // edtSecondaryNamespace
            // 
            this.edtSecondaryNamespace.Location = new System.Drawing.Point(30, 72);
            this.edtSecondaryNamespace.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.edtSecondaryNamespace.Name = "edtSecondaryNamespace";
            this.edtSecondaryNamespace.Size = new System.Drawing.Size(219, 20);
            this.edtSecondaryNamespace.TabIndex = 3;
            this.edtSecondaryNamespace.Text = "Example.JsonTypes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Main class name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Secondary classes:";
            // 
            // radSameNamespace
            // 
            this.radSameNamespace.AutoSize = true;
            this.radSameNamespace.Location = new System.Drawing.Point(3, 26);
            this.radSameNamespace.Name = "radSameNamespace";
            this.radSameNamespace.Size = new System.Drawing.Size(148, 17);
            this.radSameNamespace.TabIndex = 1;
            this.radSameNamespace.TabStop = true;
            this.radSameNamespace.Text = "Use the same namespace";
            this.radSameNamespace.UseVisualStyleBackColor = true;
            this.radSameNamespace.CheckedChanged += new System.EventHandler(this.radSameNamespace_CheckedChanged);
            // 
            // radDifferentNamespace
            // 
            this.radDifferentNamespace.AutoSize = true;
            this.radDifferentNamespace.Location = new System.Drawing.Point(3, 49);
            this.radDifferentNamespace.Name = "radDifferentNamespace";
            this.radDifferentNamespace.Size = new System.Drawing.Size(152, 17);
            this.radDifferentNamespace.TabIndex = 2;
            this.radDifferentNamespace.TabStop = true;
            this.radDifferentNamespace.Text = "Use a different namespace";
            this.radDifferentNamespace.UseVisualStyleBackColor = true;
            this.radDifferentNamespace.CheckedChanged += new System.EventHandler(this.radDifferentNamespace_CheckedChanged);
            // 
            // radNestedClasses
            // 
            this.radNestedClasses.AutoSize = true;
            this.radNestedClasses.Location = new System.Drawing.Point(3, 3);
            this.radNestedClasses.Name = "radNestedClasses";
            this.radNestedClasses.Size = new System.Drawing.Size(117, 17);
            this.radNestedClasses.TabIndex = 0;
            this.radNestedClasses.TabStop = true;
            this.radNestedClasses.Text = "Use nested classes";
            this.radNestedClasses.UseVisualStyleBackColor = true;
            this.radNestedClasses.CheckedChanged += new System.EventHandler(this.radNestedClasses_CheckedChanged);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.radNestedClasses);
            this.flowLayoutPanel3.Controls.Add(this.radSameNamespace);
            this.flowLayoutPanel3.Controls.Add(this.radDifferentNamespace);
            this.flowLayoutPanel3.Controls.Add(this.edtSecondaryNamespace);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(33, 134);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(267, 100);
            this.flowLayoutPanel3.TabIndex = 5;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DisplayMember = "DisplayName";
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(115, 90);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(185, 21);
            this.cmbLanguage.TabIndex = 4;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(12, 93);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(58, 13);
            this.lblLanguage.TabIndex = 37;
            this.lblLanguage.Text = "Language:";
            // 
            // chkApplyObfuscationAttributes
            // 
            this.chkApplyObfuscationAttributes.AutoSize = true;
            this.chkApplyObfuscationAttributes.Checked = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.ApplyObfuscationAttributes;
            this.chkApplyObfuscationAttributes.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "ApplyObfuscationAttributes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkApplyObfuscationAttributes.Location = new System.Drawing.Point(349, 136);
            this.chkApplyObfuscationAttributes.Name = "chkApplyObfuscationAttributes";
            this.chkApplyObfuscationAttributes.Size = new System.Drawing.Size(203, 17);
            this.chkApplyObfuscationAttributes.TabIndex = 12;
            this.chkApplyObfuscationAttributes.Text = "Apply obfuscation exclusion attributes";
            this.chkApplyObfuscationAttributes.UseVisualStyleBackColor = true;
            // 
            // chkExplicitDeserialization
            // 
            this.chkExplicitDeserialization.AutoSize = true;
            this.chkExplicitDeserialization.Checked = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.UseExplicitDeserialization;
            this.chkExplicitDeserialization.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "UseExplicitDeserialization", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkExplicitDeserialization.Location = new System.Drawing.Point(349, 90);
            this.chkExplicitDeserialization.Name = "chkExplicitDeserialization";
            this.chkExplicitDeserialization.Size = new System.Drawing.Size(198, 17);
            this.chkExplicitDeserialization.TabIndex = 10;
            this.chkExplicitDeserialization.Text = "Use explicit deserialization (obsolete)";
            this.chkExplicitDeserialization.UseVisualStyleBackColor = true;
            this.chkExplicitDeserialization.CheckedChanged += new System.EventHandler(this.chkExplicitDeserialization_CheckedChanged);
            // 
            // chkPascalCase
            // 
            this.chkPascalCase.AutoSize = true;
            this.chkPascalCase.Checked = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.UsePascalCase;
            this.chkPascalCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPascalCase.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "UsePascalCase", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkPascalCase.Location = new System.Drawing.Point(349, 67);
            this.chkPascalCase.Name = "chkPascalCase";
            this.chkPascalCase.Size = new System.Drawing.Size(134, 17);
            this.chkPascalCase.TabIndex = 9;
            this.chkPascalCase.Text = "Convert to PascalCase";
            this.chkPascalCase.UseVisualStyleBackColor = true;
            // 
            // edtMainClass
            // 
            this.edtMainClass.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "MainClassName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtMainClass.Location = new System.Drawing.Point(115, 37);
            this.edtMainClass.Name = "edtMainClass";
            this.edtMainClass.Size = new System.Drawing.Size(185, 20);
            this.edtMainClass.TabIndex = 1;
            this.edtMainClass.Text = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.MainClassName;
            this.edtMainClass.TextChanged += new System.EventHandler(this.edtMainClass_TextChanged);
            // 
            // edtTargetFolder
            // 
            this.edtTargetFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.edtTargetFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.edtTargetFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "TargetFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtTargetFolder.Location = new System.Drawing.Point(115, 65);
            this.edtTargetFolder.Name = "edtTargetFolder";
            this.edtTargetFolder.Size = new System.Drawing.Size(155, 20);
            this.edtTargetFolder.TabIndex = 2;
            this.edtTargetFolder.Text = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.TargetFolder;
            // 
            // chkNoHelper
            // 
            this.chkNoHelper.AutoSize = true;
            this.chkNoHelper.Checked = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.NoHelper;
            this.chkNoHelper.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "NoHelper", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkNoHelper.Location = new System.Drawing.Point(366, 113);
            this.chkNoHelper.Name = "chkNoHelper";
            this.chkNoHelper.Size = new System.Drawing.Size(162, 17);
            this.chkNoHelper.TabIndex = 11;
            this.chkNoHelper.Text = "Do not generate helper class";
            this.chkNoHelper.UseVisualStyleBackColor = true;
            // 
            // edtNamespace
            // 
            this.edtNamespace.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default, "Namespace", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtNamespace.Location = new System.Drawing.Point(115, 9);
            this.edtNamespace.Name = "edtNamespace";
            this.edtNamespace.Size = new System.Drawing.Size(185, 20);
            this.edtNamespace.TabIndex = 0;
            this.edtNamespace.Text = global::Xamasoft.JsonClassGenerator.UI.Properties.Settings.Default.Namespace;
            this.edtNamespace.TextChanged += new System.EventHandler(this.edtNamespace_TextChanged);
            // 
            // chkSingleFile
            // 
            this.chkSingleFile.AutoSize = true;
            this.chkSingleFile.Location = new System.Drawing.Point(349, 160);
            this.chkSingleFile.Name = "chkSingleFile";
            this.chkSingleFile.Size = new System.Drawing.Size(125, 17);
            this.chkSingleFile.TabIndex = 13;
            this.chkSingleFile.Text = "Generate a single file";
            this.chkSingleFile.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.Location = new System.Drawing.Point(15, 529);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 15;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // lblDone
            // 
            this.lblDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDone.AutoSize = true;
            this.lblDone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDone.ForeColor = System.Drawing.Color.Green;
            this.lblDone.Location = new System.Drawing.Point(319, 534);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(39, 13);
            this.lblDone.TabIndex = 38;
            this.lblDone.Text = "Done!";
            this.lblDone.Visible = false;
            // 
            // lnkOpenFolder
            // 
            this.lnkOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOpenFolder.AutoSize = true;
            this.lnkOpenFolder.Location = new System.Drawing.Point(364, 534);
            this.lnkOpenFolder.Name = "lnkOpenFolder";
            this.lnkOpenFolder.Size = new System.Drawing.Size(62, 13);
            this.lnkOpenFolder.TabIndex = 39;
            this.lnkOpenFolder.TabStop = true;
            this.lnkOpenFolder.Text = "Open folder";
            this.lnkOpenFolder.Visible = false;
            this.lnkOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpenFolder_LinkClicked);
            // 
            // messageTimer
            // 
            this.messageTimer.Interval = 7000;
            this.messageTimer.Tick += new System.EventHandler(this.messageTimer_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(437, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Paste && go (F9)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnPasteAndGo_Click);
            // 
            // lblDoneClipboard
            // 
            this.lblDoneClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDoneClipboard.AutoSize = true;
            this.lblDoneClipboard.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoneClipboard.ForeColor = System.Drawing.Color.Green;
            this.lblDoneClipboard.Location = new System.Drawing.Point(243, 534);
            this.lblDoneClipboard.Name = "lblDoneClipboard";
            this.lblDoneClipboard.Size = new System.Drawing.Size(183, 13);
            this.lblDoneClipboard.TabIndex = 38;
            this.lblDoneClipboard.Text = "Done! Classes copied to clipboard";
            this.lblDoneClipboard.Visible = false;
            // 
            // chkDocumentationExamples
            // 
            this.chkDocumentationExamples.AutoSize = true;
            this.chkDocumentationExamples.Location = new System.Drawing.Point(349, 183);
            this.chkDocumentationExamples.Name = "chkDocumentationExamples";
            this.chkDocumentationExamples.Size = new System.Drawing.Size(236, 17);
            this.chkDocumentationExamples.TabIndex = 40;
            this.chkDocumentationExamples.Text = "Generate documentation with data examples";
            this.chkDocumentationExamples.UseVisualStyleBackColor = true;
            // 
            // frmCSharpClassGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(707, 564);
            this.Controls.Add(this.chkDocumentationExamples);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lnkOpenFolder);
            this.Controls.Add(this.lblDoneClipboard);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.chkSingleFile);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.cmbLanguage);
            this.Controls.Add(this.chkApplyObfuscationAttributes);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkExplicitDeserialization);
            this.Controls.Add(this.chkPascalCase);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.edtMainClass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.edtTargetFolder);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkNoHelper);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNamespace);
            this.Controls.Add(this.edtNamespace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtJson);
            this.Controls.Add(this.btnGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(671, 444);
            this.Name = "frmCSharpClassGeneration";
            this.Text = "Xamasoft JSON Class Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCSharpClassGeneration_FormClosing);
            this.Load += new System.EventHandler(this.frmCSharpClassGeneration_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox edtJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkNoHelper;
        private System.Windows.Forms.TextBox edtSecondaryNamespace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radFields;
        private System.Windows.Forms.RadioButton radProperties;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radInternal;
        private System.Windows.Forms.RadioButton radPublic;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox edtTargetFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edtMainClass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.CheckBox chkPascalCase;
        private System.Windows.Forms.CheckBox chkExplicitDeserialization;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radSameNamespace;
        private System.Windows.Forms.RadioButton radDifferentNamespace;
        private System.Windows.Forms.RadioButton radNestedClasses;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox chkApplyObfuscationAttributes;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.CheckBox chkSingleFile;
        private System.Windows.Forms.Label lblDone;
        private Xamasoft.Controls.BetterLinkLabel lnkOpenFolder;
        private System.Windows.Forms.Timer messageTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblDoneClipboard;
        private System.Windows.Forms.CheckBox chkDocumentationExamples;
    }
}

