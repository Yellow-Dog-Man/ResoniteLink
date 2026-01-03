using ResoniteLink;

Console.Write("Connect to (localhost port or ws:// URL): ");

var connectionTarget = Console.ReadLine().Trim();

Uri targetUrl;

if (int.TryParse(connectionTarget, out var port))
    targetUrl = new Uri($"ws://localhost:{port}");
else if (!Uri.TryCreate(connectionTarget, UriKind.Absolute, out targetUrl))
{
    Console.WriteLine("Failed to parse URL");
    return 1;
}

if(targetUrl.Scheme != "ws")
{
    Console.WriteLine("Scheme must be ws (websocket)");
    return 1;
}

try
{
    Console.WriteLine("Connecting...");

    var link = new LinkInterface();
    await link.Connect(targetUrl, CancellationToken.None);

    Console.WriteLine("Connected.");

    // We are connected, we can start REPL loop now
    var repl = new REPL_Controller(link);

    // Run the command processing loop until it finishes
    await repl.RunLoop();

    return 0;
}
catch(Exception ex)
{
    Console.WriteLine($"Error: {ex}");
    return 2;
}