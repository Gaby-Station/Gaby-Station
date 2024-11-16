using Content.Server.Discord;
using Content.Server.GameTicking;
using Content.Shared._Gabystation.CCVar;
using Robust.Shared.Network;
using Content.Server.Database;

namespace Content.Server.Administration.Managers;

// Gabystation - Serverban & Roleban webhook
public sealed partial class BanManager
{
    [Dependency] private readonly DiscordWebhook _discord = default!;
    [Dependency] private readonly GameTicker _ticker = default!;
    private WebhookData? _webhook;

    private void InitializeDiscord()
    {
        var value = _cfg.GetCVar(GabyCVars.BanDiscordWebhook);
        if (!string.IsNullOrEmpty(value))
        {
            _discord.TryGetWebhook(value, val => _webhook = val);
        }
    }
    // TODO: This is shitcode, revamp.
    // TODO: Embed.

    /// <summary>
    /// Send to webhook a server ban message.
    /// </summary>
    private async void SendServerBanWebhook(ServerBanDef ban, string player, string admin)
    {
        try
        {
            if (_webhook is null)
                return;

            var hook = _webhook.Value.ToIdentifier();

            var id = ban.Id?.ToString() ?? "?";

            var footer = Loc.GetString("ban-manager-notify-discord-footer",
                ("round", _ticker.RoundId),
                ("id", id));

            var message = Loc.GetString("ban-manager-notify-discord",
                ("admin", admin),
                ("player", player),
                ("time", ban.BanTime),
                ("reason", ban.Reason));

            if (ban.ExpirationTime is null)
                message = Loc.GetString("ban-manager-notify-discord-perma",
                ("admin", admin),
                ("player", player),
                ("reason", ban.Reason));

            var payload = new WebhookPayload
            {
                Content = $"{message}\n{footer}",
            };

            await _discord.CreateMessage(hook, payload);
        }
        catch (Exception e)
        {
            _sawmill.Error($"Failed to send ban information to webhook!\n{e}");
        }
    }

    /// <summary>
    /// Send to webhook a role ban message.
    /// </summary>
    private async void SendRoleBanWebhook(ServerRoleBanDef ban, string player, string admin)
    { //roleban is SHIT, it send a message to every role
        try
        {
            if (_webhook is null)
                return;

            var hook = _webhook.Value.ToIdentifier();

            var id = ban.Id?.ToString() ?? "?";

            var footer = Loc.GetString("ban-manager-notify-discord-footer",
                ("round", _ticker.RoundId),
                ("id", id));

            var message = Loc.GetString("ban-manager-notify-discord-roleban",
                ("admin", admin),
                ("player", player),
                ("role", ban.Role),
                ("time", ban.BanTime),
                ("reason", ban.Reason));

            if (ban.ExpirationTime is null)
                message = Loc.GetString("ban-manager-notify-discord-perma",
                ("admin", admin),
                ("role", ban.Role),
                ("player", player),
                ("reason", ban.Reason));

            var payload = new WebhookPayload
            {
                Content = $"{message}\n{footer}",
            };

            await _discord.CreateMessage(hook, payload);
        }
        catch (Exception e)
        {
            _sawmill.Error($"Failed to send roleban information to webhook!\n{e}");
        }
    }
}
