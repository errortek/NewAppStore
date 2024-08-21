using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using CommunityToolkit.WinUI.Notifications;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NewAppStore.Views.AppsPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CertificateInstallerForMSIX : Page
    {
        public CertificateInstallerForMSIX()
        {
            this.InitializeComponent();
        }

        public List<string> Pictures = new List<string>()
        {
                        // CURRENTLY USES PLACEHOLDER LINKS!! CHANGE AFTER TESTING IS COMPLETE!!

            "https://github.com/jpbandroid/AppStore-Resources/blob/main/IviriusTextEditorFree/Screenshot%202024-04-29%20070238.png?raw=true",
            "https://github.com/jpbandroid/AppStore-Resources/blob/main/IviriusTextEditorFree/Screenshot%202024-04-29%20070256.png?raw=true",
            "https://github.com/jpbandroid/AppStore-Resources/blob/main/IviriusTextEditorFree/Screenshot%202024-04-29%20070311.png?raw=true",
            "https://github.com/jpbandroid/AppStore-Resources/blob/main/IviriusTextEditorFree/Screenshot%202024-04-29%20070330.png?raw=true"
        };

        private async void download(object sender, RoutedEventArgs e)
        {
            // CURRENTLY USES PLACEHOLDER LINKS!! CHANGE AFTER TESTING IS COMPLETE!!
            string name = Environment.UserName;
            using var client = new HttpClient();
            using var s = await client.GetStreamAsync("https://github.com/IviriusMain/Ivirius-Text-Editor/releases/download/3.0.3/Ivirius.Text.Editor_3.0.3.0_x86_x64_arm_arm64.msixbundle");
            using var fs = new FileStream("C:\\Users\\" + name + "\\Downloads\\IviriusTextEditorFree.msixbundle", FileMode.OpenOrCreate);
            await s.CopyToAsync(fs);
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Certificate Installer for MSIX download complete")
                .AddText("Take a look!")
                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater

        }
    }
}
