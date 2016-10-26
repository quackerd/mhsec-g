using System;
using System.Windows;

namespace MHSEC_G
{
    public class BugCheck
    {
        public enum ErrorCode
        {
            // Facility Model
            MODEL_INVALID_FILE_SIZE = 0x1,
            MODEL_READ_BYTE_OVERFLOW = 0x2,
            MODEL_WRITE_BYTE_OVERFLOW = 0x3,
            MODEL_READ_UINT16_OVERFLOW = 0x4,
            MODEL_WRITE_UINT16_OVERFLOW = 0x5,
            MODEL_READ_UINT32_OVERFLOW = 0x6,
            MODEL_WRITE_UINT32_OVERFLOW = 0x7,
            MODEL_WRITE_UNICODE_OVERFLOW = 0x8,

            // Faciltiy ViewModel
            VIEWMODEL_NULL_SAVE = 0x11,

            // Faciltiy Item
            ITEM_NO_CORRESPONDENCE = 0x21,
            ITEM_MAPPING_CORRUPTED = 0x22,

            // Facility Monster
            MON_GENE_MAPPING_CORRUPTED = 0x31,
            MON_GENE_IDX_OVERFLOW = 0x32,
        }
        public static void bug_check(ErrorCode error_code, string error_message)
        {
            MessageBoxResult result = MessageBox.Show("Error code: 0x" + ((int)error_code).ToString("X4") + "\nMessage: " + error_message, "MHSEC-G Bug Check",
                MessageBoxButton.OK, MessageBoxImage.Stop);
            if (result == MessageBoxResult.OK)
            {
                System.Windows.Forms.Application.Exit();
            }
            Environment.Exit((int)error_code);
        }
    }
}
