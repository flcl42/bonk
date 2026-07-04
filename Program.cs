using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

internal static class Program
{
    private static readonly Melody[] Melodies =
    [
        new(0, "New Notification 051", "00-new-notification-051-494246.mp3", "https://pixabay.com/sound-effects/technology-new-notification-051-494246/"),
        new(1, "Clear Bell Chime", "01-clear-bell-chime-487898.mp3", "https://pixabay.com/sound-effects/film-special-effects-clear-bell-chime-487898/"),
        new(2, "New Notification 059", "02-new-notification-059-494262.mp3", "https://pixabay.com/sound-effects/technology-new-notification-059-494262/"),
        new(3, "New Notification 039", "03-new-notification-039-493472.mp3", "https://pixabay.com/sound-effects/technology-new-notification-039-493472/"),
        new(4, "New Notification 064", "04-new-notification-064-494547.mp3", "https://pixabay.com/sound-effects/technology-new-notification-064-494547/"),
        new(5, "New Notification 050", "05-new-notification-050-494248.mp3", "https://pixabay.com/sound-effects/city-new-notification-050-494248/"),
        new(6, "New Notification 041", "06-new-notification-041-493473.mp3", "https://pixabay.com/sound-effects/technology-new-notification-041-493473/"),
        new(7, "New Notification 061", "07-new-notification-061-494263.mp3", "https://pixabay.com/sound-effects/technology-new-notification-061-494263/"),
        new(8, "New Notification 042", "08-new-notification-042-493470.mp3", "https://pixabay.com/sound-effects/technology-new-notification-042-493470/"),
        new(9, "New Notification 055", "09-new-notification-055-494261.mp3", "https://pixabay.com/sound-effects/technology-new-notification-055-494261/"),
        new(10, "New Notification 058", "10-new-notification-058-494258.mp3", "https://pixabay.com/sound-effects/technology-new-notification-058-494258/"),
        new(11, "New Notification 040", "11-new-notification-040-493469.mp3", "https://pixabay.com/sound-effects/technology-new-notification-040-493469/"),
        new(12, "New Notification 056", "12-new-notification-056-494256.mp3", "https://pixabay.com/sound-effects/technology-new-notification-056-494256/"),
        new(13, "New Notification 054", "13-new-notification-054-494259.mp3", "https://pixabay.com/sound-effects/technology-new-notification-054-494259/"),
        new(14, "New Notification 047", "14-new-notification-047-494238.mp3", "https://pixabay.com/sound-effects/technology-new-notification-047-494238/"),
        new(15, "New Notification 044", "15-new-notification-044-494239.mp3", "https://pixabay.com/sound-effects/technology-new-notification-044-494239/"),
        new(16, "New Notification 060", "16-new-notification-060-494264.mp3", "https://pixabay.com/sound-effects/technology-new-notification-060-494264/"),
        new(17, "New Notification 046", "17-new-notification-046-494237.mp3", "https://pixabay.com/sound-effects/technology-new-notification-046-494237/"),
        new(18, "New Notification 045", "18-new-notification-045-494236.mp3", "https://pixabay.com/sound-effects/new-notification-045-494236/"),
        new(19, "New Notification 053", "19-new-notification-053-494247.mp3", "https://pixabay.com/sound-effects/technology-new-notification-053-494247/"),
        new(20, "New Notification 052", "20-new-notification-052-494250.mp3", "https://pixabay.com/sound-effects/technology-new-notification-052-494250/"),
        new(21, "New Notification 057", "21-new-notification-057-494255.mp3", "https://pixabay.com/sound-effects/technology-new-notification-057-494255/"),
        new(22, "New Notification 062", "22-new-notification-062-494544.mp3", "https://pixabay.com/sound-effects/technology-new-notification-062-494544/"),
        new(23, "New Notification 023", "23-new-notification-023-494260.mp3", "https://pixabay.com/sound-effects/technology-new-notification-023-494260/"),
        new(24, "New Notification 038", "24-new-notification-038-487899.mp3", "https://pixabay.com/sound-effects/technology-new-notification-038-487899/"),
        new(25, "New Notification 065", "25-new-notification-065-494546.mp3", "https://pixabay.com/sound-effects/technology-new-notification-065-494546/"),
        new(26, "New Notification 066", "26-new-notification-066-494545.mp3", "https://pixabay.com/sound-effects/technology-new-notification-066-494545/"),
        new(27, "New Notification 049", "27-new-notification-049-494249.mp3", "https://pixabay.com/sound-effects/technology-new-notification-049-494249/"),
        new(28, "New Notification 048", "28-new-notification-048-494235.mp3", "https://pixabay.com/sound-effects/technology-new-notification-048-494235/"),
        new(29, "New Notification 063", "29-new-notification-063-494543.mp3", "https://pixabay.com/sound-effects/technology-new-notification-063-494543/"),
        new(30, "New Notification 043", "30-new-notification-043-493471.mp3", "https://pixabay.com/sound-effects/technology-new-notification-043-493471/"),
    ];

