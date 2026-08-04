using System.ComponentModel;

[ImmutableObject(true)]
public struct UserSettings
{
    public UserSettings(
        bool musicOn
    ) {
        this.musicOn = musicOn;
    }

    public UserSettings copy(
        bool? musicOn = null
    ) {
        return new UserSettings(
            musicOn: musicOn ?? this.musicOn
        );
    }

    public readonly bool musicOn;
}
