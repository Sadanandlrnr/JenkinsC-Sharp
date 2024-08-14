using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Single_Click
{
    public class JenkinsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("jenkinsServers")]
        [ConfigurationCollection(typeof(JenkinsServerCollection), AddItemName = "server")]
        public JenkinsServerCollection JenkinsServers => (JenkinsServerCollection)base["jenkinsServers"];
    }

    public class JenkinsServerCollection : ConfigurationElementCollection, IEnumerable<JenkinsServerElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new JenkinsServerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JenkinsServerElement)element).Url;
        }

        public new IEnumerator<JenkinsServerElement> GetEnumerator()
        {
            foreach (var key in BaseGetAllKeys())
            {
                yield return (JenkinsServerElement)BaseGet(key);
            }
        }
    }

    public class JenkinsServerElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(JobCollection), AddItemName = "job")]
        public JobCollection Jobs => (JobCollection)base[""];
    }

    public class JobCollection : ConfigurationElementCollection, IEnumerable<JobElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new JobElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobElement)element).Name;
        }

        public new IEnumerator<JobElement> GetEnumerator()
        {
            foreach (var key in BaseGetAllKeys())
            {
                yield return (JobElement)BaseGet(key);
            }
        }
    }

    public class JobElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }
}
