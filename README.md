# bonk

Tiny cross-platform notification-sound executable built with .NET 10 Native AOT.

## Install

Release assets are unpacked, self-contained executables.

Linux, bash:

```bash
repo=flcl42/bonk; arch="$(uname -m)"; asset=bonk-linux-x64; case "$arch" in aarch64|arm64) asset=bonk-linux-arm64;; esac; curl -fsSL "https://github.com/$repo/releases/latest/download/$asset" -o ./bonk; chmod +x ./bonk
```

macOS, zsh:

```zsh
repo=flcl42/bonk; arch="$(uname -m)"; asset=bonk-macos-arm64; [ "$arch" = "x86_64" ] && asset=bonk-macos-x64; curl -fsSL "https://github.com/$repo/releases/latest/download/$asset" -o ./bonk; chmod +x ./bonk
```

Windows, PowerShell:

```powershell
$repo='flcl42/bonk'; Invoke-WebRequest "https://github.com/$repo/releases/latest/download/bonk-windows-x64.exe" -OutFile ".\bonk.exe"
```

## Usage

```powershell
bonk
bonk 0
bonk 1
bonk 30
```

`bonk` with no arguments plays melody `0`, the original current sound. Pass a number from `0` through `30` to choose a different embedded melody.

If an invalid index is passed, details are written to `bonk-error.txt` in the
system temp directory.

Playback uses Windows MCI on Windows, `afplay` on macOS, and the first available
Linux player from `ffplay`, `mpg123`, `mpv`, or `cvlc`.

## Build

```powershell
dotnet publish -c Release -r win-x64 -o .
```

Local publish output is `.\bonk.exe`.

## Release

Tagged commits build and publish these raw executable assets:

- `bonk-linux-x64`
- `bonk-linux-arm64`
- `bonk-macos-x64`
- `bonk-macos-arm64`
- `bonk-windows-x64.exe`
- `bonk-windows-arm64.exe`

Push a tag such as `v1.2.0` or `release/1.2.0` to create a GitHub release.

## Melodies

The embedded MP3s are the 31 items from Universfield's Pixabay Notifications collection:

https://pixabay.com/collections/notifications-33322938/

`Resources/melodies.json` records the index, title, source page, CDN source, byte size, and duration for each embedded MP3. The sounds are published by Universfield on Pixabay under the Pixabay Content License:

https://pixabay.com/service/license-summary/

## License

Code is MIT. Embedded MP3 assets are from Pixabay and remain under the Pixabay Content License.
