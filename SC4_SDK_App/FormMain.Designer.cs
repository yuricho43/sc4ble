
namespace SC4_SDK_App
{
    partial class FormSC4
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDeviceList = new System.Windows.Forms.ComboBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.listDebug = new System.Windows.Forms.ListBox();
            this.chkSetMode = new System.Windows.Forms.CheckBox();
            this.chkSetUnit = new System.Windows.Forms.CheckBox();
            this.chkSetDistanceToBall = new System.Windows.Forms.CheckBox();
            this.chkSetTeeHight = new System.Windows.Forms.CheckBox();
            this.chkSetCarryTotal = new System.Windows.Forms.CheckBox();
            this.chkSetLoftAngle = new System.Windows.Forms.CheckBox();
            this.chkSetClub = new System.Windows.Forms.CheckBox();
            this.chkSetTargetDistance = new System.Windows.Forms.CheckBox();
            this.cmbSetCarryTotal = new System.Windows.Forms.ComboBox();
            this.cmbSetMode = new System.Windows.Forms.ComboBox();
            this.cmbSetUnit = new System.Windows.Forms.ComboBox();
            this.textSetTeeHight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textSetDistanceToBall = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textSetTargetDistance = new System.Windows.Forms.TextBox();
            this.cmbSetClub = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textSetLoftAngle = new System.Windows.Forms.TextBox();
            this.textCommand = new System.Windows.Forms.TextBox();
            this.btnDevInfo = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.timerConnect = new System.Windows.Forms.Timer(this.components);
            this.btnDFU = new System.Windows.Forms.Button();
            this.btnDat = new System.Windows.Forms.Button();
            this.btnBin = new System.Windows.Forms.Button();
            this.textDat = new System.Windows.Forms.TextBox();
            this.textBin = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timerDFU = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 22);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(111, 25);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan";
            this.btnScan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Red;
            this.btnConnect.Location = new System.Drawing.Point(12, 57);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(111, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(12, 124);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(111, 25);
            this.btnWrite.TabIndex = 6;
            this.btnWrite.Text = "Write Command";
            this.btnWrite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Device Lists:";
            // 
            // comboDeviceList
            // 
            this.comboDeviceList.FormattingEnabled = true;
            this.comboDeviceList.Location = new System.Drawing.Point(130, 24);
            this.comboDeviceList.Name = "comboDeviceList";
            this.comboDeviceList.Size = new System.Drawing.Size(134, 20);
            this.comboDeviceList.TabIndex = 9;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(475, 179);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(144, 23);
            this.btnGenerate.TabIndex = 14;
            this.btnGenerate.Text = "Generate Command";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // listDebug
            // 
            this.listDebug.FormattingEnabled = true;
            this.listDebug.ItemHeight = 12;
            this.listDebug.Location = new System.Drawing.Point(12, 177);
            this.listDebug.Name = "listDebug";
            this.listDebug.Size = new System.Drawing.Size(457, 256);
            this.listDebug.TabIndex = 19;
            // 
            // chkSetMode
            // 
            this.chkSetMode.AutoSize = true;
            this.chkSetMode.Location = new System.Drawing.Point(475, 236);
            this.chkSetMode.Name = "chkSetMode";
            this.chkSetMode.Size = new System.Drawing.Size(56, 16);
            this.chkSetMode.TabIndex = 20;
            this.chkSetMode.Text = "Mode";
            this.chkSetMode.UseVisualStyleBackColor = true;
            // 
            // chkSetUnit
            // 
            this.chkSetUnit.AutoSize = true;
            this.chkSetUnit.Location = new System.Drawing.Point(475, 259);
            this.chkSetUnit.Name = "chkSetUnit";
            this.chkSetUnit.Size = new System.Drawing.Size(45, 16);
            this.chkSetUnit.TabIndex = 21;
            this.chkSetUnit.Text = "Unit";
            this.chkSetUnit.UseVisualStyleBackColor = true;
            // 
            // chkSetDistanceToBall
            // 
            this.chkSetDistanceToBall.AutoSize = true;
            this.chkSetDistanceToBall.Location = new System.Drawing.Point(475, 305);
            this.chkSetDistanceToBall.Name = "chkSetDistanceToBall";
            this.chkSetDistanceToBall.Size = new System.Drawing.Size(111, 16);
            this.chkSetDistanceToBall.TabIndex = 23;
            this.chkSetDistanceToBall.Text = "Distance to ball";
            this.chkSetDistanceToBall.UseVisualStyleBackColor = true;
            // 
            // chkSetTeeHight
            // 
            this.chkSetTeeHight.AutoSize = true;
            this.chkSetTeeHight.Location = new System.Drawing.Point(475, 282);
            this.chkSetTeeHight.Name = "chkSetTeeHight";
            this.chkSetTeeHight.Size = new System.Drawing.Size(85, 16);
            this.chkSetTeeHight.TabIndex = 22;
            this.chkSetTeeHight.Text = "Tee Height";
            this.chkSetTeeHight.UseVisualStyleBackColor = true;
            // 
            // chkSetCarryTotal
            // 
            this.chkSetCarryTotal.AutoSize = true;
            this.chkSetCarryTotal.Location = new System.Drawing.Point(475, 213);
            this.chkSetCarryTotal.Name = "chkSetCarryTotal";
            this.chkSetCarryTotal.Size = new System.Drawing.Size(89, 16);
            this.chkSetCarryTotal.TabIndex = 27;
            this.chkSetCarryTotal.Text = "Carry/Total";
            this.chkSetCarryTotal.UseVisualStyleBackColor = true;
            // 
            // chkSetLoftAngle
            // 
            this.chkSetLoftAngle.AutoSize = true;
            this.chkSetLoftAngle.Location = new System.Drawing.Point(475, 374);
            this.chkSetLoftAngle.Name = "chkSetLoftAngle";
            this.chkSetLoftAngle.Size = new System.Drawing.Size(80, 16);
            this.chkSetLoftAngle.TabIndex = 26;
            this.chkSetLoftAngle.Text = "Loft Angle";
            this.chkSetLoftAngle.UseVisualStyleBackColor = true;
            // 
            // chkSetClub
            // 
            this.chkSetClub.AutoSize = true;
            this.chkSetClub.Location = new System.Drawing.Point(475, 351);
            this.chkSetClub.Name = "chkSetClub";
            this.chkSetClub.Size = new System.Drawing.Size(50, 16);
            this.chkSetClub.TabIndex = 25;
            this.chkSetClub.Text = "Club";
            this.chkSetClub.UseVisualStyleBackColor = true;
            // 
            // chkSetTargetDistance
            // 
            this.chkSetTargetDistance.AutoSize = true;
            this.chkSetTargetDistance.Location = new System.Drawing.Point(475, 328);
            this.chkSetTargetDistance.Name = "chkSetTargetDistance";
            this.chkSetTargetDistance.Size = new System.Drawing.Size(113, 16);
            this.chkSetTargetDistance.TabIndex = 24;
            this.chkSetTargetDistance.Text = "Target Distance";
            this.chkSetTargetDistance.UseVisualStyleBackColor = true;
            // 
            // cmbSetCarryTotal
            // 
            this.cmbSetCarryTotal.FormattingEnabled = true;
            this.cmbSetCarryTotal.Items.AddRange(new object[] {
            "Carry",
            "Total"});
            this.cmbSetCarryTotal.Location = new System.Drawing.Point(593, 208);
            this.cmbSetCarryTotal.Name = "cmbSetCarryTotal";
            this.cmbSetCarryTotal.Size = new System.Drawing.Size(121, 20);
            this.cmbSetCarryTotal.TabIndex = 28;
            this.cmbSetCarryTotal.Text = "Carry";
            // 
            // cmbSetMode
            // 
            this.cmbSetMode.FormattingEnabled = true;
            this.cmbSetMode.Items.AddRange(new object[] {
            "Practice",
            "Target"});
            this.cmbSetMode.Location = new System.Drawing.Point(593, 234);
            this.cmbSetMode.Name = "cmbSetMode";
            this.cmbSetMode.Size = new System.Drawing.Size(121, 20);
            this.cmbSetMode.TabIndex = 29;
            this.cmbSetMode.Text = "Practice";
            // 
            // cmbSetUnit
            // 
            this.cmbSetUnit.FormattingEnabled = true;
            this.cmbSetUnit.Items.AddRange(new object[] {
            "0: yard / mph / ft",
            "1: yard / m/s / ft",
            "2: m / m/s / ft",
            "3: m / mph / ft",
            "4: m / m/s m",
            "5: m / mph / m",
            "6: yard / mph / m",
            "7: yard / m/s / m"});
            this.cmbSetUnit.Location = new System.Drawing.Point(593, 256);
            this.cmbSetUnit.Name = "cmbSetUnit";
            this.cmbSetUnit.Size = new System.Drawing.Size(121, 20);
            this.cmbSetUnit.TabIndex = 30;
            this.cmbSetUnit.Text = "4: m / m/s m";
            // 
            // textSetTeeHight
            // 
            this.textSetTeeHight.Location = new System.Drawing.Point(593, 278);
            this.textSetTeeHight.Name = "textSetTeeHight";
            this.textSetTeeHight.Size = new System.Drawing.Size(74, 21);
            this.textSetTeeHight.TabIndex = 31;
            this.textSetTeeHight.Text = "55";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(671, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(671, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "m";
            // 
            // textSetDistanceToBall
            // 
            this.textSetDistanceToBall.Location = new System.Drawing.Point(593, 301);
            this.textSetDistanceToBall.Name = "textSetDistanceToBall";
            this.textSetDistanceToBall.Size = new System.Drawing.Size(74, 21);
            this.textSetDistanceToBall.TabIndex = 33;
            this.textSetDistanceToBall.Text = "1.2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(671, 330);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "m";
            // 
            // textSetTargetDistance
            // 
            this.textSetTargetDistance.Location = new System.Drawing.Point(593, 324);
            this.textSetTargetDistance.Name = "textSetTargetDistance";
            this.textSetTargetDistance.Size = new System.Drawing.Size(74, 21);
            this.textSetTargetDistance.TabIndex = 35;
            this.textSetTargetDistance.Text = "150";
            // 
            // cmbSetClub
            // 
            this.cmbSetClub.FormattingEnabled = true;
            this.cmbSetClub.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22"});
            this.cmbSetClub.Location = new System.Drawing.Point(593, 347);
            this.cmbSetClub.Name = "cmbSetClub";
            this.cmbSetClub.Size = new System.Drawing.Size(121, 20);
            this.cmbSetClub.TabIndex = 37;
            this.cmbSetClub.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(671, 375);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 12);
            this.label9.TabIndex = 39;
            this.label9.Text = "deg";
            // 
            // textSetLoftAngle
            // 
            this.textSetLoftAngle.Location = new System.Drawing.Point(593, 369);
            this.textSetLoftAngle.Name = "textSetLoftAngle";
            this.textSetLoftAngle.Size = new System.Drawing.Size(74, 21);
            this.textSetLoftAngle.TabIndex = 38;
            this.textSetLoftAngle.Text = "12";
            // 
            // textCommand
            // 
            this.textCommand.Location = new System.Drawing.Point(129, 127);
            this.textCommand.Name = "textCommand";
            this.textCommand.Size = new System.Drawing.Size(444, 21);
            this.textCommand.TabIndex = 40;
            // 
            // btnDevInfo
            // 
            this.btnDevInfo.Location = new System.Drawing.Point(476, 410);
            this.btnDevInfo.Name = "btnDevInfo";
            this.btnDevInfo.Size = new System.Drawing.Size(127, 23);
            this.btnDevInfo.TabIndex = 41;
            this.btnDevInfo.Text = "Make DevInfo Req.";
            this.btnDevInfo.UseVisualStyleBackColor = true;
            this.btnDevInfo.Click += new System.EventHandler(this.btnDevInfo_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(604, 24);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(111, 25);
            this.btnDisconnect.TabIndex = 42;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Visible = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // timerConnect
            // 
            this.timerConnect.Interval = 1000;
            this.timerConnect.Tick += new System.EventHandler(this.timerConnect_Tick);
            // 
            // btnDFU
            // 
            this.btnDFU.Location = new System.Drawing.Point(316, 25);
            this.btnDFU.Name = "btnDFU";
            this.btnDFU.Size = new System.Drawing.Size(113, 23);
            this.btnDFU.TabIndex = 43;
            this.btnDFU.Text = "DFU";
            this.btnDFU.UseVisualStyleBackColor = true;
            this.btnDFU.Click += new System.EventHandler(this.btnDFU_Click);
            // 
            // btnDat
            // 
            this.btnDat.Location = new System.Drawing.Point(348, 56);
            this.btnDat.Name = "btnDat";
            this.btnDat.Size = new System.Drawing.Size(75, 23);
            this.btnDat.TabIndex = 44;
            this.btnDat.Text = "Dat File...";
            this.btnDat.UseVisualStyleBackColor = true;
            this.btnDat.Click += new System.EventHandler(this.btnDat_Click);
            // 
            // btnBin
            // 
            this.btnBin.Location = new System.Drawing.Point(348, 86);
            this.btnBin.Name = "btnBin";
            this.btnBin.Size = new System.Drawing.Size(75, 23);
            this.btnBin.TabIndex = 45;
            this.btnBin.Text = "Bin File...";
            this.btnBin.UseVisualStyleBackColor = true;
            this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
            // 
            // textDat
            // 
            this.textDat.Location = new System.Drawing.Point(430, 57);
            this.textDat.Name = "textDat";
            this.textDat.Size = new System.Drawing.Size(285, 21);
            this.textDat.TabIndex = 46;
            // 
            // textBin
            // 
            this.textBin.Location = new System.Drawing.Point(429, 88);
            this.textBin.Name = "textBin";
            this.textBin.Size = new System.Drawing.Size(285, 21);
            this.textBin.TabIndex = 47;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(435, 35);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(153, 13);
            this.progressBar1.TabIndex = 48;
            // 
            // timerDFU
            // 
            this.timerDFU.Interval = 1000;
            this.timerDFU.Tick += new System.EventHandler(this.timerDFU_Tick);
            // 
            // FormSC4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 451);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBin);
            this.Controls.Add(this.textDat);
            this.Controls.Add(this.btnBin);
            this.Controls.Add(this.btnDat);
            this.Controls.Add(this.btnDFU);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnDevInfo);
            this.Controls.Add(this.textCommand);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textSetLoftAngle);
            this.Controls.Add(this.cmbSetClub);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textSetTargetDistance);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textSetDistanceToBall);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textSetTeeHight);
            this.Controls.Add(this.cmbSetUnit);
            this.Controls.Add(this.cmbSetMode);
            this.Controls.Add(this.cmbSetCarryTotal);
            this.Controls.Add(this.chkSetCarryTotal);
            this.Controls.Add(this.chkSetLoftAngle);
            this.Controls.Add(this.chkSetClub);
            this.Controls.Add(this.chkSetTargetDistance);
            this.Controls.Add(this.chkSetDistanceToBall);
            this.Controls.Add(this.chkSetTeeHight);
            this.Controls.Add(this.chkSetUnit);
            this.Controls.Add(this.chkSetMode);
            this.Controls.Add(this.listDebug);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.comboDeviceList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnScan);
            this.Name = "FormSC4";
            this.Text = "SC4 SDK Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboDeviceList;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ListBox listDebug;
        private System.Windows.Forms.CheckBox chkSetMode;
        private System.Windows.Forms.CheckBox chkSetUnit;
        private System.Windows.Forms.CheckBox chkSetDistanceToBall;
        private System.Windows.Forms.CheckBox chkSetTeeHight;
        private System.Windows.Forms.CheckBox chkSetCarryTotal;
        private System.Windows.Forms.CheckBox chkSetLoftAngle;
        private System.Windows.Forms.CheckBox chkSetClub;
        private System.Windows.Forms.CheckBox chkSetTargetDistance;
        private System.Windows.Forms.ComboBox cmbSetCarryTotal;
        private System.Windows.Forms.ComboBox cmbSetMode;
        private System.Windows.Forms.ComboBox cmbSetUnit;
        private System.Windows.Forms.TextBox textSetTeeHight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textSetDistanceToBall;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textSetTargetDistance;
        private System.Windows.Forms.ComboBox cmbSetClub;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textSetLoftAngle;
        private System.Windows.Forms.TextBox textCommand;
        private System.Windows.Forms.Button btnDevInfo;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Timer timerConnect;
        private System.Windows.Forms.Button btnDFU;
        private System.Windows.Forms.Button btnDat;
        private System.Windows.Forms.Button btnBin;
        private System.Windows.Forms.TextBox textDat;
        private System.Windows.Forms.TextBox textBin;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timerDFU;
    }
}

