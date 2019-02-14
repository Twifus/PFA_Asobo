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

namespace Recognizer.Dollar
{
	public class ViewForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblNone;
		private System.Windows.Forms.TabControl StrokeTabs;
		private List<Unistroke> _prototypes;

		public ViewForm(List<Unistroke> prototypes)
		{
			InitializeComponent();
			_prototypes = prototypes;
		}

        private class DoubleBufferedTabPage : TabPage
        {
            public DoubleBufferedTabPage(string name)
                : base(name)
            {
                this.DoubleBuffered = true; // reduce flicker
            }
        }

		private void ViewForm_Load(object sender, System.EventArgs e)
		{
			this.Left = Owner.Right + 1;
			this.Top = Owner.Top;

			StrokeTabs.Dock = DockStyle.Fill;
			ViewForm_Resize(null, EventArgs.Empty);

			if (_prototypes.Count == 0)
			{
				StrokeTabs.Visible = false;
				lblNone.Visible = true;
			}
			else
			{
				foreach (Unistroke p in _prototypes)
				{
					DoubleBufferedTabPage page = new DoubleBufferedTabPage(p.Name);
                    page.BackColor = SystemColors.Window;
					page.Paint += new PaintEventHandler(OnPaintPage);
					StrokeTabs.TabPages.Add(page);
				}
				int tabWidth = 0;
				for (int i = 0; i < StrokeTabs.TabCount; i++)
				{
					Rectangle r = StrokeTabs.GetTabRect(i);
					tabWidth += r.Width;
				}
                this.Width = Math.Max(Width, Math.Min(Screen.PrimaryScreen.WorkingArea.Width / 2, tabWidth + 20));
            }
		}

		private void OnPaintPage(object sender, PaintEventArgs e)
		{
			TabPage page = (TabPage) sender;
			foreach (Unistroke g in _prototypes)
			{
                if (page.Text == g.Name)
				{
                    e.Graphics.FillEllipse(Brushes.Firebrick, g.RawPoints[0].X - 5f, g.RawPoints[0].Y - 5f, 10f, 10f);

                    foreach (PointF p in g.RawPoints)
					{
						e.Graphics.FillEllipse(Brushes.Firebrick, p.X - 2f, p.Y - 2f, 4f, 4f);
					}
					break;
				}
			}
		}

		private void ViewForm_Resize(object sender, System.EventArgs e)
		{
			lblNone.Left = ClientRectangle.Width / 2 - lblNone.Width / 2;
			lblNone.Top = ClientRectangle.Height / 2 - lblNone.Height / 2;
		}

        private void ViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_prototypes.Clear();

				foreach (TabPage page in StrokeTabs.TabPages)
				{
					page.Dispose();
				}

				if (components != null)
				{
					components.Dispose();
				}
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
            this.StrokeTabs = new System.Windows.Forms.TabControl();
            this.lblNone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StrokeTabs
            // 
            this.StrokeTabs.Location = new System.Drawing.Point(16, 16);
            this.StrokeTabs.Name = "StrokeTabs";
            this.StrokeTabs.SelectedIndex = 0;
            this.StrokeTabs.Size = new System.Drawing.Size(80, 72);
            this.StrokeTabs.TabIndex = 0;
            // 
            // lblNone
            // 
            this.lblNone.AutoSize = true;
            this.lblNone.Location = new System.Drawing.Point(56, 160);
            this.lblNone.Name = "lblNone";
            this.lblNone.Size = new System.Drawing.Size(239, 13);
            this.lblNone.TabIndex = 1;
            this.lblNone.Text = "There are no prototype gestures currently loaded.";
            this.lblNone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNone.Visible = false;
            // 
            // ViewForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(352, 326);
            this.Controls.Add(this.lblNone);
            this.Controls.Add(this.StrokeTabs);
            this.Name = "ViewForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Templates";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewForm_FormClosing);
            this.Load += new System.EventHandler(this.ViewForm_Load);
            this.Resize += new System.EventHandler(this.ViewForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	}
}
