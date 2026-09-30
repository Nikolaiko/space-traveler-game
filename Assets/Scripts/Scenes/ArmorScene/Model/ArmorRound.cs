using System;

// Раунд сбора брони: таймер, скорость ленты и добыча
public class ArmorRound
{
    private readonly float duration;
    private readonly float startSpeed;
    private readonly float endSpeed;

    public ArmorLoot loot { get; } = new ArmorLoot();
    public float timeLeft { get; private set; }
    public bool finished { get; private set; }

    public event Action<ArmorLoot> onFinished;

    public ArmorRound(float duration, float startSpeed, float endSpeed) {
        this.duration = duration;
        this.startSpeed = startSpeed;
        this.endSpeed = endSpeed;
        timeLeft = duration;
    }

    // Доля прошедшего времени, от 0 до 1
    public float progress {
        get { return 1f - timeLeft / duration; }
    }

    public float beltSpeed {
        get { return finished ? 0f : startSpeed + (endSpeed - startSpeed) * progress; }
    }

    public void tick(float deltaTime) {
        if (finished) {
            return;
        }

        timeLeft = Math.Max(0f, timeLeft - deltaTime);
        if (timeLeft <= 0f) {
            finished = true;
            onFinished?.Invoke(loot);
        }
    }
}
