using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TeknoParrotUi.Common;
using Keys = System.Windows.Forms.Keys;

namespace XOutput.Tools
{
    public class TeknoParrotUIConfigWriter
    {
        public TeknoParrotUIConfigWriter(string path)
        {
            foreach (var file in new DirectoryInfo(Path.Combine(path, @"GameProfiles")).GetFiles())
            {
                var gp = DeSerializeGameProfile(file.FullName);
            }
        }

        /// <summary>
        /// Deserializes GameProfile.xml to the class.
        /// </summary>
        /// <returns>Read Gameprofile class.</returns>
        private static GameProfile DeSerializeGameProfile(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            try
            {
                var serializer = new XmlSerializer(typeof(GameProfile));
                GameProfile profile;

                using (var reader = XmlReader.Create(fileName))
                {
                    profile = (GameProfile)serializer.Deserialize(reader);
                }

                if (profile.Is64Bit && !Environment.Is64BitOperatingSystem)
                {
                    Debug.WriteLine($"Skipping loading profile (64 bit profile on 32 bit OS) {fileName}");
                    return null;
                }

                // Add filename to profile
                profile.FileName = fileName;

                return profile;
            }
            catch (Exception e)
            {
                Debugger.Break();
                return null;
            }
        }
    }
}

namespace TeknoParrotUi.Common
{
    public enum InputApi
    {
        DirectInput,
        XInput,
        RawInput
    }

    [Serializable]
    [XmlRoot("GameProfile")]
    public class GameProfile
    {
        public string GameName { get; set; }
        public string GameGenre { get; set; }
        public string GamePath { get; set; }
        public string TestMenuParameter { get; set; }
        public bool TestMenuIsExecutable { get; set; }
        public string ExtraParameters { get; set; }
        public string TestMenuExtraParameters { get; set; }
        public string IconName { get; set; }
        public string ValidMd5 { get; set; }
        public bool ResetHint { get; set; }
        public string InvalidFiles { get; set; }
        public string Description { get; set; }
        [XmlIgnore]
        public Description GameInfo { get; set; }
        [XmlIgnore]
        public string FileName { get; set; }
        public List<FieldInformation> ConfigValues { get; set; }
        public List<JoystickButtons> JoystickButtons { get; set; }
        public EmulationProfile EmulationProfile { get; set; }
        public int GameProfileRevision { get; set; }
        public bool HasSeparateTestMode { get; set; }
        public bool Is64Bit { get; set; }
        public EmulatorType EmulatorType { get; set; }
        public bool Patreon { get; set; }
        public bool RequiresAdmin { get; set; }
        public int msysType { get; set; }
        public bool InvertedMouseAxis { get; set; }
        public bool GunGame { get; set; }
        public bool DevOnly { get; set; }
        public string ExecutableName { get; set; }
        // advanced users only!
        public string CustomArguments { get; set; }
        public short xAxisMin { get; set; } = 0;
        public short xAxisMax { get; set; } = 255;
        public short yAxisMin { get; set; } = 0;
        public short yAxisMax { get; set; } = 255;
    }

    public enum GPUSTATUS
    {
        NO_INFO,
        // no support at all
        NO,
        // runs fine
        OK,
        // requires fix from Discord
        WITH_FIX,
        // runs but with issues
        HAS_ISSUES
    }

    public class Description
    {
        public string platform;
        public string release_year;
        [JsonConverter(typeof(StringEnumConverter))]
        public GPUSTATUS nvidia;
        public string nvidia_issues;
        [JsonConverter(typeof(StringEnumConverter))]
        public GPUSTATUS amd;
        public string amd_issues;
        [JsonConverter(typeof(StringEnumConverter))]
        public GPUSTATUS intel;
        public string intel_issues;
        public string general_issues;

        public override string ToString()
        {
            var nvidiaIssues = !string.IsNullOrEmpty(nvidia_issues) ? nvidia_issues + "\n" : string.Empty;
            var amdIssues = !string.IsNullOrEmpty(amd_issues) ? amd_issues + "\n" : string.Empty;
            var intelIssues = !string.IsNullOrEmpty(intel_issues) ? intel_issues + "\n" : string.Empty;
            return $"Platform: {platform}\n" +
                $"Release year: {release_year}\n" +
                "GPU Support:\n" +
                $"NVIDIA: {nvidia.ToString().Replace('_', ' ')}\n" +
                $"{nvidiaIssues}" +
                $"AMD: {amd.ToString().Replace('_', ' ')}\n" +
                $"{amdIssues}" +
                $"Intel: {intel.ToString().Replace('_', ' ')}\n" +
                $"{intelIssues}" +
                $"{(!string.IsNullOrEmpty(general_issues) ? $"GENERAL ISSUES:\n{general_issues}" : "")}";
        }
    }

