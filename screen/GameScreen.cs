using NeoLib.engine;
using NeoLib.game;

namespace NeoLib.screen;

public abstract class GameScreen
{
    protected Game Game;
    protected EcsEngine _engine;

    protected GameScreen(Game game, bool useEcsRenderSystems)
    {
        Game = game;
        _engine = new EcsEngine(useEcsRenderSystems);
    }

    public abstract void Start();
    
    public abstract void Update(float dt);
    
    // ? Not sure I need deltaTime in Render()...
    public abstract void Render();
    
    public abstract void Stop();

    public abstract void Reset();
}