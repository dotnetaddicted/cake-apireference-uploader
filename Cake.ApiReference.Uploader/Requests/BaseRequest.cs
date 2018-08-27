using System;

namespace Cake.ApiReference.Uploader
{
    internal abstract class BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public abstract string SectionKey { get; }
        public string ProductKey { get; set; }

        internal BaseRequest(RestApiCredentials credentials, BaseOptions options)
        {
            Validate(credentials);

            Validate(options);

            UserName = credentials.UserName;
            Password = credentials.Password;
            ProductKey = options.ProductKey;
        }

        protected abstract void Validate(BaseOptions options);

        private void Validate(RestApiCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.UserName))
                throw new ArgumentNullException(nameof(credentials.UserName));

            if (string.IsNullOrEmpty(credentials.Password))
                throw new ArgumentNullException(nameof(credentials.Password));

            if (string.IsNullOrEmpty(credentials.Uri))
                throw new ArgumentNullException(nameof(credentials.Uri));
        }
    }
}