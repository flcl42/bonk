# bonk

Tiny Windows notification-sound executable built with .NET 10 Native AOT.

## Usage

Run `bonk` to play melody `0`.

Run `bonk <number>` to choose a different embedded melody:

```powershell
bonk 1
bonk 30
```

Valid indexes are `0` through `30`. If an invalid index is passed, details are written to `%TEMP%\bonk-error.txt`.

## Build

```powershell
dotnet publish -c Release
```

The project defaults `PublishDir` to `C:\Programs\`, so the publish output is `C:\Programs\bonk.exe`.

## Melodies

The embedded MP3s are the 31 items from Universfield's Pixabay Notifications collection:

https://pixabay.com/collections/notifications-33322938/

`Resources/melodies.json` records the index, title, source page, CDN source, byte size, and duration for each embedded MP3. The sounds are published by Universfield on Pixabay under the Pixabay Content License:

https://pixabay.com/service/license-summary/
