using Godot;
using System;

[Tool]
public class TemporaryLabel : Node2D {
    private RichTextLabel _richTextLabel;
    private string _text = "placeholder";
    private TextEffect _effect = TextEffect.Shake;
    private Color _textColor = Colors.White;
    

    [Export]
    public Color TextColor {
        get => _textColor;
        set {
            _textColor = value;
            RefreshText();
        }
    }

    [Export]
    public TextEffect Effect {
        get => _effect;
        set {
            _effect = value;
            RefreshText();
        }
    }

    [Export]
    public string Text {
        get => _text;
        set {
            _text = value;
            RefreshText();
        }
    }

    public override void _Ready() {
        _richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        RefreshText();
    }

    public override void _PhysicsProcess(float delta) {
        
    }

    private void RefreshText() {
        if (_richTextLabel == null) {
            return;
        }

        _richTextLabel.BbcodeText = GenerateColorPrefix(TextColor) + GenerateTextEffectPrefix(Effect) + _text;
        Update();
    }

    private string GenerateColorPrefix(Color textColor) {
        return $"[color=#{textColor.ToHtml(includeAlpha: false)}]";
    }

    private string GenerateTextEffectPrefix(TextEffect textEffect) {
        switch (textEffect) {
            case TextEffect.Wave:
                return "[wave]";
            case TextEffect.Tornado:
                return "[tornado]";
            case TextEffect.Shake:
                return "[shake level=5]";
            case TextEffect.Rainbow:
                return "[rainbow]";
            default:
                throw new ArgumentOutOfRangeException(nameof(textEffect), Effect, "Did you forget to implement this?");
        }
    }

    public enum TextEffect {
        Wave,
        Tornado,
        Shake,
        Rainbow
    }
}