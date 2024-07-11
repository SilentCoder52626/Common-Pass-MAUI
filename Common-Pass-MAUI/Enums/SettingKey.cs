using System.ComponentModel.DataAnnotations;

namespace Common_Pass_MAUI.Enums
{
    public enum SettingKey
    {
        [Display(Name = "Encryption Key")]
        EncryptionKey,
        [Display(Name = "Pin")]
        ExportPin
    }
}
