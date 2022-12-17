using Firebase.Database;
using Firebase.Database.Streaming;

namespace ElectronicsShop.Services
{
    public class DataSubscriptionService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private static readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);

        public IObservable<FirebaseEvent<T>> Subscribe<T>()
        {
            return _firebaseClient
                .Child(typeof(T).Name)
                .AsObservable<T>();

        }
    }
}