    public enum FieldType
    {
        Text = 0,
        Numeric = 1,
        Bool = 2,
        Dropdown = 3
    }
    public class FieldInformation
    {
        public string CategoryName { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public FieldType FieldType { get; set; }
        public List<string> FieldOptions { get; set; }
    }

    public enum InputMapping
    {
        Test,
        Service1,
        Service2,
        Coin1,
        Coin2,
        P1Button1,
        P1Button2,
        P1Button3,
        P1Button4,
        P1Button5,
        P1Button6,
        P1ButtonUp,
        P1ButtonDown,
        P1ButtonLeft,
        P1ButtonRight,
        P1ButtonStart,
        P2Button1,
        P2Button2,
        P2Button3,
        P2Button4,
        P2Button5,
        P2Button6,
        P2ButtonUp,
        P2ButtonDown,
        P2ButtonLeft,
        P2ButtonRight,
        P2ButtonStart,
        Analog0,
        Analog1,
        Analog2,
        Analog3,
        Analog4,
        Analog5,
        Analog6,
        Analog7,
        Analog8,
        Analog9,
        Analog10,
        Analog11,
        Analog12,
        Analog13,
        Analog14,
        Analog15,
        Analog16,
        Analog17,
        Analog18,
        Analog19,
        Analog20,
        SrcGearChange1,
        SrcGearChange2,
        SrcGearChange3,
        SrcGearChange4,
        ExtensionOne1,
        ExtensionOne2,
        ExtensionOne3,
        ExtensionOne4,
        ExtensionOne11,
        ExtensionOne12,
        ExtensionOne13,
        ExtensionOne14,
        ExtensionOne15,
        ExtensionOne16,
        ExtensionOne17,
        ExtensionOne18,
        ExtensionTwo1,
        ExtensionTwo2,
        ExtensionTwo3,
        ExtensionTwo4,
        ExtensionTwo11,
        ExtensionTwo12,
        ExtensionTwo13,
        ExtensionTwo14,
        ExtensionTwo15,
        ExtensionTwo16,
        ExtensionTwo17,
        ExtensionTwo18,
        Analog0Special1,
        Analog0Special2,
        Analog2Special1,
        Analog2Special2,
        Wmmt5GearChange1,
        Wmmt5GearChange2,
        Wmmt5GearChange3,
        Wmmt5GearChange4,
        Wmmt5GearChange5,
        Wmmt5GearChange6,
        Wmmt5GearChangeUp,
        Wmmt5GearChangeDown,
        SrcGearChangeUp,
        SrcGearChangeDown,
        JvsTwoTest,
        JvsTwoService1,
        JvsTwoService2,
        JvsTwoCoin1,
        JvsTwoCoin2,
        JvsTwoP1Button1,
        JvsTwoP1Button2,
        JvsTwoP1Button3,
        JvsTwoP1Button4,
        JvsTwoP1Button5,
        JvsTwoP1Button6,
        JvsTwoP1ButtonUp,
        JvsTwoP1ButtonDown,
        JvsTwoP1ButtonLeft,
        JvsTwoP1ButtonRight,
        JvsTwoP1ButtonStart,
        JvsTwoP2Button1,
        JvsTwoP2Button2,
        JvsTwoP2Button3,
        JvsTwoP2Button4,
        JvsTwoP2Button5,
        JvsTwoP2Button6,
        JvsTwoP2ButtonUp,
        JvsTwoP2ButtonDown,
        JvsTwoP2ButtonLeft,
        JvsTwoP2ButtonRight,
        JvsTwoP2ButtonStart,
        JvsTwoAnalog0,
        JvsTwoAnalog1,
        JvsTwoAnalog2,
        JvsTwoAnalog3,
        JvsTwoAnalog4,
        JvsTwoAnalog5,
        JvsTwoAnalog6,
        JvsTwoAnalog7,
        JvsTwoAnalog8,
        JvsTwoAnalog9,
        JvsTwoAnalog10,
        JvsTwoAnalog11,
        JvsTwoAnalog12,
        JvsTwoAnalog13,
        JvsTwoAnalog14,
        JvsTwoAnalog15,
        JvsTwoAnalog16,
        JvsTwoAnalog17,
        JvsTwoAnalog18,
        JvsTwoAnalog19,
        JvsTwoAnalog20,
        JvsTwoExtensionOne1,
        JvsTwoExtensionOne2,
        JvsTwoExtensionOne3,
        JvsTwoExtensionOne4,
        JvsTwoExtensionOne11,
        JvsTwoExtensionOne12,
        JvsTwoExtensionOne13,
        JvsTwoExtensionOne14,
        JvsTwoExtensionOne15,
        JvsTwoExtensionOne16,
        JvsTwoExtensionOne17,
        JvsTwoExtensionOne18,
        JvsTwoExtensionTwo1,
        JvsTwoExtensionTwo2,
        JvsTwoExtensionTwo3,
        JvsTwoExtensionTwo4,
        JvsTwoExtensionTwo11,
        JvsTwoExtensionTwo12,
        JvsTwoExtensionTwo13,
        JvsTwoExtensionTwo14,
        JvsTwoExtensionTwo15,
        JvsTwoExtensionTwo16,
        JvsTwoExtensionTwo17,
        JvsTwoExtensionTwo18,
        PokkenButtonUp,
        PokkenButtonDown,
        PokkenButtonLeft,
        PokkenButtonRight,
        PokkenButtonStart,
        PokkenButtonA,
        PokkenButtonB,
        PokkenButtonX,
        PokkenButtonY,
        PokkenButtonL,
        PokkenButtonR,
        P1LightGun,
        P2LightGun,
    }

