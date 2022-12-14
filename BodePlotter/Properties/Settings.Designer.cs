//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BodePlotter.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MeasurementDevice {
            get {
                return ((string)(this["MeasurementDevice"]));
            }
            set {
                this["MeasurementDevice"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SourceDevice {
            get {
                return ((string)(this["SourceDevice"]));
            }
            set {
                this["SourceDevice"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int MeasurementRange {
            get {
                return ((int)(this["MeasurementRange"]));
            }
            set {
                this["MeasurementRange"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public uint StartFrequency {
            get {
                return ((uint)(this["StartFrequency"]));
            }
            set {
                this["StartFrequency"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("22000")]
        public uint StopFrequency {
            get {
                return ((uint)(this["StopFrequency"]));
            }
            set {
                this["StopFrequency"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public uint NumPoints {
            get {
                return ((uint)(this["NumPoints"]));
            }
            set {
                this["NumPoints"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.5")]
        public double ReferenceVoltage {
            get {
                return ((double)(this["ReferenceVoltage"]));
            }
            set {
                this["ReferenceVoltage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public uint OffsetCalculationTestFrequency {
            get {
                return ((uint)(this["OffsetCalculationTestFrequency"]));
            }
            set {
                this["OffsetCalculationTestFrequency"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double Offset {
            get {
                return ((double)(this["Offset"]));
            }
            set {
                this["Offset"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Ref")]
        public string RefPlotLabel {
            get {
                return ((string)(this["RefPlotLabel"]));
            }
            set {
                this["RefPlotLabel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Actual")]
        public string ActualPlotLabel {
            get {
                return ((string)(this["ActualPlotLabel"]));
            }
            set {
                this["ActualPlotLabel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FF4336")]
        public string RefPlotColor {
            get {
                return ((string)(this["RefPlotColor"]));
            }
            set {
                this["RefPlotColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#2195F2")]
        public string ActualPlotColor {
            get {
                return ((string)(this["ActualPlotColor"]));
            }
            set {
                this["ActualPlotColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Segoe UI, 12pt")]
        public string ChartFont {
            get {
                return ((string)(this["ChartFont"]));
            }
            set {
                this["ChartFont"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#000000")]
        public string ChartFontColor {
            get {
                return ((string)(this["ChartFontColor"]));
            }
            set {
                this["ChartFontColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#F0F0F0")]
        public string ChartGridColor {
            get {
                return ((string)(this["ChartGridColor"]));
            }
            set {
                this["ChartGridColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FFFFFF")]
        public string ChartBackgroundColor {
            get {
                return ((string)(this["ChartBackgroundColor"]));
            }
            set {
                this["ChartBackgroundColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("88")]
        public uint ChartFillOpacity {
            get {
                return ((uint)(this["ChartFillOpacity"]));
            }
            set {
                this["ChartFillOpacity"] = value;
            }
        }
    }
}
