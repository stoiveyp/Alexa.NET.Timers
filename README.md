# Alexa.NET.Timers
A simple package built to work with Alexa Timers

## Example code
```csharp

var client = new TimersClient(skillRequest);

var createResponse = await client.Create(new CreateTimerRequest(TimeSpan.FromHours(1),new NotifyOnlyOperation(), DisplayVisibility.Visible,true));
var listResponse = await client.List();
var get = await client.Get(createResponse.Id);

await client.Pause(createResponse.Id);
await client.Resume(createResponse.Id);
await client.Delete();

```