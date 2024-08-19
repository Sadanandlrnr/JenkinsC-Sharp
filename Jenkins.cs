using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Single_Click
{


    public class Jenkins
    {
        private readonly string jenkinsUrl;
        private readonly string jobName;
        // private static readonly string apiToken = "11dea594f1e7d1b0fcff804aafd2d70998";
        private readonly string apiToken;

        private readonly string username;

        public Dictionary<string, string> JenkinsBuildParamter = new Dictionary<string, string>();
        public Jenkins(string jenkinsUrl, string jobName, string apiToken, string username)
        {
            this.jenkinsUrl = jenkinsUrl;
            this.jobName = jobName;
            this.apiToken = apiToken;
            this.username = username;
        }

        public Jenkins()
        {

        }


        public async Task<Dictionary<string, string>> GetBuildParametersAsync()
        {
            try
            {
                // Step 1: Get the latest build number
                string latestBuildUrl = $"{jenkinsUrl}/job/{jobName}/lastBuild/api/json";
                string latestBuildResponse = await GetJenkinsDataAsync(latestBuildUrl);

                JObject latestBuildJson = JObject.Parse(latestBuildResponse);
                int latestBuildNumber = (int)latestBuildJson["number"];
                Console.WriteLine("Latest Build Number: " + latestBuildNumber);

                // Step 2: Get the build parameters of the latest build
                string buildUrl = $"{jenkinsUrl}/job/{jobName}/{latestBuildNumber}/api/json";
                string buildResponse = await GetJenkinsDataAsync(buildUrl);
                JObject buildJson = JObject.Parse(buildResponse);
                JArray actionsArray = (JArray)buildJson["actions"];

                foreach (var action in actionsArray)
                {
                    if (action["parameters"] != null)
                    {
                        foreach (var param in action["parameters"])
                        {
                            string paramName = (string)param["name"];
                            string paramValue = (string)param["value"];
                            Console.WriteLine($"Parameter Name: {paramName}, Value: {paramValue}");

                            JenkinsBuildParamter.Add(paramName, paramValue);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return JenkinsBuildParamter;


        }

        private async Task<string> GetJenkinsDataAsync(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{apiToken}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task UpdateBuildParametersAsync(string jobName, Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                // Basic Authentication
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{apiToken}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                // Build URL for triggering the job with parameters
                var url = $"{jenkinsUrl}/job/{jobName}/buildWithParameters";

                // Create content with parameters
                var content = new FormUrlEncodedContent(parameters);

                // Send POST request to Jenkins to trigger the build with updated parameters
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Build parameters updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to update build parameters. Status code: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        public async Task StartNewBuildAsync(string jobName)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{apiToken}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var url = $"{jenkinsUrl}/job/{jobName}/build";

                var response = await client.PostAsync(url, null); // Sending a POST request with no content

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("New build started successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to start a new build. Status code: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }


  

    public async Task<Dictionary<string, string>> GetDefaultJobParametersAsync()
    {
            var jobParameters = new Dictionary<string, string>();

            try
            {
                // Step 1: Get the job configuration (which contains the default parameters)
                string jobConfigUrl = $"{jenkinsUrl}/job/{jobName}/config.xml";
                string jobConfigResponse = await GetJenkinsDataAsync(jobConfigUrl);

                // Remove the XML declaration (e.g., <?xml version="1.1"?>)
                if (jobConfigResponse.StartsWith("<?xml"))
                {
                    int indexOfEndOfDeclaration = jobConfigResponse.IndexOf("?>") + 2;
                    jobConfigResponse = jobConfigResponse.Substring(indexOfEndOfDeclaration).Trim();
                }

                // Step 2: Parse the XML using XDocument
                XDocument xDoc = XDocument.Parse(jobConfigResponse);

                var parameters = xDoc.Descendants("hudson.model.StringParameterDefinition")
                                     .Select(param => new
                                     {
                                         Name = param.Element("name")?.Value,
                                         DefaultValue = param.Element("defaultValue")?.Value
                                     });

                foreach (var param in parameters)
                {
                    if (param.Name != null && param.DefaultValue != null)
                    {
                        Console.WriteLine($"Parameter Name: {param.Name}, Default Value: {param.DefaultValue}");
                        jobParameters.Add(param.Name, param.DefaultValue);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return jobParameters;
    }

   

public async Task UpdateDefaultJobParametersAsync(Dictionary<string, string> newParameters)
    {
        try
        {
            // Step 1: Get the current job configuration (config.xml)
            string jobConfigUrl = $"{jenkinsUrl}/job/{jobName}/config.xml";
            string jobConfigResponse = await GetJenkinsDataAsync(jobConfigUrl);

            // Remove the XML declaration (e.g., <?xml version="1.1"?>) if it exists
            if (jobConfigResponse.StartsWith("<?xml"))
            {
                int indexOfEndOfDeclaration = jobConfigResponse.IndexOf("?>") + 2;
                jobConfigResponse = jobConfigResponse.Substring(indexOfEndOfDeclaration).Trim();
            }

            // Step 2: Parse the XML using XDocument
            XDocument xDoc = XDocument.Parse(jobConfigResponse);

            // Step 3: Find and update the default parameters
            var parameters = xDoc.Descendants("hudson.model.StringParameterDefinition");

            foreach (var param in parameters)
            {
                string paramName = param.Element("name")?.Value;
                if (paramName != null && newParameters.ContainsKey(paramName))
                {
                    param.Element("defaultValue")?.SetValue(newParameters[paramName]);
                    Console.WriteLine($"Updated Parameter: {paramName}, New Value: {newParameters[paramName]}");
                }
            }

                // Step 4: Upload the modified config.xml back to Jenkins
                using (var httpClient = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{username}:{apiToken}");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    var content = new StringContent(xDoc.ToString(), Encoding.UTF8, "application/xml");
                    var response = await httpClient.PostAsync($"{jenkinsUrl}/job/{jobName}/config.xml", content);
                    response.EnsureSuccessStatusCode(); // Throws an exception if the status code isn't successful

                    Console.WriteLine("Job parameters updated successfully.");
                }
            }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }




}   






}
