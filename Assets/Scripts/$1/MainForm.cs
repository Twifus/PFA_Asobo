/**
 * The $1 Unistroke Recognizer (C# version)
 *
 *		Jacob O. Wobbrock, Ph.D.
 * 		The Information School
 *		University of Washington
 *		Mary Gates Hall, Box 352840
 *		Seattle, WA 98195-2840
 *		wobbrock@u.washington.edu
 *
 *		Andrew D. Wilson, Ph.D.
 *		Microsoft Research
 *		One Microsoft Way
 *		Redmond, WA 98052
 *		awilson@microsoft.com
 *
 *		Yang Li, Ph.D.
 *		Department of Computer Science and Engineering
 * 		University of Washington
 *		The Allen Center, Box 352350
 *		Seattle, WA 98195-2840
 * 		yangli@cs.washington.edu
 *
 * The Protractor enhancement was published by Yang Li and programmed here by 
 * Jacob O. Wobbrock.
 *
 *	Li, Y. (2010). Protractor: A fast and accurate gesture 
 *	  recognizer. Proceedings of the ACM Conference on Human 
 *	  Factors in Computing Systems (CHI '10). Atlanta, Georgia
 *	  (April 10-15, 2010). New York: ACM Press, pp. 2169-2172.
 * 
 * This software is distributed under the "New BSD License" agreement:
 * 
 * Copyright (c) 2007-2011, Jacob O. Wobbrock, Andrew D. Wilson and Yang Li.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *    * Redistributions of source code must retain the above copyright
 *      notice, this list of conditions and the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright
 *      notice, this list of conditions and the following disclaimer in the
 *      documentation and/or other materials provided with the distribution.
 *    * Neither the names of the University of Washington nor Microsoft,
 *      nor the names of its contributors may be used to endorse or promote 
 *      products derived from this software without specific prior written
 *      permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
 * IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Jacob O. Wobbrock OR Andrew D. Wilson
 * OR Yang Li BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
 * OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**/
using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Media;
using WobbrockLib;
using WobbrockLib.Extensions;

namespace Recognizer.Dollar.Geometric
{
	public class MainForm : System.Windows.Forms.Form
	{
		#region Fields

        private const int MinNoPoints = 5;
        private Recognizer _rec;
        private bool _recording;
        private bool _isDown;
        private List<TimePointF> _points;
        private ViewForm _viewFrm;
        private bool _similar;
        private bool _protractor;

		#endregion

		#region Form Elements

		private System.Windows.Forms.Label lblRecord;
		private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem Exit;
		private System.Windows.Forms.MenuItem LoadGesture;
		private System.Windows.Forms.MenuItem ViewGesture;
		private System.Windows.Forms.MenuItem RecordGesture;
		private System.Windows.Forms.MenuItem GesturesMenu;
        private System.Windows.Forms.MenuItem ClearGestures;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.MenuItem HelpMenu;
        private System.Windows.Forms.MenuItem About;
        private Label lblRecognizing;
        private MenuItem FileMenu;
        private MenuItem TestBatch;
        private MenuItem Separator0;
        private ProgressBar prgTesting;
        private MenuItem RotationGraph;
        private MenuItem RotateSimilar;
        private MenuItem GraphMenu;
        private MenuItem TestDirectories;
        private MenuItem SearchMenu;
        private MenuItem mniGSS;
        private MenuItem mniProtractor;
        private IContainer components;

		#endregion

        #region Start & Stop

