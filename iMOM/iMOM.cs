using Calendar.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Calendar = Calendar.NET.Calendar;


namespace iMOM
{
    public partial class iMOM : Form
    {
        private int clickcount = 0;
        private string providerMessage = string.Empty;

        public iMOM()
        {
            InitializeComponent();
            timer911.Stop();
            timer1.Stop();

            RoundingPictureBox(pictureBox911);
            RoundingPictureBox(pictureBoxAlertDashboard);
            RoundingPictureBox(pictureBoxMonitor);
            RoundingPictureBox(pictureBoxScheduler);
            RoundingPictureBox(pictureBoxHealthResources);
            RoundingPictureBox(pictureBoxChat);
            RoundingPictureBox(pictureBoxReport);
            RoundingPictureBox(pictureBoxGinaAlert);
            RoundingPictureBox(pictureBoxGinaRefill);
            RoundingPictureBox(pictureBoxGinaThreshold);
            RoundingButton(buttonAlertDashboard);
        }

        private void RoundingPictureBox(PictureBox pictureBox1)
        {
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox1.Region = new Region(gp);
        }

        private void RoundingButton(Button button)
        {
            Rectangle r = new Rectangle(0, 0, button.Width, button.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            button.Region = new Region(gp);
        }

        private void LaunchAlertMonitor()
        {
            notifyIcon1.Visible = false;
            buttonAlertDashboard.BackColor = Color.Transparent;

            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageAlert);

            tabControlMain.SelectedTab = tabPageAlert;
            dataGridViewAlert.Rows.Clear();

            dataGridViewAlert.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewAlert.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewAlert.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridViewAlert.Rows.Add(DateTime.Now.AddDays(-3).Date, "Contraction Monitor Status", "No Contractions");
            dataGridViewAlert.Rows.Add(DateTime.Now.AddDays(-2).Date, "Contraction Monitor Status", "No Contractions");
            dataGridViewAlert.Rows.Add(DateTime.Now.AddDays(-1).Date, "Subcutaneous Terbutaline infusion pump",
                "Unable to dispense medication. Please Check.");
            dataGridViewAlert.Rows[2].DefaultCellStyle.ForeColor = Color.Red;

            if (providerMessage != string.Empty)
            {
                dataGridViewAlert.Rows.Add(DateTime.Now.Date, "Contraction Monitor Status", providerMessage);
                dataGridViewAlert.Rows[dataGridViewAlert.RowCount-1].DefaultCellStyle.ForeColor = Color.Red;
                providerMessage = string.Empty;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (buttonAlertDashboard.BackColor == Color.Red)
                buttonAlertDashboard.BackColor = Color.Transparent;
            else
                buttonAlertDashboard.BackColor = Color.Red;

        }

        private void iMOM_Load(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageMonitor);
            tabControlMain.TabPages.Remove(tabPageAlert);
            tabControlMain.TabPages.Remove(tabPageScheduler);
            tabControlMain.TabPages.Remove(tabPageHealthResources);
            tabControlMain.TabPages.Remove(tabPage911);
            tabControlMain.TabPages.Remove(tabPageProvideChat);
            tabControlMain.TabPages.Remove(tabPageReport);
            tabControlMain.TabPages.Remove(tabPageGina);
            tabControlMain.TabPages.Remove(tabPagePrescription);
            tabControlMain.TabPages.Remove(tabPageThreshold);
            tabControlMain.TabPages.Remove(tabPageProviderAlert);
        }


        private void buttonAlertDashboard_Click(object sender, EventArgs e)
        {
            LaunchAlertMonitor();
        }


