using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace Cake.ApiReference.Uploader
{
    internal class DotNetRequest : BaseRequest
    {
        public string ContentName => "dotNetApiRefRequest";

        public override string SectionKey => ProductSections.Net;

        [JsonIgnore]
        public ApiFileInfo DllFile { get; set; }

        [JsonIgnore]
        public ApiFileInfo XmlFile { get; set; }

        internal DotNetRequest(RestApiCredentials credentials, DotNetOptions options) : base(credentials, options)
        {
            DllFile = new ApiFileInfo
            {
                FileName = Path.GetFileName(options.DllFilePath),
                FileData = File.ReadAllBytes(options.DllFilePath),
                FileMimeType = MimeMapping.GetMimeMapping(options.DllFilePath)
            };

            XmlFile = new ApiFileInfo
            {
                FileName = Path.GetFileName(options.XmlFilePath),
                FileData = File.ReadAllBytes(options.XmlFilePath),
                FileMimeType = MimeMapping.GetMimeMapping(options.XmlFilePath)
            };
        }

        protected override void Validate(BaseOptions options)
        {
            if (options is DotNetOptions == false)
                throw new InvalidOperationException("DotNetRequest options should be instance of DotNetOptions class");

            if (string.IsNullOrEmpty(options.ProductKey))
                throw new ArgumentNullException(nameof(options.ProductKey));

            var dotNetOptions = options as DotNetOptions;

            if (string.IsNullOrEmpty(dotNetOptions.DllFilePath))
                throw new ArgumentNullException(nameof(dotNetOptions.DllFilePath));

            if (string.IsNullOrEmpty(dotNetOptions.XmlFilePath))
                throw new ArgumentNullException(nameof(dotNetOptions.XmlFilePath));
        }
    }
}