    public enum AnalogType
    {
        None,
        Gas,
        Brake,
        SWThrottle,
        SWThrottleReverse,
        Wheel,
        AnalogJoystick,
        AnalogJoystickReverse,
        KeyboardWheelHalfValue, //Not needed anymore but removing this will delete user profile for everyone!!
        Minimum,
        Maximum
    }

    public enum RawMouseButton
    {
        None,
        LeftButton,
        RightButton,
        MiddleButton,
        Button4,
        Button5
    }

    public enum RawDeviceType
    {
        None,
        Mouse,
        Keyboard
    }

    [Serializable]
    public class JoystickButtons
    {
        public string ButtonName { get; set; }
        public JoystickButton DirectInputButton { get; set; }
        public XInputButton XInputButton { get; set; }
        public RawInputButton RawInputButton { get; set; }
        public InputMapping InputMapping { get; set; }
        public AnalogType AnalogType { get; set; }
        public string BindNameDi { get; set; }
        public string BindNameXi { get; set; }
        public string BindNameRi { get; set; }
        public string BindName { get; set; }
        public bool HideWithDirectInput { get; set; }
        public bool HideWithXInput { get; set; }
        public bool HideWithRawInput { get; set; }
        public bool HideWithKeyboardForAxis { get; set; }
        public bool HideWithoutKeyboardForAxis { get; set; }
        public bool HideWithRelativeAxis { get; set; }
        public bool HideWithoutRelativeAxis { get; set; }
    }

    [Serializable]
    public class JoystickButton
    {
        public int Button { get; set; }
        public bool IsAxis { get; set; }
        public bool IsAxisMinus { get; set; }
        public bool IsFullAxis { get; set; }
        public int PovDirection { get; set; }
        public bool IsReverseAxis { get; set; }
        public Guid JoystickGuid { get; set; }
    }

    public class XInputButton
    {
        public bool IsLeftThumbX { get; set; }
        public bool IsRightThumbX { get; set; }
        public bool IsLeftThumbY { get; set; }
        public bool IsRightThumbY { get; set; }
        public bool IsAxisMinus { get; set; }
        public bool IsLeftTrigger { get; set; }
        public bool IsRightTrigger { get; set; }
        public short ButtonCode { get; set; }
        public bool IsButton { get; set; }
        public int ButtonIndex { get; set; }
        public int XInputIndex { get; set; }
    }

    public class RawInputButton
    {
        public string DevicePath { get; set; }
        public RawDeviceType DeviceType { get; set; }
        public RawMouseButton MouseButton { get; set; }
        public Keys KeyboardKey { get; set; }
    }

