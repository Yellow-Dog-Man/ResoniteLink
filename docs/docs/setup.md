---
title: 'Setting up Resonite'
---

## Setting up Resonite
In order to use ResoniteLink, you'll need to have Resonite open and running a session with ResoniteLink enabled.

## In Game
1. Open the Resonite Dashboard
2. Navigate to the "Session" page
3. Navigate to the "Settings" tab of the "Session" page
4. Select "Enable ResoniteLink" on the bottom left of the UI
5. You should now see the text `ResoniteLink running on port: <port number>`.
6. Connect from external application using the provided port

## Headless

### Config File
In the configuration for a particular world, add the `"enableResoniteLink"` key with value `true`. Optionally, you can also specify a port with the `"forceResoniteLinkPort"` key.

### CLI
In a running headless, you can use `enableResoniteLink <port>`. For a random port, use `0`.

