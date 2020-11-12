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
        public static void WriteConfig(string fbPath, List<FinalBurnInputSwitches> inputSwitches, List<DirectDevice> devices)
        {
            #region cps.ini
            var sb = new StringBuilder();
            sb.AppendLine("FinalBurn Neo - Hardware Default Preset");
            sb.AppendLine();
            sb.AppendLine("CPS-1/CPS-2/CPS-3 hardware");
            sb.AppendLine();
            sb.AppendLine("version 0x100000");
            sb.AppendLine();

            foreach (var device in devices)
            {
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
            sb.AppendLine("");
            sb.AppendLine("input  \"Reset\"            switch 0x3D");
            sb.AppendLine("input  \"Diagnostic\"       switch 0x3C");
            sb.AppendLine("input  \"Service\"          switch 0x3E");

            File.WriteAllText(Path.Combine(fbPath, @"config\presets\cps.ini"), sb.ToString());
            #endregion

            #region neogeo.ini
            sb.Clear();
            sb.AppendLine("FinalBurn Neo - Hardware Default Preset");
            sb.AppendLine();
            sb.AppendLine("Neo-Geo hardware");
            sb.AppendLine();
            sb.AppendLine("version 0x100000");
            sb.AppendLine();

            foreach (var device in devices)
            {
                var fbis = inputSwitches.Where(i => i.WindowsIndex == device.WindowsIndex).SingleOrDefault();
                if (fbis == null)
                    continue;

                sb.AppendLine($"input  \"P{device.PlayerIndex} Coin\"          switch 0x{fbis.Coin}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Start\"         switch 0x{fbis.Start}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Select\"        switch 0x{fbis.Select}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Down\"          switch 0x{fbis.Down}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Left\"          switch 0x{fbis.Left}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Right\"         switch 0x{fbis.Right}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button A\"      switch 0x{fbis.A}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button B\"      switch 0x{fbis.B}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button C\"      switch 0x{fbis.C}");
                sb.AppendLine($"input  \"P{device.PlayerIndex} Button D\"      switch 0x{fbis.D}");
            }
            sb.AppendLine("");
            sb.AppendLine("input  \"Reset\"            switch 0x3D");
            sb.AppendLine("input  \"Test\"             switch 0x3C");
            sb.AppendLine("input  \"Service\"          switch 0x0A");

            File.WriteAllText(Path.Combine(fbPath, @"config\presets\neogeo.ini"), sb.ToString());
            #endregion
        }
    }
}
