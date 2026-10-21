using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;
using Android.Widget;
using System.IO;

namespace BrowserPro.Platforms.Android;

public sealed class BrowserDownloadListener : Java.Lang.Object, IDownloadListener
{
    public void OnDownloadStart(string? url, string? userAgent, string? contentDisposition,
        string? mimeType, long contentLength)
    {
        if (string.IsNullOrWhiteSpace(url) ||
            !Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            return;

        var context = global::Android.App.Application.Context;
        var suggestedName = URLUtil.GuessFileName(url, contentDisposition, mimeType);
        var fileName = Path.GetFileName(suggestedName ?? "download");
        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "download";

        var request = new DownloadManager.Request(global::Android.Net.Uri.Parse(url));
        if (!string.IsNullOrWhiteSpace(userAgent))
            request.AddRequestHeader("User-Agent", userAgent);
        request.SetTitle(fileName);
        request.SetDescription("Descărcare BrowserPro");
        request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
        request.SetDestinationInExternalPublicDir(
            global::Android.OS.Environment.DirectoryDownloads, Path.Combine("Browser", fileName));

        var manager = context.GetSystemService(Context.DownloadService) as DownloadManager;
        manager?.Enqueue(request);
        Toast.MakeText(context, $"Descărcare: {fileName}", ToastLength.Short)?.Show();
    }
}