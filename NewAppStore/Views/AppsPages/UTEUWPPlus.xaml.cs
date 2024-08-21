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
    public sealed partial class UTEUWPPlus : Page
    {
        public UTEUWPPlus()
        {
            this.InitializeComponent();
        }

        public List<string> Pictures = new List<string>()
        {
            "https://github.com/jpbandroid/AppStore-Resources/blob/main/UTEUWP-Plus/Screenshot%202024-04-29%20061330.png?raw=true",
            "https://github.com/jpbandroid/AppStore-Resources/blob/main/UTEUWP-Plus/Screenshot%202024-04-29%20061347.png?raw=true",
            "https://github.com/jpbandroid/AppStore-Resources/blob/main/UTEUWP-Plus/Screenshot%202024-04-29%20061417.png?raw=true"
        };

        private async void download(object sender, RoutedEventArgs e)
        {
            string name = Environment.UserName;
            using var client = new HttpClient();
            using var s = await client.GetStreamAsync("https://github.com/jpbandroid/UTE-UWP-Plus/releases/download/10.0.26016.1000/UTE.UWP+_10.0.26016.1000_x86_x64_arm_arm64.msixbundle");
            using var fs = new FileStream("C:\\Users\\" + name + "\\Downloads\\UTEUWPPlus.msixbundle", FileMode.OpenOrCreate);
            await s.CopyToAsync(fs);
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("UltraTextEdit UWP Plus download complete")
                .AddText("Take a look!")
                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater

        }
    }
}
