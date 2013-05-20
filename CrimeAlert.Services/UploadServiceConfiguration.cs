using System.Configuration;

namespace CrimeAlert.Services
{
    public class UploadServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("CloudUrl", DefaultValue = "", IsRequired = true)]
        public string CloudUrl
        {
            get { return (string)this["CloudUrl"]; }
            set { this["CloudUrl"] = value; }
        }

        [ConfigurationProperty("BucketName", DefaultValue = "", IsRequired = true)]
        public string BucketName
        {
            get { return (string)this["BucketName"]; }
            set { this["BucketName"] = value; }
        }

        [ConfigurationProperty("AmazonS3AccessKey", DefaultValue = "", IsRequired = true)]
        public string AmazonS3AccessKey
        {
            get { return (string)this["AmazonS3AccessKey"]; }
            set { this["AmazonS3AccessKey"] = value; }
        }

        [ConfigurationProperty("AmazonS3SecretKey", DefaultValue = "", IsRequired = true)]
        public string AmazonS3SecretKey
        {
            get { return (string)this["AmazonS3SecretKey"]; }
            set { this["AmazonS3SecretKey"] = value; }
        }
    }
}