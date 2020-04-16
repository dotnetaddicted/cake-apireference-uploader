using Cake.ApiReference.Uploader;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Cake.ApiReference.Uploader.ApiRefUploaderAliases.UploadHtmlApiReferences(null,
            new RestApiCredentials
            {
                UserName = "user",
                Password = "pass",
                Uri = "http://contoso.com" + "/GenerateDotNetHtmlApiRef"
            },
            new DotNetHtmlOptions
            {
                ProductKey = "API_REF_PRODUCT_KEY",
                ZipFilePath = "zipPath.ToString()"
            });
        }
    }
}