        private void pictureBoxMonitor_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageMonitor);
            tabControlMain.SelectedTab = tabPageMonitor;
        }

        private void pictureBoxAlertDashboard_Click(object sender, EventArgs e)
        {
            LaunchAlertMonitor();
        }

        private void pictureBoxStartCapture_Click(object sender, EventArgs e)
        {

            if (clickcount == 0)
            {
                richTextBoxMonitorAlert.ForeColor = Color.Red;
                richTextBoxMonitorAlert.Text =
                    "Warning Device connectivity Issue: the monitoring device is not paired up with your mobile device. Please re-connect via bluetooth and click start again.";

                clickcount = clickcount + 1;
            }
            else
            {

                richTextBoxMonitorAlert.ForeColor = Color.Green;
                richTextBoxMonitorAlert.AppendText(
                    "Starting capture. Please do not move so accurate readings can be measured.");

                //notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                //notifyIcon1.Icon = SystemIcons.Application;
                //notifyIcon1.BalloonTipText = "New alert in Alert Dashboard.";
                //notifyIcon1.BalloonTipTitle = "Alert";
                //notifyIcon1.ShowBalloonTip(20);

                progressBar1.Maximum = 100000;
                progressBar1.Step = 1;

                for (int j = 0; j < 100000; j++)
                {
                    progressBar1.PerformStep();
                }

                richTextBoxMonitorAlert.Text =
                    "Contraction measurement capture completed successfully.Please check Alert Dashboard for feedback from provider.";

            }
        }


        private void Home()
        {
            HomeWithTab(tabPagePatient);
        }

        private void HomeWithTab(TabPage tabPage)
        {
            tabControlMain.TabPages.Add(tabPagePatient);
            tabControlMain.TabPages.Add(tabPageProvider);
            tabControlMain.SelectedTab = tabPage;

        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageMonitor);
            Home();
        }

        private void pictureBoxHomeAlert_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageAlert);
            timer1.Stop();
            Home();
        }

        private void pictureBoxPairing_Click(object sender, EventArgs e)
        {
            richTextBoxMonitorAlert.ForeColor = Color.Green;
            richTextBoxMonitorAlert.Text = "Pairing Complete...";
        }

        private CustomEvent usEvent;
        private CustomEvent drEvent;
        private CustomEvent bloodworkEvent;
        private CustomEvent monitorEvent;
        private void pictureBoxScheduler_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageScheduler);
            tabControlMain.SelectedTab = tabPageScheduler;

            calendarScheduler.CalendarDate = DateTime.Now.Date;

            //https://www.codeproject.com/Articles/378900/Calendar-NET
            usEvent = new CustomEvent
            {
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                EventLengthInHours = 2,
                IgnoreTimeComponent = true,
                RecurringFrequency = RecurringFrequencies.Monthly,
                EventColor = Color.Black,
                EventText = "Ultrasound.",
                ThisDayForwardOnly = true
            };

            calendarScheduler.AddEvent(usEvent);

            drEvent = new CustomEvent
            {
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 10, 0, 0),
                EventLengthInHours = 2,
                IgnoreTimeComponent = true,
                EventColor = Color.Brown,
                RecurringFrequency = RecurringFrequencies.Monthly,
                EventText = "Dr Appt.",
                ThisDayForwardOnly = true
            };

            calendarScheduler.AddEvent(drEvent);

            bloodworkEvent = new CustomEvent
            {
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 2, 7, 0, 0),
                EventLengthInHours = 1,
                IgnoreTimeComponent = true,
                EventColor = Color.Blue,
                RecurringFrequency = RecurringFrequencies.Monthly,
                EventText = "Fasting Blood draw.",
                ThisDayForwardOnly = true
            };

            calendarScheduler.AddEvent(bloodworkEvent);

            monitorEvent = new CustomEvent
            {
                EventColor = Color.Chartreuse,
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 2, 9, 0, 0),
                RecurringFrequency = RecurringFrequencies.Daily,
                EventLengthInHours = 1,
                EventText = "Contractions Monitor.",
                ThisDayForwardOnly = true
            };

            calendarScheduler.AddEvent(monitorEvent);
        }

        private void pictureBoxSchedulerHome_Click(object sender, EventArgs e)
        {
            calendarScheduler.RemoveEvent(monitorEvent);
            calendarScheduler.RemoveEvent(usEvent);
            calendarScheduler.RemoveEvent(bloodworkEvent);
            calendarScheduler.RemoveEvent(drEvent);


            tabControlMain.TabPages.Remove(tabPageScheduler);
            Home();
        }

        private void pictureBox911_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPage911);
            tabControlMain.SelectedTab = tabPage911;
        }

        private void pictureBoxHome911_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPage911);
            pictureBox911Gif.Visible = false;
            dataGridView911.Rows.Clear();
            timer911.Stop();
            Home();
        }

        private int cnt = 1;

        private void pictureBoxCall911_Click(object sender, EventArgs e)
        {
            timer911.Start();
            dataGridView911.Rows.Clear();
            cnt = 1;
        }

        private void timer911_Tick(object sender, EventArgs e)
        {
            switch (cnt)
            {
                case 1:
                    pictureBox911Gif.Visible = true;
                    dataGridView911.Rows.Add("Ambulance left the hospital. ETA 10 mins");
                    cnt++;
                    break;
                case 2:
                    dataGridView911.Rows.Add("Near Main st. and Broadway. ETA 7 mins");
                    cnt++;
                    break;
                case 3:
                    dataGridView911.Rows.Add("Near Roster st. ETA 3 mins");
                    cnt++;
                    break;
                case 4:
                    dataGridView911.Rows.Add("In Aspen Community. ETA 1 min");
                    cnt++;
                    break;
                case 5:
                    dataGridView911.Rows.Add("Outside 1 W oak St. Arrived.");
                    cnt++;
                    timer911.Stop();
                    break;
            }
        }

        private void pictureBoxHealthResources_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageHealthResources);
            tabControlMain.SelectedTab = tabPageHealthResources;

            dataGridViewResource.Rows.Clear();

            dataGridViewResource.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewResource.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridViewResource.Rows.Add("Contractions Monitor",
                "https://www.youtube.com/watch?v=C0r0svRSfvY?autoplay=1 frameborder = 0");
            dataGridViewResource.Rows.Add("Bed Rest", "https://www.youtube.com/watch?v=gNGoJo4F1Fs");
            dataGridViewResource.Rows.Add("Depression", "https://www.youtube.com/watch?v=33BXVed9TdY");
        }

        private void pictureBoxHomeResources_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageHealthResources);
            webBrowserResource.Stop();
            Home();
        }

        private void dataGridViewResource_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            labelLoading.Visible = true;
            this.webBrowserResource.Url = new Uri(dataGridViewResource.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void pictureBoxChat_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageProvideChat);
            tabControlMain.SelectedTab = tabPageProvideChat;
        }

        private void buttonChatSubmit_Click(object sender, EventArgs e)
        {
            if (richTextBoxTypeChat.Text.ToLower().Contains("test"))
            {
                richTextBoxChatResponse.Text =
                    @"Here are some of the typical tests your care provider may require in what they determine to be a high risk pregnancy:

                Specialized or targeted ultrasound
                Invasive testing :
                Cordocentesis
                Amniocentesis
                Chorionic Villus Sampling (CVS)
                Non-Invasive Prenatal Testing NIPT
                Cell-free DNA
                Nuchal Translucency Screen
                Trimester Screens
                Cervical length measurement
                Prenatal lab work and tests
                Biophysical profile";
                richTextBoxTypeChat.Text = string.Empty;
            }
            else if (richTextBoxTypeChat.Text.ToLower().Contains("home"))
            {
                richTextBoxChatResponse.Text =
                    @"Since you have hypertension we would like to monitor you at the hospital during delivery so we can give the best care to you.";
                richTextBoxTypeChat.Text = string.Empty;
            }
        }

        private void richTextBoxTypeChat_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonChatSubmit.PerformClick();
            }
        }

        private void pictureBoxHomeChat_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageProvideChat);
            Home();
        }

        private void buttonToggleCalendarView_Click(object sender, EventArgs e)
        {
            if (calendarScheduler.CalendarView == CalendarViews.Day)
            {
                calendarScheduler.CalendarView = CalendarViews.Month;
            }
            else
            {
                calendarScheduler.CalendarView = CalendarViews.Day;
            }
        }

        private void pictureBoxReport_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageReport);
            tabControlMain.SelectedTab = tabPageReport;

            dataGridViewReportResult.Rows.Clear();
            dataGridViewReportResult.Rows.Add("2/25/2018", "Ultrasound", "Normal");
            dataGridViewReportResult.Rows.Add("2/29/2018", "Fasting Blood work", "Slightly high sugar");
            dataGridViewReportResult.Rows.Add("3/3/2018", "Dr appt", "Slight BP");

            dataGridViewReportResult.Rows[0].Selected = true;

            richTextBoxReportResult.Text =
                    @"Fetal Anatomy: Skull/brain appears normal, heart not examined, spine appears normal, abdomen appears