    [STAThread]
    private static int Main(string[] args)
    {
        Melody melody;

        try
        {
            melody = SelectMelody(args);
        }
        catch (Exception ex)
        {
            WriteError(ex);
            return 1;
        }

        string tempPath = Path.Combine(Path.GetTempPath(), $"bonk-{melody.Index}-{Environment.ProcessId}-{Guid.NewGuid():N}.mp3");

        try
        {
            ExtractResource(tempPath, melody.ResourceName);
            PlayMp3(tempPath);
            return 0;
        }
        catch (Exception ex)
        {
            WriteError(ex);
            return 1;
        }
        finally
        {
            TryDelete(tempPath);
        }
    }

    private static Melody SelectMelody(string[] args)
    {
        if (args.Length == 0)
        {
            return Melodies[0];
        }

        if (args.Length != 1)
        {
            throw new ArgumentException($"Usage: bonk [0-{Melodies.Length - 1}]");
        }

        if (!int.TryParse(args[0], NumberStyles.None, CultureInfo.InvariantCulture, out int index) ||
            (uint)index >= (uint)Melodies.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(args), args[0], $"Melody must be a number from 0 to {Melodies.Length - 1}.");
        }

        return Melodies[index];
    }

    private static void ExtractResource(string path, string resourceName)
    {
        using Stream? resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

        if (resource is null)
        {
            throw new InvalidOperationException($"Missing embedded resource: {resourceName}");
        }

        using FileStream file = File.Create(path);
        resource.CopyTo(file);
    }

    private static void PlayMp3(string path)
    {
        string alias = $"bonk{Environment.ProcessId}{Guid.NewGuid():N}";

        try
        {
            RunMci($"""open "{path}" type mpegvideo alias {alias}""");
            RunMci($"play {alias} wait");
        }
        finally
        {
            _ = mciSendString($"close {alias}", null, 0, IntPtr.Zero);
        }
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            // Best effort cleanup only.
        }
    }

    private static void WriteError(Exception ex)
    {
        StringBuilder message = new();
        message.AppendLine(ex.ToString());
        message.AppendLine();
        message.AppendLine($"Usage: bonk [0-{Melodies.Length - 1}]");
        message.AppendLine();
        message.AppendLine("Available melodies:");

        foreach (Melody melody in Melodies)
        {
            message.Append(CultureInfo.InvariantCulture, $"{melody.Index}: {melody.Title}");
            message.AppendLine();
        }

        File.WriteAllText(Path.Combine(Path.GetTempPath(), "bonk-error.txt"), message.ToString());
    }

    private static void RunMci(string command)
    {
        int result = mciSendString(command, null, 0, IntPtr.Zero);

        if (result == 0)
        {
            return;
        }

        StringBuilder message = new(256);

        if (!mciGetErrorString(result, message, message.Capacity))
        {
            message.Append("Unknown MCI error.");
        }

        throw new InvalidOperationException($"{message} Command: {command}");
    }

    [DllImport("winmm.dll", EntryPoint = "mciSendStringW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    private static extern int mciSendString(string command, StringBuilder? returnValue, int returnLength, IntPtr callback);

    [DllImport("winmm.dll", EntryPoint = "mciGetErrorStringW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool mciGetErrorString(int error, StringBuilder errorText, int errorTextLength);

    private sealed record Melody(int Index, string Title, string ResourceName, string SourceUrl);
}
