using Android.OS;
using Firebase.Analytics;
using GoViatic.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAnalytic))]
public class FirebaseAnalytic : IFirebaseAnalytics
{
    public void LogEvent(string eventId)
    {
        LogEvent(eventId, null);
    }

    public void LogEvent(string eventId, string paramName, string value)
    {
        LogEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            });
    }

    public void LogEvent(string eventId, IDictionary<string, string> parameters)
    {
        var firebaseAnalytics = FirebaseAnalytics.GetInstance(Android.App.Application.Context);
        if (parameters == null)
        {
            firebaseAnalytics.LogEvent(eventId, null);
            return;
        }
        var bundle = new Bundle();
        foreach (var item in parameters)
        {
            bundle.PutString(item.Key, item.Value);
        }
        firebaseAnalytics.LogEvent(eventId, bundle);
    }
}