using System;
using Ext.Net;

namespace Web.Core.Framework
{
    public class Dialog
    {
        // constant variable
        private const string DefaultErrorTitle = @"Thông báo lỗi";
        private const string DefaultNotificationTitle = @"Thông báo từ hệ thống";
        private const string DefaultAlertTitle = @"Cảnh báo";
        private const int DefaultNotificationWidth = 450;
        private const int DefaultNotificationHeight = 200;

        /// <summary>
        /// Show notification dialog at bottom right conner
        /// </summary>
        /// <param name="content">notification content</param>
        public static void ShowNotification(string content)
        {
            ShowNotification(DefaultNotificationTitle, content);
        }

        /// <summary>
        /// Show notification dialog at bottom right conner
        /// </summary>
        /// <param name="title">notitication title</param>
        /// <param name="content">notification content</param>
        public static void ShowNotification(string title, string content)
        {
            Notification.Show(new NotificationConfig()
            {
                Title = title,
                Icon = Icon.Information,
                Html = content,
                AutoHide = true,
                HideDelay = 2000,
                AlignCfg = new NotificationAlignConfig()
                {
                    ElementAnchor = AnchorPoint.BottomRight
                }
            });
        }

        /// <summary>
        /// Show error message box in the center of the screen
        /// </summary>
        /// <param name="content">error message</param>
        public static void ShowError(string content)
        {
            ShowError(DefaultErrorTitle, content);
        }

        /// <summary>
        /// Show exception messeage box
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowError(Exception ex)
        {
            ShowError("Exception", ex.Message);
        }

        /// <summary>
        /// Show error message box in the center of the screen
        /// </summary>
        /// <param name="title">error title</param>
        /// <param name="content">error message</param>
        public static void ShowError(string title, string content)
        {
            ShowError(title, content, DefaultNotificationWidth, DefaultNotificationHeight);
        }

        /// <summary>
        /// Show error message box in the center of the screen
        /// </summary>
        /// <param name="title">error title</param>
        /// <param name="content">error message</param>
        /// <param name="width">message box with</param>
        /// <param name="height">message box height</param>
        public static void ShowError(string title, string content, int width, int height)
        {
            // show notitication
            Notification.Show(new NotificationConfig
            {
                Title = title,
                AlignCfg = new NotificationAlignConfig()
                {
                    ElementAnchor = AnchorPoint.Center,
                    TargetAnchor = AnchorPoint.Center
                },
                Width = width,
                Height = height,
                Icon = Icon.Information,
                AutoHide = false,
                CloseVisible = true,
                Html = content,
                Modal = true
            });
        }

        /// <summary>
        /// Show alert message box in the center of the screen
        /// </summary>
        /// <param name="content">alert content</param>
        public static void Alert(string content)
        {
            ExtNet.MessageBox.Alert(DefaultAlertTitle, content).Show();
        }

        /// <summary>
        /// Show alert message box in the center of the screen
        /// </summary>
        /// <param name="title">alert title</param>
        /// <param name="content">alert content</param>
        public static void Alert(string title, string content)
        {
            ExtNet.MessageBox.Alert(title, content).Show();
        }
    }
}
