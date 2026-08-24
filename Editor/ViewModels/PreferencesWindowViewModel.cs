using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Editor.Models;
using ReactiveUI;

namespace Editor.ViewModels;

public class PreferencesWindowViewModel : ViewModelBase
{
    private string? _defaultLicense;
    private string? _defaultCopyright;
    private string? _gitHubToken;
    private bool _revealGitHubToken;
    private bool _minifyJson;
    private bool _easterEggs;
    private int _indentation;
    private string? _defaultImportRepository;

    public PreferencesWindowViewModel(Preferences preferences)
    {
        Preferences = preferences;
        DefaultLicense = Preferences.DefaultLicense;
        DefaultCopyright = Preferences.DefaultCopyright;
        GitHubToken = Preferences.GitHubToken;
        MinifyJson = Preferences.MinifyJson;
        EasterEggs = Preferences.EasterEggs;
        Indentation = Preferences.Indentation;
        DefaultImportRepository = Preferences.DefaultImportRepository;
    }

    public Preferences Preferences { get; }

    public Interaction<Preferences, Unit> SaveAction { get; } = new();

    public Interaction<Preferences, Unit> CancelAction { get; } = new();

    public string? DefaultLicense
    {
        get => _defaultLicense;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                value = null;

            this.RaiseAndSetIfChanged(ref _defaultLicense, value);
        }
    }

    public string? DefaultCopyright
    {
        get => _defaultCopyright;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                value = null;

            this.RaiseAndSetIfChanged(ref _defaultCopyright, value);
        }
    }

    public string? GitHubToken
    {
        get => _gitHubToken;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                value = null;

            this.RaiseAndSetIfChanged(ref _gitHubToken, value);
        }
    }

    public bool RevealGitHubToken
    {
        get => _revealGitHubToken;
        set => this.RaiseAndSetIfChanged(ref _revealGitHubToken, value);
    }

    public bool MinifyJson
    {
        get => _minifyJson;
        set => this.RaiseAndSetIfChanged(ref _minifyJson, value);
    }

    public bool EasterEggs
    {
        get => _easterEggs;
        set => this.RaiseAndSetIfChanged(ref _easterEggs, value);
    }

    public int Indentation
    {
        get => _indentation;
        set => this.RaiseAndSetIfChanged(ref _indentation, value);
    }

    public string? DefaultImportRepository
    {
        get => _defaultImportRepository;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                value = null;

            this.RaiseAndSetIfChanged(ref _defaultImportRepository, value);
        }
    }

    public async Task Save()
    {
        Preferences.DefaultLicense = DefaultLicense;
        Preferences.DefaultCopyright = DefaultCopyright;
        Preferences.GitHubToken = GitHubToken;
        Preferences.MinifyJson = MinifyJson;
        Preferences.EasterEggs = EasterEggs;
        Preferences.DefaultImportRepository = DefaultImportRepository;

        await SaveAction.Handle(Preferences);
    }

    public async Task Cancel()
    {
        await CancelAction.Handle(Preferences);
    }

    public void OpenGithubTokensPage()
    {
        var url = "https://github.com/settings/tokens";

        // https://stackoverflow.com/a/43232486
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
    }
}
