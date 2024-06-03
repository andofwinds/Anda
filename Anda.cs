using Anda.AndaFramework;
using AndaFramework.Logging;

namespace Anda;

public class Anda
{
    public static void Main(string[] args)
    {
        Console.WriteLine("----- Anda bootloader -----");
        Logger.Log("LOADER", $"Found Anda Framework: {Metadata._ver}");
        
        using (AndaWindowRuntime andaWindowRuntime = new AndaWindowRuntime(1600, 900, "Anda"))
        {
            Logger.Log("LOADER", $"Bootloader completed. Loading runtime window: {andaWindowRuntime.Size.X}x{andaWindowRuntime.Size.Y}, {andaWindowRuntime.Title}");
            Console.WriteLine("----- End bootloader -----");
            andaWindowRuntime.Run();
        }
    }
}