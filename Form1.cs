using System.Collections.Generic;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Single_Click
{
    public partial class Form1 : Form
    {
        private JenkinsJobMonitor jobMonitor;

        private JenkinsConfigurationSection configSection = (JenkinsConfigurationSection)ConfigurationManager.GetSection("jenkinsConfiguration");
        private JenkinsConfigurationSection jenkinsClients = (JenkinsConfigurationSection)ConfigurationManager.GetSection("jenkinsConfiguration");

        private string username = null;

        Jenkins jenkinsJob = new Jenkins();
        private string apiToken = null;
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
            jobMonitor = new JenkinsJobMonitor();
            jobMonitor.JobStatusUpdated += JobMonitor_JobStatusUpdated;
        }

        private void JobMonitor_JobStatusUpdated(object sender, JobStatusEventArgs e)
        {
            // Ensure the UI is updated on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(() => statusTextBox.AppendText(e.StatusMessage + Environment.NewLine)));
            }
            else
            {
                statusTextBox.AppendText(e.StatusMessage + Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var configSection = (JenkinsConfigurationSection)ConfigurationManager.GetSection("jenkinsConfiguration");
            ParameterType.Items.Add("Default");
            ParameterType.Items.Add("LatestBuild");
            if (configSection != null)
            {
                foreach (var server in configSection.JenkinsServers)
                {
                    Console.WriteLine($"Jenkins URL: {server.Url}");

                    JenkinsURL.Items.Add(server.Url);

                    foreach (var job in server.Jobs)
                    {

                        Console.WriteLine($"  Job Name: {job.Name}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Jenkins configuration section not found.");
            }

        }

        private void JenkinsURL_Leave(object sender, EventArgs e)
        {
            if (configSection != null)
            {
                Jobs.Items.Clear();

                JobCollection jobCollection = configSection.JenkinsServers.Where(x => x.Url == JenkinsURL.Text).First().Jobs;



                foreach (var job in jobCollection)
                {
                    if (!job.Name.StartsWith("#"))
                        Jobs.Items.Add(job.Name);
                    else
                        apiToken = job.Name.Split('#')[1];
                }
            }

            if (JenkinsURL.Text.Contains("localhost:8080"))
            {
                username = "sadanandlrnr";
                apiToken = "11f15a3cc13a855d449e0f1af1e2bc3afb";
            }
            else
                username = "PataskarS";

        }

        private void JenkinsURL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Jobs.Items.Clear();
            Jobs.Text = null;
            lblMessage.Text = "";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            dataGridView1.ReadOnly = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

            // jenkinsJob = new Jenkins(JenkinsURL.Text, Jobs.Text, "11dacac6d7d4d8c06de569406216122cf9", "PataskarS");

            // jenkinsJob = new Jenkins(JenkinsURL.Text, Jobs.Text, apiToken, "PataskarS"); 11f15a3cc13a855d449e0f1af1e2bc3afb

            // Local Jenkins 
            
            jenkinsJob = new Jenkins(JenkinsURL.Text, Jobs.Text, apiToken, username);

            // var buildParameters = await jenkinsJob.GetBuildParametersAsync();

            var buildParameters = await jenkinsJob.GetDefaultJobParametersAsync();

            var list = buildParameters.Select(x => new { Key = x.Key, Value = x.Value }).ToList();

            if (list.Count > 0)
            {
                dataGridView1.Rows.Clear();

                foreach (var lst in list)
                {
                    dataGridView1.Rows.Add(lst.Key, lst.Value);


                }
                int totalWidth = 0;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Width = 400; // Set the width in pixels
                    totalWidth += column.Width;
                }

                dataGridView1.Width = totalWidth + 50;
            }
            else
            {
                dataGridView1.Rows.Clear();
                lblMessage.Text = "No Parameters are Set !!!";

                
                
            }








        }

        

        private async void button2_Click(object sender, EventArgs e)
        {

            var UpdatedBuildParameters = Helper.ConvertToDictionary(dataGridView1);
            await jenkinsJob.UpdateDefaultJobParametersAsync(UpdatedBuildParameters);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //await jenkinsJob.StartNewBuildAsync(Jobs.Text);
            statusTextBox.Text = "";

            await jobMonitor.StartNewBuildAndMonitorWithConsoleOutputAsync(Jobs.Text, JenkinsURL.Text, username, apiToken);
        }

        private void Jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        public static class MessageBoxManager
        {
            public static Form CenterOwner { get; set; }

            public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            {
                using (Form messageBoxForm = new Form())
                {
                    messageBoxForm.StartPosition = FormStartPosition.Manual;
                    messageBoxForm.Location = new Point(CenterOwner.Left + (CenterOwner.Width - messageBoxForm.Width) / 2,
                                                        CenterOwner.Top + (CenterOwner.Height - messageBoxForm.Height) / 2);
                    messageBoxForm.Size = new Size(0, 0); // MessageBox itself will determine size
                    messageBoxForm.Show();

                    return MessageBox.Show(CenterOwner, text, caption, buttons, icon);
                }
            }
        }
    }
}
