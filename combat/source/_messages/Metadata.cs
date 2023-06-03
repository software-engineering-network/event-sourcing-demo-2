using System;

namespace EventSourcingDemo.Combat
{
    public record Metadata(Guid TraceId, Guid UserId);
}
