using System.Diagnostics;
using OpenTelemetry;

namespace BlazorServerActivity;

public class BaggageActivityEnricher : BaseProcessor<Activity>
{
    public override void OnStart(Activity data)
    {
        base.OnStart(data);

        foreach (var (key, value) in Baggage.Current.GetBaggage())
        {
            data.AddTag(key, value);
        }
    }
}