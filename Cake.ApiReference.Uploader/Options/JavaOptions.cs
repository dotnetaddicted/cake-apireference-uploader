using Newtonsoft.Json;

namespace Cake.ApiReference.Uploader
{
    public class JavaOptions : BaseOptions
    {
        public string Version { get; set; }

        [JsonIgnore]
        public string JarFilePath { get; set; }
    }
}