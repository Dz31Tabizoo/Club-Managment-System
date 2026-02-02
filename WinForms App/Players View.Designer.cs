namespace WinForms_App
{
    partial class frm_Players_View
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
            this.components = new System.ComponentModel.Container();
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.dgvPlayers = new Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // dgvPlayers
            // 
            this.dgvPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayers.Location = new System.Drawing.Point(3, 307);
            this.dgvPlayers.Name = "dgvPlayers";
            this.dgvPlayers.Size = new System.Drawing.Size(1173, 385);
            this.dgvPlayers.TabIndex = 2;
            // 
            // frm_Players_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.dgvPlayers);
            this.Name = "frm_Players_View";
            this.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Text = "Players View";
            this.Load += new System.EventHandler(this.frm_Players_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
        private Krypton.Toolkit.KryptonDataGridView dgvPlayers;
    }
}