    [Serializable]
    [XmlRoot("JoystickMapping")]
    public class JoystickMapping
    {
        public JoystickButtons Start { get; set; }
        public JoystickButtons Up { get; set; }
        public JoystickButtons Down { get; set; }
        public JoystickButtons Left { get; set; }
        public JoystickButtons Right { get; set; }
        public JoystickButtons Button1 { get; set; }
        public JoystickButtons Button2 { get; set; }
        public JoystickButtons Button3 { get; set; }
        public JoystickButtons Button4 { get; set; }
        public JoystickButtons Button5 { get; set; }
        public JoystickButtons Button6 { get; set; }
        public JoystickButtons Service { get; set; }
        public JoystickButtons Test { get; set; }
        public JoystickButtons GasAxis { get; set; }
        public JoystickButtons BrakeAxis { get; set; }
        public JoystickButtons WheelAxis { get; set; }
        public JoystickButtons SonicItem { get; set; }
        public JoystickButtons SrcViewChange1 { get; set; }
        public JoystickButtons SrcViewChange2 { get; set; }
        public JoystickButtons SrcViewChange3 { get; set; }
        public JoystickButtons SrcViewChange4 { get; set; }
        public JoystickButtons SrcGearChange1 { get; set; }
        public JoystickButtons SrcGearChange2 { get; set; }
        public JoystickButtons SrcGearChange3 { get; set; }
        public JoystickButtons SrcGearChange4 { get; set; }
        public JoystickButtons GunUp { get; set; }
        public JoystickButtons GunDown { get; set; }
        public JoystickButtons GunLeft { get; set; }
        public JoystickButtons GunRight { get; set; }
        public JoystickButtons GunTrigger { get; set; }
        public int GunMultiplier { get; set; }
        public JoystickButtons InitialD6ShiftDown { get; set; }
        public JoystickButtons InitialD6ShiftUp { get; set; }
        public JoystickButtons InitialD6ViewChange { get; set; }
        public JoystickButtons InitialD6MenuUp { get; set; }
        public JoystickButtons InitialD6MenuDown { get; set; }
        public JoystickButtons InitialD6MenuLeft { get; set; }
        public JoystickButtons InitialD6MenuRight { get; set; }
        public JoystickButtons MachStormAxisX { get; set; }
        public JoystickButtons MachStormAxisY { get; set; }
        public JoystickButtons MachStormThrottle { get; set; }
        public JoystickButtons MachStormMachineGun { get; set; }
        public JoystickButtons MachStormMissiles { get; set; }
        public JoystickButtons ShiningCrossX { get; set; }
        public JoystickButtons ShiningCrossY { get; set; }
        public JoystickButtons ShiningCrossJump { get; set; }
        public JoystickButtons ShiningCrossAttack { get; set; }
        public JoystickButtons ShiningCrossForce { get; set; }
        public JoystickButtons ShiningCrossView { get; set; }
        public JoystickButtons ShiningCrossUse { get; set; }
        public JoystickButtons HandBrake { get; set; }
    }

    public enum EmulationProfile
    {
        TaitoTypeXGeneric,
        SegaJvs,
        SegaRacingClassic,
        SegaJvsGoldenGun,
        SegaJvsLetsGoIsland,
        SegaJvsDreamRaiders,
        NamcoPokken,
        NamcoMkdx,
        ProjectDivaNu,
        EuropaRFordRacing,
        EuropaRSegaRally3,
        FastIo,
        NamcoMachStorm,
        TaitoTypeXBattleGear,
        WackyRaces,
        VirtuaRLimit,
        ShiningForceCrossRaid,
        SegaSonicAllStarsRacing,
        BorderBreak,
        SegaInitialD,
        SegaInitialDLindbergh,
        SegaRTuned,
        ChaseHq2,
        NamcoWmmt5,
        Outrun2SPX,
        AfterBurnerClimax,
        Vt3Lindbergh,
        SegaRtv,
        VirtuaTennis4,
        DevThing1,
        ArcadeLove,
        LGS,
        GtiClub3,
        ExBoard,
        Daytona3,
        Hotd4,
        Vf5Lindbergh,
        Mballblitz,
        GRID,
        Vf5cLindbergh,
        SegaJvsLetsGoJungle,
        CSNEO,
        GuiltyGearRE2,
        Rambo,
        TooSpicy,
        RawThrillsFNF,
        LuigisMansion,
        Theatrhythm,
        RawThrillsFNFH2O,
        LostLandAdventuresPAL,
        GSEVO,
        SegaToolsIDZ,
        TokyoCop,
        StarTrekVoyager,
        RingRiders,
        AliensExtermination,
        FarCry,
        //DO NOT USE, ONLY FOR MIGRATIONS!
        FNFDrift,
        GHA,
    }

    public enum EmulatorType
    {
        //Open Source
        OpenParrot,
        //Lindbergh (Linux/BudgieLoader)
        Lindbergh,
        //Other
        TeknoParrot,
        //System N2 (Linux/special BudgieLoader)
        N2,
        //Open Source Konami
        OpenParrotKonami,
        //SegaTools
        SegaTools,
    }
}

