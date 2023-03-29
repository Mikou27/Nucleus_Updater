
namespace Updater
{
    partial class Updater
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.label = new System.Windows.Forms.Label();
            this.btn_yes = new System.Windows.Forms.Button();
            this.prog_DownloadBar = new System.Windows.Forms.ProgressBar();
            this.btn_no = new System.Windows.Forms.Button();
            this.text1 = new System.Windows.Forms.Label();
            this.label_changelog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(130, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 13);
            this.label.TabIndex = 0;
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_yes
            // 
            this.btn_yes.BackColor = System.Drawing.Color.Transparent;
            this.btn_yes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_yes.ForeColor = System.Drawing.Color.White;
            this.btn_yes.Location = new System.Drawing.Point(31, 39);
            this.btn_yes.Name = "btn_yes";
            this.btn_yes.Size = new System.Drawing.Size(100, 23);
            this.btn_yes.TabIndex = 1;
            this.btn_yes.Text = "Yes";
            this.btn_yes.UseVisualStyleBackColor = false;
            this.btn_yes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_yes_MouseClick);
            // 
            // prog_DownloadBar
            // 
            this.prog_DownloadBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.prog_DownloadBar.ForeColor = System.Drawing.Color.Black;
            this.prog_DownloadBar.Location = new System.Drawing.Point(31, 39);
            this.prog_DownloadBar.Name = "prog_DownloadBar";
            this.prog_DownloadBar.Size = new System.Drawing.Size(207, 23);
            this.prog_DownloadBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prog_DownloadBar.TabIndex = 2;
            this.prog_DownloadBar.Visible = false;
            // 
            // btn_no
            // 
            this.btn_no.BackColor = System.Drawing.Color.Transparent;
            this.btn_no.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_no.ForeColor = System.Drawing.Color.White;
            this.btn_no.Location = new System.Drawing.Point(137, 39);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(100, 23);
            this.btn_no.TabIndex = 3;
            this.btn_no.Text = "No";
            this.btn_no.UseVisualStyleBackColor = false;
            this.btn_no.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_no_MouseClick);
            // 
            // text1
            // 
            this.text1.AutoSize = true;
            this.text1.BackColor = System.Drawing.Color.Transparent;
            this.text1.ForeColor = System.Drawing.Color.White;
            this.text1.Location = new System.Drawing.Point(35, 5);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(0, 13);
            this.text1.TabIndex = 4;
            this.text1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_changelog
            // 
            this.label_changelog.AutoSize = true;
            this.label_changelog.BackColor = System.Drawing.Color.Transparent;
            this.label_changelog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_changelog.ForeColor = System.Drawing.Color.Yellow;
            this.label_changelog.Location = new System.Drawing.Point(217, 65);
            this.label_changelog.Name = "label_changelog";
            this.label_changelog.Size = new System.Drawing.Size(49, 12);
            this.label_changelog.TabIndex = 5;
            this.label_changelog.Text = "Changelog";
            this.label_changelog.Click += new System.EventHandler(this.label_changelog_Click);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Updater.Properties.Resources.updaterBack1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(267, 80);
            this.Controls.Add(this.label_changelog);
            this.Controls.Add(this.btn_no);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.btn_yes);
            this.Controls.Add(this.prog_DownloadBar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(283, 119);
            this.MinimumSize = new System.Drawing.Size(283, 107);
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nucleus  Updater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Updater_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btn_yes;
        private System.Windows.Forms.ProgressBar prog_DownloadBar;
        private System.Windows.Forms.Button btn_no;
        private System.Windows.Forms.Label text1;
        private System.Windows.Forms.Label label_changelog;
    }
}

