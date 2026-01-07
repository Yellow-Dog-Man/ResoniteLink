---
title: Why is ResoniteLink that way?
---

## Why WebSocket?
- It's pretty commonly supported in lots of languages and environments (even ones where raw TCP/UDP isn't)
- It forms a session with easy back & forth communication
- Allows either side to initiate sending a message (as compared to HTTP REST API for example)
- Allows sending binary messages
    - These allow for more efficient implementation of sending asset data

## Why JSON?
- It's very ubiquitous and supported in pretty much every common language, often with multiple libraries to choose from
- It's relatively simple structure of basic value types, lists and dictionaries, which is sufficient to fully decompose Resonite's data model

## Why open source?
Resonite has a highly creative and skilled community, which has been building outstanding in-game content as well as external tools and mods for the game that open up many more possibilities for Resonite.

By creating this protocol and making the client implementation open, we want to make it easy to build with it and open up even more possibilities and workflows than before. Making this open removes a lot of the friction that's normally associated with decompiling and reverse engineering parts of the engine.

## Why the name "ResoniteLink"?
We were considering "cool" unique names. However, we ended up following similar philosophy to naming "ResonitePackage" - we want this tool to be recognizable as Resonite-related tool/protocol at first sight and a "cool name" would obscure that fact.