        /// <summary>
        /// Program entrypoint.
        /// </summary>
        [STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

        /// <summary>
        /// 
        /// </summary>
		public MainForm()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			InitializeComponent();
			_rec = new Recognizer();
            _rec.ProgressChangedEvent += new ProgressEventHandler(OnProgressChanged);
			_points = new List<TimePointF>(256);
			_viewFrm = null;
            _similar = true;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.lblRecord = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMenu = new System.Windows.Forms.MenuItem();
            this.Exit = new System.Windows.Forms.MenuItem();
            this.GesturesMenu = new System.Windows.Forms.MenuItem();
            this.RecordGesture = new System.Windows.Forms.MenuItem();
            this.LoadGesture = new System.Windows.Forms.MenuItem();
            this.ViewGesture = new System.Windows.Forms.MenuItem();
            this.ClearGestures = new System.Windows.Forms.MenuItem();
            this.Separator0 = new System.Windows.Forms.MenuItem();
            this.TestBatch = new System.Windows.Forms.MenuItem();
            this.TestDirectories = new System.Windows.Forms.MenuItem();
            this.SearchMenu = new System.Windows.Forms.MenuItem();
            this.mniGSS = new System.Windows.Forms.MenuItem();
            this.mniProtractor = new System.Windows.Forms.MenuItem();
            this.GraphMenu = new System.Windows.Forms.MenuItem();
            this.RotationGraph = new System.Windows.Forms.MenuItem();
            this.RotateSimilar = new System.Windows.Forms.MenuItem();
            this.HelpMenu = new System.Windows.Forms.MenuItem();
            this.About = new System.Windows.Forms.MenuItem();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblRecognizing = new System.Windows.Forms.Label();
            this.prgTesting = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblRecord
            // 
            this.lblRecord.BackColor = System.Drawing.Color.Transparent;
            this.lblRecord.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecord.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblRecord.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRecord.Location = new System.Drawing.Point(0, 0);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(352, 24);
            this.lblRecord.TabIndex = 2;
            this.lblRecord.Text = "[Recording]";
            this.lblRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRecord.Visible = false;
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMenu,
            this.GesturesMenu,
            this.SearchMenu,
            this.GraphMenu,
            this.HelpMenu});
            // 
            // FileMenu
            // 
            this.FileMenu.Index = 0;
            this.FileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Exit});
            this.FileMenu.Text = "&File";
            // 
            // Exit
            // 
            this.Exit.Index = 0;
            this.Exit.Text = "E&xit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // GesturesMenu
            // 
            this.GesturesMenu.Index = 1;
            this.GesturesMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.RecordGesture,
            this.LoadGesture,
            this.ViewGesture,
            this.ClearGestures,
            this.Separator0,
            this.TestBatch,
            this.TestDirectories});
            this.GesturesMenu.Text = "&Gestures";
            this.GesturesMenu.Popup += new System.EventHandler(this.GestureMenu_Popup);
            // 
            // RecordGesture
            // 
            this.RecordGesture.Index = 0;
            this.RecordGesture.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.RecordGesture.Text = "&Record";
            this.RecordGesture.Click += new System.EventHandler(this.RecordGesture_Click);
            // 
            // LoadGesture
            // 
            this.LoadGesture.Index = 1;
            this.LoadGesture.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.LoadGesture.Text = "&Load...";
            this.LoadGesture.Click += new System.EventHandler(this.LoadGesture_Click);
            // 
            // ViewGesture
            // 
            this.ViewGesture.Index = 2;
            this.ViewGesture.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.ViewGesture.Text = "&View";
            this.ViewGesture.Click += new System.EventHandler(this.ViewGesture_Click);
            // 
            // ClearGestures
            // 
            this.ClearGestures.Index = 3;
            this.ClearGestures.Text = "&Clear";
            this.ClearGestures.Click += new System.EventHandler(this.ClearGestures_Click);
            // 
            // Separator0
            // 
            this.Separator0.Index = 4;
            this.Separator0.Text = "-";
            // 
            // TestBatch
            // 
            this.TestBatch.Index = 5;
            this.TestBatch.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.TestBatch.Text = "&Test Batch...";
            this.TestBatch.Click += new System.EventHandler(this.TestBatch_Click);
            // 
            // TestDirectories
            // 
            this.TestDirectories.Index = 6;
            this.TestDirectories.Text = "Test &Directory Tree...";
            this.TestDirectories.Click += new System.EventHandler(this.TestDirectories_Click);
            // 
            // SearchMenu
            // 
            this.SearchMenu.Index = 2;
            this.SearchMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniGSS,
            this.mniProtractor});
            this.SearchMenu.Text = "&Search";
            this.SearchMenu.Popup += new System.EventHandler(this.SearchMenu_Popup);
            // 
            // mniGSS
            // 
            this.mniGSS.Checked = true;
            this.mniGSS.Index = 0;
            this.mniGSS.RadioCheck = true;
            this.mniGSS.Text = "&Golden Section Search";
            this.mniGSS.Click += new System.EventHandler(this.mniGSS_Click);
            // 
            // mniProtractor
            // 
            this.mniProtractor.Index = 1;
            this.mniProtractor.RadioCheck = true;
            this.mniProtractor.Text = "&Protractor";
            this.mniProtractor.Click += new System.EventHandler(this.mniProtractor_Click);
            // 
            // GraphMenu
            // 
            this.GraphMenu.Index = 3;
            this.GraphMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.RotationGraph,
            this.RotateSimilar});
            this.GraphMenu.Text = "G&raph";
            this.GraphMenu.Popup += new System.EventHandler(this.GraphMenu_Popup);
            // 
            // RotationGraph
            // 
            this.RotationGraph.Index = 0;
            this.RotationGraph.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.RotationGraph.Text = "Rotation &Graph...";
            this.RotationGraph.Click += new System.EventHandler(this.RotationGraph_Click);
            // 
            // RotateSimilar
            // 
            this.RotateSimilar.Checked = true;
            this.RotateSimilar.Index = 1;
            this.RotateSimilar.Text = "&Pair Similar";
            this.RotateSimilar.Click += new System.EventHandler(this.RotateSimilar_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.Index = 4;
            this.HelpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.About});
            this.HelpMenu.Text = "&Help";
            // 
            // About
            // 
            this.About.Index = 0;
            this.About.Text = "&About...";
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblResult.Location = new System.Drawing.Point(324, 24);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(28, 13);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "Test";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRecognizing
            // 
            this.lblRecognizing.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecognizing.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRecognizing.Location = new System.Drawing.Point(0, 24);
            this.lblRecognizing.Name = "lblRecognizing";
            this.lblRecognizing.Size = new System.Drawing.Size(324, 23);
            this.lblRecognizing.TabIndex = 0;
            this.lblRecognizing.Text = "Recognizing...";
            this.lblRecognizing.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblRecognizing.Visible = false;
            // 
            // prgTesting
            // 
            this.prgTesting.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgTesting.Location = new System.Drawing.Point(0, 39);
            this.prgTesting.Name = "prgTesting";
            this.prgTesting.Size = new System.Drawing.Size(352, 23);
            this.prgTesting.TabIndex = 3;
            this.prgTesting.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(352, 203);
            this.Controls.Add(this.prgTesting);
            this.Controls.Add(this.lblRecognizing);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblRecord);
            this.DoubleBuffered = true;
            this.Menu = this.MainMenu;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "$1 Recognizer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region File Menu

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        #endregion

        #region Gestures Menu

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureMenu_Popup(object sender, System.EventArgs e)
        {
            RecordGesture.Checked = _recording;
            ViewGesture.Checked = (_viewFrm != null && !_viewFrm.IsDisposed);
            ClearGestures.Enabled = (_rec.NumGestures > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGesture_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gestures";
            dlg.Multiselect = true;
            dlg.RestoreDirectory = false;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    string name = dlg.FileNames[i];
                    _rec.LoadGesture(name);
                }
                ReloadViewForm();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewGesture_Click(object sender, System.EventArgs e)
        {
            if (_viewFrm != null && !_viewFrm.IsDisposed)
            {
                _viewFrm.Close();
                _viewFrm = null;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                _viewFrm = new ViewForm(_rec.Gestures);
                _viewFrm.Owner = this;
                _viewFrm.Show();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReloadViewForm()
        {
            if (_viewFrm != null && !_viewFrm.IsDisposed)
            {
                _viewFrm.Close();
                _viewFrm = new ViewForm(_rec.Gestures);
                _viewFrm.Owner = this;
                _viewFrm.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordGesture_Click(object sender, System.EventArgs e)
        {
            _points.Clear();
            Invalidate();
            _recording = !_recording; // recording will happen on mouse-up
            lblRecord.Visible = _recording;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearGestures_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, "This will clear all loaded gestures. (It will not delete any XML files.)", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                _rec.ClearGestures();
                ReloadViewForm();
            }
        }

        /// <summary>
        /// This menu command allows the user to multi-select a handful of gesture XML files from a directory, and 
        /// to produce an output file containing the recognition results for everything in the directory.
        /// </summary>
        /// <param name="sender">The sender of this event.</param>
        /// <param name="e">The arguments for this event.</param>
        /// <remarks>
        /// The gestures loaded must conform to a naming convention where each example of a particular gesture is named 
        /// with the same string, followed by a numerical identifier for that example. As in:
        ///
        ///     circle01.xml        // circle gestures
        ///     circle02.xml
        ///     circle03.xml
        ///     square01.xml        // square gestures
        ///     square02.xml
        ///     square03.xml
        ///     triangle01.xml      // triangle gestures
        ///     triangle02.xml
        ///     triangle03.xml
        ///
        /// The same number of examples should be read in for each gesture category. The testing procedure will load a 
        /// random subset of each gesture and test on the remaining gestures.
        /// 
        /// <b>Warning.</b> This process will throw an exception if the number of gesture examples for each gesture is 
        /// unbalanced.
        /// </remarks>
        private void TestBatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gesture Batch";
            dlg.Multiselect = true;
            dlg.RestoreDirectory = false;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                InfoForm ifrm = new InfoForm();
                if (ifrm.ShowDialog(this) == DialogResult.OK)
                {
                    prgTesting.Visible = true;
                    lblRecognizing.Visible = true;
                    Application.DoEvents();

                    // each slot in the list contains a gesture Category, which contains a list of gesture prototypes.
                    List<Category> categories = _rec.AssembleBatch(dlg.FileNames);

                    if (categories != null)
                    {
                        string[] rstr = _rec.TestBatch(
                            ifrm.Subject.ToString(),
                            ifrm.Speed, 
                            categories, 
                            dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\')),
                            _protractor
                            );
                        if (rstr != null)
                        {
                            MessageBox.Show(this, "Testing complete.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(this, "There was an error writing the output file during testing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // error assembling batch
                    {
                        MessageBox.Show(this, "Unreadable files, or unbalanced number of gestures in categories.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    lblRecognizing.Visible = false;
                    prgTesting.Visible = false;
                }
            }
        }

        /// <summary>
        /// This menu command allows the user to select a top-level directory in which subfolders are assumed to 
        /// represent gestures from one subject, and subsubfolders represent gestures of a given speed. A single
        /// output file for the entire directory structure is made.
        /// </summary>
        /// <param name="sender">The sender of this event.</param>
        /// <param name="e">The arguments for this event.</param>
        /// <remarks>
        /// The top-level directory can have any name, e.g., "logs", and its subdirectories are assumed to represent
        /// individual subjects, e.g., "s01", "s02", "s03", etc. Within each subject, the directories are assumed to
        /// represent speeds, e.g., "fast", "medium", and "slow". In reality, these two tokens (the subject name and 
        /// speed name) are just used to form the first two columns of the output, so they can be any string whatsoever.
        /// 
        /// The gesture files themselves loaded must conform to a naming convention where each example of a particular gesture 
        /// is named with the same string, followed by a numerical identifier for that example. As in:
        ///
        ///     circle01.xml        // circle gestures
        ///     circle02.xml
        ///     circle03.xml
        ///     square01.xml        // square gestures
        ///     square02.xml
        ///     square03.xml
        ///     triangle01.xml      // triangle gestures
        ///     triangle02.xml
        ///     triangle03.xml
        ///
        /// The same number of examples should be read in for each gesture category. The testing procedure will load a 
        /// random subset of each gesture and test on the remaining gestures.
        /// 
        /// <b>Warning.</b> This process will throw an exception if the number of gesture examples for each gesture is 
        /// unbalanced.
        /// </remarks>
        private void TestDirectories_Click(object sender, EventArgs e)
        {
            FoldersForm frm = new FoldersForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = "Select top-level folder.";
                dlg.SelectedPath = Directory.GetCurrentDirectory();

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string path = String.Format("{0}\\{1}__{2}.txt", dlg.SelectedPath, dlg.SelectedPath.Substring(dlg.SelectedPath.LastIndexOf('\\') + 1), Environment.TickCount);
                    StreamWriter outfile = new StreamWriter(path, true, Encoding.UTF8);

                    string[] subjectDirectories = Directory.GetDirectories(dlg.SelectedPath);
                    for (int i = 0; i < subjectDirectories.Length; i++)
                    {
                        string[] speedDirectories = Directory.GetDirectories(subjectDirectories[i]);
                        for (int j = 0; j < speedDirectories.Length; j++)
                        {
                            prgTesting.Visible = true;
                            lblRecognizing.Text = String.Format("Processing \"{0}\" ({1}/{2}) in \"{3}\" ({4}/{5}).", 
                                speedDirectories[j].Substring(speedDirectories[j].LastIndexOf('\\') + 1), 
                                j + 1, 
                                speedDirectories.Length, 
                                subjectDirectories[i].Substring(subjectDirectories[i].LastIndexOf('\\') + 1), 
                                i + 1, 
                                subjectDirectories.Length
                                );
                            lblRecognizing.Visible = true;
                            Application.DoEvents();

                            // Each slot in the list contains a gesture Category, which contains a list of gesture prototypes.
                            string[] filenames = Directory.GetFiles(speedDirectories[j]);
                            List<Category> categories = _rec.AssembleBatch(filenames);
                            if (categories != null)
                            {
                                string[] rstr = _rec.TestBatch(
                                    subjectDirectories[i].Substring(subjectDirectories[i].LastIndexOf('\\') + 1), 
                                    speedDirectories[j].Substring(speedDirectories[j].LastIndexOf('\\') + 1), 
                                    categories, 
                                    dlg.SelectedPath,
                                    _protractor
                                    );
                                if (rstr != null)
                                {
                                    SystemSounds.Question.Play(); // success
                                    // now append the file at rstr[0] to the outfile
                                    StreamReader r = new StreamReader(rstr[0], Encoding.UTF8);
                                    string line = "tmp";
                                    while ((line = r.ReadLine()) != String.Empty) { };
                                    while ((line = r.ReadLine()) == String.Empty) { };
                                    while (line != String.Empty)
                                    {
                                        outfile.WriteLine(line);
                                        line = r.ReadLine();
                                    }
                                    r.Close();
                                    File.Delete(rstr[0]);
                                    File.Delete(rstr[1]);
                                }
                                else // failure
                                {
                                    string msg = String.Format("There was an error in the current log.");
                                    MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else // error assembling batch
                            {
                                MessageBox.Show(this, "Unreadable files, or unbalanced number of gestures in categories.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            lblRecognizing.Visible = false;
                            prgTesting.Visible = false;
                        }
                    }

                    outfile.Close();
                    lblRecognizing.Text = "Recognizing...";
                }
            }
        }

        /// <summary>
        /// Update this function when progress changes (callback).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnProgressChanged(object source, ProgressEventArgs e)
        {
            prgTesting.Value = (int) (e.Percent * 100.0);
            Application.DoEvents();
        }

        #endregion

        #region Search Menu

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchMenu_Popup(object sender, EventArgs e)
        {
            mniGSS.Checked = !_protractor;
            mniProtractor.Checked = _protractor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniGSS_Click(object sender, EventArgs e)
        {
            _protractor = false;
            if (_points.Count >= MinNoPoints && _rec.NumGestures > 0)
            {
                RecognizeAndDisplayResults();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniProtractor_Click(object sender, EventArgs e)
        {
            _protractor = true;
            if (_points.Count >= MinNoPoints && _rec.NumGestures > 0)
            {
                RecognizeAndDisplayResults();
            }
        }

        #endregion

        #region Graph Menu

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphMenu_Popup(object sender, EventArgs e)
        {
            RotateSimilar.Checked = _similar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotationGraph_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gesture Pairs";
            dlg.Multiselect = true;
            dlg.RestoreDirectory = false;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.FileNames.Length % 2 != 0)
                {
                    MessageBox.Show(this, "Pairs of two gestures must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FolderBrowserDialog fld = new FolderBrowserDialog();
                    fld.Description = "Select a folder where the results file will be written.";
                    fld.SelectedPath = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\'));
                    if (fld.ShowDialog() == DialogResult.OK)
                    {
                        string[] filenames = new string[dlg.FileNames.Length];
                        Array.Copy(dlg.FileNames, filenames, dlg.FileNames.Length);

                        Array.Sort(filenames); // sorts alphabetically
                        if (!_similar) // doing rotation graphs for dissimilar gestures
                        {
                            bool dissimilar = false;
                            while (!dissimilar)
                            {
                                for (int j = 0; j < filenames.Length * 2; j++) // random shuffle
                                {
                                    int pos1 = RandomEx.Integer(0, filenames.Length - 1);
                                    int pos2 = RandomEx.Integer(0, filenames.Length - 1);
                                    string tmp = filenames[pos1];
                                    filenames[pos1] = filenames[pos2];
                                    filenames[pos2] = tmp;
                                }
                                for (int j = 0; j < filenames.Length; j += 2) // ensure no pairs are same category
                                {
                                    string cat1 = Category.ParseName(Unistroke.ParseName(filenames[j + 1]));
                                    string cat2 = Category.ParseName(Unistroke.ParseName(filenames[j]));
                                    dissimilar = (cat1 != cat2); // set the flag
                                    if (!dissimilar)
                                        break;
                                }
                            }
                        }

                        // now do the rotating and declare victory
                        bool failed = false;
                        for (int i = 0; i < filenames.Length; i += 2)
                        {
                            if (!_rec.CreateRotationGraph(filenames[i + 1], filenames[i], fld.SelectedPath, _similar))
                            {
                                MessageBox.Show(this, "There was an error reading or writing files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                failed = true;
                            }
                        }
                        if (!failed)
                        {
                            MessageBox.Show(this, "Finished rotations of gesture pair(s).", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateSimilar_Click(object sender, EventArgs e)
        {
            _similar = !_similar;
        }

        #endregion

        #region About Menu

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, System.EventArgs e)
        {
            AboutForm frm = new AboutForm(_points);
            frm.ShowDialog(this);
        }

        #endregion

        #region Window Form Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (_points.Count > 0)
            {
                e.Graphics.FillEllipse(_recording ? Brushes.Firebrick : Brushes.DarkBlue, _points[0].X - 5f, _points[0].Y - 5f, 10f, 10f);
            }
            foreach (TimePointF p in _points)
            {
                e.Graphics.FillEllipse(_recording ? Brushes.Firebrick : Brushes.DarkBlue, p.X - 2f, p.Y - 2f, 4f, 4f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            lblResult.Text = String.Empty;
        }

        #endregion

        #region Mouse Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			_isDown = true;
			_points.Clear();
            _points.Add(new TimePointF(e.X, e.Y, TimeEx.NowMs));
			Invalidate();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MainForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (_isDown)
			{
                _points.Add(new TimePointF(e.X, e.Y, TimeEx.NowMs));
				Invalidate(new Rectangle(e.X - 2, e.Y - 2, 4, 4));
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MainForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (_isDown)
			{
				_isDown = false;

                if (_points.Count >= MinNoPoints)
				{
					if (_recording)
					{
						SaveFileDialog dlg = new SaveFileDialog();
						dlg.Filter = "Gestures (*.xml)|*.xml";
						dlg.Title = "Save Gesture As";
						dlg.AddExtension = true;
                        dlg.RestoreDirectory = false;

						if (dlg.ShowDialog(this) == DialogResult.OK)
						{
							_rec.SaveGesture(dlg.FileName, _points);  // resample, scale, translate to origin
							ReloadViewForm();
						}

						dlg.Dispose();
						_recording = false;
						lblRecord.Visible = false;
						Invalidate();
					}
					else if (_rec.NumGestures > 0) // not recording, so testing
					{
                        RecognizeAndDisplayResults();
					}
				}
			}
		}

        /// <summary>
        /// 
        /// </summary>
        private void RecognizeAndDisplayResults()
        {
            lblRecognizing.Visible = true;
            Application.DoEvents(); // forces label to display

            NBestList result = _rec.Recognize(_points, _protractor); // where all the action is!!
            lblResult.Text = String.Format("{0}: {1} ({2}px, {3}{4})",
                result.Name,
                Math.Round(result.Score, 2),
                Math.Round(result.Distance, 2),
                Math.Round(result.Angle, 2), (char) 176);

            lblRecognizing.Visible = false;
        }

		#endregion

	}
}