normal, stomach visible, bladder visible, hands both visible, feet both visible. ";
        }

        private void pictureBoxReportHome_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageReport);
            Home();
        }

        private void dataGridViewReportResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                richTextBoxReportResult.Text =
                    @"Fetal Anatomy: Skull/brain appears normal, heart not examined, spine appears normal, abdomen appears
normal, stomach visible, bladder visible, hands both visible, feet both visible. ";
            }
            if (e.RowIndex == 1)
            {
                richTextBoxReportResult.Text =
                    @"Oral glucose tolerance test (OGTT) conducted.Fasting: 92 mg/dL (Normal). 1 Hour: above 180 mg/dL (High).";
            }
            if (e.RowIndex ==2)
            {
                richTextBoxReportResult.Text =
                    @"A blood pressure of 145 mm HG observed (Normal < than 140/90 mm Hg.)";
            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabPageProvider)
            {
                dataGridViewProviderPatientList.Rows.Clear();

                dataGridViewProviderPatientList.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridViewProviderPatientList.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dataGridViewProviderPatientList.Rows.Add("W12345", "Gina Gardoni", "3/11/1987");
            }
        }

        private void dataGridViewProviderPatientList_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePatient);
            tabControlMain.TabPages.Remove(tabPageProvider);
            tabControlMain.TabPages.Add(tabPageGina);
            tabControlMain.SelectedTab = tabPageGina;
        }

        private void pictureBoxGinaHome_Click_1(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageGina);
            HomeWithTab(tabPageProvider);
        }

        private void pictureBoxGinaRefill_Click(object sender, EventArgs e)
        {
            cmbMedications.SelectedItem = "Terbutaline";
            cmbNumRefills.SelectedIndex = 0;

            tabControlMain.TabPages.Remove(tabPageGina);
            tabControlMain.TabPages.Add(tabPagePrescription);
            tabControlMain.SelectedTab = tabPagePrescription;
        }

        private void pictureBoxPrescriptionHome_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPagePrescription);
            tabControlMain.TabPages.Add(tabPageGina);
            tabControlMain.SelectedTab = tabPageGina;
            lblPrescriptionMessage.Visible = false;

        }

        private void btnSubmitRefill_Click(object sender, EventArgs e)
        {
            lblPrescriptionMessage.Visible = true;
            richTextBoxMedicationList.Text += string.Format("\n({0}) - {1} - {2}", DateTime.Now.ToShortDateString(), cmbMedications.SelectedItem, txtDosage.Text);
        }

        private void pictureBoxGinaThreshold_Click(object sender, EventArgs e)
        {
            lblThresholdUpdateDate.Text = DateTime.Now.ToShortDateString();

            tabControlMain.TabPages.Remove(tabPageGina);
            tabControlMain.TabPages.Add(tabPageThreshold);
            tabControlMain.SelectedTab = tabPageThreshold;

        }

        private void pictureBoxThresholdHome_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageThreshold);
            tabControlMain.TabPages.Add(tabPageGina);
            tabControlMain.SelectedTab = tabPageGina;
            lblThresholdSaved.Visible = false;

        }

        private void pictureBoxGinaAlert_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageGina);
            tabControlMain.TabPages.Add(tabPageProviderAlert);
            tabControlMain.SelectedTab = tabPageProviderAlert;

        }

        private void pictureBoxProviderAlertHome_Click(object sender, EventArgs e)
        {
            tabControlMain.TabPages.Remove(tabPageProviderAlert);
            tabControlMain.TabPages.Add(tabPageGina);
            tabControlMain.SelectedTab = tabPageGina;
            lblMessageSent.Visible = false;


        }

        private void btnSaveThreshold_Click(object sender, EventArgs e)
        {
            lblThresholdUpdateDate.Text = DateTime.Now.ToShortDateString();
            lblThresholdSaved.Visible = true;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            providerMessage = richTextBoxMessageToPatient.Text;
            lblMessageSent.Visible = true;
            timer1.Start();
        }
    }
}
