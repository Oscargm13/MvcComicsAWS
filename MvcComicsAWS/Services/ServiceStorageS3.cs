using Amazon.S3.Model;
using Amazon.S3;

namespace MvcComicsAWS.Services
{
    public class ServiceStorageS3
    {
        private string BucketName;
        private IAmazonS3 ClientS3;


        public ServiceStorageS3(IConfiguration configuration, IAmazonS3 clientS3)
        {
            BucketName = configuration.GetValue<string>("AWS:BucketName");
            ClientS3 = clientS3;
        }

        public async Task<bool> UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                Key = fileName,
                BucketName = BucketName,
                InputStream = stream
            };

            PutObjectResponse response = await ClientS3.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
