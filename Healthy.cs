using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;

namespace Healthy;

public class HealthyPlugin : BasePlugin
{
    public override string ModuleName => "Healthy";
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "Wangsir";
    public override string ModuleDescription => "Maintain blood volume at 100(don't Use with \"Buddha\")";

    private bool enable;
    public override void Load(bool hotReload)
    {
        enable=true;
    }
    [ConsoleCommand("css_health", "set health to 100")]
    public void OnHelloCommand(CCSPlayerController? client, CommandInfo commandInfo)
    {
        if (client == null)
        {
            enable=!enable;
            commandInfo.ReplyToCommand("Healthy is "+(enable?"enable":"disable"));
            return;
        }
        if(enable)
        {
            commandInfo.ReplyToCommand("set your health to 100");
            var clientpawn = client.PlayerPawn.Value;
            clientpawn.Health = 100;
        }
    }
    [GameEventHandler(HookMode.Post)]
    public HookResult OnEventPlayerHurtPost(EventPlayerHurt @event, GameEventInfo info)
    {
        if(enable)
        {
            @event.DmgHealth=0;
            @event.Userid.PlayerPawn.Value.Health = 100;
        }
        return HookResult.Continue;
    }
}
