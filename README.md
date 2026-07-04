# bonk

Tiny Windows notification-sound executable built with .NET 10 Native AOT.

## Install

Release assets are unpacked, self-contained Windows executables.

Windows, PowerShell:

```powershell
$repo='flcl42/bonk'; $dir='C:\Programs'; New-Item -ItemType Directory -Force $dir | Out-Null; Invoke-WebRequest "https://github.com/$repo/releases/latest/download/bonk-windows-x64.exe" -OutFile "$dir\bonk.exe"; $p=[Environment]::GetEnvironmentVariable('Path','User'); if (($p -split ';') -notcontains $dir) { [Environment]::SetEnvironmentVariable('Path', ((@($p -split ';') + $dir | Where-Object { $_ }) -join ';'), 'User'); $env:Path += ";$dir" }
```

## Usage

```powershell
bonk
bonk 0
bonk 1
bonk 30
```

`bonk` with no arguments plays melody `0`, the original current sound. Pass a number from `0` through `30` to choose a different embedded melody.

If an invalid index is passed, details are written to `%TEMP%\bonk-error.txt`.

## Build

```powershell
dotnet publish -c Release
```

The project defaults `PublishDir` to `C:\Programs\`, so local publish output is `C:\Programs\bonk.exe`.

## Release

Tagged commits build and publish this raw executable asset:

- `bonk-windows-x64.exe`

Push a tag such as `v1.1.0` or `release/1.1.0` to create a GitHub release.

## Melodies

The embedded MP3s are the 31 items from Universfield's Pixabay Notifications collection:

https://pixabay.com/collections/notifications-33322938/

`Resources/melodies.json` records the index, title, source page, CDN source, byte size, and duration for each embedded MP3. The sounds are published by Universfield on Pixabay under the Pixabay Content License:

https://pixabay.com/service/license-summary/

## License

Code is MIT. Embedded MP3 assets are from Pixabay and remain under the Pixabay Content License.
