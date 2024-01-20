using System.Diagnostics;
using OpenTelemetry;

namespace BlazorServerActivity;

// will never be instantiated
public class CircuitIdProcessor : BaseProcessor<Activity>
{
    private readonly CircuitIdContainer circuitIdContainer;

    public CircuitIdProcessor(CircuitIdContainer circuitIdContainer)
    {
        this.circuitIdContainer = circuitIdContainer;
    }

    public override void OnStart(Activity data)
    {
        data.AddTag("CircuitId", this.circuitIdContainer.CircuitId);

        base.OnStart(data);
    }
}