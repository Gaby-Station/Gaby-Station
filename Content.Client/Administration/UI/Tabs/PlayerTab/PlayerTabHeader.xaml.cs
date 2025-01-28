﻿using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Input;

namespace Content.Client.Administration.UI.Tabs.PlayerTab;

[GenerateTypedNameReferences]
public sealed partial class PlayerTabHeader : Control
{
    public event Action<Header>? OnHeaderClicked;

    public PlayerTabHeader()
    {
        RobustXamlLoader.Load(this);

        UsernameLabel.OnKeyBindDown += UsernameClicked;
        CharacterLabel.OnKeyBindDown += CharacterClicked;
        JobLabel.OnKeyBindDown += JobClicked;
        AntagonistLabel.OnKeyBindDown += AntagonistClicked;
        PlaytimeLabel.OnKeyBindDown += PlaytimeClicked;
        CountryLabel.OnKeyBindDown += CountryClicked;
    }

    public Label GetHeader(Header header)
    {
        return header switch
        {
            Header.Username => UsernameLabel,
            Header.Character => CharacterLabel,
            Header.Job => JobLabel,
            Header.Antagonist => AntagonistLabel,
            Header.Playtime => PlaytimeLabel,
            Header.Country => CountryLabel,
            _ => throw new ArgumentOutOfRangeException(nameof(header), header, null)
        };
    }

    public void ResetHeaderText()
    {
        UsernameLabel.Text = Loc.GetString("player-tab-username");
        CharacterLabel.Text = Loc.GetString("player-tab-character");
        JobLabel.Text = Loc.GetString("player-tab-job");
        AntagonistLabel.Text = Loc.GetString("player-tab-antagonist");
        PlaytimeLabel.Text = Loc.GetString("player-tab-playtime");
        CountryLabel.Text = Loc.GetString("player-tab-country");
    }

    private void HeaderClicked(GUIBoundKeyEventArgs args, Header header)
    {
        if (args.Function != EngineKeyFunctions.UIClick)
        {
            return;
        }

        OnHeaderClicked?.Invoke(header);
        args.Handle();
    }

    private void UsernameClicked(GUIBoundKeyEventArgs args)
    {
        HeaderClicked(args, Header.Username);
    }

    private void CharacterClicked(GUIBoundKeyEventArgs args)
    {
        HeaderClicked(args, Header.Character);
    }

    private void JobClicked(GUIBoundKeyEventArgs args)
    {
        HeaderClicked(args, Header.Job);
    }

    private void AntagonistClicked(GUIBoundKeyEventArgs args)
    {
        HeaderClicked(args, Header.Antagonist);
    }

    private void PlaytimeClicked(GUIBoundKeyEventArgs args)
    {
        HeaderClicked(args, Header.Playtime);
    }

    private void CountryClicked(GUIBoundKeyEventArgs args)
    {
        HeaderClicked(args, Header.Country);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            UsernameLabel.OnKeyBindDown -= UsernameClicked;
            CharacterLabel.OnKeyBindDown -= CharacterClicked;
            JobLabel.OnKeyBindDown -= JobClicked;
            AntagonistLabel.OnKeyBindDown -= AntagonistClicked;
            PlaytimeLabel.OnKeyBindDown -= PlaytimeClicked;
            CountryLabel.OnKeyBindDown -= CountryClicked;
        }
    }

    public enum Header
    {
        Username,
        Character,
        Job,
        Antagonist,
        Playtime,
        Country
    }
}
