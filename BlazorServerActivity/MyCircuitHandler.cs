using Microsoft.AspNetCore.Components.Server.Circuits;
using OpenTelemetry;

namespace BlazorServerActivity;

public class MyCircuitHandler : CircuitHandler
{
    private const string CircuitIdBaggageName = "CircuitId";

    private readonly CircuitIdContainer circuitIdContainer;

    public MyCircuitHandler(CircuitIdContainer circuitIdContainer)
    {
        this.circuitIdContainer = circuitIdContainer;
    }

    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        Baggage.Current.SetBaggage(CircuitIdBaggageName, circuit.Id);
        this.circuitIdContainer.CircuitId = circuit.Id;

        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        Baggage.Current.RemoveBaggage(CircuitIdBaggageName);

        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }
}