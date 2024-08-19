using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Single_Click
{
    public delegate void JobStatusUpdatedEventHandler(object sender, JobStatusEventArgs e);

    public class JobStatusEventArgs : EventArgs
    {
        public string StatusMessage { get; }

        public JobStatusEventArgs(string statusMessage)
        {
            StatusMessage = statusMessage;
        }
    }

    public class JenkinsJobMonitor
    {
        public event JobStatusUpdatedEventHandler JobStatusUpdated;

        protected virtual void OnJobStatusUpdated(string statusMessage)
        {
            JobStatusUpdated?.Invoke(this, new JobStatusEventArgs(statusMessage));
        }

        public async Task StartNewBuildAndMonitorAsync(string jobName, string jenkinsUrl, string username, string apiToken)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{apiToken}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var startBuildUrl = $"{jenkinsUrl}/job/{jobName}/build";
                var startBuildResponse = await client.PostAsync(startBuildUrl, null);

                if (startBuildResponse.IsSuccessStatusCode)
                {
                    OnJobStatusUpdated("New build started successfully.");

                    while (true)
                    {
                        Thread.Sleep(2000);
                        var buildStatusUrl = $"{jenkinsUrl}/job/{jobName}/lastBuild/api/json";
                        var statusResponse = await client.GetAsync(buildStatusUrl);

                        if (statusResponse.IsSuccessStatusCode)
                        {
                            var statusContent = await statusResponse.Content.ReadAsStringAsync();
                            dynamic buildInfo = JsonConvert.DeserializeObject(statusContent);

                            string buildStatus = buildInfo.result;

                            if (buildStatus != null)
                            {
                                OnJobStatusUpdated($"Build status: {buildStatus}");
                                break;
                            }
                            else
                            {
                                OnJobStatusUpdated("Build is still in progress...");
                            }
                        }
                        else
                        {
                            OnJobStatusUpdated($"Failed to retrieve build status. Status code: {statusResponse.StatusCode}");
                        }

                        await Task.Delay(TimeSpan.FromSeconds(10));
                    }
                }
                else
                {
                    OnJobStatusUpdated($"Failed to start a new build. Status code: {startBuildResponse.StatusCode}");
                    var responseContent = await startBuildResponse.Content.ReadAsStringAsync();
                    OnJobStatusUpdated(responseContent);
                }
            }
        }

        public  async Task StartNewBuildAndMonitorWithConsoleOutputAsync(string jobName, string jenkinsUrl, string username, string apiToken)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{apiToken}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var startBuildUrl = $"{jenkinsUrl}/job/{jobName}/build";
                var startBuildResponse = await client.PostAsync(startBuildUrl, null);

                if (startBuildResponse.IsSuccessStatusCode)
                {
                    OnJobStatusUpdated("New build started successfully.");

                    int buildNumber = -1;  // Initialize buildNumber

                    // Polling to find out the build number from the queue
                    while (true)
                    {
                        var queueUrl = $"{jenkinsUrl}/job/{jobName}/api/json?tree=builds[number,url]";
                        var queueResponse = await client.GetAsync(queueUrl);

                        if (queueResponse.IsSuccessStatusCode)
                        {
                            var queueContent = await queueResponse.Content.ReadAsStringAsync();
                            dynamic queueInfo = JsonConvert.DeserializeObject(queueContent);

                            if (queueInfo.builds.Count > 0)
                            {
                                buildNumber = queueInfo.builds[0].number;  // Extract build number
                                OnJobStatusUpdated($"Build queued with build number: {buildNumber+1}");
                                buildNumber += 1;
                                break;
                            }
                        }

                        OnJobStatusUpdated("Build is still in queue...");
                        Thread.Sleep(5000); // Wait for 5 seconds before checking again
                    }

                    // Now monitor the build status using the build number
                    while (true)
                    {
                        Thread.Sleep(2000); // Poll every 2 seconds

                        var buildStatusUrl = $"{jenkinsUrl}/job/{jobName}/{buildNumber}/api/json";
                        HttpResponseMessage statusResponse = null;

                        // Keep trying until we get a successful response
                        while (true)
                        {
                            statusResponse = await client.GetAsync(buildStatusUrl);

                            if (statusResponse.IsSuccessStatusCode)
                            {
                                break; // Exit loop when successful
                            }

                            OnJobStatusUpdated("Retrying to retrieve build status...");
                            Thread.Sleep(5000); // Wait before retrying
                        }

                        //var statusContent = await statusResponse.Content.ReadAsStringAsync();

                        string statusContent = null;

                        // Keep trying until the content is successfully retrieved
                        while (true)
                        {
                            try
                            {
                                statusContent = await statusResponse.Content.ReadAsStringAsync();
                                
                                if (!string.IsNullOrEmpty(statusContent))
                                {
                                    break; // Exit loop when successful
                                }
                            }
                            catch (Exception ex)
                            {
                                OnJobStatusUpdated($"Error retrieving content: {ex.Message}. Retrying...");
                            }

                            Thread.Sleep(5000); // Wait before retrying
                        }


                        dynamic buildInfo = JsonConvert.DeserializeObject(statusContent);

                        string buildStatus = buildInfo.result;

                        if (buildStatus == null)
                        {
                            var consoleOutputUrl = $"{jenkinsUrl}/job/{jobName}/{buildNumber}/logText/progressiveText?start=0";
                            var consoleResponse = await client.GetAsync(consoleOutputUrl);

                            if (consoleResponse.IsSuccessStatusCode)
                            {
                                var consoleContent = await consoleResponse.Content.ReadAsStringAsync();
                                OnJobStatusUpdated(consoleContent);
                            }
                            else
                            {
                                OnJobStatusUpdated($"Failed to retrieve console output. Status code: {consoleResponse.StatusCode}");
                            }

                            OnJobStatusUpdated("Build is still in progress...");
                        }
                        else
                        {
                            OnJobStatusUpdated($"Build status: {buildStatus}");
                             break;
                        }
                    }
                }
                else
                {
                    OnJobStatusUpdated($"Failed to start a new build. Status code: {startBuildResponse.StatusCode}");
                    var responseContent = await startBuildResponse.Content.ReadAsStringAsync();
                    OnJobStatusUpdated(responseContent);
                }
            }
        }

    }

}
