
namespace MediaTekDocuments.view
{
    partial class FrmAuthentification
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
            this.txtbxIdentifiant = new System.Windows.Forms.TextBox();
            this.txtbxMdp = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblMdp = new System.Windows.Forms.Label();
            this.btnConnexion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbxIdentifiant
            // 
            this.txtbxIdentifiant.Location = new System.Drawing.Point(176, 34);
            this.txtbxIdentifiant.Name = "txtbxIdentifiant";
            this.txtbxIdentifiant.Size = new System.Drawing.Size(178, 22);
            this.txtbxIdentifiant.TabIndex = 0;
            // 
            // txtbxMdp
            // 
            this.txtbxMdp.Location = new System.Drawing.Point(176, 82);
            this.txtbxMdp.Name = "txtbxMdp";
            this.txtbxMdp.PasswordChar = '*';
            this.txtbxMdp.Size = new System.Drawing.Size(178, 22);
            this.txtbxMdp.TabIndex = 2;
            this.txtbxMdp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxMdp_KeyPress);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(44, 39);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(77, 17);
            this.lblNom.TabIndex = 3;
            this.lblNom.Text = "Identifiant :";
            // 
            // lblMdp
            // 
            this.lblMdp.AutoSize = true;
            this.lblMdp.Location = new System.Drawing.Point(44, 87);
            this.lblMdp.Name = "lblMdp";
            this.lblMdp.Size = new System.Drawing.Size(101, 17);
            this.lblMdp.TabIndex = 5;
            this.lblMdp.Text = "Mot de passe :";
            // 
            // btnConnexion
            // 
            this.btnConnexion.Location = new System.Drawing.Point(107, 142);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(190, 25);
            this.btnConnexion.TabIndex = 6;
            this.btnConnexion.Text = "Connexion";
            this.btnConnexion.UseVisualStyleBackColor = true;
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);
            this.btnConnexion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnConnexion_KeyPress);
            // 
            // FrmAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 196);
            this.Controls.Add(this.btnConnexion);
            this.Controls.Add(this.lblMdp);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtbxMdp);
            this.Controls.Add(this.txtbxIdentifiant);
            this.Name = "FrmAuthentification";
            this.Text = "Authentification";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnConnexion_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxIdentifiant;
        private System.Windows.Forms.TextBox txtbxMdp;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblMdp;
        private System.Windows.Forms.Button btnConnexion;
    }
}