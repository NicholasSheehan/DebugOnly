# DebugOnly

Works exactly the same as the "EditorOnly" tag in Unity explained here very simpily by [@John_O_Really](https://twitter.com/John_O_Really/status/1069984103074807808)

Objects tagged with "DebugOnly" will be left out in release builds, but will remain in debug builds. Good for having extra objects whilst debugging a build.

Included is a very simple demo, one scene with 3 different colored cubes.
- A red cube tagged with "EditorOnly"
- A green cube tagged with "DebugOnly"
- A blue cube left untagged

## Installation
1. Copy the DebugOnly folder into the "Assets" folder
2. Profit

## Testing
To test:
1. Run the scene in the Editor, all 3 cubes should be visible
2. Build and run the scene with development mode turned off, only the blue cube will be visible.
3. Build and run the scene with development mode turned ON, both the green and blue cube will be visible.

## Coming Soon
- More in depth and screenshots coming soon!
- CI builds
- Submodule
