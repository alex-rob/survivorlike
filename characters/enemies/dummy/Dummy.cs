using Godot;

namespace Survivorlike.characters.enemies.dummy;

public partial class Dummy : EnemyEntity
{
    private Color _originalColor;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var meshSurface = (StandardMaterial3D)GetNode<MeshInstance3D>("MeshInstance3D")
            .GetSurfaceOverrideMaterial(0);
        _originalColor = meshSurface.GetAlbedo();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    /// <summary>
    /// Overrides the HurtAnimation of EnemyEntity.
    /// </summary>
    protected override void PlayHurtAnimation()
    {
        var meshSurface = (StandardMaterial3D)GetNode<MeshInstance3D>("MeshInstance3D")
            .GetSurfaceOverrideMaterial(0);
        
        /* Set mesh albedo to white for  */
        meshSurface.SetAlbedo(Colors.White); // Set mesh albedo to white
        var timer = GetTree().CreateTimer(0.05f);
        timer.Timeout += () => meshSurface.SetAlbedo(_originalColor);
    }
}