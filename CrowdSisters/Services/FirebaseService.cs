using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Drawing;
namespace CrowdSisters.Services
{
    public class FirebaseService
    {

        public async Task<string> subirStorage(Stream archivo, string nombre)
        {
            string email = "firebase@gmail.com";
            string clave = "Firebase123";
            string ruta = "crowdsisters-21e4f.appspot.com";
            string api_key = "AIzaSyBlfgt8XuB4oawkTRiPheV6_BxpAHA6zDg";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            using (var originalImage = Image.FromStream(archivo))
            using (var resizedImage = new Bitmap(626, 417))
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(originalImage, 0, 0, 626, 417);

                // Guardar la imagen redimensionada en un nuevo stream
                using (var resizedStream = new MemoryStream())
                {
                    resizedImage.Save(resizedStream, originalImage.RawFormat);
                    resizedStream.Seek(0, SeekOrigin.Begin);

                    var task = new FirebaseStorage(
                        ruta,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true

                        })
                        .Child("FotosProyecto")
                        .Child(nombre)
                        .PutAsync(resizedStream, cancellation.Token);

                    var downloadURL = await task;

                    return downloadURL;
                }
            }
        }
    }
}
