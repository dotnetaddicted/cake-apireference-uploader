# Cake ApiReference Uploader
CakeBuild extension for uploading Api references to specified URL

# How to upload ApiReference for .NET

```csharp
UploadApiReferences(
    new RestApiCredentials {
        UserName = "username", 
        Password = "password", 
        Uri = "https://api.domain.com/v1/GenerateApiReferenceDotNet"
    },
    new DotNetOptions {
        ProductKey = "productKey",
        DllFilePath = "C:\SomeProduct.dll",
        XmlFilePath = "C:\SomeProduct.xml"
    });  
```       

# How to upload ApiReference for Java

```csharp        
UploadApiReferences(
    new RestApiCredentials {
        UserName = "username", 
        Password = "password", 
        Uri = "https://api.domain.com/v1/GenerateApiReferenceJava"
    },
    new JavaOptions {
        ProductKey = "productKey",
        Version = "18.8",
        JarFilePath  = "C:\SomeProduct.javadoc"
    });   
``` 
