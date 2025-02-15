﻿namespace MetX.Controls
{
    partial class TechniquesEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechniquesEditor));
            this.FileTree = new System.Windows.Forms.TreeView();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.QuickScriptFileGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.QuickScriptFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PipelineFilesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.QuickScriptPipelineMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DataConnectionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DataConnectionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputLocationsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputLocationMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.QuickScriptTemplateGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.QuickScriptTemplateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.XsTemplatesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.XSLTemplateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PipelineProvidersMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PipelineProviderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
            this.NewTechniquesFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.quickScriptFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pipelinerFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pipelinerStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addXLGStepSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.addOutputLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addXLGXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.addProviderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.RightClickMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileTree
            // 
            this.FileTree.ContextMenuStrip = this.RightClickMenu;
            this.FileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileTree.ImageIndex = 15;
            this.FileTree.ImageList = this.imageList1;
            this.FileTree.Indent = 12;
            this.FileTree.Location = new System.Drawing.Point(0, 31);
            this.FileTree.Name = "FileTree";
            this.FileTree.SelectedImageIndex = 13;
            this.FileTree.ShowLines = false;
            this.FileTree.ShowRootLines = false;
            this.FileTree.Size = new System.Drawing.Size(283, 516);
            this.FileTree.StateImageList = this.imageList1;
            this.FileTree.TabIndex = 0;
            this.FileTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QuickScriptFileGroupMenu,
            this.QuickScriptFileMenu,
            this.PipelineFilesMenu,
            this.QuickScriptPipelineMenu,
            this.SettingsMenu,
            this.DataConnectionsMenu,
            this.DataConnectionMenu,
            this.OutputLocationsMenu,
            this.OutputLocationMenu,
            this.QuickScriptTemplateGroupMenu,
            this.QuickScriptTemplateMenu,
            this.XsTemplatesMenu,
            this.XSLTemplateMenu,
            this.PipelineProvidersMenu,
            this.PipelineProviderMenu});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(265, 364);
            // 
            // QuickScriptFileGroupMenu
            // 
            this.QuickScriptFileGroupMenu.Name = "QuickScriptFileGroupMenu";
            this.QuickScriptFileGroupMenu.Size = new System.Drawing.Size(264, 24);
            this.QuickScriptFileGroupMenu.Text = "Quick Script Files";
            // 
            // QuickScriptFileMenu
            // 
            this.QuickScriptFileMenu.Name = "QuickScriptFileMenu";
            this.QuickScriptFileMenu.Size = new System.Drawing.Size(264, 24);
            this.QuickScriptFileMenu.Text = "Quick Script File Menu";
            // 
            // PipelineFilesMenu
            // 
            this.PipelineFilesMenu.Name = "PipelineFilesMenu";
            this.PipelineFilesMenu.Size = new System.Drawing.Size(264, 24);
            this.PipelineFilesMenu.Text = "Pipeline Files";
            // 
            // QuickScriptPipelineMenu
            // 
            this.QuickScriptPipelineMenu.Name = "QuickScriptPipelineMenu";
            this.QuickScriptPipelineMenu.Size = new System.Drawing.Size(264, 24);
            this.QuickScriptPipelineMenu.Text = "Quick Script Pipeline Menu";
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Size = new System.Drawing.Size(264, 24);
            this.SettingsMenu.Text = "Settings";
            // 
            // DataConnectionsMenu
            // 
            this.DataConnectionsMenu.Name = "DataConnectionsMenu";
            this.DataConnectionsMenu.Size = new System.Drawing.Size(264, 24);
            this.DataConnectionsMenu.Text = "Data Connections";
            // 
            // DataConnectionMenu
            // 
            this.DataConnectionMenu.Name = "DataConnectionMenu";
            this.DataConnectionMenu.Size = new System.Drawing.Size(264, 24);
            this.DataConnectionMenu.Text = "Data Connection Menu";
            // 
            // OutputLocationsMenu
            // 
            this.OutputLocationsMenu.Name = "OutputLocationsMenu";
            this.OutputLocationsMenu.Size = new System.Drawing.Size(264, 24);
            this.OutputLocationsMenu.Text = "Output Locations";
            // 
            // OutputLocationMenu
            // 
            this.OutputLocationMenu.Name = "OutputLocationMenu";
            this.OutputLocationMenu.Size = new System.Drawing.Size(264, 24);
            this.OutputLocationMenu.Text = "Output Location Menu";
            // 
            // QuickScriptTemplateGroupMenu
            // 
            this.QuickScriptTemplateGroupMenu.Name = "QuickScriptTemplateGroupMenu";
            this.QuickScriptTemplateGroupMenu.Size = new System.Drawing.Size(264, 24);
            this.QuickScriptTemplateGroupMenu.Text = "Quick Script Templates";
            // 
            // QuickScriptTemplateMenu
            // 
            this.QuickScriptTemplateMenu.Name = "QuickScriptTemplateMenu";
            this.QuickScriptTemplateMenu.Size = new System.Drawing.Size(264, 24);
            this.QuickScriptTemplateMenu.Text = "Quick Script Template Menu";
            // 
            // XsTemplatesMenu
            // 
            this.XsTemplatesMenu.Name = "XsTemplatesMenu";
            this.XsTemplatesMenu.Size = new System.Drawing.Size(264, 24);
            this.XsTemplatesMenu.Text = "XSL Templates";
            // 
            // XSLTemplateMenu
            // 
            this.XSLTemplateMenu.Name = "XSLTemplateMenu";
            this.XSLTemplateMenu.Size = new System.Drawing.Size(264, 24);
            this.XSLTemplateMenu.Text = "XSL Template Menu";
            // 
            // PipelineProvidersMenu
            // 
            this.PipelineProvidersMenu.Name = "PipelineProvidersMenu";
            this.PipelineProvidersMenu.Size = new System.Drawing.Size(264, 24);
            this.PipelineProvidersMenu.Text = "Pipeline Providers";
            // 
            // PipelineProviderMenu
            // 
            this.PipelineProviderMenu.Name = "PipelineProviderMenu";
            this.PipelineProviderMenu.Size = new System.Drawing.Size(264, 24);
            this.PipelineProviderMenu.Text = "Pipeline Provider Menu";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "126Edit.ico");
            this.imageList1.Images.SetKeyName(1, "107259NewContentPage32x32.png");
            this.imageList1.Images.SetKeyName(2, "1421584661_4.png");
            this.imageList1.Images.SetKeyName(3, "1421584690_2.png");
            this.imageList1.Images.SetKeyName(4, "1421584694_2.png");
            this.imageList1.Images.SetKeyName(5, "1421584753_Cut.png");
            this.imageList1.Images.SetKeyName(6, "1421584808_Copy.png");
            this.imageList1.Images.SetKeyName(7, "1421584898_clipboard.png");
            this.imageList1.Images.SetKeyName(8, "1421584916_Noun_Project_100Icon_10px_grid-06-48.png");
            this.imageList1.Images.SetKeyName(9, "1421584946_file-48.png");
            this.imageList1.Images.SetKeyName(10, "add_48.png");
            this.imageList1.Images.SetKeyName(11, "AnnotateDefault.ico");
            this.imageList1.Images.SetKeyName(12, "arrow_down_48.png");
            this.imageList1.Images.SetKeyName(13, "button_play.png");
            this.imageList1.Images.SetKeyName(14, "cancel_48.png");
            this.imageList1.Images.SetKeyName(15, "circle_blue.png");
            this.imageList1.Images.SetKeyName(16, "circle_green.png");
            this.imageList1.Images.SetKeyName(17, "circle_orange.png");
            this.imageList1.Images.SetKeyName(18, "console-mock.jpg");
            this.imageList1.Images.SetKeyName(19, "cross_48.png");
            this.imageList1.Images.SetKeyName(20, "database_add_48.png");
            this.imageList1.Images.SetKeyName(21, "database_remove_48.png");
            this.imageList1.Images.SetKeyName(22, "dragon2.png");
            this.imageList1.Images.SetKeyName(23, "floppy_disk_48.png");
            this.imageList1.Images.SetKeyName(24, "folder_48.png");
            this.imageList1.Images.SetKeyName(25, "hops2.png");
            this.imageList1.Images.SetKeyName(26, "Long-dragon-running.png");
            this.imageList1.Images.SetKeyName(27, "refresh_48.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator1,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(283, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTechniquesFileMenuItem,
            this.toolStripSeparator3,
            this.quickScriptFileToolStripMenuItem,
            this.quickScriptToolStripMenuItem,
            this.toolStripSeparator2,
            this.pipelinerFileToolStripMenuItem,
            this.pipelinerStepToolStripMenuItem,
            this.addXLGStepSettingsToolStripMenuItem,
            this.toolStripSeparator4,
            this.addOutputLocationToolStripMenuItem,
            this.addConnectionToolStripMenuItem,
            this.addXLGXMLToolStripMenuItem,
            this.toolStripSeparator5,
            this.addProviderToolStripMenuItem,
            this.toolStripMenuItem2});
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(40, 28);
            this.newToolStripButton.Text = "&New";
            // 
            // NewTechniquesFileMenuItem
            // 
            this.NewTechniquesFileMenuItem.Name = "NewTechniquesFileMenuItem";
            this.NewTechniquesFileMenuItem.Size = new System.Drawing.Size(255, 24);
            this.NewTechniquesFileMenuItem.Text = "New Pattern File";
            this.NewTechniquesFileMenuItem.Click += new System.EventHandler(this.NewTechniquesFileMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(252, 6);
            // 
            // quickScriptFileToolStripMenuItem
            // 
            this.quickScriptFileToolStripMenuItem.Name = "quickScriptFileToolStripMenuItem";
            this.quickScriptFileToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.quickScriptFileToolStripMenuItem.Text = "Add Quick Script Group";
            // 
            // quickScriptToolStripMenuItem
            // 
            this.quickScriptToolStripMenuItem.Name = "quickScriptToolStripMenuItem";
            this.quickScriptToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.quickScriptToolStripMenuItem.Text = "Add Quick Script Step";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(252, 6);
            // 
            // pipelinerFileToolStripMenuItem
            // 
            this.pipelinerFileToolStripMenuItem.Name = "pipelinerFileToolStripMenuItem";
            this.pipelinerFileToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.pipelinerFileToolStripMenuItem.Text = "Add Pipeline Group";
            // 
            // pipelinerStepToolStripMenuItem
            // 
            this.pipelinerStepToolStripMenuItem.Name = "pipelinerStepToolStripMenuItem";
            this.pipelinerStepToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.pipelinerStepToolStripMenuItem.Text = "Add Pipeline Step";
            // 
            // addXLGStepSettingsToolStripMenuItem
            // 
            this.addXLGStepSettingsToolStripMenuItem.Name = "addXLGStepSettingsToolStripMenuItem";
            this.addXLGStepSettingsToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.addXLGStepSettingsToolStripMenuItem.Text = "Add Step Settings";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(252, 6);
            // 
            // addOutputLocationToolStripMenuItem
            // 
            this.addOutputLocationToolStripMenuItem.Name = "addOutputLocationToolStripMenuItem";
            this.addOutputLocationToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.addOutputLocationToolStripMenuItem.Text = "Add Output Location";
            // 
            // addConnectionToolStripMenuItem
            // 
            this.addConnectionToolStripMenuItem.Name = "addConnectionToolStripMenuItem";
            this.addConnectionToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.addConnectionToolStripMenuItem.Text = "Add Connection";
            // 
            // addXLGXMLToolStripMenuItem
            // 
            this.addXLGXMLToolStripMenuItem.Name = "addXLGXMLToolStripMenuItem";
            this.addXLGXMLToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.addXLGXMLToolStripMenuItem.Text = "Add XSL Template";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(252, 6);
            // 
            // addProviderToolStripMenuItem
            // 
            this.addProviderToolStripMenuItem.Name = "addProviderToolStripMenuItem";
            this.addProviderToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.addProviderToolStripMenuItem.Text = "Add Pipeline Provider";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(255, 24);
            this.toolStripMenuItem2.Text = "Add Quick Script Template";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.saveToolStripButton.Text = "&Save";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.printToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 31);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.pasteToolStripButton.Text = "&Paste";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TechniquesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(283, 547);
            this.Controls.Add(this.FileTree);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TechniquesEditor";
            this.Text = "Code Pattern";
            this.RightClickMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView FileTree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton newToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem NewTechniquesFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem quickScriptFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem pipelinerFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pipelinerStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem addXLGStepSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addXLGXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem addOutputLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem addProviderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem QuickScriptFileGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem QuickScriptFileMenu;
        private System.Windows.Forms.ToolStripMenuItem PipelineFilesMenu;
        private System.Windows.Forms.ToolStripMenuItem QuickScriptPipelineMenu;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem DataConnectionsMenu;
        private System.Windows.Forms.ToolStripMenuItem DataConnectionMenu;
        private System.Windows.Forms.ToolStripMenuItem OutputLocationsMenu;
        private System.Windows.Forms.ToolStripMenuItem OutputLocationMenu;
        private System.Windows.Forms.ToolStripMenuItem QuickScriptTemplateGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem QuickScriptTemplateMenu;
        private System.Windows.Forms.ToolStripMenuItem XsTemplatesMenu;
        private System.Windows.Forms.ToolStripMenuItem XSLTemplateMenu;
        private System.Windows.Forms.ToolStripMenuItem PipelineProvidersMenu;
        private System.Windows.Forms.ToolStripMenuItem PipelineProviderMenu;
    }
}
