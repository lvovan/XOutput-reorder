using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOutput.Devices.Input.DirectInput;

namespace XOutput.Tools
{
    /// <summary>
    /// Defines the final burn "switch" value for a given Windows joystick index
    /// </summary>
    public class FinalBurnInputSwitches
    {
        public int WindowsIndex { get; set; }
        public string Coin { get; set; }
        public string Start { get; set; }
        public string Up { get; set; }
        public string Down { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
        public string LP { get; set; }
        public string MP { get; set; }
        public string HP { get; set; }
        public string LK { get; set; }
        public string MK { get; set; }
        public string HK { get; set; }

        public string Select { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
    }

    public static class FinalBurnConfigWriter
    {
        public static void WriteConfig(string fbPath, string fbVersion, List<FinalBurnInputSwitches> inputSwitches, List<DirectDevice> devices)
        {
            #region cps.ini
            var sb = new StringBuilder();
            sb.AppendLine("FinalBurn Neo - Hardware Default Preset");
            sb.AppendLine();
            sb.AppendLine("CPS-1/CPS-2/CPS-3 hardware");
            sb.AppendLine();
            sb.AppendLine($"version {fbVersion}");
            sb.AppendLine();

            foreach (var device in devices)
            {
                if (device.PlayerIndex == 0)
                    continue;

                var fbis = inputSwitches.Where(i => i.WindowsIndex == device.WindowsIndex).SingleOrDefault();
                if (fbis == null)
                    continue;

                sb.AppendLine($"input  \"P{device.PlayerIndex} Coin\"          switch {fbis.Coin}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Start\"         switch {fbis.Start}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Up\"            switch {fbis.Up}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Down\"          switch {fbis.Down}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Left\"          switch {fbis.Left}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Right\"         switch {fbis.Right}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Weak Punch\"    switch {fbis.LP}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Medium Punch\"  switch {fbis.MP}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Strong Punch\"  switch {fbis.HP}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Weak Kick\"     switch {fbis.LK}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Medium Kick\"   switch {fbis.MK}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Strong Kick\"   switch {fbis.HK}");
            }
            sb.AppendLine("input  \"Reset\"            switch 0x3D");
            sb.AppendLine("input  \"Diagnostic\"       switch 0x3C");
            sb.AppendLine("input  \"Service\"          switch 0x3E");
            sb.AppendLine("input  \"Volume Up\"        switch undefined");
            sb.AppendLine("input  \"Volume Down\"      switch undefined");
            sb.AppendLine();
            var keysCps = new List<string>
            {
                "System Pause", "System FFWD", "System Load State", "System Save State","System UNDO State",
                "P1 3x Punch", "P1 3x Kick", "P2 3x Punch", "P2 3x Kick"
            };
            for (int p = 1; p <= 2; p++)
                for (int n = 1; n <= 8; n++)
                    keysCps.Add($"P{p} Auto-Fire Button {n}");

            foreach (var key in keysCps)
                sb.AppendLine($"macro \"{key}\" undefined");

            File.WriteAllText(Path.Combine(fbPath, @"config\presets\cps.ini"), sb.ToString());
            #endregion

            #region neogeo.ini
            sb.Clear();
            sb.AppendLine("FinalBurn Neo - Hardware Default Preset");
            sb.AppendLine();
            sb.AppendLine("Neo-Geo hardware");
            sb.AppendLine();
            sb.AppendLine($"version {fbVersion}");
            sb.AppendLine();

            foreach (var device in devices)
            {
                var fbis = inputSwitches.Where(i => i.WindowsIndex == device.WindowsIndex).SingleOrDefault();
                if (fbis == null)
                    continue;

                sb.AppendLine($"input  \"P{device.PlayerIndex} Coin\"          switch {fbis.Coin}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Start\"         switch {fbis.Start}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Select\"        switch {fbis.Select}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Up\"            switch {fbis.Up}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Down\"          switch {fbis.Down}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Left\"          switch {fbis.Left}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Right\"         switch {fbis.Right}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button A\"      switch {fbis.A}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button B\"      switch {fbis.B}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button C\"      switch {fbis.C}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button D\"      switch {fbis.D}");
            }
            sb.AppendLine("input  \"Reset\"        switch 0x3D");
            sb.AppendLine("input  \"Test\"         switch 0x3C");
            sb.AppendLine("input  \"Dip 1\"        constant 0x00");
            sb.AppendLine("input  \"Dip 2\"        constant 0x00");
            sb.AppendLine("input  \"System\"       constant 0x00");
            sb.AppendLine("input  \"Slots\"        constant 0x00");
            sb.AppendLine("input  \"Debug Dip 1\"  constant 0x00");
            sb.AppendLine("input  \"Debut Dip 2\"  constant 0x00");
            sb.AppendLine();

            var keysNeo = new List<string>
            {
                "System Pause", "System FFWD", "System Load State", "System Save State","System UNDO State",
            };

            for (int p = 1; p <= 2; p++)
            {
                for (int n = 1; n <= 4; n++)
                    keysCps.Add($"P{p} Auto-Fire Button {n}");
                keysCps.Add($"P{p} Buttons AB");
                keysCps.Add($"P{p} Buttons AC");
                keysCps.Add($"P{p} Buttons AD");
                keysCps.Add($"P{p} Buttons BC");
                keysCps.Add($"P{p} Buttons BD");
                keysCps.Add($"P{p} Buttons CD");
                keysCps.Add($"P{p} Buttons ABC");
                keysCps.Add($"P{p} Buttons ABD");
                keysCps.Add($"P{p} Buttons ACD");
                keysCps.Add($"P{p} Buttons BCD");
                keysCps.Add($"P{p} Buttons ABCD");
            }

            File.WriteAllText(Path.Combine(fbPath, @"config\presets\neogeo.ini"), sb.ToString());
            #endregion

            // Delete game-specific ini files
            foreach (var file in new DirectoryInfo(Path.Combine(fbPath, @"config\games")).GetFiles())
            {
                file.Delete();
            }

        }
    }
}
