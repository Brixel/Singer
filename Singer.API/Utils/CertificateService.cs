using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Singer.Services.Utils;

public static class CertificateService
{
    public static X509Certificate2 LoadCert(string certThumbprint)
    {
        X509Certificate2 cert = null;
        using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = certStore.Certificates.Find(
               X509FindType.FindByThumbprint,
               // Replace below with your cert's thumbprint
               certThumbprint,
               false);
            // Get the first cert with the thumbprint
            if (certCollection.Count > 0)
            {
                cert = certCollection[0];
            }
        }

        return cert;
    }

    public static X509Certificate2 LoadCert(string fileName, string password)
    {
        return new X509Certificate2(Path.Combine(".", fileName), password,
        X509KeyStorageFlags.MachineKeySet |
           X509KeyStorageFlags.PersistKeySet |
           X509KeyStorageFlags.Exportable);
        ;
    }

}
