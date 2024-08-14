using System.Collections.Generic;
using System.Configuration;

namespace Single_Click
{
    public partial class Form1 : Form
    {
        private JenkinsConfigurationSection configSection = (JenkinsConfigurationSection)ConfigurationManager.GetSection("jenkinsConfiguration");
        private JenkinsConfigurationSection jenkinsClients = (JenkinsConfigurationSection)ConfigurationManager.GetSection("jenkinsConfiguration");

        Jenkins jenkinsJob = new Jenkins();
        private string apiToken = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var configSection = (JenkinsConfigurationSection)ConfigurationManager.GetSection("jenkinsConfiguration");

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
                JobCollection jobCollection = configSection.JenkinsServers.Where(x => x.Url == JenkinsURL.Text).First().Jobs;



                foreach (var job in jobCollection)
                {
                    if(!job.Name.StartsWith("#"))
                        Jobs.Items.Add(job.Name);
                    else
                        apiToken= job.Name.Split('#')[1];
                }
            }

        }

        private void JenkinsURL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            dataGridView1.ReadOnly = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

            // jenkinsJob = new Jenkins(JenkinsURL.Text, Jobs.Text, "11dacac6d7d4d8c06de569406216122cf9", "PataskarS");

            jenkinsJob = new Jenkins(JenkinsURL.Text, Jobs.Text, apiToken, "PataskarS");
            var buildParameters = await jenkinsJob.GetBuildParametersAsync();

            var list = buildParameters.Select(x => new { Key = x.Key, Value = x.Value }).ToList();

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

        private async void button2_Click(object sender, EventArgs e)
        {

            var UpdatedBuildParameters = Helper.ConvertToDictionary(dataGridView1);
            await jenkinsJob.UpdateBuildParametersAsync(Jobs.Text, UpdatedBuildParameters);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
