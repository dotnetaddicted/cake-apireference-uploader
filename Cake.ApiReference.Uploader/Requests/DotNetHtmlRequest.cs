using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;

namespace Cake.ApiReference.Uploader
{
    internal class DotNetHtmlRequest : BaseRequest
    {
        public string ContentName => "dotNetHtmlApiRefRequest";

        public override string SectionKey => ProductSections.Net;

        [JsonIgnore]
        public ApiFileInfo ZipFile { get; set; }

        internal DotNetHtmlRequest(RestApiCredentials credentials, DotNetHtmlOptions options) : base(credentials, options)
        {
            ZipFile = new ApiFileInfo
            {
                FileName = Path.GetFileName(options.ZipFilePath),
                FileData = File.ReadAllBytes(options.ZipFilePath),
                FileMimeType = MimeMapping.GetMimeMapping(options.ZipFilePath)
            };

        }

        protected override void Validate(BaseOptions options)
        {
            if (options is DotNetHtmlOptions == false)
                throw new InvalidOperationException("DotNetHtmlRequest options should be instance of DotNetHtmlRequest class");

            if (string.IsNullOrEmpty(options.ProductKey))
                throw new ArgumentNullException(nameof(options.ProductKey));

            var dotNetHtmlOptions = options as DotNetHtmlOptions;

            if (string.IsNullOrEmpty(dotNetHtmlOptions.ZipFilePath))
                throw new ArgumentNullException(nameof(dotNetHtmlOptions.ZipFilePath));
        }
    }
}
