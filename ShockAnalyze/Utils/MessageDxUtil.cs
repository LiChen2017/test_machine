using System.Windows.Forms;

namespace ShockAnalyze
{
    public class MessageDxUtil
    {
        /// 显示一般的提示信息
        /// 提示信息
        public static DialogResult ShowTips(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Tip Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// 显示警告信息
        /// 警告信息
        public static DialogResult ShowWarning(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// 显示错误信息
        /// 错误信息
        public static DialogResult ShowError(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// 显示询问用户信息，并显示错误标志
        /// 错误信息
        public static DialogResult ShowYesNoAndError(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Error Message", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        /// 显示询问用户信息，并显示提示标志
        /// 错误信息
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Tip Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        /// 显示询问用户信息，并显示警告标志
        /// 警告信息
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        /// 显示询问用户信息，并显示提示标志
        /// 错误信息
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "Tip Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }
    }
}

