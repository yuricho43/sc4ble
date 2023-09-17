
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
            this.btnScan = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSetSvc = new System.Windows.Forms.Button();
            this.btnGetSvc = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.btnGetChar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDeviceList = new System.Windows.Forms.ComboBox();
            this.comboServiceList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listCharList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.listNotfication = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listReponse = new System.Windows.Forms.ListBox();
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
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 22);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(156, 25);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "1. Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 53);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(156, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "2. Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSetSvc
            // 
            this.btnSetSvc.Location = new System.Drawing.Point(12, 115);
            this.btnSetSvc.Name = "btnSetSvc";
            this.btnSetSvc.Size = new System.Drawing.Size(156, 25);
            this.btnSetSvc.TabIndex = 3;
            this.btnSetSvc.Text = "4. Set Service";
            this.btnSetSvc.UseVisualStyleBackColor = true;
            this.btnSetSvc.Visible = false;
            // 
            // btnGetSvc
            // 
            this.btnGetSvc.Location = new System.Drawing.Point(12, 84);
            this.btnGetSvc.Name = "btnGetSvc";
            this.btnGetSvc.Size = new System.Drawing.Size(156, 25);
            this.btnGetSvc.TabIndex = 2;
            this.btnGetSvc.Text = "3. Get Services";
            this.btnGetSvc.UseVisualStyleBackColor = true;
            this.btnGetSvc.Click += new System.EventHandler(this.btnGetSvc_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(12, 239);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(156, 25);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "8. Read Status";
            this.btnRead.UseVisualStyleBackColor = true;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(12, 208);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(156, 25);
            this.btnWrite.TabIndex = 6;
            this.btnWrite.Text = "7. Write Command";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(12, 177);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(156, 25);
            this.btnSubscribe.TabIndex = 5;
            this.btnSubscribe.Text = "6. Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            // 
            // btnGetChar
            // 
            this.btnGetChar.Location = new System.Drawing.Point(12, 146);
            this.btnGetChar.Name = "btnGetChar";
            this.btnGetChar.Size = new System.Drawing.Size(156, 25);
            this.btnGetChar.TabIndex = 4;
            this.btnGetChar.Text = "5. Get Characteristics";
            this.btnGetChar.UseVisualStyleBackColor = true;
            this.btnGetChar.Click += new System.EventHandler(this.btnGetChar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Device Lists:";
            // 
            // comboDeviceList
            // 
            this.comboDeviceList.FormattingEnabled = true;
            this.comboDeviceList.Location = new System.Drawing.Point(266, 22);
            this.comboDeviceList.Name = "comboDeviceList";
            this.comboDeviceList.Size = new System.Drawing.Size(234, 20);
            this.comboDeviceList.TabIndex = 9;
            // 
            // comboServiceList
            // 
            this.comboServiceList.FormattingEnabled = true;
            this.comboServiceList.Location = new System.Drawing.Point(266, 70);
            this.comboServiceList.Name = "comboServiceList";
            this.comboServiceList.Size = new System.Drawing.Size(234, 20);
            this.comboServiceList.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Service Lists:";
            // 
            // listCharList
            // 
            this.listCharList.FormattingEnabled = true;
            this.listCharList.ItemHeight = 12;
            this.listCharList.Location = new System.Drawing.Point(184, 124);
            this.listCharList.Name = "listCharList";
            this.listCharList.Size = new System.Drawing.Size(316, 88);
            this.listCharList.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Char. List :";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 309);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(156, 23);
            this.btnGenerate.TabIndex = 14;
            this.btnGenerate.Text = "Generate Command";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // listNotfication
            // 
            this.listNotfication.FormattingEnabled = true;
            this.listNotfication.ItemHeight = 12;
            this.listNotfication.Location = new System.Drawing.Point(519, 245);
            this.listNotfication.Name = "listNotfication";
            this.listNotfication.Size = new System.Drawing.Size(332, 136);
            this.listNotfication.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "Notification : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(434, 391);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "Response:";
            // 
            // listReponse
            // 
            this.listReponse.FormattingEnabled = true;
            this.listReponse.ItemHeight = 12;
            this.listReponse.Location = new System.Drawing.Point(519, 391);
            this.listReponse.Name = "listReponse";
            this.listReponse.Size = new System.Drawing.Size(332, 148);
            this.listReponse.TabIndex = 17;
            // 
            // listDebug
            // 
            this.listDebug.FormattingEnabled = true;
            this.listDebug.ItemHeight = 12;
            this.listDebug.Location = new System.Drawing.Point(520, 22);
            this.listDebug.Name = "listDebug";
            this.listDebug.Size = new System.Drawing.Size(332, 184);
            this.listDebug.TabIndex = 19;
            // 
            // chkSetMode
            // 
            this.chkSetMode.AutoSize = true;
            this.chkSetMode.Location = new System.Drawing.Point(12, 366);
            this.chkSetMode.Name = "chkSetMode";
            this.chkSetMode.Size = new System.Drawing.Size(56, 16);
            this.chkSetMode.TabIndex = 20;
            this.chkSetMode.Text = "Mode";
            this.chkSetMode.UseVisualStyleBackColor = true;
            // 
            // chkSetUnit
            // 
            this.chkSetUnit.AutoSize = true;
            this.chkSetUnit.Location = new System.Drawing.Point(12, 389);
            this.chkSetUnit.Name = "chkSetUnit";
            this.chkSetUnit.Size = new System.Drawing.Size(45, 16);
            this.chkSetUnit.TabIndex = 21;
            this.chkSetUnit.Text = "Unit";
            this.chkSetUnit.UseVisualStyleBackColor = true;
            // 
            // chkSetDistanceToBall
            // 
            this.chkSetDistanceToBall.AutoSize = true;
            this.chkSetDistanceToBall.Location = new System.Drawing.Point(12, 435);
            this.chkSetDistanceToBall.Name = "chkSetDistanceToBall";
            this.chkSetDistanceToBall.Size = new System.Drawing.Size(111, 16);
            this.chkSetDistanceToBall.TabIndex = 23;
            this.chkSetDistanceToBall.Text = "Distance to ball";
            this.chkSetDistanceToBall.UseVisualStyleBackColor = true;
            // 
            // chkSetTeeHight
            // 
            this.chkSetTeeHight.AutoSize = true;
            this.chkSetTeeHight.Location = new System.Drawing.Point(12, 412);
            this.chkSetTeeHight.Name = "chkSetTeeHight";
            this.chkSetTeeHight.Size = new System.Drawing.Size(85, 16);
            this.chkSetTeeHight.TabIndex = 22;
            this.chkSetTeeHight.Text = "Tee Height";
            this.chkSetTeeHight.UseVisualStyleBackColor = true;
            // 
            // chkSetCarryTotal
            // 
            this.chkSetCarryTotal.AutoSize = true;
            this.chkSetCarryTotal.Location = new System.Drawing.Point(12, 343);
            this.chkSetCarryTotal.Name = "chkSetCarryTotal";
            this.chkSetCarryTotal.Size = new System.Drawing.Size(89, 16);
            this.chkSetCarryTotal.TabIndex = 27;
            this.chkSetCarryTotal.Text = "Carry/Total";
            this.chkSetCarryTotal.UseVisualStyleBackColor = true;
            // 
            // chkSetLoftAngle
            // 
            this.chkSetLoftAngle.AutoSize = true;
            this.chkSetLoftAngle.Location = new System.Drawing.Point(12, 504);
            this.chkSetLoftAngle.Name = "chkSetLoftAngle";
            this.chkSetLoftAngle.Size = new System.Drawing.Size(80, 16);
            this.chkSetLoftAngle.TabIndex = 26;
            this.chkSetLoftAngle.Text = "Loft Angle";
            this.chkSetLoftAngle.UseVisualStyleBackColor = true;
            // 
            // chkSetClub
            // 
            this.chkSetClub.AutoSize = true;
            this.chkSetClub.Location = new System.Drawing.Point(12, 481);
            this.chkSetClub.Name = "chkSetClub";
            this.chkSetClub.Size = new System.Drawing.Size(50, 16);
            this.chkSetClub.TabIndex = 25;
            this.chkSetClub.Text = "Club";
            this.chkSetClub.UseVisualStyleBackColor = true;
            // 
            // chkSetTargetDistance
            // 
            this.chkSetTargetDistance.AutoSize = true;
            this.chkSetTargetDistance.Location = new System.Drawing.Point(12, 458);
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
            this.cmbSetCarryTotal.Location = new System.Drawing.Point(142, 340);
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
            this.cmbSetMode.Location = new System.Drawing.Point(142, 366);
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
            this.cmbSetUnit.Location = new System.Drawing.Point(142, 388);
            this.cmbSetUnit.Name = "cmbSetUnit";
            this.cmbSetUnit.Size = new System.Drawing.Size(121, 20);
            this.cmbSetUnit.TabIndex = 30;
            this.cmbSetUnit.Text = "4: m / m/s m";
            // 
            // textSetTeeHight
            // 
            this.textSetTeeHight.Location = new System.Drawing.Point(142, 410);
            this.textSetTeeHight.Name = "textSetTeeHight";
            this.textSetTeeHight.Size = new System.Drawing.Size(74, 21);
            this.textSetTeeHight.TabIndex = 31;
            this.textSetTeeHight.Text = "55";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(220, 418);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 441);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "m";
            // 
            // textSetDistanceToBall
            // 
            this.textSetDistanceToBall.Location = new System.Drawing.Point(142, 433);
            this.textSetDistanceToBall.Name = "textSetDistanceToBall";
            this.textSetDistanceToBall.Size = new System.Drawing.Size(74, 21);
            this.textSetDistanceToBall.TabIndex = 33;
            this.textSetDistanceToBall.Text = "1.2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(220, 462);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "m";
            // 
            // textSetTargetDistance
            // 
            this.textSetTargetDistance.Location = new System.Drawing.Point(142, 456);
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
            this.cmbSetClub.Location = new System.Drawing.Point(142, 479);
            this.cmbSetClub.Name = "cmbSetClub";
            this.cmbSetClub.Size = new System.Drawing.Size(121, 20);
            this.cmbSetClub.TabIndex = 37;
            this.cmbSetClub.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(220, 507);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 12);
            this.label9.TabIndex = 39;
            this.label9.Text = "deg";
            // 
            // textSetLoftAngle
            // 
            this.textSetLoftAngle.Location = new System.Drawing.Point(142, 501);
            this.textSetLoftAngle.Name = "textSetLoftAngle";
            this.textSetLoftAngle.Size = new System.Drawing.Size(74, 21);
            this.textSetLoftAngle.TabIndex = 38;
            this.textSetLoftAngle.Text = "150";
            // 
            // textCommand
            // 
            this.textCommand.Location = new System.Drawing.Point(12, 550);
            this.textCommand.Name = "textCommand";
            this.textCommand.Size = new System.Drawing.Size(403, 21);
            this.textCommand.TabIndex = 40;
            this.textCommand.Text = "150";
            // 
            // FormSC4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 591);
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
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listReponse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listNotfication);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listCharList);
            this.Controls.Add(this.comboServiceList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboDeviceList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.btnGetChar);
            this.Controls.Add(this.btnSetSvc);
            this.Controls.Add(this.btnGetSvc);
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
        private System.Windows.Forms.Button btnSetSvc;
        private System.Windows.Forms.Button btnGetSvc;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button btnGetChar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboDeviceList;
        private System.Windows.Forms.ComboBox comboServiceList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listCharList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ListBox listNotfication;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listReponse;
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
    }
}

