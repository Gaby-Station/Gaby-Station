using Robust.Client.AutoGenerated;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Utility;

namespace Content.Client._Shitmed.Choice.UI;

[GenerateTypedNameReferences]
[Virtual]
public partial class ChoiceControl : Control
{
    public ChoiceControl() => RobustXamlLoader.Load(this);

    public void Set(string name, Texture? texture)
    {
        NameLabel.SetMessage(name);
        Texture.Texture = texture;
    }

    public void Set(FormattedMessage msg, Texture? texture)
    {
        NameLabel.SetMessage(msg);
        Texture.Texture = texture;
    }
}