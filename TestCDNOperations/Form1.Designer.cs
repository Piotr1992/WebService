namespace TestCDNOperations
{
    partial class Form1
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
            this.btnSerialize = new System.Windows.Forms.Button();
            this.tbDeserialized = new System.Windows.Forms.TextBox();
            this.btnDeserialize = new System.Windows.Forms.Button();
            this.tbSerialized = new System.Windows.Forms.TextBox();
            this.cbObject = new System.Windows.Forms.ComboBox();
            this.bntBuildZam = new System.Windows.Forms.Button();
            this.tbError = new System.Windows.Forms.TextBox();
            this.btnOpenZam = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.btnAddAdres = new System.Windows.Forms.Button();
            this.btnTestService = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSerialize
            // 
            this.btnSerialize.Location = new System.Drawing.Point(354, 89);
            this.btnSerialize.Name = "btnSerialize";
            this.btnSerialize.Size = new System.Drawing.Size(121, 23);
            this.btnSerialize.TabIndex = 0;
            this.btnSerialize.Text = "Serialize->";
            this.btnSerialize.UseVisualStyleBackColor = true;
            this.btnSerialize.Click += new System.EventHandler(this.btnSerialize_Click);
            // 
            // tbDeserialized
            // 
            this.tbDeserialized.Location = new System.Drawing.Point(12, 12);
            this.tbDeserialized.Multiline = true;
            this.tbDeserialized.Name = "tbDeserialized";
            this.tbDeserialized.Size = new System.Drawing.Size(327, 357);
            this.tbDeserialized.TabIndex = 1;
            this.tbDeserialized.Text = "https://89.171.52.234:8080/b2b/webservice.asmx";
            this.tbDeserialized.TextChanged += new System.EventHandler(this.tbDeserialized_TextChanged);
            // 
            // btnDeserialize
            // 
            this.btnDeserialize.Location = new System.Drawing.Point(354, 129);
            this.btnDeserialize.Name = "btnDeserialize";
            this.btnDeserialize.Size = new System.Drawing.Size(121, 23);
            this.btnDeserialize.TabIndex = 2;
            this.btnDeserialize.Text = "<-Deserialize";
            this.btnDeserialize.UseVisualStyleBackColor = true;
            this.btnDeserialize.Click += new System.EventHandler(this.btnDeserialize_Click);
            // 
            // tbSerialized
            // 
            this.tbSerialized.Location = new System.Drawing.Point(481, 12);
            this.tbSerialized.Multiline = true;
            this.tbSerialized.Name = "tbSerialized";
            this.tbSerialized.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSerialized.Size = new System.Drawing.Size(327, 357);
            this.tbSerialized.TabIndex = 3;
            this.tbSerialized.Text = "b2b_danebinarne";
            this.tbSerialized.TextChanged += new System.EventHandler(this.tbSerialized_TextChanged);
            // 
            // cbObject
            // 
            this.cbObject.FormattingEnabled = true;
            this.cbObject.Location = new System.Drawing.Point(354, 39);
            this.cbObject.Name = "cbObject";
            this.cbObject.Size = new System.Drawing.Size(121, 21);
            this.cbObject.TabIndex = 4;
            this.cbObject.SelectedIndexChanged += new System.EventHandler(this.cbObject_SelectedIndexChanged);
            // 
            // bntBuildZam
            // 
            this.bntBuildZam.Location = new System.Drawing.Point(370, 189);
            this.bntBuildZam.Name = "bntBuildZam";
            this.bntBuildZam.Size = new System.Drawing.Size(75, 23);
            this.bntBuildZam.TabIndex = 5;
            this.bntBuildZam.Text = "BuildZam->";
            this.bntBuildZam.UseVisualStyleBackColor = true;
            this.bntBuildZam.Click += new System.EventHandler(this.bntBuildZam_Click);
            // 
            // tbError
            // 
            this.tbError.Location = new System.Drawing.Point(29, 436);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.Size = new System.Drawing.Size(751, 92);
            this.tbError.TabIndex = 6;
            this.tbError.TextChanged += new System.EventHandler(this.tbError_TextChanged);
            // 
            // btnOpenZam
            // 
            this.btnOpenZam.Location = new System.Drawing.Point(386, 274);
            this.btnOpenZam.Name = "btnOpenZam";
            this.btnOpenZam.Size = new System.Drawing.Size(75, 23);
            this.btnOpenZam.TabIndex = 7;
            this.btnOpenZam.Text = "OpenZam";
            this.btnOpenZam.UseVisualStyleBackColor = true;
            this.btnOpenZam.Click += new System.EventHandler(this.btnOpenZam_Click);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(386, 228);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 8;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // btnAddAdres
            // 
            this.btnAddAdres.Location = new System.Drawing.Point(354, 304);
            this.btnAddAdres.Name = "btnAddAdres";
            this.btnAddAdres.Size = new System.Drawing.Size(91, 23);
            this.btnAddAdres.TabIndex = 9;
            this.btnAddAdres.Text = "<- Build Adres";
            this.btnAddAdres.UseVisualStyleBackColor = true;
            this.btnAddAdres.Click += new System.EventHandler(this.btnAddAdres_Click);
            // 
            // btnTestService
            // 
            this.btnTestService.Location = new System.Drawing.Point(354, 343);
            this.btnTestService.Name = "btnTestService";
            this.btnTestService.Size = new System.Drawing.Size(121, 23);
            this.btnTestService.TabIndex = 10;
            this.btnTestService.Text = "<- TestAdresAdd";
            this.btnTestService.UseMnemonic = false;
            this.btnTestService.UseVisualStyleBackColor = true;
            this.btnTestService.Click += new System.EventHandler(this.btnTestService_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 373);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "DokumentNagInfo ->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 402);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "DokumentNagElemInfo ->";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 552);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTestService);
            this.Controls.Add(this.btnAddAdres);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.btnOpenZam);
            this.Controls.Add(this.tbError);
            this.Controls.Add(this.bntBuildZam);
            this.Controls.Add(this.cbObject);
            this.Controls.Add(this.tbSerialized);
            this.Controls.Add(this.btnDeserialize);
            this.Controls.Add(this.tbDeserialized);
            this.Controls.Add(this.btnSerialize);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSerialize;
        private System.Windows.Forms.TextBox tbDeserialized;
        private System.Windows.Forms.Button btnDeserialize;
        private System.Windows.Forms.TextBox tbSerialized;
        private System.Windows.Forms.ComboBox cbObject;
        private System.Windows.Forms.Button bntBuildZam;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.Button btnOpenZam;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button btnAddAdres;
        private System.Windows.Forms.Button btnTestService;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

