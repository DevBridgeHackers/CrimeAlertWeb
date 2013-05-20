using System;
using System.IO;
using CrimeAlert.ServiceContracts;
using CrimeAlert.Services.Exceptions;
using DevBridge.Amazon;

namespace CrimeAlert.Services
{
    public class UploadService : IUploadService
    {
        private readonly UploadServiceConfiguration configuration;

        public UploadService(IConfigurationLoaderService configurationLoaderService)
        {
            configuration = configurationLoaderService.LoadConfig<UploadServiceConfiguration>();
        }

        public bool UploadFile(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException(path);
                }

                var fileName = Path.GetFileName(path);
                var cloudFront = new CloudFront(configuration.AmazonS3AccessKey, configuration.AmazonS3SecretKey);
                var fileStream = new FileStream(path, FileMode.Open);

                return cloudFront.UploadObject(configuration.BucketName, fileName, fileStream, null, Amazon.S3.Model.S3CannedACL.PublicRead, null);
            }
            catch (Exception exception)
            {
                throw new UploadServiceException(string.Format("Upload failed. File path: {0}", path), exception);
            }
        }
    }
}
