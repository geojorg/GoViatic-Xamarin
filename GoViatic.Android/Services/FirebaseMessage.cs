using Android.App;
using Firebase.Messaging;

namespace BalotoRandom.Droid
{
    [Service(Name = "com.geojorgco.goviatic.FirebaseMessage")]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessage : FirebaseMessagingService
    {
        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
        }
        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);
        }
    }
}