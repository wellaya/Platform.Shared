using Platform.Application.Common.Interfaces;

namespace Platform.Functions.Services;

/// <summary>
/// Minimal ICurrentUser for isolated-worker Functions. Replace with real claims extraction
/// once authentication (e.g. Easy Auth, JWT bearer, APIM) is wired into the middleware pipeline.
/// </summary>
public class FunctionsCurrentUser : ICurrentUser
{
    public string? Id => null;
    public string? Name => null;
    public bool IsAuthenticated => false;
}