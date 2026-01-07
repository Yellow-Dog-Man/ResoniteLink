---
title: 'Getting Started'
---

## Getting started

Getting started with ResoniteLink is fairly easy.
You can reference it in your projects using the package `YellowDogMan.ResoniteLink` or:

```bash
$ dotnet add package YellowDogMan.ResoniteLink
```

### How IDs work
IDs in ResoniteLink are strings and can be allocated either by Resonite or the client (e.g. your tool).

If you create object (slot, component) through ResoniteLink, you can assign it your own ID. You must take care not to collide with another ID - prefixing it with a custom string will often work well. Resonite will then remember this ID during the ResoniteLink session. This saves you from fetching the data back after creating objects and allows you to set references to new objects as part of the creation.

You can also leave ID to be null when creating objects - Resonite will then allocate an ID. You'll need to query the object to learn this ID.

Any Resonite IDs are prefixed with `Reso_`. It's NOT recommended to use this prefix for ID's you'll allocate.

The IDs are NOT persistent. After saving the world and loading it again, they will be different.
