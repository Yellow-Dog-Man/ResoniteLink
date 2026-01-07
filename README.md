# ⚠️ Beta WARNING! ⚠️
Currently, this library is in "beta" state. It's just been recently released, and we're seeking for feedback on the protocol and are still open to significant breaking changes.

As such, please take care when building with this protocol in this stage and be prepared for potential significant breaking changes (e.g. changes to schema, naming things, structure and such).

This stage won't likely run long. Once we're happy that we got some feedback, we'll remove the label and will be a lot more hesitant to introduce breaking changes, providing more stability.

# Nuget Package
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![NuGet](https://img.shields.io/nuget/v/YellowDogMan.ResoniteLink.svg)](https://www.nuget.org/packages/YellowDogMan.ResoniteLink) [![GitHub deployments](https://img.shields.io/github/deployments/Yellow-Dog-Man/ResoniteLink/github-pages?style=flat&label=Documentation)](https://yellow-dog-man.github.io/ResoniteLink/)

# What is Resonite?
Resonite is a social VR sandbox where you can build anything in-game, realtime and with realtime collaboration and implicit network synchronization. It is like being inside a virtual universe / game-engine that you have control over and that you can build together with your friends or colleagues.

Everything in Resonite is built around a shared data model - from the overall scene hierarchy, components to the individual fields - everything is accessible, modifiable and scriptable, giving users unprecedented level of control.

You can get Resonite completely for free on Steam here: https://store.steampowered.com/app/2519830/Resonite/

# What is ResoniteLink?
While Resonite has a heavy focus on building things in-game, interoperability with external tools and scripts is also important, as it plugs Resonite into a larger ecosystem. ResoniteLink is a simple WebSocket protocol designed as a foundational building block to allow anyone to more easily build external tools and interfaces to read and write Resonite's data model.

The protocol closely follow's Resonite's data model and deconstructs it into primitive JSON types which can be used from nearly any programming language. With this, you can integrate Resonite worlds with any tool you can imagine.

You can find the documentation [hosted on GitHub pages](https://yellow-dog-man.github.io/ResoniteLink/).

## Use case examples
- Unity/Unreal/Godot/Blender... SDK
    - You can bring existing content from those tools into Resonite easily
    - They will also allow integrating Resonite with other tools as part of your workflow
- Procedural world/asset generation with external tools
- External scripts controlling a world during live events
- Monitoring state of the world with external tools
- Twitch chat integration
    - With the REPL classes, you can let Twitch chat control the Resonite world at fine level - at your own risk :3
- External utilities & tools for processing worlds
- Use Resonite as visualization tool for external tools
    - E.g. 3D scanning software can easily send data to Resonite for realtime VR visualization
    - Any tool will benefit from free collaboration & network synchronization and fully built VR interactions without having to build this by itself

## Does this replace in-game building?
No. 

Building in-game is still our primary focus. Any content you bring into Resonite via this protocol can still be edited and worked with using the in-game tools.

This is meant to enhance the interoperability and open new workflows, but not replace any of them.

# What is purpose of this repository?
There are two core purposes of this repository:

## ResoniteLink protocol reference
You can use this repository as a reference and examples for implementing your own versions of this protocol in other languages. As this library is used within Resonite itself, this repository will always reflect the latest state of the protocol.

To get started, check the "Models" folder, which contains the JSON models for core data model primitives as well as the command messages and responses. Their fields should be documented on their meaning and purpose too within the code.

## Building block for C# apps & tools
If you want to build ResoniteLink apps in C#, this library will also give you a head start by providing classes for the models, so you don't have to work with JSON directly as well as some utility classes for managing WebSocket connection and easily getting responses via async API.

We will be using this library for other official projects, such as Unity SDK for Resonite, which will also serve as another implementation example.

# Roadmap
The protocol is not complete and additional functionality will be added over time. Some crucial for certain use-cases and some convenience to make certain use-patterns easier. Here are some features that are currently planned to be added at some point:

- Support more data model types
    - Arrays
    - Dictionaries
    - SyncVars
- Asset API for generating/importing assets externally (Textures, Meshes, Audio, Animations...)
    - Reading assets will not likely be supported until we introduce more robust asset protection system
- Type reflection - ability to fetch list and structure of supported component types by the current instance
    - You'll be able to use this for component type hinting for your tooling
    - It will also be useful for generating strongly typed bindings for tools
- Type validation
    - Resonite follows the C# type system, which could be complex to emulate in other languages. The API will allow you to let Resonite validate types
- Screenshot capture API
    - When connected to graphical client of Resonite a command will allow requesting render of the scene from particular viewpoint
- Optionally monitoring fields for changes
- Sending messages from Resonite to ResoniteLink for custom handling of events

# How does this relate to Resonite importers?
Resonite can import wide variety of content in-game - 3D models, images, audio, video... even Minecraft worlds! This protocol can potentially be used to do the same, by making the importer code external. There is some overlap between the two, with certain pros and cons.

However, in a number of cases it's easier to run the code to generate Resonite data model structures within the external tool itself.

## Pros of in-game importers
- Seamless integration
    - Just drag & drop file into Resonite and it imports with a in-game importer guide
    - More efficient and easier implementation - it has full access to Resonite's classes and helper methods for assets

## Pros of ResoniteLink importers
- Can run within source tool without relying on export/exchange formats
- Currently, it's easier to build them, without need for modding Resonite
    - This will change in the future as we plan to make the import/export system modular & open source
- Can be written in any language, not just C# (and other CLR languages)

# Community project/library links
There are none yet! ;(

If you build something cool, let us know (just create an issue on this repository) with a link and we'll add it when we can!
