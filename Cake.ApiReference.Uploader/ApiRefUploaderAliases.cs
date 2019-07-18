using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Cake.Core;
using Cake.Core.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using LogLevel = Cake.Core.Diagnostics.LogLevel;
using Verbosity = Cake.Core.Diagnostics.Verbosity;

namespace Cake.ApiReference.Uploader
{
    [CakeAliasCategory("Cake.ApiReference.Uploader")]
    public static class ApiRefUploaderAliases
    {
        [CakeMethodAlias]
        public static void UploadApiReferences(this ICakeContext context, RestApiCredentials credentials, DotNetOptions options)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var request = new DotNetRequest(credentials, options);

            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Uploading API Refs for {0}-{1} at {2}",
                request.ProductKey, request.SectionKey, credentials.Uri);

            var multipartContent = new MultipartFormDataContent();
            var json = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            multipartContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), request.ContentName);

            var dllContent = new ByteArrayContent(request.DllFile.FileData);
            dllContent.Headers.ContentType = new MediaTypeHeaderValue(request.DllFile.FileMimeType);
            multipartContent.Add(dllContent, "dllFile", request.DllFile.FileName);

            var xmlContent = new ByteArrayContent(request.XmlFile.FileData);
            xmlContent.Headers.ContentType = new MediaTypeHeaderValue(request.XmlFile.FileMimeType);
            multipartContent.Add(xmlContent, "xmlFile", request.XmlFile.FileName);

            PostContent(context, credentials, multipartContent);
        }

        [CakeMethodAlias]
        public static void UploadHtmlApiReferences(this ICakeContext context, RestApiCredentials credentials, DotNetHtmlOptions options)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var request = new DotNetHtmlRequest(credentials, options);

            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Uploading API Refs for {0}-{1} at {2}",
                request.ProductKey, request.SectionKey, credentials.Uri);

            var multipartContent = new MultipartFormDataContent();
            var json = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            multipartContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), request.ContentName);

            var zipFileContent = new ByteArrayContent(request.ZipFile.FileData);
            zipFileContent.Headers.ContentType = new MediaTypeHeaderValue(request.ZipFile.FileMimeType);
            multipartContent.Add(zipFileContent, "zipFile", request.ZipFile.FileName);
             
            PostContent(context, credentials, multipartContent);
        }
        
               
        [CakeMethodAlias]
        public static void UploadApiReferences(this ICakeContext context, RestApiCredentials credentials, JavaOptions options)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var request = new JavaRequest(credentials, options);

            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Uploading API Refs for {0}-{1} at {2}",
                request.ProductKey, request.SectionKey, credentials.Uri);

            var multipartContent = new MultipartFormDataContent();
            var json = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            multipartContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), request.ContentName);

            var content = new ByteArrayContent(request.JarFile.FileData);
            content.Headers.ContentType = new MediaTypeHeaderValue(request.JarFile.FileMimeType);
            multipartContent.Add(content, "jarFile", request.JarFile.FileName);

            PostContent(context, credentials, multipartContent);
        }

        private static void PostContent(ICakeContext context, RestApiCredentials credentials,
            MultipartFormDataContent multipartContent)
        {
            var client = new HttpClient {Timeout = TimeSpan.FromSeconds(2400)};

            HttpResponseMessage response = client.PostAsync(credentials.Uri, multipartContent).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                string errorMsg = response.Content.ReadAsStringAsync().Result;
                context.Log.Write(Verbosity.Normal, LogLevel.Error,
                    "Uploading API references completed with error: '{0}'",
                    errorMsg);

                throw new Exception(errorMsg);
            }

            context.Log.Write(Verbosity.Normal, LogLevel.Information, "SUCCESS! API references has been uploaded.");
        }
    }
}