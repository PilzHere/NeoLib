namespace NeoLib.game;

public abstract class Game
{
    public string GameTitle { get; protected set; }
    public uint VersionMajor { get; protected set; }
    public uint VersionMinor { get; protected set; }
    public uint VersionRevision { get; protected set; }
    public string VersionState { get; protected set; }
    //public bool CloseGame { get; protected set; }

    private const int MaximumUps = 500;
    private const int DesiredUpsStandard = 60;
    
    public int _desiredUps { get; protected set; }
    protected int UpsRounded { get; set; }
    protected float MsPerUpdate { get; set; }
    protected double LagInMs { get; set; }
    protected double Lag { get; set; }
    protected DateTime InitTime { get; set; }
    //protected double Previous { get; set; }
    protected uint UpdateCount { get; set; }

    // ? for physics maybe...
    protected float Accumulator = 0f;
    protected float Slice = 0f;
    protected float Step = 0f;

    public int DesiredUps
    {
        get => _desiredUps;
        set
        {
            if (value > 0 && value <= MaximumUps)
            {
                _desiredUps = value;
                UpdateMsPerUpdate();
            }
        }
    }

    private void UpdateMsPerUpdate()
    {
        MsPerUpdate = 1.0f / _desiredUps;
    }

    protected Game()
    {
        InitTime = DateTime.UtcNow;

        DesiredUps = DesiredUpsStandard;
        UpdateMsPerUpdate();
    }

    public abstract void Run();

    protected abstract void Update(float dt);

    protected abstract void Stop();
}