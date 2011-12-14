// --------------------------------------------------------------------------------------------------------------------
// <copyright file="frmWinBoard.cs" company="SharpChess">
//   Peter Hughes
// </copyright>
// <summary>
//   Summary description for Form1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region License

// SharpChess
// Copyright (C) 2011 Peter Hughes
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace SharpChess
{
    #region Using

    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;

    #endregion

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmWinBoard : Form
    {
        #region Constants and Fields

        /// <summary>
        ///   Required designer variable.
        /// </summary>
        private readonly Container components = null;

        /// <summary>
        /// The col direction.
        /// </summary>
        private ColumnHeader colDirection;

        /// <summary>
        /// The col message.
        /// </summary>
        private ColumnHeader colMessage;

        /// <summary>
        /// The lvw win board.
        /// </summary>
        private ListView lvwWinBoard;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="frmWinBoard"/> class.
        /// </summary>
        public frmWinBoard()
        {
            // Required for Windows Form Designer support
            this.InitializeComponent();

            // TODO: Add any constructor code after InitializeComponent call
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The delegatetype win board closed.
        /// </summary>
        public delegate void delegatetypeWinBoardClosed();

        #endregion

        #region Public Events

        /// <summary>
        /// The win board closed event.
        /// </summary>
        public event delegatetypeWinBoardClosed WinBoardClosedEvent;

        #endregion

        #region Public Methods

        /// <summary>
        /// The log win board message.
        /// </summary>
        /// <param name="strDirection">
        /// The str direction.
        /// </param>
        /// <param name="strMessage">
        /// The str message.
        /// </param>
        public void LogWinBoardMessage(string strDirection, string strMessage)
        {
            string[] lvi = { strDirection, strMessage };
            this.lvwWinBoard.Items.Add(new ListViewItem(lvi));
            this.lvwWinBoard.EnsureVisible(this.lvwWinBoard.Items.Count - 1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ResourceManager resources = new ResourceManager(typeof(frmWinBoard));
            this.lvwWinBoard = new ListView();
            this.colDirection = new ColumnHeader();
            this.colMessage = new ColumnHeader();
            this.SuspendLayout();

            // lvwWinBoard
            this.lvwWinBoard.Columns.AddRange(new[] { this.colDirection, this.colMessage });
            this.lvwWinBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwWinBoard.FullRowSelect = true;
            this.lvwWinBoard.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwWinBoard.Location = new Point(0, 0);
            this.lvwWinBoard.Name = "lvwWinBoard";
            this.lvwWinBoard.Size = new Size(248, 262);
            this.lvwWinBoard.TabIndex = 143;
            this.lvwWinBoard.View = System.Windows.Forms.View.Details;

            // colDirection
            this.colDirection.Text = "Dir";
            this.colDirection.Width = 29;

            // colMessage
            this.colMessage.Text = "Message";
            this.colMessage.Width = 199;

            // frmWinBoard
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(248, 262);
            this.Controls.Add(this.lvwWinBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.Name = "frmWinBoard";
            this.Text = "WinBoard Message Log";
            this.Closing += new CancelEventHandler(this.frmWinBoard_Closing);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// The frm win board_ closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void frmWinBoard_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.WinBoardClosedEvent();
        }

        #endregion
    }
}