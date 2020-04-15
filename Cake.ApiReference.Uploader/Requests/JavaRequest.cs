using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace Cake.ApiReference.Uploader
{
    internal class JavaRequest : BaseRequest
    {
        public string ContentName => "javaApiRefRequest";

        public override string SectionKey => ProductSections.Java;

        public string Version { get; set; }

        [JsonIgnore]
        public ApiFileInfo JarFile { get; set; }


        internal JavaRequest(RestApiCredentials credentials, JavaOptions options) : base(credentials, options)
        {
            Version = options.Version;

            JarFile = new ApiFileInfo
            {
                FileName = Path.GetFileName(options.JarFilePath),
                FileData = File.ReadAllBytes(options.JarFilePath),
                FileMimeType = MimeMapping.MimeUtility.GetMimeMapping(options.JarFilePath)
            };
        }

        protected override void Validate(BaseOptions options)
        {
            if (options is JavaOptions == false)
                throw new InvalidOperationException("JavaRequest options should be instance of JavaOptions class");

            if (string.IsNullOrEmpty(options.ProductKey))
                throw new ArgumentNullException(nameof(options.ProductKey));

            var javaOptions = options as JavaOptions;

            if (string.IsNullOrEmpty(javaOptions.Version))
                throw new ArgumentNullException(nameof(javaOptions.Version));

            if (string.IsNullOrEmpty(javaOptions.JarFilePath))
                throw new ArgumentNullException(nameof(javaOptions.JarFilePath));
        }
    }
}